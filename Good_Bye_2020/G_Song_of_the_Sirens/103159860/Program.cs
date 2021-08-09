using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using CompLib.Mathematics;
using CompLib.Collections.Generic;

public class Program
{
    int N, Q;
    string S0;
    string T;

    int[] K;
    string[] W;

    int MaxLen;

    List<string> S;

    ModInt[] B;

    public void Solve()
    {
        var sc = new Scanner();
#if DEBUG
        N = 100000;
        Q = 100000;
        S0 = new string('s', 100);
        T = new string('s', N);

        K = new int[Q];
        W = new string[Q];
        for (int i = 0; i < Q; i++)
        {
            K[i] = N;
            W[i] = "s";
        }
        W[N - 1] = new string('s', 1000000);
        W[N - 2] = "a";
        MaxLen = int.MinValue;
        foreach (var s in W)
        {

            MaxLen = Math.Max(MaxLen, s.Length);
        }
#else
        N = sc.NextInt();
        Q = sc.NextInt();
        S0 = sc.Next();
        T = sc.Next();

        MaxLen = int.MinValue;
        K = new int[Q];
        W = new string[Q];
        for (int i = 0; i < Q; i++)
        {
            K[i] = sc.NextInt();
            W[i] = sc.Next();

            MaxLen = Math.Max(MaxLen, W[i].Length);
        }
#endif

        S = new List<string>();
        S.Add(S0);
        for (int i = 0; i < N && S[i].Length < MaxLen; i++)
        {
            S.Add($"{S[i]}{T[i]}{S[i]}");
        }
        B = new ModInt[N + 1];
        B[0] = 1;
        for (int i = 1; i <= N; i++)
        {
            B[i] = B[i - 1] * 2;
        }

        var ar = new (ModInt[] num, int len)[N];
        for (int i = 0; i < N; i++)
        {
            var t = new ModInt[26];
            t[T[i] - 'a'] = 1;
            ar[i] = (t, 1);
        }
        var st = new SegmentTree<(ModInt[] num, int len)>(ar,
            (l, r) =>
            {
                var ans = new ModInt[26];
                for (int i = 0; i < 26; i++)
                {
                    ans[i] = l.num[i] * B[r.len] + r.num[i];
                }
                return (ans, l.len + r.len);
            },
            (new ModInt[26], 0));

        var rnd = new Random();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < Q; i++)
        {
            int k = K[i];
            string w = W[i];

            // 長さ|w| 以上になるS_i

            int ng = -1;
            int ok = S.Count;

            while (ok - ng > 1)
            {
                int mid = (ok + ng) / 2;
                if (S[mid].Length >= w.Length) ok = mid;
                else ng = mid;
            }

            // |s_k|は|w|未満
            if (ok > k)
            {
                Console.WriteLine("0");
                continue;
            }



            long mod1 = rnd.Next();
            long mod2 = rnd.Next();

            long hashW1 = 0;
            long hashW2 = 0;
            foreach (char c in w)
            {
                hashW1 *= 256;
                hashW2 *= 256;
                hashW1 += c;
                hashW2 += c;
                hashW1 %= mod1;
                hashW2 %= mod2;
            }

            long tmpHash1 = 0;
            long tmpHash2 = 0;

            long p1 = 1;
            long p2 = 1;

            for (int j = 0; j < w.Length - 1; j++)
            {
                tmpHash1 *= 256;
                tmpHash2 *= 256;
                tmpHash1 += S[ok][j];
                tmpHash2 += S[ok][j];
                tmpHash1 %= mod1;
                tmpHash2 %= mod2;

                p1 *= 256;
                p2 *= 256;
                p1 %= mod1;
                p2 %= mod2;
            }
            // s_okにwがいくつあるか?
            int cnt1 = 0;

            for (int j = w.Length - 1; j < S[ok].Length; j++)
            {
                tmpHash1 *= 256;
                tmpHash2 *= 256;
                tmpHash1 += S[ok][j];
                tmpHash2 += S[ok][j];
                tmpHash1 %= mod1;
                tmpHash2 %= mod2;

                if (hashW1 == tmpHash1 && hashW2 == tmpHash2)
                {
                    cnt1++;
                }

                tmpHash1 -= S[ok][j - (w.Length - 1)] * p1;
                tmpHash2 -= S[ok][j - (w.Length - 1)] * p2;

                tmpHash1 %= mod1;
                tmpHash2 %= mod2;

                if (tmpHash1 < 0) tmpHash1 += mod1;
                if (tmpHash2 < 0) tmpHash2 += mod2;
            }

            // s_ok + c + s_ok
            // cを含む部分列にwがいくつあるか?
            int[] cnt2 = new int[26];
            long pHash1 = 0;
            long pHash2 = 0;
            for (int j = S[ok].Length - (w.Length - 1); j < S[ok].Length; j++)
            {
                pHash1 *= 256;
                pHash2 *= 256;
                pHash1 += S[ok][j];
                pHash2 += S[ok][j];
                pHash1 %= mod1;
                pHash2 %= mod2;
            }

            ModInt ans = cnt1 * B[k - ok];
            var array = st.Query(ok, k).num;
            for (char c = 'a'; c <= 'z'; c++)
            {
                long cHash1 = pHash1;
                long cHash2 = pHash2;
                cHash1 *= 256;
                cHash2 *= 256;
                cHash1 += c;
                cHash2 += c;
                cHash1 %= mod1;
                cHash2 %= mod2;

                if (hashW1 == cHash1 && hashW2 == cHash2)
                {
                    cnt2[c - 'a']++;
                }

                for (int j = 0; j < w.Length - 1; j++)
                {
                    cHash1 -= S[ok][S[ok].Length - (w.Length - 1) + j] * p1;
                    cHash2 -= S[ok][S[ok].Length - (w.Length - 1) + j] * p2;
                    cHash1 %= mod1;
                    cHash2 %= mod2;
                    if (cHash1 < 0) cHash1 += mod1;
                    if (cHash2 < 0) cHash2 += mod2;

                    cHash1 *= 256;
                    cHash2 *= 256;
                    cHash1 += S[ok][j];
                    cHash2 += S[ok][j];
                    cHash1 %= mod1;
                    cHash2 %= mod2;

                    if (hashW1 == cHash1 && hashW2 == cHash2)
                    {
                        cnt2[c - 'a']++;
                    }
                }

                if (cnt2[c - 'a'] == 0) continue;
                ans += cnt2[c - 'a'] * array[c - 'a'];
            }

            // Console.WriteLine("----------");
            // Console.WriteLine($"{ok} {S[ok]} {cnt1}");
            Console.WriteLine(ans);
        }
        Console.Out.Flush();

    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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



namespace CompLib.Collections.Generic
{
    using System;
    using System.Diagnostics;

    public class SegmentTree<T>
    {
        // 見かけ上の大きさ、実際の大きさ
        private readonly int _n, _size;
        private T[] _array;

        private T _identity;
        private Func<T, T, T> _operation;

        public SegmentTree(int n, Func<T, T, T> operation, T identity)
        {
            _n = n;
            _size = 1;
            while (_size < _n)
            {
                _size *= 2;
            }

            _identity = identity;
            _operation = operation;
            _array = new T[_size * 2];
            for (int i = 1; i < _size * 2; i++)
            {
                _array[i] = _identity;
            }
        }

        public SegmentTree(T[] a, Func<T, T, T> operation, T identity)
        {
            _n = a.Length;
            _size = 1;
            while (_size < _n)
            {
                _size *= 2;
            }

            _identity = identity;
            _operation = operation;
            _array = new T[_size * 2];
            for (int i = 0; i < a.Length; i++)
            {
                _array[i + _size] = a[i];
            }
            for (int i = a.Length; i < _size; i++)
            {
                _array[i + _size] = identity;
            }

            for (int i = _size - 1; i >= 1; i--)
            {
                _array[i] = operation(_array[i * 2], _array[i * 2 + 1]);
            }
        }

        /// <summary>
        /// A[i]をnに更新 O(log N)
        /// </summary>
        /// <param name="i"></param>
        /// <param name="n"></param>
        public void Update(int i, T n)
        {
            Debug.Assert(0 <= i && i < _n);
            i += _size;
            _array[i] = n;
            while (i > 1)
            {
                i /= 2;
                _array[i] = _operation(_array[i * 2], _array[i * 2 + 1]);
            }
        }

        /// <summary>
        /// A[left] op A[left+1] ... op A[right-1]を求める
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public T Query(int left, int right)
        {
            Debug.Assert(0 <= left && left <= right && right <= _n);
            T sml = _identity;
            T smr = _identity;

            left += _size;
            right += _size;
            while (left < right)
            {
                if ((left & 1) != 0) sml = _operation(sml, _array[left++]);
                if ((right & 1) != 0) smr = _operation(_array[--right], smr);
                left >>= 1;
                right >>= 1;
            }
            return _operation(sml, smr);
        }

        /// <summary>
        /// op(a[0],a[1],...,a[n-1])を返します
        /// </summary>
        /// <returns></returns>
        public T All()
        {
            return _array[1];
        }

        /// <summary>
        /// f(op(a[l],a[l+1],...a[r-1])) = trueとなる最大のrを返します
        /// </summary>
        /// <param name="l"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public int MaxRight(int l, Func<T, bool> f)
        {
            Debug.Assert(0 <= l && l <= _n);
#if DEBUG
            Debug.Assert(f(_identity));
#endif
            if (l == _n) return _n;
            l += _size;
            T sm = _identity;
            do
            {
                while (l % 2 == 0) l >>= 1;
                if (!f(_operation(sm, _array[l])))
                {
                    while (l < _size)
                    {
                        l <<= 1;
                        if (f(_operation(sm, _array[l])))
                        {
                            sm = _operation(sm, _array[l]);
                            l++;
                        }
                    }
                    return l - _size;
                }
                sm = _operation(sm, _array[l]);
                l++;
            } while ((l & -l) != l);
            return _n;
        }
        /// <summary>
        /// f(op(a[l],a[l+1],...a[r-1])) = trueとなる最小のlを返します
        /// </summary>
        /// <param name="r"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public int MinLeft(int r, Func<T, bool> f)
        {
            Debug.Assert(0 <= r && r <= _n);
#if DEBUG
            Debug.Assert(f(_identity));
#endif
            if (r == 0) return 0;
            r += _size;
            T sm = _identity;

            do
            {
                r--;
                while (r > 1 && (r % 2 != 0)) r >>= 1;
                if (!f(_operation(_array[r], sm)))
                {
                    while (r < _size)
                    {
                        r = (2 * r + 1);
                        if (f(_operation(_array[r], sm)))
                        {
                            sm = _operation(_array[r], sm);
                            r--;
                        }
                    }
                    return r + 1 - _size;
                }
                sm = _operation(_array[r], sm);
            } while ((r & -r) != r);
            return 0;
        }

        public T this[int i]
        {
            set { Update(i, value); }
            get
            {
                Debug.Assert(0 <= i && i < _n);
                return _array[i + _size];
            }
        }
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
