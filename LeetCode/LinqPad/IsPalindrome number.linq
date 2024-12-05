<Query Kind="Program" />

void Main()
{
	Solution solution = new();
	//var r = solution.IsPalindrome(4251524);
	var r = solution.IsPalindrome(1001);
	Console.WriteLine(r);
}

// You can define other methods, fields, classes and namespaces here

public class Solution
{

	public bool IsPalindrome(int x)
	{
		// Determine leftmost non-zero digit number
		int leftMax = 10;			// Max length of 10-base int number
		
		int i;
		var divider = 1_000_000_000;
		for (i = leftMax; i > 0; i--)
			if (x / divider > 0) break;
			else divider /= 10;
			
		var multiplier = 1;
		var pal = true;
		for (var j = i; j > i / 2; j--)
		{
			pal &= x / divider == x / multiplier;
			divider /= 10;
			multiplier *= 10;
		}
		
		return pal;
	}
}

public class Solution1
{
	public bool IsPalindrome(int x)
	{
		var xOrig = x;
		int xReversed = 0;

		var iReversed = 0;
		var started = false;
		
		var pow10 = 1_000_000_000;
		var pow10Reversed = 1;
		
		for (var i = 9; i >= 0; i--)
		{			
			var digit = i > 0 ? x / pow10 : x;
			if (digit > 0)
			{
				started = true;
				xReversed += iReversed == 0 ? digit : digit * pow10Reversed;
			}
			if (started)
			{
				iReversed++;
				pow10Reversed *= 10;
			}

			x = x % pow10;
			pow10 /= 10;
		}

		return xOrig == xReversed;

		int Pow10(int n)
		{
			var r = 10;
			for (var i = 1; i < n; i++) r *= 10;
			return r;
		}
	}
}