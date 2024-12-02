type MultiDimensionalArray = (MultiDimensionalArray | number)[]
let curLevel: number = 0;


const nestedArray1: MultiDimensionalArray = [1, 2, 3, [4, 5, 6], [7, 8, [9, 10, 11], 12], [13, 14, 15]];

const flat = function (arr: MultiDimensionalArray, level: number): MultiDimensionalArray {
    const result: MultiDimensionalArray = [];

    if (level == 0) return arr;

    curLevel++;
    for (let item of arr) {
        if (typeof item === 'number') result.push(item);
        else if (curLevel > level) result.push(item)
        else for (let item1 of flat(item, level)) result.push(item1);
    }

    curLevel--;
    return result;
};

console.log(flat(nestedArray1, 0));