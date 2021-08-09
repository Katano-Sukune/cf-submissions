import java.util.*
import kotlin.*

import java.io.InputStream
import java.lang.NumberFormatException
import java.lang.StringBuilder
import kotlin.comparisons.compareBy


fun main(args: Array<String>) {
    Program().solve()
}

class Program {
    fun solve() {
        val sc = FastScanner()

        val n = sc.nextInt()
        val m = sc.nextInt()

        val a = Array(n) { sc.nextInt() }

        // 全部 - m強が中央値になる m未満が中央値になる

        // m強が中央値
        // m強 - m以下 > 0
        var cntA = 0L
        val s = Array(n) { if (a[it] > m) 1 else -1 }

        val sumA = Array(n + 1) { 0 }
        for (i in 0 until n) {
            sumA[i + 1] = sumA[i] + s[i]
        }
        // sumA[i] - sumA[j] > 0
        val stA = SegmentTree({ l, r -> l + r }, 0)
        for (i in 0..n) {
            cntA += stA.query(0, sumA[i] + 200000)
            stA[sumA[i] + 200000]++
        }

        // m未満が中央値
        // m未満 - m以上 >= 0

        var cntB = 0L
        val t = Array(n) { if (a[it] < m) 1 else -1 }
        val sumB = Array(n + 1) { 0 }
        // println(t.joinToString(" "))
        for (i in 0 until n) {
            sumB[i + 1] += sumB[i] + t[i]
        }
        // sumB[i] - sumB[j] > 0
        val stB = SegmentTree({ l, r -> l + r }, 0)
        for (i in 0..n) {
            cntB += stB.query(0, sumB[i] + 200000 + 1)
            stB[sumB[i] + 200000]++
        }


        val all = (n + 1).toLong() * n / 2

        println(all - cntA - cntB)
        // println("$all $cntA $cntB")
    }
}

class SegmentTree<T>(private val op: (T, T) -> T, private val id: T) {
    companion object {
        const val size = 1 shl 21
    }

    @Suppress("UNCHECKED_CAST")
    private val array = Array<Any?>(size * 2) { id } as Array<T>

    operator fun get(i: Int) = array[i + size]
    operator fun set(i: Int, n: T) = update(i, n)

    fun update(i: Int, n: T) {
        var index = i + size
        array[index] = n
        while (index > 1) {
            index /= 2
            array[index] = op(array[index * 2], array[index * 2 + 1])
        }
    }

    private fun query(left: Int, right: Int, k: Int, l: Int, r: Int): T {
        if (left <= l && r <= right) {
            return array[k]
        }

        if (r <= left || right <= l) {
            return id
        }

        return op(query(left, right, k * 2, l, (l + r) / 2), query(left, right, k * 2 + 1, (l + r) / 2, r))
    }

    fun query(left: Int, right: Int) = query(left, right, 1, 0, size)
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