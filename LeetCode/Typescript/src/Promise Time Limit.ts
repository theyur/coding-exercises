export {}

/*
function timeLimit(fn: Fn, t: number): Fn {

    return async function (...args) {

        return new Promise(async (resolve, reject) => {
            let r: any;

            setTimeout(() => {
                if (r !== undefined)
                    resolve(r);
                else
                    reject("Time Limit Exceeded");
            }, t);

            r = await fn.apply(null, args);
        });
    }
}
*/

type Fn = (...params: any[]) => Promise<any>;

function timeLimit(fn: Fn, t: number): Fn {
    return async function (...args) {
        return new Promise((resolve, reject) => {
            const timer = setTimeout(() => {
                reject("Time Limit Exceeded");
            }, t);

            fn(...args)
                .then((result) => {
                    clearTimeout(timer); // Clear the timeout if `fn` resolves in time
                    resolve(result);
                })
                .catch((error) => {
                    clearTimeout(timer); // Clear the timeout if `fn` rejects
                    reject(error);
                });
        });
    };
}


const limited = timeLimit(async (n) => {
    await new Promise(res => setTimeout(res, 100));
    return n * n;
}, 150);
limited(5).then(r => console.log(r)).catch(console.log) // "Time Limit Exceeded" at t=100ms
