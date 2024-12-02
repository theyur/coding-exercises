// See https://aka.ms/new-console-template for more information

using System.Collections;

Console.WriteLine("Hello, World!");

var numbers = new List<int> { 2, 3, 4, 5, 6, 7 };

var result = Result.waiter(numbers, 3);

Console.WriteLine(string.Join(", ", result));


internal class Result
{

    /*
     * Complete the 'waiter' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts following parameters:
     *  1. INTEGER_ARRAY number
     *  2. INTEGER q
     */

    public static List<int> waiter(List<int> number, int q)
    {
        var primes = new PrimeIterator();
        primes.MoveNext();
        
        var answers = new List<int>();

        var plates = new Stack<int>(number);
        for (var i = 0; i < q; i++)
        {
            var a = new Stack<int>();
            var b = new Stack<int>();

            var prime = primes.Current;

            while (plates.Count > 0)
            {
                var plate = plates.Pop();
                
                if (plate % prime == 0) b.Push(plate);
                else a.Push(plate);
            }

            while (b.Count > 0) answers.Add(b.Pop());

            plates = a;
            primes.MoveNext();
        }
        
        while (plates.Count > 0) answers.Add(plates.Pop());
        
        return answers;
    }
}

internal class PrimeIterator : IEnumerator<int>
{
    private int _current = 1;

    public int Current => _current;

    object IEnumerator.Current => _current;

    public void Dispose() { }

    public bool MoveNext()
    {
        _current++;
        while (!IsPrime(_current)) _current++;
        return true;
    }

    public void Reset() => _current = 1;

    private bool IsPrime(int n)
    {
        if (n < 2) return false;
        
        for (var i = 2; i <= Math.Sqrt(n); i++)
            if (n % i == 0)
                return false;
        
        return true;
    }
}