<Query Kind="Program" />

void Main()
{
	bool IsPalindrome(string input)
	{
		var stillPalindrome = true;
		
		for (var i = 0; i < input.Length / 2 && stillPalindrome; i++)
		{
			stillPalindrome &= input[i] == input[^(i + 1)];
		}
		
		return stillPalindrome;
	}
	
	Console.WriteLine(IsPalindrome("12344321"));
}

// You can define other methods, fields, classes and namespaces here
