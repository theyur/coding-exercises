export {}

class RLEIterator {
    private _pairs: Array<[number, number]> = new Array<[number, number]>();
    private _index = 0;
    private _curSum: number = 0;

    constructor(encoding: number[]) {
        for (let i = 0; i < encoding.length; i += 2)
            this._pairs.push([encoding[i], encoding[i + 1]]);
    }

    next(n: number): number {
        do {
            if (this._index >= this._pairs.length) return -1;
            this._curSum += this._pairs[this._index++][0];
        } while (this._curSum < n);

        this._index--;
        this._curSum -= this._pairs[this._index][0];

        this._pairs[this._index][0] -= n - this._curSum;
        this._curSum = 0;

        return this._pairs[this._index][1];
    }
}

/**
 * Your RLEIterator object will be instantiated and called as such:
 */

const encoding = [3, 8, 0, 9, 2, 5];
//const encoding = [923381016,843,898173122,924,540599925,391,705283400,275,811628709,850,895038968,590,949764874,580,450563107,660,996257840,917,793325084,82];

let obj = new RLEIterator(encoding)
let param_1 = obj.next(2);
let param_2 = obj.next(1);
let param_3 = obj.next(1);
let param_4 = obj.next(2);

console.log(param_1);
console.log(param_2);
console.log(param_3);
console.log(param_4);