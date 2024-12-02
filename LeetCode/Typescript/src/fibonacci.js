/**
 * @return {Generator<number>}
 */
var fibGenerator = function*(n) {
    let a = 0, b = 1;
    for (let i = 1; i < n; i++) {
        yield a;
        [a, b] = [b, a + b];
    }

    while (true) {
        yield a;
        [a, b] = [b, a + b];
    }
};

/**
 * const gen = fibGenerator();
 * gen.next().value; // 0
 * gen.next().value; // 1
 */