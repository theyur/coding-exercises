package main

type ListNode struct {
	Val  int
	Next *ListNode
}

func main() {
	list1 := &ListNode{1, &ListNode{2, &ListNode{4, nil}}}
	list2 := &ListNode{1, &ListNode{3, &ListNode{4, nil}}}

	r := mergeTwoLists(list1, list2)

	for r != nil {
		println(r.Val)
		r = r.Next
	}
}

func mergeTwoLists(list1 *ListNode, list2 *ListNode) *ListNode {
	if list1 == nil && list2 == nil {
		return nil
	}

	var r = new(ListNode)

	if list1 == nil {
		r = list2
	} else if list2 == nil {
		r = list1
	} else if list1.Val < list2.Val {
		r = list1
		r.Next = mergeTwoLists(list1.Next, list2)
	} else {
		r = list2
		r.Next = mergeTwoLists(list1, list2.Next)
	}

	return r
}
