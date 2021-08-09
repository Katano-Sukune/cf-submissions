using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Collections;
using CompLib.Mathematics;
using CompLib.Util;

public class Program
{
    int N, M;
    string S;
    int[] X, Y, Len;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        S = sc.Next();
        X = new int[M];
        Y = new int[M];
        Len = new int[M];
        for (int i = 0; i < M; i++)
        {
            X[i] = sc.NextInt() - 1;
            Y[i] = sc.NextInt() - 1;
            Len[i] = sc.NextInt();
        }

        /*
         * 文字列S 長さN
         * 
         * S,Tについて
         * Sの各文字を別の文字に対応させる　同型
         * 
         * [x,x+len) と[y,y+len)が同型か?
         */

        var rnd = new Random();
        int b = rnd.Next((int)1e9);
        ModInt[][] hash = new ModInt[26][];
        for (int i = 0; i < 26; i++)
        {
            hash[i] = new ModInt[N + 1];
        }

        ModInt[] invPow = new ModInt[N];
        ModInt p = 1;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < 26; j++)
            {
                hash[j][i + 1] = hash[j][i];
            }
            hash[S[i] - 'a'][i + 1] += p;
            invPow[i] = ModInt.Inverse(p);
            p *= b;
        }



        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });

        for (int q = 0; q < M; q++)
        {
            List<long> hashS = new List<long>();
            List<long> hashT = new List<long>();

            for (int i = 0; i < 26; i++)
            {
                var ss = (hash[i][X[q] + Len[q]] - hash[i][X[q]]) * invPow[X[q]];
                if (ss.num != 0) hashS.Add(ss.num);
                // hashS[i] = (hash[i][X[q] + Len[q]] - hash[i][X[q]]);
                var tt = (hash[i][Y[q] + Len[q]] - hash[i][Y[q]]) * invPow[Y[q]];
                if (tt.num != 0) hashT.Add(tt.num);
                // hashT[i] = (hash[i][Y[q] + Len[q]] - hash[i][Y[q]]);
            }

            bool f = hashS.Count == hashT.Count;
            if (f)
            {
                hashS.Sort();
                hashT.Sort();
                // Sに含まれる文字x
                // Tに含まれる文字y
                // xはyに置き換える
                // Console.WriteLine(string.Join(" ", cntS));
                // Console.WriteLine(string.Join(" ", cntT));


                // Console.WriteLine(string.Join(" ", hashS));
                // Console.WriteLine(string.Join(" ", hashT));
                for (int i = 0; f && i < hashS.Count; i++)
                {
                    f &= hashS[i] == hashT[i];
                }
            }

            if (f)
            {
                Console.WriteLine("YES");
            }
            else
            {
                Console.WriteLine("NO");
            }
        }

        Console.Out.Flush();
    }

    public static void Main(string[] args) => new Program().Solve();
}

// https://bitbucket.org/camypaper/complib
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
        public const long Mod = (int)1e9 + 7;
        // public const long Mod = 998244353;

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


namespace CompLib.Collections
{
    using Num = System.Int32;

    public class FenwickTree
    {
        private readonly Num[] _array;
        public readonly int Count;

        public FenwickTree(int size)
        {
            _array = new Num[size + 1];
            Count = size;
        }

        /// <summary>
        /// A[i]にnを加算
        /// </summary>
        /// <param name="i"></param>
        /// <param name="n"></param>
        public void Add(int i, Num n)
        {
            i++;
            for (; i <= Count; i += i & -i)
            {
                _array[i] += n;
            }
        }

        /// <summary>
        /// [0,r)の和を求める
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public Num Sum(int r)
        {
            Num result = 0;
            for (; r > 0; r -= r & -r)
            {
                result += _array[r];
            }

            return result;
        }

        /// <summary>
        /// [0,i)の和がw以上になるi
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        public int LowerBound(int w)
        {
            if (w <= 0) return 0;
            int x = 0;
            int k = 1;
            while (k * 2 < Count) k *= 2;
            for (; k > 0; k /= 2)
            {
                if (x + k < Count && _array[x + k] < w)
                {
                    w -= _array[x + k];
                    x += k;
                }
            }
            return x + 1;
        }

        // [l,r)の和を求める
        public Num Sum(int l, int r) => Sum(r) - Sum(l);
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
