<Query Kind="Program">
  <RuntimeVersion>9.0</RuntimeVersion>
</Query>

void Main()
{	
	var r = Result.equalStacks([3,2,1,1,1], [4,3,2], [1,1,4,1]);
	
	Console.WriteLine(r);
}

// You can define other methods, fields, classes and namespaces here

class Result
{

	/*
     * Complete the 'equalStacks' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER_ARRAY h1
     *  2. INTEGER_ARRAY h2
     *  3. INTEGER_ARRAY h3
     */

	class ListElement
	{
		public int I { get; set; }
		public int Sum { get; set; }
		public List<int> List { get; set; }
	}
	
	class ListManager
	{
		public ListElement Primary { get; private set; }
		public ListElement Secondary1 { get; private set; }
		public ListElement Secondary2 { get; private set; }
		
		private ListElement[] _lists = new ListElement[3];
		
		public ListManager(List<int> h0, List<int> h1, List<int> h2)
		{
			Primary = new() { I = 0, Sum = h0.Sum(), List = h0 };
			Secondary1 = new() { I = 0, Sum = h1.Sum(), List = h1 };
			Secondary2 = new() { I = 0, Sum = h2.Sum(), List = h2 };

			if (Primary.Sum <= Secondary1.Sum && Primary.Sum <= Secondary1.Sum)
			{
				_lists[0] = Primary;
				_lists[1] = Secondary1;
				_lists[2] = Secondary2;
			}
			else if (Secondary1.Sum <= Primary.Sum && Secondary1.Sum <= Secondary2.Sum)
			{
				_lists[0] = Secondary1;
				_lists[1] = Primary;
				_lists[2] = Secondary2;
			}
			else if (Secondary2.Sum <= Primary.Sum && Secondary2.Sum <= Secondary1.Sum)
			{
				_lists[0] = Secondary2;
				_lists[1] = Primary;
				_lists[2] = Secondary1;
			}
			else throw new Exception();
		}

		public void SetPrimary(int idx)
		{
			switch (idx)
			{
				case 0:
					Primary = _lists[0];
					Secondary1 = _lists[1];
					Secondary2 = _lists[2];
					break;
				case 1:
					Primary = _lists[1];
					Secondary1 = _lists[0];
					Secondary2 = _lists[2];
					break;
				case 2:
					Primary = _lists[2];
					Secondary1 = _lists[0];
					Secondary2 = _lists[1];
					break;
			}
		}
	}

	public static int equalStacks(List<int> h1, List<int> h2, List<int> h3)
	{
		ListManager lm = new(h1, h2, h3);

		do
		{
			while (lm.Secondary1.Sum > lm.Primary.Sum)
			{
				lm.Secondary1.Sum -= lm.Secondary1.List[lm.Secondary1.I];
				lm.Secondary1.I++;
				if (lm.Secondary1.I == lm.Secondary1.List.Count) return 0;
			}

			if (lm.Secondary1.Sum < lm.Primary.Sum)
			{
				lm.SetPrimary(1);
				continue;
			}

			while (lm.Secondary2.Sum > lm.Primary.Sum)
			{
				lm.Secondary2.Sum -= lm.Secondary2.List[lm.Secondary2.I];
				lm.Secondary2.I++;
				if (lm.Secondary2.I == lm.Secondary2.List.Count) return 0;
			}

			if (lm.Secondary2.Sum < lm.Primary.Sum) lm.SetPrimary(2);
			
		} while (lm.Primary.Sum != lm.Secondary1.Sum && lm.Primary.Sum != lm.Secondary2.Sum);

		return lm.Primary.Sum;
	}
}