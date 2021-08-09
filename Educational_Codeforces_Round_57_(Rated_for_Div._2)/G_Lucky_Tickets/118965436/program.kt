import kotlin.*
import kotlin.math.*

fun main(args: Array<String>) {
    Program().solve()
}

class Program {
    fun solve() {
        val sc = MyScanner()
        val n = sc.nextInt()
        val k = sc.nextInt()
        val d = sc.intArray()

        val h = 21
        val s = 1 shl h

        val a = ModIntArray(s) { ModInt(0) }
        for (i in d) {
            a[i] = ModInt(1)
        }
        FastFourierTransform.butterfly(a, h)
        for (i in 0 until s) {
            a[i] = ModInt.pow(a[i], n / 2)
        }

        FastFourierTransform.butterflyInv(a, h)
        val invS = ModInt.inverse(s)
        for (i in 0 until s) {
            a[i] *= invS
        }

        var ans = ModInt(0)
        for (i in a) {
            ans += i * i
        }
        println(ans)
    }
}

inline class ModInt(val num: Long) {
    companion object {
        // const val mod: Long = 1000000007L
        const val mod: Long = 998244353L

        inline fun pow(v: ModInt, k: Long) = pow(v.num, k)

        inline fun pow(v: Long, k: Long): ModInt {
            var ret = 1L
            var tmpV = v
            var tmpK = k % (mod - 1)
            while (tmpK > 0) {
                if ((tmpK and 1L) == 1L) ret = (ret * tmpV) % mod
                tmpK = tmpK shr 1
                tmpV = (tmpV * tmpV) % mod
            }
            return ModInt(ret)
        }

        inline fun pow(v: Int, k: Long): ModInt = pow(v.toLong(), k)
        inline fun pow(v: ModInt, k: Int) = pow(v.num, k.toLong())
        inline fun pow(v: Int, k: Int) = pow(v.toLong(), k.toLong())
        inline fun pow(v: Long, k: Int) = pow(v, k.toLong())

        inline fun inverse(v: ModInt) = pow(v.num, mod - 2)
        inline fun inverse(v: Long) = pow(v, mod - 2)
        inline fun inverse(v: Int) = pow(v.toLong(), mod - 2)
    }

    constructor(num: Int) : this(num.toLong())

    inline operator fun plus(right: ModInt): ModInt {
        var ret = num + right.num
        if (ret >= mod) ret -= mod
        return ModInt(ret)
    }

    inline operator fun plus(right: Long) = plus(rem(right))
    inline operator fun plus(right: Int) = plus(rem(right))

    inline operator fun minus(right: ModInt): ModInt {
        var ret = num - right.num
        if (ret < 0) ret += mod
        return ModInt(ret)
    }

    inline operator fun minus(right: Long) = minus(rem(right))
    inline operator fun minus(right: Int) = minus(rem(right))

    inline operator fun times(right: ModInt) = ModInt((num * right.num) % mod)
    inline operator fun times(right: Long) = times(rem(right))
    inline operator fun times(right: Int) = times(rem(right))

    inline override fun toString(): String {
        return num.toString()
    }


    inline fun rem(num: Long): ModInt {
        return if (num >= 0) ModInt(num % mod) else ModInt(num % mod + mod)
    }

    inline fun rem(num: Int): ModInt {
        return if (num >= 0) ModInt(num % mod) else ModInt(num % mod + mod)
    }
}

inline class ModIntArray(val ar: LongArray) : Collection<ModInt> {
    inline operator fun get(i: Int) = ModInt(ar[i])
    inline operator fun set(i: Int, v: ModInt) {
        ar[i] = v.num
    }

    inline override val size get() = ar.size
    inline override fun contains(element: ModInt) = ar.contains(element.num)
    inline override fun containsAll(elements: Collection<ModInt>) = elements.all(::contains)
    inline override fun isEmpty() = ar.isEmpty()
    inline override fun iterator(): Iterator<ModInt> = object : Iterator<ModInt> {
        var index = 0
        override fun hasNext(): Boolean = index < size
        override fun next(): ModInt = get(index++)
    }
}

inline fun ModIntArray(size: Int) = ModIntArray(LongArray(size))
inline fun ModIntArray(size: Int, init: (Int) -> ModInt) = ModIntArray(LongArray(size) { init(it).num })

class FastFourierTransform {
    companion object {
        inline fun ceilPow2(n: Int): Int {
            var x = 0
            while ((1 shl x) < n) x++
            return x
        }

        val revBsf = IntArray(1 shl 23) {
            var x = 0
            while ((it and (1 shl x)) != 0) x++
            x
        }
        val sumE = ModIntArray(22) { ModInt(0) }
        val sumIE = ModIntArray(22) { ModInt(0) }

        init {
            val es = ModIntArray(22) { ModInt(0) }
            val ies = ModIntArray(22) { ModInt(0) }

            // 998244353 = 119 * 2^23 + 1
            // g = 3
            // cnt2 = 23
            // (mod-1) >> cnt2 = 119
            val g = 3
            val cnt2 = 23
            var e = ModInt.pow(g, (ModInt.mod - 1) shr cnt2)
            var ie = ModInt.inverse(e)
            for (i in cnt2 downTo 2) {
                es[i - 2] = e
                ies[i - 2] = ie
                e *= e
                ie *= ie
            }
            var now = ModInt(1)
            var iNow = ModInt(1)
            for (i in 0..cnt2 - 2) {
                sumE[i] = es[i] * now
                sumIE[i] = ies[i] * iNow
                now *= ies[i]
                iNow *= es[i]
            }
        }

        inline fun butterfly(a: ModIntArray, h: Int) {
            for (ph in 1..h) {
                val p = 1 shl (h - ph)
                var now = ModInt(1)
                for (s in 0 until (1 shl (ph - 1))) {
                    val offset = s shl (h - ph + 1)
                    for (i in 0 until p) {
                        val l = a[i + offset]
                        val r = a[i + offset + p] * now
                        a[i + offset] = l + r
                        a[i + offset + p] = l - r
                    }
                    now *= sumE[revBsf[s]]
                }
            }
        }

        inline fun butterflyInv(a: ModIntArray, h: Int) {
            for (ph in h downTo 1) {
                val p = 1 shl (h - ph)
                var iNow = ModInt(1)
                for (s in 0 until (1 shl (ph - 1))) {
                    val offset = s shl (h - ph + 1)
                    for (i in 0 until p) {
                        val l = a[i + offset]
                        val r = a[i + offset + p]
                        a[i + offset] = l + r
                        a[i + offset + p] = (l - r) * iNow
                    }
                    iNow *= sumIE[revBsf[s]]
                }
            }
        }

        // mod = 998244353
        inline fun convolution(a: ModIntArray, b: ModIntArray): ModIntArray {
            val n = a.size
            val m = b.size
            if (min(n, m) <= 60) {
                val ans = ModIntArray(n + m - 1) { ModInt(0) }
                for (i in 0 until n) {
                    for (j in 0 until m) {
                        ans[i + j] += a[i] * b[i]
                    }
                }
                return ans
            }
            val h = ceilPow2(n + m - 1)
            val z = 1 shl h
            val tmpA = ModIntArray(z) { if (it < n) a[it] else ModInt(0) }
            butterfly(tmpA, h)
            val tmpB = ModIntArray(z) { if (it < m) b[it] else ModInt(0) }
            butterfly(tmpB, h)
            for (i in 0 until z) {
                tmpA[i] *= tmpB[i]
            }
            butterflyInv(tmpA, h)
            val iz = ModInt.inverse(z)
            return ModIntArray(n + m - 1) { tmpA[it] * iz }
        }
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
