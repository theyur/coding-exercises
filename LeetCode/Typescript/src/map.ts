function map(arr: number[], fn: (n: number, i: number) => number): number[] {
    const mapped: number[] = new Array(arr.length);

    for (let i = 0; i < arr.length; i++) {
        mapped[i] = fn(arr[i], i);
    }

    return mapped;
};