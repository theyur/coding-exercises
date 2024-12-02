<Query Kind="Program" />

void Main()
{
	FizzBuzz fizzBuzz = new(15);
	
	Action printFizz        = () => Console.WriteLine("Fizz");
	Action printBuzz        = () => Console.WriteLine("Buzz");
	Action printFizzBuzz    = () => Console.WriteLine("FizzBuzz");
	Action<int> printNumber = n => Console.WriteLine(n);
	
	Thread tFizz     = new(() => fizzBuzz.Fizz(printFizz));
	Thread tBuzz     = new(() => fizzBuzz.Buzz(printBuzz));
	Thread tFizzBuzz = new(() => fizzBuzz.Fizzbuzz(printFizzBuzz));
	Thread tNumber   = new(() => fizzBuzz.Number(printNumber));
	
	tFizz.Name     = "Fizz";     tFizz.Start();
	tBuzz.Name     = "Buzz";     tBuzz.Start();
	tFizzBuzz.Name = "FizzBuzz"; tFizzBuzz.Start();
	tNumber.Name   = "Number";   tNumber.Start();
	
	tFizz.Join();
	tBuzz.Join();
	tFizzBuzz.Join();
	tNumber.Join();
	
	Console.WriteLine("\n\nThe end!");
}

// You can define other methods, fields, classes and namespaces here
public class FizzBuzz
{
	private volatile int _n;
	private volatile int _i = 1;

	private System.Threading.ManualResetEventSlim _mreFizzBuzz = new(false);
	private System.Threading.ManualResetEventSlim _mreFizz     = new(false);
	private System.Threading.ManualResetEventSlim _mreBuzz     = new(false);
	private System.Threading.ManualResetEventSlim _mreNumber   = new(true);

	public FizzBuzz(int n) => _n = n;

	private void ReleaseFor(int n)
	{
		switch ((n % 3, n % 5))
		{
			case (0, > 0):
				_mreFizz.Set();
				break;
			case ( > 0, 0):
				_mreBuzz.Set();
				break;
			case (0, 0):
				_mreFizzBuzz.Set();
				break;
			default:
				_mreNumber.Set();
				break;
		}
	}
	
	private void ReleaseAll()
	{
		_mreFizz.Set();
		_mreBuzz.Set();
		_mreFizzBuzz.Set();
		_mreNumber.Set();
	}

	// printFizz() outputs "fizz".
	public void Fizz(Action printFizz)
	{
		while (_i <= _n)
		{
			_mreFizz.Wait();
			if (_i > _n) return;

			printFizz();

			_mreFizz.Reset();

			ReleaseFor(++_i);
		}

		ReleaseAll();
	}

	// printBuzzz() outputs "buzz".
	public void Buzz(Action printBuzz)
	{
		while (_i <= _n)
		{
			_mreBuzz.Wait();
			if (_i > _n) return;

			printBuzz();

			_mreBuzz.Reset();

			ReleaseFor(++_i);
		}

		ReleaseAll();
	}

	// printFizzBuzz() outputs "fizzbuzz".
	public void Fizzbuzz(Action printFizzBuzz)
	{
		while (_i <= _n)
		{
			_mreFizzBuzz.Wait();
			if (_i > _n) return;
			
			printFizzBuzz();
			
			_mreFizzBuzz.Reset();
			
			ReleaseFor(++_i);
		}

		ReleaseAll();
	}

	// printNumber(x) outputs "x", where x is an integer.
	public void Number(Action<int> printNumber)
	{
		do
		{
			_mreNumber.Wait();
			if (_i > _n) return;

			printNumber(_i);
			
			_mreNumber.Reset();

			ReleaseFor(++_i);
		} while (_i <= _n);
		
		ReleaseAll();
	}
}