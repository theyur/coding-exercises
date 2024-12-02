<Query Kind="Program" />

void Main()
{
	var p = "1 2 3 4 5".Split(' ').Select(int.Parse).ToList();
	//var p = "5 4 3 2 1".Split(' ').Select(int.Parse).ToList();
	//var p = "1 1 1 1 1".Split(' ').Select(int.Parse).ToList();
	//var p = "3 2 5 4".Split(' ').Select(int.Parse).ToList();
	//var p = "6 5 8 4 7 10 9".Split(' ').Select(int.Parse).ToList();
	//var p = "4 3 7 5 6 4 2".Split(' ').Select(int.Parse).ToList();
	
	var result = Result.poisonousPlants(p);
	
}

// You can define other methods, fields, classes and namespaces here
class Result
{

	/*
     * Complete the 'poisonousPlants' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts INTEGER_ARRAY p as parameter.
     */

	public static int poisonousPlants(List<int> plant)
	{
		int days = 0;
		
		int[] life = new int[plant.Count];		
		life[0] = int.MaxValue;
		for (var i = 1; i < life.Length; i++) life[i] = 1;
		
		Stack<int> S = new();
		S.Push(0);
		
		for (var i = 1; i < plant.Count; i++)
		{
			while ((life[i] > life[S.Peek()] || (plant[i] <= plant[S.Peek()] && life[S.Peek()] != int.MaxValue)))
			{
				life[i] = Math.Max(life[i], life[S.Peek()] + 1);
				S.Pop();
			}
			
			if (plant[i] <= plant[S.Peek()] && life[S.Peek()] == int.MaxValue) life[i] = int.MaxValue;
			
			if (life[i] != int.MaxValue) days = Math.Max(days, life[i]);
			
			S.Push(i);
		}
		
		return days;
	}
}
