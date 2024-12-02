type MultiDimensionalArray1 = (MultiDimensionalArray1 | number)[]
let currentLevel: number = -1;

function* inorderTraversal1(arr: MultiDimensionalArray1, level: number = -1): Generator<MultiDimensionalArray1 | number, void> {
    if (level == 0) {
        for (let item of arr) yield item;
        return;
    }

    currentLevel++;

    if (level >= 0 && currentLevel > level) {
        currentLevel--;
        yield arr;
        return;
    }

    for (const item of arr)
        if (typeof item === "number") yield item;
        else yield* inorderTraversal1(item, level);

    currentLevel--;
}


const nestedArray2: MultiDimensionalArray1 = [1, 2, 3, [4, 5, 6], [7, 8, [9, 10, 11], 12], [13, 14, 15]];

const flat1 = function (arr: MultiDimensionalArray1, n: number): MultiDimensionalArray1 {
    let gen = inorderTraversal1(arr, n);
    return [...gen];
};

console.log(flat1(nestedArray2, 2));