package main

import (
	"fmt"
)

func main() {
	//firstList := [][]int{{0, 2}, {5, 10}, {13, 23}, {24, 25}}
	//secondList := [][]int{{1, 5}, {8, 12}, {15, 24}, {25, 26}}

	firstList := [][]int{{17, 20}}
	secondList := [][]int{{2, 3}, {6, 8}, {12, 14}, {19, 20}}

	r := intervalIntersection(firstList, secondList)

	fmt.Println(r)
}

func intervalIntersection(firstList [][]int, secondList [][]int) [][]int {
	i := 0
	j := 0
	r := [][]int{}

	for i < len(firstList) && j < len(secondList) {
		_r := analyze2Elements(firstList[i], secondList[j])
		fmt.Println(i, j, _r, firstList[i], secondList[j])

		if len(_r) == 0 {
			fmt.Println("Empty return, i: ", i, ", j: ", j, "; f[1]: ", firstList[i][1], ", s[1]: ", secondList[j][1])
			if firstList[i][1] < secondList[j][1] {
				i++
			} else {
				j++
			}

			continue
		}

		r = append(r, _r)

		fmt.Println("  > ", firstList[i][1], ", ", secondList[j][1])
		if firstList[i][1] < secondList[j][1] {
			i++
			fmt.Println("++ i: ", i)
		} else if firstList[i][1] > secondList[j][1] {
			j++
			fmt.Println("++ j: ", j)
		} else {
			i++
			j++
			fmt.Println("++ i: ", i, ", ++ j: ", j)
		}
	}

	return r
}

func analyze2Elements(first, second []int) []int {

	fmt.Println("----", first, second)

	if first[1] < second[0] || first[0] > second[1] {
		return []int{}
	}

	var r [2]int

	if first[0] > second[0] {
		r[0] = first[0]
	} else {
		r[0] = second[0]
	}

	if first[1] < second[1] {
		r[1] = first[1]
	} else {
		r[1] = second[1]
	}

	if first[1] == second[0] {
		r = [2]int{first[1], first[1]}
	}

	if first[0] == second[1] {
		r = [2]int{first[0], first[0]}
	}

	return r[:]
}
