package main

import (
	"fmt"
	"slices"
)

func sortedSquares(nums []int) []int {

	for i := 0; i < len(nums); i++ {
		nums[i] *= nums[i]
	}

	slices.Sort(nums)

	return nums
}

func main() {
	nums := []int{-4, -1, 0, 3, 10}
	fmt.Println(sortedSquares(nums)) // Output: [0 1 9 16 100]
}
