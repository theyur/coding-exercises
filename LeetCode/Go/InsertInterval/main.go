package main

import (
	"cmp"
	"fmt"
	"slices"
)

func main() {
	//intervals := [][]int{{1, 3}, {6, 9}}
	//newInterval := []int{2, 5}
	//intervals := [][]int{{1, 2}, {3, 5}, {6, 7}, {8, 10}, {12, 16}}
	//newInterval := []int{4, 8}
	intervals := [][]int{{1, 5}}
	//newInterval := []int{2, 3}
	newInterval := []int{0, 0}
	//intervals := [][]int{{3, 5}, {12, 15}}
	//newInterval := []int{6, 6}

	r := insert(intervals, newInterval)
	fmt.Println(r)
}

const (
	Before = iota
	InProg
	After
)

func insert(intervals [][]int, newInterval []int) [][]int {
	if len(intervals) == 0 {
		return [][]int{newInterval}
	}

	if len(newInterval) == 0 {
		return intervals
	}

	state := Before

	r := [][]int{}
	ni := slices.Clone(newInterval)

	i := 0
	for ; i < len(intervals); i++ {
		l := max(intervals[i][0], ni[0])
		h := min(intervals[i][1], ni[1])

		if l > h {
			switch state {
			case Before, After:
				r = append(r, intervals[i])
				fmt.Println("> ", state, r, ni, i)
			case InProg:
				r = append(r, ni)
				r = append(r, intervals[i])
				fmt.Println(">> ", state, r, ni, i)
				state = After
			}
			continue
		}

		state = InProg

		l1 := min(intervals[i][0], ni[0])
		h1 := max(intervals[i][1], ni[1])

		ni = []int{l1, h1}
		fmt.Println(">>> ", state, r, ni, i)
	}

	if state == Before || state == InProg {
		r = append(r, ni)
		fmt.Println("Checking..: ", i, r)
		if len(r) > 1 && r[len(r)-1][0] < r[len(r)-2][0] {
			slices.SortFunc(r, func(a, b []int) int {
				return cmp.Compare(a[0], b[0])
			})
		}
	}

	return r
}
