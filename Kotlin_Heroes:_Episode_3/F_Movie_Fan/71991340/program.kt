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
        val t = sc.nextInt()
        val sj = StringJoiner("\n")
        for (i in 0 until t) {
            val n = sc.nextInt()
            val m = sc.nextInt()
            val a = Array(n) { 0 }
            val b = Array(n) { 0 }
            for (j in 0 until n) {
                a[j] = sc.nextInt()
                b[j] = sc.nextInt()
            }
            sj.add(q(n, m, a, b))
        }

        println(sj.toString())
    }

    private fun q(n: Int, m: Int, a: Array<Int>, b: Array<Int>): String {
        val sortedByA = (0 until n).sortedWith(compareBy({ a[it] }, { b[it] }))

        var ng = -1
        var ok = n
        while (ok - ng > 1) {
            val med = (ok + ng) / 2
            var flag = true
            var day = 0
            var index = 0
            val pq = PriorityQueue<Int>(16) { l, r -> b[l].compareTo(b[r]) }
            while (index < n) {
                if (pq.size <= 0 && day < a[sortedByA[index]]) {
                    day = a[sortedByA[index]]
                }

                // 公開がday日の奴を追加
                while (index < n && day == a[sortedByA[index]]) {
                    pq.add(sortedByA[index])
                    index++
                }

                // 終了したやつがあればfalse
                if (pq.size > 0 && b[pq.peek()!!] + med < day) {
                    flag = false
                    break
                }

                // m個観る
                var cnt = 0
                while (cnt < m && pq.size > 0) {
                    pq.poll()
                    cnt++
                }

                day++
            }

            while (pq.size > 0) {
                if (b[pq.peek()!!] + med < day) {
                    flag = false
                    break
                }
                var cnt = 0
                while (cnt < m && pq.size > 0) {
                    pq.poll()
                    cnt++
                }
                day++
            }

            if (flag) {
                ok = med
            } else {
                ng = med
            }
        }

        // ok日
        val ans = Array(n) { -1 }
        var day = 0
        var index = 0
        val pq = PriorityQueue<Int>(16) { l, r -> b[l].compareTo(b[r]) }
        while (index < n) {
            if (pq.size <= 0 && day < a[sortedByA[index]]) {
                day = a[sortedByA[index]]
            }

            // 公開がday日の奴を追加
            while (index < n && day == a[sortedByA[index]]) {
                pq.add(sortedByA[index])
                index++
            }

            // m個観る
            var cnt = 0
            while (cnt < m && pq.size > 0) {
                ans[pq.poll()] = day
                cnt++
            }
            day++
        }

        while (pq.size > 0) {
            var cnt = 0
            while (cnt < m && pq.size > 0) {
                ans[pq.poll()] = day
                cnt++
            }
            day++
        }

        return "$ok\n${ans.joinToString(" ")}"
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
