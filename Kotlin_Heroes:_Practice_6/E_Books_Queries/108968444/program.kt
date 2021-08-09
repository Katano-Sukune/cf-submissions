import kotlin.*
import kotlin.math.*

fun main(args: Array<String>) {
    Program().solve()
}

class Program {
    fun solve() {
        val sc = MyScanner()
        val q = sc.nextInt()

        val maxID = 200000

        val idx = Array(maxID + 1) { -1 }
        var l = 0
        var r = 0
        for (i in 0 until q) {
            val t = sc.nextChar()
            val id = sc.nextInt()

            when (t) {
                'L' -> {
                    idx[id] = l - 1
                    l--
                }
                'R' -> {
                    idx[id] = r
                    r++
                }
                '?' -> {
                    val p = idx[id]
                    println(min(p - l, r - p - 1))
                }
            }
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
