<Query Kind="Program" />

void Main()
{
	ListNode[] lists = new ListNode[]
	{
		new(1, new(4, new(5))),
		new(1, new(3, new(4))),
		new(2, new(6)),
	};
	
	Solution solution = new();

	var r = solution.MergeKLists(lists);
	
	Debugger.Break();
}

// You can define other methods, fields, classes and namespaces here
/**
 * Definition for singly-linked list.
 */
 public class ListNode {
     public int val;
     public ListNode next;
     public ListNode(int val=0, ListNode next=null) {
         this.val = val;
         this.next = next;
     }
 }
 
public class Solution1
{
	public ListNode MergeKLists(ListNode[] lists)
	{
		ListNode result = new();
		var startNode = result;
		
		ListNode min = getMin();
		if (min == null) return null;
		
		result.val = min.val;

		while (result != null)
		{
			min = getMin();
			result.next = min;
			result = result.next;
		}
		
		return startNode;
		
		ListNode getMin()
		{
			var min = int.MaxValue;
			var minIdx = -1;
			
			for (var i = 0; i < lists.Length; i++)
				if (lists[i] != null && lists[i].val < min)
					(min, minIdx) = (lists[i].val, i);
					
			if (min == int.MaxValue) return null;
			
			ListNode r = lists[minIdx];
			lists[minIdx] = lists[minIdx].next;
			return r;
		}
	}
}

public class Solution
{
	public ListNode MergeKLists(ListNode[] lists)
	{
		List<int> tempList = [];

		for (var i = 0; i < lists.Length; i++)
			while (lists[i] != null)
			{
				tempList.Add(lists[i].val);
				lists[i] = lists[i].next;
			}
			
		if (tempList.Count == 0) return null;
		
		tempList.Sort();
		
		ListNode start = new(tempList[0]);
		var cur = start;

		for (var i = 1; i < tempList.Count; i++)
		{
			cur.next = new(tempList[i]);
			cur = cur.next;
		}
		
		return start;
	}
}