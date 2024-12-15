<Query Kind="Program" />

void Main()
{
	List<int> input = [1,2,3,4,5,5,6,6,7,8];
	//List<int> input = [1,2,3,4,5,5,4,3,2,1];
	//List<int> input = [1,0,0,1];
	//List<int> input = [1,2];
	//List<int> input = [];

	input.Reverse();
	var head = input.Select(n => new ListNode(n)).Aggregate((cur, prev) => { prev.next = cur; return prev; });
	
	Solution solution = new();
	var r = solution.ReverseList(head);
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
	public ListNode ReverseList(ListNode head)
	{
		if (head?.next == null) return head;

		ListNode tmp, prev = null;
		for (ListNode cur = head; cur != null;)
		{
			tmp = cur.next; cur.next = prev; prev = cur; cur = tmp;
		}
		
		return prev;
	}
}