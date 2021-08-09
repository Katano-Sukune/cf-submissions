fun main(args: Array<String>) {
    Program().solve()
}

class Program {
    fun solve() {
        val sc = MyScanner()

        fun Q() {
            val (n, k) = sc.intList()
            val s = sc.next()
            val f = BooleanArray(n) { false }
            var cnt = s.count { it == '1' }
            var ans = 0
            var pos = 0
            while (cnt > 0) {
                f[pos] = true
                if (s[pos] == '1') cnt--
                ans++
                if (cnt <= 0) break
                var t = 0
                while (t < k) {
                    pos = (pos + 1) % n
                    if (!f[pos]) t++
                }
            }
            println(ans)
        }

        val t = sc.nextInt()
        for (i in 1..t) {
            Q()
        }
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
