import java.util.*
import kotlin.*

import java.io.InputStream
import java.lang.NumberFormatException
import java.lang.StringBuilder


fun main(args: Array<String>) {
    Program().solve()
}

class Program {
    fun solve() {
        val sc = FastScanner()
        val sj = StringJoiner("\n")
        val t = sc.nextInt()
        for (i in 0 until t) {
            val n = sc.nextInt()
            val a = Array(n) { sc.nextInt() }
            sj.add(q(n, a))
        }

        println(sj.toString())
    }

    private fun q(n: Int, a: Array<Int>): String {
        // a 各開発者の能力
        // チームを作る チームの強さ 能力の合計

        val p = mutableListOf<Int>()
        val m = mutableListOf<Int>()

        var sum = 0L
        for (i in 0 until n) {
            if (a[i] > 0) {
                p.add(i)
                sum += a[i]
            }
            if (a[i] < 0) {
                m.add(i)
            }
        }

        // plus最小を消す
        val t = p.minBy { a[it] }!!
        val tt = sum - a[t]


        // minus最小を追加
        val f = m.size > 0
        val s = if (f) m.maxBy { a[it] }!! else -1
        val ss = if (f) sum + a[s] else Long.MIN_VALUE

        val ans = CharArray(n) { '0' }
        for (i in p.indices) {
            ans[p[i]] = '1'
        }
        return if (tt < ss) {
            ans[s] = '1'
            "$ss\n${String(ans)}"
        } else {
            ans[t] = '0'
            "$tt\n${String(ans)}"
        }
    }
}


class FastScanner {
    companion object {
        val input: InputStream = System.`in`
        val buffer = ByteArray(1024) { 0 }
        fun isPrintableChar(c: Int): Boolean = c in 33..126
    }

    var ptr = 0
    var buflen = 0
    private fun hasNextByte(): Boolean {
        if (ptr < buflen) {
            return true
        } else {
            ptr = 0
            buflen = input.read(buffer)
            if (buflen <= 0) {
                return false
            }
        }
        return true
    }

    private fun readByte(): Int = if (hasNextByte()) buffer[ptr++].toInt() else -1

    private fun skipUnprintable() {
        while (hasNextByte() && !isPrintableChar(buffer[ptr].toInt())) ptr++
    }

    fun hasNext(): Boolean {
        skipUnprintable()
        return hasNextByte()
    }

    fun next(): String {
        if (!hasNext()) throw NoSuchElementException()
        val sb = StringBuilder()
        var b = readByte()
        while (isPrintableChar(b)) {
            sb.appendCodePoint(b)
            b = readByte()
        }
        return sb.toString()
    }

    fun nextInt(): Int {
        if (!hasNext()) throw NoSuchElementException()
        var n = 0
        var b = readByte()
        // '-' = 45
        val minus = b == 45
        if (minus) {
            b = readByte()
        }

        // '0' = 48 '9' = 57
        if (b !in 48..57) {
            throw NumberFormatException()
        }

        while (true) {
            if (b in 48..57) {
                n *= 10
                n += b - 48
            } else if (b == -1 || !isPrintableChar(b)) {
                return if (minus) -n else n
            } else {
                throw NumberFormatException()
            }
            b = readByte()
        }
    }

    fun nextLong(): Long {
        if (!hasNext()) throw NoSuchElementException()
        var n = 0L
        var b = readByte()
        // '-' = 45
        val minus = b == 45
        if (minus) {
            b = readByte()
        }

        // '0' = 48 '9' = 57
        if (b !in 48..57) {
            throw NumberFormatException()
        }

        while (true) {
            if (b in 48..57) {
                n *= 10
                n += b - 48
            } else if (b == -1 || !isPrintableChar(b)) {
                return if (minus) -n else n
            } else {
                throw NumberFormatException()
            }
            b = readByte()
        }
    }

    fun nextDouble(): Double = next().toDouble()
}
