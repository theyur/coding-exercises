package main

import (
	"cmp"
	"fmt"
	"slices"
)

func main() {
	//intervals := [][]int{{1, 3}, {2, 6}, {8, 10}, {15, 18}}
	//intervals := [][]int{{1, 4}, {4, 5}}
	//intervals := [][]int{{1, 4}, {5, 6}}
	//intervals := [][]int{{1, 4}, {0, 2}, {3, 5}}
	intervals := [][]int{{2, 3}, {4, 5}, {6, 7}, {8, 9}, {1, 10}}
	//intervals := [][]int{{2, 3}, {5, 5}, {2, 2}, {3, 4}, {3, 4}}

	r := merge(intervals)

	fmt.Println(r)
}

func merge(intervals [][]int) [][]int {

	if len(intervals) <= 1 {
		return intervals
	}

	eltCmp := func(a, b []int) int {
		return cmp.Compare(a[0], b[0])
	}

	slices.SortFunc(intervals, eltCmp)

	r := [][]int{}
	ri := [2]int{intervals[0][0], intervals[0][1]}

	for f, s := 0, 1; f < len(intervals) && s < len(intervals); {

		fs := ri[0]
		fe := ri[1]

		ss := intervals[s][0]
		se := intervals[s][1]

		fmt.Println(ri, intervals[s])

		start := max(fs, ss)
		end := min(fe, se)

		startExt := min(fs, ss)
		endExt := max(fe, se)

		if start <= end {
			ri[0] = startExt
			ri[1] = endExt

			fmt.Println(ri)

			s++
		} else if ri[0] >= 0 {
			r = append(r, []int{ri[0], ri[1]})

			f = s
			s++

			ri[0] = intervals[f][0]
			ri[1] = intervals[f][1]
		} else {
			r = append(r, intervals[f], intervals[s])

			f = s + 1
			s = s + 2
		}
	}

	if ri[0] >= 0 {
		r = append(r, ri[:])
	}

	return r
}
