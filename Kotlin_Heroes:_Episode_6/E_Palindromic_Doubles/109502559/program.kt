import kotlin.math.max

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
        val a = sc.intList()
        // aの部分列　回文最長

        // 各数2回まで
        val idx = Array(n + 1) { mutableListOf<Int>() }
        for (i in 0 until n) {
            idx[a[i]].add(i)
        }

        var size = 1
        while (size < n) size *= 2

        var ans = 0
        val st = SegmentTree<Int>(size, { l, r -> max(l, r) }, 0)
        for (i in 0 until n) {
            if (idx[a[i]][0] == i) {
                // iは前
                if (idx[a[i]].size == 1) {
                    // iは1個
                    val mx = st.query(idx[a[i]][0] + 1, n)
                    ans = max(ans, mx + 1)
                } else {
                    // iは後ろ
                    val mx2 = st.query(idx[a[i]][0] + 1, n)
                    ans = max(ans, mx2 + 1)

                    val mx = st.query(idx[a[i]][1] + 1, n)
                    st[idx[a[i]][1]] = mx + 2
                    ans = max(ans, mx + 2)


                }
            } else {
                // 後ろ
                val mx2 = st.query(idx[a[i]][1] + 1, n)
                ans = max(ans, mx2 + 1)
            }
        }
        println(ans)
    }
}

class SegmentTree<T>(val size: Int, private val op: (T, T) -> T, private val id: T) {

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
