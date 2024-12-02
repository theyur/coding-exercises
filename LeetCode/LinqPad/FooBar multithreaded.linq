<Query Kind="Program" />

void Main()
{
	
}

public class FooBar
{
	private int n;

	System.Threading.ManualResetEventSlim mreFoo = new (true);
	System.Threading.ManualResetEventSlim mreBar = new (false);

	public FooBar(int n)
	{
		this.n = n;
	}

	public void Foo(Action printFoo)
	{

		for (int i = 0; i < n; i++)
		{
			mreFoo.Wait();
			
			// printFoo() outputs "foo". Do not change or remove this line.
			printFoo();
			
			mreFoo.Reset();
			mreBar.Set();
		}
	}

	public void Bar(Action printBar)
	{
		for (int i = 0; i < n; i++)
		{
			mreBar.Wait();
			
			// printBar() outputs "bar". Do not change or remove this line.
			printBar();
			
			mreBar.Reset();
			mreFoo.Set();
		}
	}
}
