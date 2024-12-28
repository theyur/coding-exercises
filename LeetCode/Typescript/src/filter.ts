export {}

type Fn = (n: number, i: number) => any

function filter(arr: number[], fn: Fn): number[] {
    const filtered: number[] = [];

    for (let i = 0; i < arr.length; i++) {
        let r = fn(arr[i], i);
        if (r) filtered.push(arr[i]);
    }

    return filtered;
};