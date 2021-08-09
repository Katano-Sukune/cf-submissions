fun main(args: Array<String>) {
    Program().solve()
}

class Program {
    fun solve() {
        val sc = MyScanner()
        val n = sc.nextInt()
        val a = sc.intList().sorted()
        val b = sc.intList().sorted()

        val m = sc.nextInt()
        val c = sc.intList()

        val max1 = Array(n + 1) { Int.MIN_VALUE }
        for (i in 0 until n) {
            max1[i + 1] = Math.max(max1[i], b[i] - a[i])
        }

        val max2 = Array(n + 1) { Int.MIN_VALUE }
        for (i in n - 1 downTo 0) {
            max2[i] = Math.max(max2[i + 1], b[i + 1] - a[i])
        }

        val ans = Array(m) { -1 }
        for (q in 0 until m) {
            var ok = -1
            var ng = n
            while (ng - ok > 1) {
                val mid = (ok + ng) / 2
                if (a[mid] <= c[q]) ok = mid
                else ng = mid
            }

            // 0~ok max1
            ans[q] = Math.max(Math.max(max1[ng], max2[ng]), b[ng] - c[q])
        }

        println(ans.joinToString(" "))
    }
}

class MyScanner {
    private var ptr = 0
    private var l = listOf<String>()

    fun next(): String {
        while (l.size <= ptr) {
            l = readLine()!!.split(' ')
            ptr = 0
        }
        return l[ptr++]
    }

    fun nextInt() = next().toInt()
    fun nextLong() = next().toLong()
    fun nextDouble() = next().toDouble()
    fun nextCharArray() = next().toCharArray()
    fun nextChar() = next()[0]

    fun list(): List<String> {
        ptr = l.size
        return readLine()!!.split(' ')
    }

    fun intList() = list().map { s -> s.toInt() }
    fun longList() = list().map { s -> s.toLong() }
    fun doubleList() = list().map { s -> s.toDouble() }

    fun array() = list().toTypedArray()
    fun intArray() = intList().toIntArray()
    fun longArray() = longList().toLongArray()
    fun doubleArray() = doubleList().toDoubleArray()
}
