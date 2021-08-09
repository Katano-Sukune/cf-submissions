using System;
using System.Linq;
using CompLib.Util;
using CompLib.Internal;
using CompLib.Mathematics;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using CompLib.Algorithm;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int k = sc.NextInt();
        int[] d = sc.IntArray();

        int h = 21;
        var a = new ModInt<Mod998244353>[1 << h];
        foreach (int i in d) a[i] = 1;

        FastFourierTransform<Mod998244353>.Butterfly(a, h);
        int tmp = n / 2;
        for (int i = 0; i < (1 << h); i++)
        {
            a[i] = ModInt<Mod998244353>.Pow(a[i], tmp);
        }
        FastFourierTransform<Mod998244353>.ButterflyInv(a, h);
        var inv = ModInt<Mod998244353>.Inverse(1 << h);
        ModInt<Mod998244353> ans = 0;
        foreach (var i in a)
        {
            ModInt<Mod998244353> m = i * inv;
            ans += m * m;
        }
        Console.WriteLine(ans);
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}



namespace CompLib.Algorithm
{
    static class FastFourierTransform<T> where T : IMod
    {
        private readonly static ModInt<T>[] sumE, sumIE;
        static FastFourierTransform()
        {
            int mod = default(T).Mod;
            int cnt2 = InternalBit.BSF(mod - 1);
            var es = new ModInt<T>[cnt2 - 1];
            var ies = new ModInt<T>[cnt2 - 1];
            var e = ModInt<T>.Pow(InternalMath.PrimitiveRoot(mod), (mod - 1) >> cnt2);
            var ie = ModInt<T>.Inverse(e);
            for (int i = cnt2; i >= 2; i--)
            {
                es[i - 2] = e;
                ies[i - 2] = ie;
                e *= e;
                ie *= ie;
            }
            sumE = new ModInt<T>[cnt2 - 1];
            sumIE = new ModInt<T>[cnt2 - 1];
            ModInt<T> iNow = 1;
            ModInt<T> now = 1;
            for (int i = 0; i <= cnt2 - 2; i++)
            {
                sumIE[i] = ies[i] * iNow;
                sumE[i] = es[i] * now;
                now *= ies[i];
                iNow *= es[i];
            }
        }

        public static void Butterfly(ModInt<T>[] a, int h)
        {
            for (int ph = 1; ph <= h; ph++)
            {
                int w = 1 << (ph - 1);
                int p = 1 << (h - ph);

                ModInt<T> now = 1;
                for (int s = 0; s < w; s++)
                {
                    int offset = s << (h - ph + 1);
                    for (int i = 0; i < p; i++)
                    {
                        var l = a[i + offset];
                        var r = a[i + offset + p] * now;
                        a[i + offset] = l + r;
                        a[i + offset + p] = l - r;
                    }
                    now *= sumE[InternalBit.GetRevBSF(s)];
                }
            }
        }

        public static void ButterflyInv(ModInt<T>[] a, int h)
        {
            for (int ph = h; ph >= 1; ph--)
            {
                int w = 1 << (ph - 1);
                int p = 1 << (h - ph);
                ModInt<T> iNow = 1;
                for (int s = 0; s < w; s++)
                {
                    int offset = s << (h - ph + 1);
                    for (int i = 0; i < p; i++)
                    {
                        var l = a[i + offset];
                        var r = a[i + offset + p];
                        a[i + offset] = l + r;
                        a[i + offset + p] = (default(T).Mod + l.num - r.num) * iNow;
                    }
                    iNow *= sumIE[InternalBit.GetRevBSF(s)];
                }
            }
        }

        public static ModInt<T>[] Convolution(ModInt<T>[] a, ModInt<T>[] b)
        {
            int n = a.Length;
            int m = b.Length;
            if (n == 0 || m == 0) return Array.Empty<ModInt<T>>();

            int h = InternalBit.CeilPow2(n + m - 1);
            int z = 1 << h;
            ModInt<T>[] a2 = new ModInt<T>[z];
            Array.Copy(a, a2, n);
            Butterfly(a2, h);
            ModInt<T>[] b2 = new ModInt<T>[z];
            Array.Copy(b, b2, m);
            Butterfly(b2, h);

            for (int i = 0; i < z; i++)
            {
                a2[i] *= b2[i];
            }
            ButterflyInv(a2, h);
            Array.Resize(ref a2, n + m - 1);
            ModInt<T> iz = ModInt<T>.Inverse(z);
            for (int i = 0; i < n + m - 1; i++) a2[i] *= iz;
            return a2;
        }

        public static ModInt<T>[] Convolution(long[] a, long[] b)
        {
            int n = a.Length;
            int m = b.Length;
            if (n == 0 || m == 0) return Array.Empty<ModInt<T>>();
            int h = InternalBit.CeilPow2(n + m - 1);
            int z = 1 << h;

            ModInt<T>[] a2 = new ModInt<T>[z];
            Array.Copy(a, a2, n);
            Butterfly(a2, h);
            ModInt<T>[] b2 = new ModInt<T>[z];
            Array.Copy(b, b2, m);
            Butterfly(b2, h);

            for (int i = 0; i < z; i++)
            {
                a2[i] *= b2[i];
            }
            ButterflyInv(a2, h);
            Array.Resize(ref a2, n + m - 1);
            ModInt<T> iz = ModInt<T>.Inverse(z);
            for (int i = 0; i < n + m - 1; i++) a2[i] *= iz;
            return a2;
        }
    }

    public static class FastFourierTransform
    {
        const ulong Mod1 = 754974721;
        const ulong Mod2 = 167772161;
        const ulong Mod3 = 469762049;
        const ulong M2M3 = Mod2 * Mod3;
        const ulong M1M3 = Mod1 * Mod3;
        const ulong M1M2 = Mod1 * Mod2;
        const ulong M1M2M3 = unchecked(Mod1 * Mod2 * Mod3);
        static readonly ulong[] Offset = { 0, 0, M1M2M3, unchecked(2 * M1M2M3), unchecked(3 * M1M2M3) };

        // internal::inv_gcd(MOD2 * MOD3, MOD1).second;
        const ulong I1 = 190329765;
        // Mod2 * Mod3, Mod2
        const ulong I2 = 58587104;
        // Mod1* Mod2, Mod3
        const ulong I3 = 187290749;

        public static long[] ConvolutionLL(long[] a, long[] b)
        {
            int n = a.Length;
            int m = b.Length;
            if (n == 0 || m == 0) return Array.Empty<long>();
            unchecked
            {
                var c1 = FastFourierTransform<M1>.Convolution(a, b);
                var c2 = FastFourierTransform<M2>.Convolution(a, b);
                var c3 = FastFourierTransform<M3>.Convolution(a, b);

                long[] c = new long[n + m - 1];
                for (int i = 0; i < n + m - 1; i++)
                {
                    ulong x = 0;
                    x += ((ulong)c1[i].num * I1) % Mod1 * M2M3;
                    x += ((ulong)c2[i].num * I2) % Mod2 * M1M3;
                    x += ((ulong)c3[i].num * I3) % Mod3 * M1M2;

                    long diff = c1[i].num - InternalMath.SafeMod((long)x, (long)Mod1);
                    if (diff < 0) diff += (long)Mod1;
                    x -= Offset[diff % 5];
                    c[i] = (long)x;
                }
                return c;
            }
        }

        struct M1 : IMod
        {
            public int Mod => (int)Mod1;
        }

        struct M2 : IMod
        {
            public int Mod => (int)Mod2;
        }

        struct M3 : IMod
        {
            public int Mod => (int)Mod3;
        }
    }


}



namespace CompLib.Internal
{
    static class InternalBit
    {
        private static readonly int[] RevBSF;

        static InternalBit()
        {
            RevBSF = new int[1 << 24];
            for (int i = 0; i < (1 << 24); i++) RevBSF[i] = BSF(~i);
        }
        public static int CeilPow2(int n)
        {
            int x = 0;
            while ((1 << x) < n) x++;
            return x;
        }

        public static int BSF(int n)
        {
            switch (n)
            {
                case 998244352:
                    return 23;
                case 754974720:
                    return 24;
                case 167772160:
                    return 25;
                case 469762048:
                    return 26;
            }
            Debug.Assert(n != 0);
            int x = 0;
            while (((1 << x) & n) == 0) x++;
            return x;
        }

        public static int GetRevBSF(int n)
        {
            return RevBSF[n];
        }
    }
}

namespace CompLib.Internal
{
    static class InternalMath
    {
        // 原始根
        public static int PrimitiveRoot(int m)
        {
            switch (m)
            {
                case 2:
                    return 1;
                case 167772161:
                case 469762049:
                case 998244353:
                    return 3;
                case 754974721:
                    return 11;
            }
            throw new NotImplementedException();
        }

        public static Tuple<long, long> InvGCD(long a, long b)
        {
            a = SafeMod(a, b);
            if (a == 0) return new Tuple<long, long>(0, b);
            long s = b, t = a;
            long m0 = 0, m1 = 1;
            while (t != 0)
            {
                long u = s / t;
                s -= t * u;
                m0 -= m1 * u;

                var tmp = s;
                s = t;
                t = tmp;
                tmp = m0;
                m0 = m1;
                m1 = tmp;
            }
            if (m0 < 0) m0 += b / s;
            return new Tuple<long, long>(s, m0);
        }

        public static long SafeMod(long x, long m)
        {
            x %= m;
            if (x < 0) x += m;
            return x;
        }
    }
}

namespace CompLib.Mathematics
{
    struct ModInt<T> where T : IMod
    {
        public long num;
        public ModInt(long n) { num = n; }
        public override string ToString() { return num.ToString(); }
        public static ModInt<T> operator +(ModInt<T> l, ModInt<T> r) { l.num += r.num; int mod = default(T).Mod; if (l.num >= mod) l.num -= mod; return l; }
        public static ModInt<T> operator -(ModInt<T> l, ModInt<T> r) { l.num -= r.num; if (l.num < 0) { int mod = default(T).Mod; l.num += mod; } return l; }
        public static ModInt<T> operator *(ModInt<T> l, ModInt<T> r) { int mod = default(T).Mod; return new ModInt<T>(l.num * r.num % mod); }
        public static implicit operator ModInt<T>(long n) { int mod = default(T).Mod; n %= mod; if (n < 0) n += mod; return new ModInt<T>(n); }
        public static ModInt<T> Pow(long v, long k)
        {
            int mod = default(T).Mod;
            long ret = 1;
            for (k %= mod - 1; k > 0; k >>= 1, v = v * v % mod)
                if ((k & 1) == 1) ret = ret * v % mod;
            return new ModInt<T>(ret);
        }
        public static ModInt<T> Pow(ModInt<T> v, long k) { return Pow(v.num, k); }
        public static ModInt<T> Inverse(ModInt<T> v) { return Pow(v, default(T).Mod - 2); }
    }

    interface IMod
    {
        int Mod { get; }
    }

    public struct Mod998244353 : IMod
    {
        public int Mod { get { return 998244353; } }
    }

    public struct Mod1000000007 : IMod
    {
        public int Mod { get { return 1000000007; } }
    }
}


namespace CompLib.Util
{
    using System;
    using System.Linq;

    class Scanner
    {
        private string[] _line;
        private int _index;
        private const char Separator = ' ';

        public Scanner()
        {
            _line = new string[0];
            _index = 0;
        }

        public string Next()
        {
            if (_index >= _line.Length)
            {
                string s;
                do
                {
                    s = Console.ReadLine();
                } while (s.Length == 0);

                _line = s.Split(Separator);
                _index = 0;
            }

            return _line[_index++];
        }

        public string ReadLine()
        {
            _index = _line.Length;
            return Console.ReadLine();
        }

        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public decimal NextDecimal() => decimal.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            string s = Console.ReadLine();
            _line = s.Length == 0 ? new string[0] : s.Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
