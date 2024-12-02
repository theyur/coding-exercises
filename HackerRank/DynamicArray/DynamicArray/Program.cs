// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

var rawInput = File.ReadAllLines("input0.txt");

var n = rawInput[0].Split(' ').Select(int.Parse).ToArray()[0];
var queries = rawInput.Skip(1).Select(x => x.Split(' ').Select(int.Parse).ToList()).ToList();

var result = Result.dynamicArray(n, queries);

foreach(var r in result) Console.WriteLine(r);


class Result
{

    /*
     * Complete the 'dynamicArray' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. 2D_INTEGER_ARRAY queries
     */

    public static List<int> dynamicArray(int n, List<List<int>> queries)
    {
        List<int> results = [];
        
        var lastAnswer = 0;
        
        var arr = new List<int>[n];
        for (var i = 0; i < n; i++) arr[i] = [];
        
        foreach(var q in queries)
        {
            if (q[0] == 1)
            {
                var idx = (q[1] ^ lastAnswer) % n;                
                arr[idx].Add(q[2]);
            }
            else
            {
                var idx = (q[1] ^ lastAnswer) % n;
                lastAnswer = arr[idx][q[2] % arr[idx].Count];
                results.Add(lastAnswer);    
            }
        }
        
        return results;        
    }

}
