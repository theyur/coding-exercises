<Query Kind="Program" />

void Main()
{
	var solution = new Solution();
	
	var r = solution.IsRobotBounded("GGLLGG");
	
	r.Dump();
}

// You can define other methods, fields, classes and namespaces here
public class Solution
{
	struct MovingData
	{
		public int sum;
		public int val;
	}
	
	public bool IsRobotBounded(string instructions)
	{
		var moves = new MovingData[2];	// 0 - vert, 1 - horz

		moves[0].val = 1;
		moves[1].val = 1;

		ref MovingData data = ref moves[0];

		var turnCount = 0;
		var prevTurn = '$';

		for (var i = 1; i <= 4; i++)
		{
			foreach (var ch in instructions)
			{
				if (ch == 'L' || ch == 'R')
				{
					turnCount++;

					if ((prevTurn ^ ch) == 0)
						moves[turnCount % 2].val *= -1;
					else
						prevTurn = ch;

					continue;
				}

				moves[turnCount % 2].sum += moves[turnCount % 2].val;
			}
			
			if (moves[0].sum == 0 && moves[1].sum == 0) break;	// skip 3 extra loops
		}

		return moves[0].sum == 0 && moves[1].sum == 0;
	}
}