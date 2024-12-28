<Query Kind="Program" />

void Main()
{
	
}

// You can define other methods, fields, classes and namespaces here
public class Solution
{
	public int[][] IntervalIntersection(int[][] firstList, int[][] secondList)
	{
		List<int[]> r = [];

		for (var (f, s) = (0, 0); f < firstList.Length && s < secondList.Length;)
		{
			var (fs, fe, ss, se) = (
				firstList[f][0],
				firstList[f][1],
				secondList[s][0],
				secondList[s][1]
			);
			
			var start = Math.Max(fs, ss);
			var end = Math.Min(fe, se);
			
			if (start <= end) r.Add([start, end]);
			
			if (fe <= se) f++;
			else s++;
		}
		
		return r.ToArray();
	}
}