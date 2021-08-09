fun main(args: Array<String>) {
    Program().solve()
}

class Program {
    fun solve() {
        val sc = MyScanner()
        val t = sc.nextInt()
        val sb = StringBuilder()
        fun Q(a1: Int, b1: Int, a2: Int, b2: Int): String {
            if (a1 > b1)
                return Q(b1, a1, a2, b2)
            else if (a2 > b2) {
                return Q(a1, b1, b2, a2)
            }
            return if (b1 == b2 && a1 + a2 == b1) "YES" else "NO"
        }
        for (i in 0 until t) {
            sb.appendln(Q(sc.nextInt(), sc.nextInt(), sc.nextInt(), sc.nextInt()))
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
