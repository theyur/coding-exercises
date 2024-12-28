<Query Kind="Program" />

void Main()
{
	Solution solution = new();

	//string a = "abcd", b = "cdabcdab";
	//string a = "a", b = "aa";
	//string a = "abc", b = "wxyz";
	string a = "abc", b = "cabcabca";

	solution.RepeatedStringMatch(a, b).Dump();
}

// You can define other methods, fields, classes and namespaces here
public class Solution
{
	public int RepeatedStringMatch(string a, string b)
	{
		var times = b.Length / a.Length + 2;

		Span<char> span = new char[times * a.Length].AsSpan();

		for (var i = 0; i < span.Length; i++) span[i] = a[i % a.Length];

		var idx = span.IndexOf(b);
		if (idx < 0) return -1;
		
		var len = idx + b.Length;
		var r  = len % a.Length == 0 ? len / a.Length : len / a.Length + 1;
				
		return r;
	}
}