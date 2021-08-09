fun main(args: Array<String>) {
    Program().solve()
}

class Program {
    fun solve() {
        val sc = MyScanner()
        val t = sc.nextInt()
        val sb = StringBuilder()
        fun Q(n: Int, a: IntArray): String {
            var alice = 0
            var pa = 0
            var i = 0
            var bob = 0
            var pb = 0
            var j = n-1
            var cnt = 0
            while (i <= j) {

                if (cnt % 2 == 0) {
                    pa = 0
                    while (pa <= pb && i <= j) {
                        pa += a[i]
                        i++
                    }
                    alice += pa
                } else {
                    pb = 0
                    while (pb <= pa && i <= j) {
                        pb += a[j]
                        j--
                    }
                    bob += pb
                }
                cnt++
            }
            return "$cnt $alice $bob"

        }
        for (i in 0 until t) {
            sb.appendln(Q(sc.nextInt(), sc.intArray()))
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
