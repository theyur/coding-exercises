<Query Kind="Program" />

void Main()
{
	Solution solution = new();
	//var r = solution.TwoSum([2,7,11,15], 9);
	//var r = solution.TwoSum([3,3], 6);
	var r = solution.TwoSum([3,2,4], 6);
	//var r = solution.TwoSum([-1,-2,-3,-4,-5], -8);
	//var r = solution.TwoSum([5,75,25], 100);
	//var r = solution.TwoSum([2,5,5,11], 10);
	
	Debugger.Break();
}
/*
public class Solution
{
	public int[] TwoSum(int[] nums, int target)
	{
		var l = nums.ToList();
		l.Sort();

		var i1 = 0;
		var i2 = 0;
		
		for (var i = 0; i < l.Count; i++)
		{
			var idx = l.FindIndex(i + 1, x => x == (target - l[i]));
			if (idx < 0) continue;
			
			i1 = i;
			i2 = idx;
			
			if (Math.Abs(l[i1]) > Math.Abs(l[i2])) (i1, i2) = (i2, i1);
			break;
		}

		var found = 0;
		var ii1 = 0; var ii2 = 0;
		for (var i = 0; i < nums.Length && found < 2; i++)
		{
			if (nums[i] == l[i1])
			{
				ii1 = i;
				found++;
				nums[i] = int.MinValue;
				l[i1] = int.MinValue;
				continue;
			}
			if (nums[i] == l[i2])
			{
				ii2 = i;
				found++;
				nums[i] = int.MinValue;
				l[i2] = int.MinValue;
			}
		}
		return new[] {Math.Min(ii1, ii2), Math.Max(ii1, ii2)};
	}
}
*/

public class Solution
{
	public int[] TwoSum(int[] nums, int target)
	{
		Dictionary<int, int> dict = [];
		for (var i = 0; i < nums.Length; i++) 
			if (dict.ContainsKey(nums[i])) dict[nums[i]] = i;
			else dict.Add(nums[i], i);

		for (var i = 0; i < nums.Length; i++)
			if (dict.ContainsKey(target - nums[i]) && dict[target - nums[i]] != i)
				return new int[] { i, dict[target - nums[i]] };

		return new int[0];
	}
}
