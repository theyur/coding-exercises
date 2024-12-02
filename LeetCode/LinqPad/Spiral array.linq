<Query Kind="Program" />

void Main()
{
	var solution = new Solution();
	
	//int[][] p = [[1,2,3],[4,5,6],[7,8,9]];
	int[][] p = [[1,2,3,4],[5,6,7,8],[9,10,11,12]];
	//int[][] p = [[1],[2],[3],[4],[5],[6],[7],[8],[9],[10]];
	//int[][] p = [[1,2,3,4,5,6,7,8,9,10],[11,12,13,14,15,16,17,18,19,20]];	
	//int[][] p = [[1,2,3,4,5],[6,7,8,9,10],[11,12,13,14,15]];
	//int[][] p = [[1,2],[3,4]];
	//int[][] p = [[2,5,8],[4,0,-1]];
	
	var r = solution.SpiralOrder(p);
	
	Console.WriteLine(string.Join(", ", r));
}


public class Solution
{
    public IList<int> SpiralOrder(int[][] matrix)
	{
		if (matrix.Length == 1) return matrix[0];
		if (matrix[0].Length == 1) return matrix.SelectMany(x => x).ToList();
		
		List<int> result = [];
		
		int lX = 0;
		int rX = matrix[0].Length - 1;
		int bY = 0;
		int tY = matrix.Length - 1;

		do
		{
			for (var i = lX; i <= rX; i++) result.Add(matrix[bY][i]);

			for (var i = bY + 1; i <= tY; i++) result.Add(matrix[i][rX]);

			for (var i = rX - 1; i >= lX && tY > bY; i--) result.Add(matrix[tY][i]);

			for (var i = tY - 1; i > bY && lX < rX; i--) result.Add(matrix[i][lX]);
			
			lX++; rX--;
			bY++; tY--;
		} while (lX <= rX && bY <= tY);
	
		return result;
	}
}