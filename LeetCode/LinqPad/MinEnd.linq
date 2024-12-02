<Query Kind="Program" />

void Main()
{
	var solution = new Solution();
	
	//var r = solution.MinEnd(3, 1);
	//var r = solution.MinEnd(2, 7);
	//var r = solution.MinEnd(3, 4);
	//var r = solution.MinEnd(6715154, 7193485);
	var r = solution.MinEnd(36845498, 15573081);
	
	Console.WriteLine(r);
}

// You can define other methods, fields, classes and namespaces here

public class Solution
{
    public long MinEnd(int n, long x) 
	{
		var zeros = new long[32];
		
		var j = 0;
		//zeros[0] = 1;
		
		//var lim =  + 1;
		for (long i = 1; i <= Int64.MaxValue & j < 32; i <<= 1)
			if ((x & i) != i)
			{
				zeros[j] = i;
				j++;
				
				//if (j == 18) Debugger.Break();
			}
			
		long GetNumberFromZerosByMask(uint mask)
		{
			long sum = 0;
			
			var lim = 32 - System.Numerics.BitOperations.LeadingZeroCount(mask);

			for (var i = 0; i <= lim; i++)
				if ((mask & (1 << i)) != 0) sum += zeros[i];

			return sum;
		}

		x.ToString("b32").Dump();
		zeros.Dump();

		long last = x;

		for (uint i = 1; i < n; i++)
		{
			var prevLast = last;
			last = x + GetNumberFromZerosByMask(i);
			/*
			if ((last & 7193485) != 7193485)
				Debugger.Break();
			
			if (prevLast >= last)
				Debugger.Break();
			*/	
			//if (i == 8) Debugger.Break();			
		}
		
		return last;
	}
}