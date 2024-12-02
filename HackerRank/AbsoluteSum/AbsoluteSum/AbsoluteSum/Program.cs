// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

Console.WriteLine("Hello, World!");

//var r = Result.playingWithNumbers(new List<int> { -1, 2, -3 }, new List<int> { 1, -2 });

var arrayRaw = "887 916 336 -493 1422 -28 -60 1927 -1427 -1737";
var queriesRaw = "1369 430 -1531 -1124 1136 1803 -1059 168 457 43";

//var arrayRaw = "1374 920 -538 -325 371 1527 981 -1874 -1171 -1282 926 -328 -506 -1730 1858 -1896 -546 -1368 -365 1751 809 -1179 -1585 652 -400 -1061 -1369 13 -587 -1540 571 -379 602 903 493 -757 281 -1442 1690 -620 -730 118 1772 676 928 1857 354 -966 -684 625 -872 -830 20 -1369 -716 -150 -724 -246 -1452 1556 1489 -229 351 1501 -1765 -915 1857 492 366 1937 -552 1229 1408 -122 -396 1238 1794 -429 1012 -1530 -405 1764 539 -841 -819 -689 1918 997 -1744 -184 -1500 -726 -1591 140 -1787 83 -465 1508 805 -612";
//var queriesRaw = "-1829 1344 -1569 -1423 1811 1802 1731 -1306 -737 -627 -1466 -1417 -1259 -1638 -1625 -37 -1900 1551 -72 1132 931 1895 -164 1982 997 1774 1669 -1096 -467 -1341 -1685 -1543 -1108 1757 419 1413 -173 10 -1211 -1588 -1302 1373 1256 600 1905 1812 -1668 229 1151 -659 -1225 -1270 -82 -85 -1973 -1851 1386 -1300 -43 -714 -191 -591 582 1337 -1156 -5 770 1777 -1256 -143 1885 1206 1568 -614 755 -260 -203 -1507 -22 -869 -1190 -1909 -499 -809 249 1334 1649 -1755 1747 -1530 -47 -1798 991 1034 498 893 -1126 -1997 1189 1730";


//var array = arrayRaw.Split(' ').Select(int.Parse).ToList();
//var queries = queriesRaw.Split(' ').Select(int.Parse).ToList();

var array = File.ReadAllText("input11.txt").Split(' ').Select(int.Parse).ToList();
var queries = File.ReadAllText("queries11.txt").Split(' ').Select(int.Parse).ToList();

var expectedResults = File.ReadAllText("output11.txt").Split('\n').Select(long.Parse).ToList();

var r = Result.playingWithNumbers(array, queries);

Console.WriteLine(string.Join(", ", r.Take(100)));

// Expected output:
// 16285
// 19733
// 10129
// 10273
// 10177
// 22573
// 14215
// 15223
// 18409
// 18753

class Result
{
    /*
     * Complete the 'playingWithNumbers' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts following parameters:
     *  1. INTEGER_ARRAY arr
     *  2. INTEGER_ARRAY queries
     */

    private record struct Element(int Frequency, int ElementsAtLeft);
    
    public static List<long> playingWithNumbers(List<int> arr, List<int> queries)
    {
        var elements = new Element[4002];

        long sumNegative = 0;
        long sumPositive = 0;

        foreach (var number in arr)
        {
            elements[number + 2000].Frequency++;
            
            if (number < 0)
                sumNegative += -number;
            else
                sumPositive += number;
        }

        var totalAtLeft = 0;
        for (var i = 1; i < elements.Length; i++)
        {
            totalAtLeft += elements[i - 1].Frequency;
            elements[i].ElementsAtLeft = totalAtLeft;
        }

        List<int> a = new();
        a.Reverse();
        
        var indexZero = 2000;
        
        var result = new List<long>();
        
        foreach (var (query, idx) in queries.Select((q, idx) => (q, idx)))
        {
            if (query == 0) continue;
            
            var absQuery = Math.Abs(query);
            
            var newIndexZero = indexZero - query;
            var newIndexZeroValid = Math.Min(Math.Max(newIndexZero, -1), elements.Length - 2);
            var indexZeroValid = Math.Min(Math.Max(indexZero, 0), elements.Length - 1);


            var minIndex = Math.Min(indexZero, newIndexZero);
            var maxIndex = Math.Max(indexZero, newIndexZero);

            var deltaValueNegative = 0;
            var deltaValuePositive = 0;

            for (var i = Math.Max(minIndex, 0); i <= Math.Min(maxIndex, elements.Length - 1); i++)
            {
                deltaValueNegative += elements[i].Frequency * (maxIndex - i);
                deltaValuePositive += elements[i].Frequency * (i - minIndex);
            }

            var stubNegative = (newIndexZero < elements.Length - 1 && indexZero >= elements.Length - 1) || indexZero <= elements.Length - 1 ? 0 : 1;
            var stubPositive = (newIndexZero < 0 && indexZero >= 0) || newIndexZero >= 0 ? 1 : 0;    
            if (newIndexZero < indexZero)
            {
                sumNegative -= elements[Math.Max(newIndexZeroValid + stubNegative, 0)].ElementsAtLeft * absQuery + deltaValueNegative;
                sumPositive += (elements[^1].ElementsAtLeft - elements[Math.Min(indexZeroValid + stubPositive, elements.Length - 1)].ElementsAtLeft) * absQuery + deltaValuePositive;
            }
            else
            {
                sumNegative += elements[indexZeroValid].ElementsAtLeft * absQuery + deltaValueNegative;
                sumPositive -= (elements[^1].ElementsAtLeft - elements[Math.Min(newIndexZeroValid + 1, elements.Length - 1)].ElementsAtLeft) * absQuery + deltaValuePositive;
            }

            long sumAbs = sumNegative + sumPositive;
            
            indexZero = newIndexZero;

            result.Add(sumAbs);
        }
        
        return result;
    }
}