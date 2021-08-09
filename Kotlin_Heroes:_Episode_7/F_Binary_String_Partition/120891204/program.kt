import kotlin.math.max

fun main(args: Array<String>) {
    Program().solve()
}

class Program {
    fun solve() {
        val sc = MyScanner()
        val s = sc.next()
        val n = s.length

        val idx = Array(2) { mutableListOf<Int>() }
        for (i in 0 until n) {
            idx[s[i] - '0'].add(i)
        }
        val ans = Array(n) { -1 }
        for (k in 1..n) {
            var cnt = 0
            var pos = 0

            while (pos < n) {
                cnt++
                var ok0 = idx[0].size
                var ng0 = -1
                while (ok0 - ng0 > 1) {
                    val mid0 = (ok0 + ng0) / 2
                    if (idx[0][mid0] >= pos) ok0 = mid0
                    else ng0 = mid0
                }

                var ok1 = idx[1].size
                var ng1 = -1
                while (ok1 - ng1 > 1) {
                    val mid1 = (ok1 + ng1) / 2
                    if (idx[1][mid1] >= pos) ok1 = mid1
                    else ng1 = mid1
                }

                // ok0, ng0 pos以降直後の0,1
                // k+1個先
                if (ok0 + k >= idx[0].size || ok1 + k >= idx[1].size) break
                pos = max(idx[0][ok0 + k], idx[1][ok1 + k])

                // println("$k $pos")
            }

            ans[k - 1] = cnt
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
