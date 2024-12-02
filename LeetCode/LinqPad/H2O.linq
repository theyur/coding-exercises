<Query Kind="Program" />

void Main()
{
	H2O h20 = new();
	
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

public class H2O
{
	private volatile int _numO = 0;
	private volatile int _numH = 0;

	private object _lockO = new();
	private object _lockH = new();
	
	private System.Threading.AutoResetEvent _areO = new(false);
	private System.Threading.AutoResetEvent _areH = new(false);

    public H2O() {}

    public void Hydrogen(Action releaseHydrogen)
	{
		lock (_lockH)
		{
			var fullReset = false;

			if (_numH == 2)
				if (_numO < 1) _areH.WaitOne();
				else
				{
					System.Threading.Interlocked.Exchange(ref _numO, 0);
					System.Threading.Interlocked.Exchange(ref _numH, 0);

					fullReset = true;
				}
			else _areH.Reset();

			releaseHydrogen();
			System.Threading.Interlocked.Add(ref _numH, 1);
			
			if (_numO < 1) _areO.Set();
			else if (_numH == 2)
            {
                System.Threading.Interlocked.Exchange(ref _numO, 0);
                System.Threading.Interlocked.Exchange(ref _numH, 0);

                fullReset = true;
            }
			
			if (fullReset)
			{
				_areO.Reset();
				_areH.Reset();
			}
		}
	}

    public void Oxygen(Action releaseOxygen)
	{
		lock (_lockO)
		{
			var fullReset = false;

			if (_numO == 1) 
				if (_numH < 2) _areO.WaitOne();
				else
				{
					System.Threading.Interlocked.Exchange(ref _numO, 0);
					System.Threading.Interlocked.Exchange(ref _numH, 0);
					
					fullReset = true;
				}
			else _areO.Reset();

			releaseOxygen();
			System.Threading.Interlocked.Exchange(ref _numO, 1);

			switch (_numH)
			{
				case 0: case 1: _areH.Set(); break;
				case 2:
					System.Threading.Interlocked.Exchange(ref _numO, 0);
					System.Threading.Interlocked.Exchange(ref _numH, 0);

					fullReset = true;
					break;
			}

			if (fullReset)
			{
				_areO.Reset();
				_areH.Reset();
			}
		}
	}
}