<Query Kind="Program" />

void Main()
{
	var solution = new Solution();
	
	var r = solution.TakeCharacters("aabaaaacaabc", 2);
	//var r = solution.TakeCharacters("caccbbba", 1);
	//var r = solution.TakeCharacters("aabbccca", 2);
	//var r = solution.TakeCharacters("abc", 1);
	
	Console.WriteLine(r);
}

// You can define other methods, fields, classes and namespaces here
public class Solution
{
	public int TakeCharacters(string s, int k)
	{
		if (k == 0) return 0;
		
		var counts = new int[3] {0, 0, 0};

		for (var i = 0; i < s.Length; i++) counts[((byte)s[i]) - 97]++;
		
		if (counts[0] < k || counts[1] < k || counts[2] < k) return -1;
		
		var i1 = 0;
		var i2 = 0;
		
		var longest = 0;
		var I = 0;

		do
		{
			for (I = i2; I < s.Length && enoughLetters(counts); I++) counts[(byte)s[I] - 97]--;

			i2 = I;

			var t = enoughLetters(counts) ? 0 : 1;
			if (I - i1 - t > longest) longest = I - i1 - t;
			
			while (!enoughLetters(counts)) counts[(byte)s[i1++] - 97]++;
		} while (I < s.Length);
		
		return s.Length - longest;
		
		bool enoughLetters(int[] letters) => letters[0] >= k && letters[1] >= k && letters[2] >= k;
	}
}