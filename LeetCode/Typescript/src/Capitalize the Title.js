/**
 * @param {string} title
 * @return {string}
 */
var capitalizeTitle = function(title) {

    let makeLowercase = function(wordStart, i) {
        for (let j = wordStart; j < i; j++)
            if (title[j] >= 'A' && title[j] <= 'Z') result[j] = title.charCodeAt(j) + 32;
            else result[j] = title.charCodeAt(j);
    };

    let capitalize = function (wordStart, i) {
        if (title[wordStart] >= 'a') result[wordStart] = title.charCodeAt(wordStart) - 32;
        else result[wordStart] = title.charCodeAt(wordStart);

        for (let j = wordStart + 1; j < i; j++)
            if (title[j] >= 'A' && title[j] <= 'Z') result[j] = title.charCodeAt(j) + 32;
            else result[j] = title.charCodeAt(j);
    };

    let result = new Array(title.length), wordStart = 0;

    title += ' ';
    for (let i = 0; i < title.length; i++)
        if (title[i] === ' ') {
            if (i - wordStart <= 2) makeLowercase(wordStart, i);
            else capitalize(wordStart, i)

            wordStart = i + 1;
            result[i] = 32;
        }

    return String.fromCharCode.apply(String, result.slice(0, result.length - 1));
};

console.log(capitalizeTitle("tO Be Or nOt To bE"));