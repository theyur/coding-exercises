/*
let array1: Array<any> = [
    {"id":"1"},
    {"id":"1"},
    {"id":"2"}
];

const fn = function (item: { id: any; }) {
    return item.id;
}

let r = array1.groupBy(fn);

//console.log(r);

interface Array<T> {
    groupBy(fn: (item: T) => string): Record<string, T[]>
}


Array.prototype.groupBy = function(fn) {

    const dict: Record<string, any[]> = {};

    for (let i = 0; i < this.length; i++) {
        let key = fn(this[i]);

        if (!dict[key]) dict[key] = [];
        dict[key].push(this[i]);
    }

    return dict;
}

/**
 * [1,2,3].groupBy(String) // {"1":[1],"2":[2],"3":[3]}
 */
Array.prototype.groupBy = function (fn) {
    var dict = {};
    for (var i = 0; i < this.length; i++) {
        var key = fn(this[i]);
        if (!dict[key])
            dict[key] = [];
        dict[key].push(this[i]);
    }
    return dict;
};
var array1 = [
    { "id": "1" },
    { "id": "1" },
    { "id": "2" }
];
var fn = function (item) {
    return item.id;
};
var r = array1.groupBy(fn);
console.log(r);
