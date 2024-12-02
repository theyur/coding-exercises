type Counter = {
    increment: () => number,
    decrement: () => number,
    reset: () => number,
}

function createCounter(init: number): Counter {
    let i = init;

    return {
        increment: () => {
            console.log(++i);
            return i;
        },
        decrement: () => {
            console.log(--i);
            return i;
        },
        reset: () => {
            i = init;
            console.log(i);
            return i
        },
    };
}


const counter = createCounter(5)
counter.increment(); // 6
counter.reset(); // 5
counter.decrement(); // 4
