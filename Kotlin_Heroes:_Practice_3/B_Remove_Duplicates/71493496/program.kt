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
        val n = sc.nextInt()
        val a = Array(n) { sc.nextInt() }

        val hs = HashSet<Int>()

        val ans = mutableListOf<Int>()

        for (i in n - 1 downTo 0) {
            if (hs.add(a[i])) {
               ans.add(a[i])
            }
        }

        println(hs.size)
        println(ans.reversed().joinToString(" "))
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
