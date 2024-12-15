<Query Kind="Program" />

void Main()
{
	//List<int> input = [1,2,3,4,5,5,4,3,2,1];
	//List<int> input = [1,0,0,1];
	List<int> input = [1,2];

	input.Reverse();
	var head = input.Select(n => new ListNode(n)).Aggregate((cur, prev) => { prev.next = cur; return prev; });
	
	Solution solution = new();
	var r = solution.IsPalindrome(head);
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
	public bool IsPalindrome(ListNode head)
	{
		if (head.next == null) return true;
		if (head.next.next == null) return head.val == head.next.val;
		
		ListNode slow, fast;
		
		for (slow = head.next, fast = head.next.next; fast?.next != null; slow = slow.next, fast = fast.next.next) ;
		
		ListNode prev = null, cur, tmp;
		
		for (cur = slow; cur != null;)
		{
			tmp = cur.next; cur.next = prev; prev = cur; cur = tmp;
		}
		
		ListNode headLeft  = new(head.val, head);
		ListNode headRight = new(prev.val, prev);
		for (; headRight != null; headLeft = headLeft.next, headRight = headRight.next)
			if (headLeft.val != headRight.val) return false;
		
		return true;
	}
}