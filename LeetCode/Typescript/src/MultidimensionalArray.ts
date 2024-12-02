type MultidimensionalArray = (MultidimensionalArray | number)[]

function* inorderTraversal(arr: MultidimensionalArray): Generator<number, void, unknown> {
    for (const item of arr) {
        if (typeof item === "number") {
            yield item;
        } else {
            // TypeScript now understands that `item` must be `MultidimensionalArray`
            yield* inorderTraversal(item);
        }
    }
};

/**
 * const gen = inorderTraversal([1, [2, 3]]);
 * gen.next().value; // 1
 * gen.next().value; // 2
 * gen.next().value; // 3
 */

const nestedArray: MultidimensionalArray = [1, [2, [3, 4], 5], 6];
const generator = inorderTraversal(nestedArray);

console.log([...generator]); // Output: [1, 2, 3, 4, 5, 6]
