import kotlin.*
import kotlin.math.*

fun main(args: Array<String>) {
    Program().solve()
}

class Program {
    fun solve() {
        val sc = MyScanner()
        val n = sc.nextInt()
        val s = Array(n) { sc.next() }

        s.sortBy { it.length }

        var flag = true
        for (i in 0 until n - 1) {
            if (!flag) break
            var f2 = false
            for (j in 0..(s[i + 1].length - s[i].length)) {
                if (f2) break
                var f3 = true
                for (k in s[i].indices) {
                    if (!f3) break
                    f3 = f3 && (s[i][k] == s[i + 1][k + j])
                }
                f2 = f2 || f3
            }
            flag = flag && f2
        }
        if (flag) {
            println("YES")
            println(s.joinToString("\n"))
        } else {
            println("NO")
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
