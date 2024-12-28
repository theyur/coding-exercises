export {}

type JSONValue = null | boolean | number | string | JSONValue[] | { [key: string]: JSONValue };
type OnceFn = (...args: JSONValue[]) => JSONValue | undefined

function once(fn: Function): OnceFn {

    let calledNum: number = 0;

    return function (...args) {
        if (calledNum++ == 0) return fn.apply(null, args);

        return undefined;
    };
}

/**
 * let fn = (a,b,c) => (a + b + c)
 * let onceFn = once(fn)
 *
 */

let fn = once((a:number,b:number,c:number) => (a + b + c)); // 6

fn([[1,2,3],[2,3,6]]);

