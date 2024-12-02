// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

var heights = new List<int> { 10, 30, 50, 40, 20 };

var result = Result.largestRectangle(heights);

Console.WriteLine(result);


class Result
{

    /*
     * Complete the 'largestRectangle' function below.
     *
     * The function is expected to return a LONG_INTEGER.
     * The function accepts INTEGER_ARRAY h as parameter.
     */

    public static long largestRectangle(List<int> h)
    {
        long maxArea = 0;
        Stack<int> stack = new Stack<int>();
        int i = 0;
        while (i < h.Count)
        {
            if (stack.Count == 0 || h[stack.Peek()] <= h[i])
            {
                stack.Push(i);
                i++;
            }
            else
            {
                int top = stack.Pop();
                long area = h[top] * (stack.Count == 0 ? i : i - stack.Peek() - 1);
                maxArea = Math.Max(maxArea, area);
            }
        }
        while (stack.Count > 0)
        {
            int top = stack.Pop();
            long area = h[top] * (stack.Count == 0 ? i : i - stack.Peek() - 1);
            maxArea = Math.Max(maxArea, area);
        }
        return maxArea;
    }

}