fun main(args: Array<String>) {
    Program().solve()
}

class Program {
    fun solve() {
        val sc = MyScanner()
        val t = sc.nextInt()
        val sb = StringBuilder()
        fun Q(n: Int): String {
            if (n == 2 || n == 3) return "-1"
            val ans = mutableListOf<Int>()
            if (n % 2 == 0) {
                // 2 4 6 ... n/2 n/2-3 n/2-1 n/2-5
                var i = 2

                while (i <= n) {
                    ans.add(i)
                    i += 2
                }
                ans.add(n - 3)
                ans.add(n - 1)
                i = n - 5
                while (i >= 1) {
                    ans.add(i)
                    i -= 2
                }
            } else {
                // 1 3 5 ... n n - 3 n - 1 n-5
                var i = 1
                while (i <= n) {
                    ans.add(i)
                    i += 2
                }

                ans.add(n - 3)
                ans.add(n - 1)
                i = n - 5
                while (i >= 2) {
                    ans.add(i)
                    i -= 2
                }
            }
            return ans.joinToString(" ")
        }
        for (i in 0 until t) {
            sb.appendln(Q(sc.nextInt()))
        }
        print(sb.toString())
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
