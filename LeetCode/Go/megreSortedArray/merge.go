package main

func main() {
	nums1 := []int{1, 2, 3, 0, 0, 0}
	nums2 := []int{2, 5, 6}
	merge(nums1, 3, nums2, 3)

	for _, v := range nums1 {
		println(v)
	}
}

func merge(nums1 []int, m int, nums2 []int, n int) {
	i, j := m-1, n-1

	for k := m + n - 1; k >= 0; k-- {
		if i < 0 {
			nums1[k] = nums2[j]
			j--
		} else if j < 0 {
			nums1[k] = nums1[i]
			i--
		} else if nums1[i] > nums2[j] {
			nums1[k] = nums1[i]
			i--
		} else {
			nums1[k] = nums2[j]
			j--
		}
	}
}
