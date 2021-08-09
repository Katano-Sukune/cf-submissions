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
            val k = sc.nextInt()
            val xy = Array(n - 1) { sc.nextInt() to sc.nextInt() }
            sj.add(q(n, k, xy))
        }

        println(sj.toString())
    }

    private fun q(n: Int, k: Int, xy: Array<Pair<Int, Int>>): String {
        // 無向木グラフ
        // 葉の数がk個の連結成分を構成できるか?

        if (k == 1) {
            return "Yes\n1\n1"
        }

        val edges = Array(n) { mutableListOf<Int>() }
        for (i in 0 until n - 1) {
            edges[xy[i].first - 1].add(xy[i].second - 1)
            edges[xy[i].second - 1].add(xy[i].first - 1)
        }

        val q = ArrayDeque<Int>()
        val flag = Array(n) { false }

        q.add(0)
        q.add(edges[0][0])
        flag[0] = true
        flag[edges[0][0]] = true
        var cnt = 2
        while (q.size > 0) {
            if (cnt >= k) break
            val d = q.poll()!!
            var c = 0
            for (i in edges[d].indices) {
                val to = edges[d][i]
                if (flag[to]) continue
                c++
                q.add(to)
                flag[to] = true
                if (c > 1) {
                    cnt++
                }
                if (cnt >= k) {
                    break
                }
            }
        }

        if (cnt >= k) {
            val ans = mutableListOf<Int>()
            for (i in 0 until n) {
                if (flag[i]) ans.add(i + 1)
            }

            return "Yes\n${ans.size}\n${ans.joinToString(" ")}"
        } else {
            return "No"
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
