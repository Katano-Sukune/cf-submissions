import java.util.*
import kotlin.*
import kotlin.math.*

fun main(args: Array<String>) {
    Program().solve()
}

class Program {
    fun solve() {
        val sc = MyScanner()
        val n = sc.nextInt()
        val m = sc.nextInt()
        val a = sc.longList()

        var p = 0
        for (i in 0 until n) {
            if (a[i] < a[p]) p = i
        }

        val ar = mutableListOf<Triple<Int, Int, Long>>()
        for (i in 0 until m) {
            val x = sc.nextInt() - 1
            val y = sc.nextInt() - 1
            val w = sc.nextLong()
            ar.add(Triple(x, y, w))
        }
        for (i in 0 until n) {
            if (i == p) continue
            ar.add(Triple(i, p, a[i] + a[p]))
        }
        ar.sortBy { it.third }
        var ans = 0L
        val uf = UnionFind(n)
        for (t in ar) {
            if (uf.unite(t.first, t.second)) ans += t.third
        }
        println(ans)
    }
}

class UnionFind(private val v: Int) {
    // 頂点iの親 par[i] == iなら iは根
    private val par = Array(v) { it }

    // iを根とする部分木のサイズ
    private val size = Array(v) { 1 }

    // iが含まれる木の根
    fun find(i: Int): Int = if (par[i] == i) i else find(par[i])

    // xとyが同じグループに含まれるか?
    fun same(x: Int, y: Int) = find(x) == find(y)

    // iが含まれるグループのサイズ
    fun getSize(i: Int) = size[find(i)]

    // xとyを繋げる 元から同じグループならfalse
    fun unite(x: Int, y: Int): Boolean {
        val rootX = find(x)
        val rootY = find(y)

        if (rootX == rootY) return false

        // データ構造をマージする一般的なテク
        if (size[rootX] > size[rootY]) {
            par[rootY] = rootX
            size[rootX] += size[rootY]
        } else {
            par[rootX] = rootY
            size[rootY] += size[rootX]
        }

        return true
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
