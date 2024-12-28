<Query Kind="Program" />

void Main()
{
	Solution solution = new();
	//var s = "abaababaab";
	//var s = "abaс";
	//var s = "abcabcabcabc";
	//var s = "ababab";
	//var s = "ababababab";
	//var s = "abbabbabbabbabbabbabb";
	//var s = "zzz";
	var s = "aaaaaaaaaaaaa";
	
	var r = solution.RepeatedSubstringPattern(s).Dump();
}

// You can define other methods, fields, classes and namespaces here
public class Solution
{
	public bool RepeatedSubstringPattern(string s)
	{
		if (s.Length == 1) return false;
		if (s.Length == 2) return s[0] == s[1];
        if (s.Length == 3) return s[0] == s[1] && s[1] == s[2];

		var sameChars = true;
		for (var k = 0; k < s.Length - 1; k++)
			if (!(sameChars &= s[k] == s[k + 1])) break;
		if (sameChars) return true;

		var divider = 2;
		List<int> dividers = [];
		
		for (; divider <= s.Length / 2; divider++)
			if (s.Length % divider == 0)
			{
				var match = true;
				var limit = s.Length / divider;
				for (var j = 1; j < divider; j++)
					if (!(match &= s[..limit] == s[(limit * j)..(limit * (j + 1))])) break;
				
				if (match) return true;
			}
			
		return false;
	}
}