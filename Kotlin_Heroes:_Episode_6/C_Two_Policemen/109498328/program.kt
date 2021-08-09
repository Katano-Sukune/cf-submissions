import kotlin.math.max
import kotlin.math.min

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

        val n = sc.nextInt()
        var x = sc.nextInt() - 1
        var y = sc.nextInt() - 1
        if (x > y) {
            val tmp = x
            x = y
            y = tmp
        }
        fun f(t: Int): Boolean {
            // 左、右
            // 行ける最大
            var xMax = -1
            if (x <= t) {
                xMax = max(xMax, t - x)
                xMax = max(xMax, x + (t - x) / 2)
            }
            // 右、左
            // 行ける最大

            var yMin = n
            if (n - y - 1 <= t) {
                yMin = min(yMin, n - 1 - (t - (n - y - 1)))
                yMin = min(yMin, y - (t - (n - y - 1)) / 2)
            }

            return yMin <= xMax + 1
        }

        var ok = 2 * n
        var ng = -1
        while (ok - ng > 1) {
            val mid = (ok + ng) / 2
            if (f(mid)) ok = mid
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
