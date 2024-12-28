function hasTrailingZeros(nums: number[]): boolean {
    var evenCount: number = 0;
    for (let n of nums) {
        if (n % 2 == 0) evenCount++;
        if (evenCount == 2) return true;
    }
    return false;
}