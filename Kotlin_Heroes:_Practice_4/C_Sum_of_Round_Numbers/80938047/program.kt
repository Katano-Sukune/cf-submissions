fun main(args: Array<String>) {
    Program().solve()
}

class Program {
    fun solve() {
        val sc = MyScanner()
        val t = sc.nextInt()
        val sb = StringBuilder()
        fun Q(n: String): String {
            val ls = mutableListOf<String>()
            for (i in n.indices) {
                if (n[n.length - 1 - i] != '0') {
                    ls.add("${n[n.length - 1 - i]}${String(CharArray(i) { '0' })}")
                }
            }
            return "${ls.size}\n${ls.joinToString(" ")}"
        }
        for (i in 0 until t) {
            sb.appendln(Q(sc.next()))
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
