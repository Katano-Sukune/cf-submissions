using System;
using System.Linq;
using CompLib.Algorithm;
using CompLib.Mathematics;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int k = sc.NextInt();
        int[] d = sc.IntArray();
        int h = 21;
        int z = 1 << h;
        ModInt[] a = new ModInt[z];
        for (int i = 0; i < k; i++) a[d[i]] = 1;
        FastFourierTransform.Butterfly(a, h);
        for (int i = 0; i < z; i++)
        {
            a[i] = ModInt.Pow(a[i], n / 2);
        }
        FastFourierTransform.ButterflyInv(a, h);
        var inv = ModInt.Inverse(z);
        for (int i = 0; i < z; i++)
        {
            a[i] *= inv;
        }

        ModInt ans = 0;
        for (int i = 0; i < z; i++)
        {
            ans += a[i] * a[i];
        }

        Console.WriteLine(ans);
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}



namespace CompLib.Algorithm
{
    using System;
    using CompLib.Mathematics;
    public static class FastFourierTransform
    {
        private static readonly ModInt[] SumE, SumIE;
        private static readonly int[] RevBSF;
        static FastFourierTransform()
        {
            // 998244353 = 119 * 2^23 + 1

            //*
            const int cnt2 = 23;
            var es = new ModInt[cnt2 - 1];
            var ies = new ModInt[cnt2 - 1];
            // var e = ModInt.Pow(3, 119);
            ModInt e = 15311432;
            var ie = ModInt.Inverse(e);

            for (int i = cnt2; i >= 2; i--)
            {
                es[i - 2] = e;
                ies[i - 2] = ie;
                e *= e;
                ie *= ie;
            }

            ModInt now = 1;
            ModInt iNow = 1;
            SumE = new ModInt[cnt2 - 1];
            SumIE = new ModInt[cnt2 - 1];
            for (int i = 0; i <= cnt2 - 2; i++)
            {
                SumIE[i] = ies[i] * iNow;
                SumE[i] = es[i] * now;
                now *= ies[i];
                iNow *= es[i];
            }
            /*
            */

            RevBSF = new int[1 << 24];
            for (int i = 0; i < (1 << 24); i++)
            {
                int j = 0;
                while ((i & (1 << j)) != 0) j++;
                RevBSF[i] = j;
            }
        }

        public static void Butterfly(ModInt[] a, int h)
        {
            for (int ph = 1; ph <= h; ph++)
            {
                int w = 1 << (ph - 1);
                int p = 1 << (h - ph);

                ModInt now = 1;
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
                    now *= SumE[GetRevBSF(s)];
                }
            }
        }

        public static void ButterflyInv(ModInt[] a, int h)
        {
            for (int ph = h; ph >= 1; ph--)
            {
                int w = 1 << (ph - 1);
                int p = 1 << (h - ph);
                ModInt iNow = 1;
                for (int s = 0; s < w; s++)
                {
                    int offset = s << (h - ph + 1);
                    for (int i = 0; i < p; i++)
                    {
                        var l = a[i + offset];
                        var r = a[i + offset + p];
                        a[i + offset] = l + r;
                        a[i + offset + p] = (l - r) * iNow;
                    }
                    iNow *= SumIE[GetRevBSF(s)];
                }
            }
        }

        public static ModInt[] Convolution(ModInt[] a, ModInt[] b)
        {
            int n = a.Length;
            int m = b.Length;
            if (n == 0 || m == 0) return Array.Empty<ModInt>();

            int h = CeilPow2(n + m - 1);
            int z = 1 << h;
            ModInt[] a2 = new ModInt[z];
            Array.Copy(a, a2, n);
            Butterfly(a2, h);
            ModInt[] b2 = new ModInt[z];
            Array.Copy(b, b2, m);
            Butterfly(b2, h);

            for (int i = 0; i < z; i++)
            {
                a2[i] *= b2[i];
            }
            ButterflyInv(a2, h);
            Array.Resize(ref a2, n + m - 1);
            ModInt iz = ModInt.Inverse(z);
            for (int i = 0; i < n + m - 1; i++) a2[i] *= iz;
            return a2;
        }

        private static int CeilPow2(int n)
        {
            int x = 0;
            while ((1 << x) < n) x++;
            return x;
        }

        private static int GetRevBSF(int n)
        {
            return RevBSF[n];
        }
    }
}

namespace CompLib.Mathematics
{
    #region ModInt
    /// <summary>
    /// [0,<see cref="Mod"/>) までの値を取るような数
    /// </summary>
    public struct ModInt
    {
        /// <summary>
        /// 剰余を取る値．
        /// </summary>
        // public const long Mod = (int)1e9 + 7;
        public const long Mod = 998244353;

        /// <summary>
        /// 実際の数値．
        /// </summary>
        public long num;
        /// <summary>
        /// 値が <paramref name="n"/> であるようなインスタンスを構築します．
        /// </summary>
        /// <param name="n">インスタンスが持つ値</param>
        /// <remarks>パフォーマンスの問題上，コンストラクタ内では剰余を取りません．そのため，<paramref name="n"/> ∈ [0,<see cref="Mod"/>) を満たすような <paramref name="n"/> を渡してください．このコンストラクタは O(1) で実行されます．</remarks>
        public ModInt(long n) { num = n; }
        /// <summary>
        /// このインスタンスの数値を文字列に変換します．
        /// </summary>
        /// <returns>[0,<see cref="Mod"/>) の範囲内の整数を 10 進表記したもの．</returns>
        public override string ToString() { return num.ToString(); }
        public static ModInt operator +(ModInt l, ModInt r) { l.num += r.num; if (l.num >= Mod) l.num -= Mod; return l; }
        public static ModInt operator -(ModInt l, ModInt r) { l.num -= r.num; if (l.num < 0) l.num += Mod; return l; }
        public static ModInt operator *(ModInt l, ModInt r) { return new ModInt(l.num * r.num % Mod); }
        public static implicit operator ModInt(long n) { n %= Mod; if (n < 0) n += Mod; return new ModInt(n); }

        /// <summary>
        /// 与えられた 2 つの数値からべき剰余を計算します．
        /// </summary>
        /// <param name="v">べき乗の底</param>
        /// <param name="k">べき指数</param>
        /// <returns>繰り返し二乗法により O(N log N) で実行されます．</returns>
        public static ModInt Pow(ModInt v, long k) { return Pow(v.num, k); }

        /// <summary>
        /// 与えられた 2 つの数値からべき剰余を計算します．
        /// </summary>
        /// <param name="v">べき乗の底</param>
        /// <param name="k">べき指数</param>
        /// <returns>繰り返し二乗法により O(N log N) で実行されます．</returns>
        public static ModInt Pow(long v, long k)
        {
            long ret = 1;
            for (k %= Mod - 1; k > 0; k >>= 1, v = v * v % Mod)
                if ((k & 1) == 1) ret = ret * v % Mod;
            return new ModInt(ret);
        }
        /// <summary>
        /// 与えられた数の逆元を計算します．
        /// </summary>
        /// <param name="v">逆元を取る対象となる数</param>
        /// <returns>逆元となるような値</returns>
        /// <remarks>法が素数であることを仮定して，フェルマーの小定理に従って逆元を O(log N) で計算します．</remarks>
        public static ModInt Inverse(ModInt v) { return Pow(v, Mod - 2); }
    }
    #endregion
    #region Binomial Coefficient
    public class BinomialCoefficient
    {
        public ModInt[] fact, ifact;
        public BinomialCoefficient(int n)
        {
            fact = new ModInt[n + 1];
            ifact = new ModInt[n + 1];
            fact[0] = 1;
            for (int i = 1; i <= n; i++)
                fact[i] = fact[i - 1] * i;
            ifact[n] = ModInt.Inverse(fact[n]);
            for (int i = n - 1; i >= 0; i--)
                ifact[i] = ifact[i + 1] * (i + 1);
            ifact[0] = ifact[1];
        }
        public ModInt this[int n, int r]
        {
            get
            {
                if (n < 0 || n >= fact.Length || r < 0 || r > n) return 0;
                return fact[n] * ifact[n - r] * ifact[r];
            }
        }
        public ModInt RepeatedCombination(int n, int k)
        {
            if (k == 0) return 1;
            return this[n + k - 1, k];
        }
    }
    #endregion
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
