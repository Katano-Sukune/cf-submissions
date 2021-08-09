fun main(args: Array<String>) {
    Program().solve()
}

class Program {
    fun solve() {
        val sc = MyScanner()
        val t = sc.nextInt()
        for (q in 1..t) {
            Q(sc)
        }
    }

    fun Q(sc: MyScanner) {
        val n = sc.nextLong()
        val s = sc.nextLong()
        // n日
        // s問題

        // a_i i日に解いた問題数
        // 1 <= a_i <= n
        // a_i <= a_{i+1}
        // 2* a_i >= a_{i+1}

        // a_n 最大
        //  2 4

        // 2 3 4
        var ng = s + 1
        var ok = 0L
        while (ng - ok > 1) {
            val mid = (ok + ng) / 2
            var tmp = mid
            var day = 0L
            var sum = 0L
            while (day < n && tmp > 1) {
                sum += tmp
                tmp = (tmp + 1) / 2
                day++
            }

            // 最小 sum + (n-day)
            if (sum <= s - (n - day)) ok = mid
            else ng = mid

        }

        println(ok)
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
