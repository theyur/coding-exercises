<Query Kind="Program" />

void Main()
{
	ZeroEvenOdd zeo = new(5);
	
	Thread threadZero = new(() => zeo.Zero(printNumber));
	Thread threadEven = new(() => zeo.Even(printNumber));
	Thread threadOdd  = new(() => zeo.Odd(printNumber));
	
	threadZero.Start();
	threadEven.Start();
	threadOdd .Start();
	
	threadZero.Join();
	threadEven.Join();
	threadOdd .Join();
	
	Console.WriteLine("====");
	//Console.ReadLine();
	
	void printNumber(int n) => Console.Write(n);
}

public class ZeroEvenOdd
{
	private int n;
	private int _cur = 0;
	private bool finish = false;

	private bool printedEven = false;

	System.Threading.ManualResetEventSlim mreZero = new(true);
	System.Threading.ManualResetEventSlim mreEven = new(false);
	System.Threading.ManualResetEventSlim mreOdd  = new(false);

	public ZeroEvenOdd(int n)
	{
		this.n = n;
	}

	// printNumber(x) outputs "x", where x is an integer.
	public void Zero(Action<int> printNumber)
	{
		for (var i = 0; i < n; i++)
		{
			mreZero.Wait();

			printNumber(0);

			mreZero.Reset();
			printedEven = !printedEven;

			if (printedEven) mreOdd.Set();
			else mreEven.Set();

		}

		mreZero.Wait();

		finish = true;
		
		mreEven.Set();
		mreOdd.Set();
	}

	public void Even(Action<int> printNumber)
	{
		do
		{
			mreEven.Wait();
			if (finish) return;

			if (++_cur > n) return;
			printNumber(_cur);

			mreEven.Reset();
			mreZero.Set();

			if (_cur == n) break;
		} while (!finish);
	}

	public void Odd(Action<int> printNumber)
	{
		do
		{
			mreOdd.Wait();
			if (finish) return;
			
			if (++_cur > n) return;
			printNumber(_cur);

			mreOdd.Reset();
			mreZero.Set();

			if (_cur == n) break;
		} while (!finish);
	}
}
