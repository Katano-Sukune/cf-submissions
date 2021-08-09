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
        val k = sc.nextInt()
        val s = sc.next()

        // 括弧列 s
        // 1 接頭辞を消す
        // 2 連続部分列を消す
        // 括弧列　保つ
        // 2はk回まで
        // sが空になるまで操作最大


        // (())()

        var cnt = 0
        var zero = 0
        for (c in s) {
            if (c == '(') {
                cnt++
            } else {
                cnt--
            }
            if (cnt == 0) {
                zero++
            }
        }

        println(zero + min(n / 2 - zero, k))
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
