<Query Kind="Program" />

void Main()
{
	int[] nums1 = [1,3]; int[] nums2 = [2];
	//int[] nums1 = [1, 2]; int[] nums2 = [3, 4];
	//int[] nums1 = []; int[] nums2 = [1];

	Solution solution = new();

	var r = solution.FindMedianSortedArrays(nums1, nums2);

	Console.WriteLine(r);
}

public class Solution
{
	public double FindMedianSortedArrays(int[] nums1, int[] nums2)
	{
		var arr = new int[nums1.Length + nums2.Length];

		var a = 0; var i = 0; var j = 0;

		if (nums1.Length == 0) arr = nums2;
		else if (nums2.Length == 0) arr = nums1;
		else
			while (!(i == nums1.Length && j == nums2.Length))
			{
				if (nums1[i] <= nums2[j]) arr[a++] = nums1[i++];
				else if (nums1[i] > nums2[j]) arr[a++] = nums2[j++];

				if (i == nums1.Length)
				{
					for (var j1 = j; j1 < nums2.Length; j1++) arr[a++] = nums2[j1];
					break;
				}

				if (j == nums2.Length)
				{
					for (var i1 = i; i1 < nums1.Length; i1++) arr[a++] = nums1[i1];
					break;
				}
			}

		return arr.Length % 2 == 0
			? (arr[arr.Length / 2 - 1] + arr[arr.Length / 2]) / 2.0
			: arr[arr.Length / 2];
	}
}
