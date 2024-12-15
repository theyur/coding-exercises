<Query Kind="Program" />

void Main()
{
	//List<int> input = [1,2,3,4,5,6,7,8,9,0];
	//List<int> input = [1,2,3,4,5,5,4,3,2,1];
	//List<int> input = [1,0,0,1];
	//List<int> input = [3,5];
	//List<int> input = [];
	//List<int> input = [1,2,3,4,5];
	List<int> input = [1,2,3];

	input.Reverse();
	var head = input.Select(n => new ListNode(n)).Aggregate((cur, prev) => { prev.next = cur; return prev; });
	
	Solution solution = new();
	var r = solution.ReverseBetween(head, 1, 2);
	Console.WriteLine(r);
}

// You can define other methods, fields, classes and namespaces here

//Definition for singly-linked list.
public class ListNode {
    public int val;
    public ListNode next;
    public ListNode(int val=0, ListNode next=null) {
        this.val = val;
        this.next = next;
    }
}
 
public class Solution
{
	public ListNode ReverseBetween(ListNode head, int left, int right)
	{
		if (head?.next == null) return head;
		
		if (left == right) return head;
		
		if (head.next.next == null)
		{
			var t = head.next;
			t.next = head;
			head.next = null;
			return t;
		}
		
		ListNode root = new(int.MinValue, head);
		ListNode back = root;
		
		var i = 1;
		for (; head != null && i < left; head = head.next, back = back.next, i++) ;

		if (right - left == 1)
		{
			back.next = head.next; var t = head.next.next; head.next.next = head; head.next = t;			
			return root.ne;
		}

		var firstTime = true;
		ListNode cur, prev = null, tmp, headReversed = null;//, head1 = head.next;
		for (cur = head; cur != null && i <= right; i++)
		{
			if (firstTime) { headReversed = cur; firstTime = false; }
			tmp = cur.next; cur.next = prev; prev = cur; cur = tmp;
		}

		back.next = prev;
		headReversed.next = cur;
		
		return root.next;
	}
}