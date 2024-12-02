// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

var inputFile = new StreamReader("input15.txt");

string[] firstMultipleInput = inputFile.ReadLine().TrimEnd().Split(' ');

int n1 = Convert.ToInt32(firstMultipleInput[0]);

int n2 = Convert.ToInt32(firstMultipleInput[1]);

int n3 = Convert.ToInt32(firstMultipleInput[2]);

List<int> h1 = inputFile.ReadLine().TrimEnd().Split(' ').ToList().Select(h1Temp => Convert.ToInt32(h1Temp)).ToList();

List<int> h2 = inputFile.ReadLine().TrimEnd().Split(' ').ToList().Select(h2Temp => Convert.ToInt32(h2Temp)).ToList();

List<int> h3 = inputFile.ReadLine().TrimEnd().Split(' ').ToList().Select(h3Temp => Convert.ToInt32(h3Temp)).ToList();

int result = Result.equalStacks(h1, h2, h3);

Console.WriteLine(result);

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
		
		public readonly ListElement[] Lists = new ListElement[3];
		
		public ListManager(List<int> h0, List<int> h1, List<int> h2)
		{
			Primary = new() { I = 0, Sum = h0.Sum(), List = h0 };
			Secondary1 = new() { I = 0, Sum = h1.Sum(), List = h1 };
			Secondary2 = new() { I = 0, Sum = h2.Sum(), List = h2 };

			if (Primary.Sum <= Secondary1.Sum && Primary.Sum <= Secondary1.Sum)
			{
				Lists[0] = Primary;
				Lists[1] = Secondary1;
				Lists[2] = Secondary2;
			}
			else if (Secondary1.Sum <= Primary.Sum && Secondary1.Sum <= Secondary2.Sum)
			{
				Lists[0] = Secondary1;
				Lists[1] = Primary;
				Lists[2] = Secondary2;
			}
			else if (Secondary2.Sum <= Primary.Sum && Secondary2.Sum <= Secondary1.Sum)
			{
				Lists[0] = Secondary2;
				Lists[1] = Primary;
				Lists[2] = Secondary1;
			}
			else throw new Exception();
		}

		public void SetPrimary(int idx)
		{
			switch (idx)
			{
				case 0:
					Primary = Lists[0];
					Secondary1 = Lists[1];
					Secondary2 = Lists[2];
					break;
				case 1:
					Primary = Lists[1];
					Secondary1 = Lists[2];
					Secondary2 = Lists[0];
					break;
				case 2:
					Primary = Lists[2];
					Secondary1 = Lists[0];
					Secondary2 = Lists[1];
					break;
			}
			
			Lists[0] = Primary;
			Lists[1] = Secondary1;
			Lists[2] = Secondary2;
		}
	}

	public static int equalStacks(List<int> h1, List<int> h2, List<int> h3)
	{
		ListManager lm = new(h1, h2, h3);

		var iterationCount = 0;
		
		do
		{
			if (iterationCount++ % 10000 == 0) Console.WriteLine($"{iterationCount} {lm.Primary.Sum} {lm.Secondary1.Sum} {lm.Secondary2.Sum}");
			
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