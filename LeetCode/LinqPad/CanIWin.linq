<Query Kind="Program" />

void Main()
{
	Solution solution = new();
	
	solution.CanIWin(10, 11).Dump();
	solution.CanIWin(10, 0).Dump();
	solution.CanIWin(10, 1).Dump();
}

// You can define other methods, fields, classes and namespaces here
public class Solution
{
	private Dictionary<int, bool> _hash = [];

	public bool CanIWin(int maxChoosableInteger, int desiredTotal)
	{
		var progTotal = maxChoosableInteger * (maxChoosableInteger + 1) / 2;
		if (progTotal < desiredTotal) return false;
		if (progTotal == desiredTotal) return maxChoosableInteger % 2 != 0;

		Span<int> numbers = stackalloc int[maxChoosableInteger];
		numbers = Enumerable.Range(1, maxChoosableInteger).ToArray();

		return PossibleToForceWin(numbers, desiredTotal);
	}
	
	private bool PossibleToForceWin(Span<int> numbers, int total)
	{
		if (numbers[^1] >= total) return true;

		var h = GetHash(numbers);
		if (_hash.ContainsKey(h)) return _hash[h];

		for (var i = 0; i < numbers.Length; i++)
		{
			Span<int> nums = stackalloc int[numbers.Length - 1];
			numbers[..i].CopyTo(nums);
			numbers[(i + 1)..].CopyTo(nums[i..]);

			if (!PossibleToForceWin(nums, total - numbers[i]))
			{
				_hash.Add(h, true);
				return true;
			}
		}

		_hash.Add(h, false);
		return false;

		int GetHash(Span<int> arr)
		{
			var hash = new HashCode();
			for (var i = 0; i < arr.Length; i++) hash.Add(arr[i]);
			return hash.ToHashCode();
		}
	}
}