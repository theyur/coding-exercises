<Query Kind="Program" />

void Main()
{
	var solution = new Solution();

	//int[][] p = [[1,1,1],[1,0,1],[1,1,1]];
	int[][] p = [[0,1,2,0],[3,4,5,2],[1,3,1,5]];

	solution.SetZeroes(p);

	//Console.WriteLine(string.Join(", ", r));
}


public class Solution {
    public void SetZeroes(int[][] matrix)
    {
        int[] iX = new int[matrix[0].Length];
        int[] iY = new int[matrix.Length];

        for (var i = 0; i < matrix.Length; i++)
            for (var j = 0; j < matrix[0].Length; j++)
                if (matrix[i][j] == 0)
				{
					iX[j] = 1;
					iY[i] = 1;
				}

        for (var l = 0; l < iY.Length; l++)
			if (iY[l] == 1)
            	for (var k = 0; k < matrix[0].Length; k++)
                	matrix[l][k] = 0;

        for (var l = 0; l < iX.Length; l++)
			if (iX[l] == 1)
            	for (var k = 0; k < matrix.Length; k++)
                	matrix[k][l] = 0;
	}
}