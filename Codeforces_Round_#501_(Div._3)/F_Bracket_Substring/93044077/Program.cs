using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Mathematics;
using CompLib.String;

public class Program
{
    int N;
    string S;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        S = sc.Next();

        int cnt = 0;
        int min = 0;
        foreach (var c in S)
        {
            if (c == '(') cnt++;
            else cnt--;
            min = Math.Min(min, cnt);
        }
        /*
         * Sを部分文字列に含むカッコ列の個数
         */

        // iで間違えたときどこに飛ぶか?
        int[] next = new int[S.Length];
        for (int i = 0; i < S.Length; i++)
        {
            if (i == 0)
            {
                next[i] = 0;
                continue;
            }

            bool f2 = false;
            for (int len = i; len >= 1; len--)
            {
                bool f = true;
                for (int j = 0; f && j < len - 1; j++)
                {
                    f &= S[j] == S[i - len + 1 + j];
                }
                if (!f) continue;
                if (S[len - 1] != S[i])
                {
                    next[i] = len;
                    f2 = true;
                    break;
                }
            }

        }

        // i文字目まで作る、カウンタ、Sより前、Sより後, Sの何文字までできたか?
        var dp = new ModInt[2 * N + 1, N + 1, S.Length, 2];
        dp[0, 0, 0, 0] = 1;
        for (int i = 0; i < 2 * N; i++)
        {
            for (int j = 0; j <= N; j++)
            {
                for (int k = 0; k < S.Length; k++)
                {
                    // (
                    if (j + 1 <= N)
                    {
                        if (S[k] == '(')
                        {
                            if (k + 1 == S.Length)
                            {
                                dp[i + 1, j + 1, 0, 1] += dp[i, j, k, 0];
                            }
                            else
                            {
                                dp[i + 1, j + 1, k + 1, 0] += dp[i, j, k, 0];
                            }
                        }
                        else
                        {
                            // どこに飛ぶ
                            dp[i + 1, j + 1, next[k], 0] += dp[i, j, k, 0];
                        }


                    }

                    if (j - 1 >= 0)
                    {
                        if (S[k] == ')')
                        {
                            if (k + 1 == S.Length)
                            {
                                dp[i + 1, j - 1, 0, 1] += dp[i, j, k, 0];
                            }
                            else
                            {
                                dp[i + 1, j - 1, k + 1, 0] += dp[i, j, k, 0];
                            }
                        }
                        else
                        {
                            dp[i + 1, j - 1, next[k], 0] += dp[i, j, k, 0];
                        }

                    }
                }

                if (j + 1 <= N)
                {
                    dp[i + 1, j + 1, 0, 1] += dp[i, j, 0, 1];
                }
                if (j - 1 >= 0)
                {
                    dp[i + 1, j - 1, 0, 1] += dp[i, j, 0, 1];
                }
            }
        }

        Console.WriteLine(dp[2 * N, 0, 0, 1]);
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}


namespace CompLib.String
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    public static partial class StringACL
    {
        private const int ThresholdNative = 10;
        private const int ThresholdDoubling = 40;

        #region SuffixArray
        public static int[] SuffixArray(int[] s, int upper)
        {
            Debug.Assert(0 <= upper);

            // C++みたいに内部で何もしないループをスキップするのか?
            // してくれるなら #ifいらない
#if DEBUG
            foreach (var d in s)
            {
                Debug.Assert(0 <= d && d <= upper);
            }
#endif
            return SAIS(s, upper);
        }

        public static int[] SuffixArray<T>(T[] s, Comparison<T> cmp)
        {
            int n = s.Length;
            int[] idx = new int[n];
            for (int i = 0; i < n; i++)
            {
                idx[i] = i;
            }

            Array.Sort(idx, (l, r) => cmp(s[l], s[r]));

            int[] s2 = new int[n];
            int now = 0;
            for (int i = 0; i < n; i++)
            {
                if (0 < i && cmp(s[idx[i - 1]], s[idx[i]]) != 0) now++;
                s2[idx[i]] = now;
            }

            return SAIS(s2, now);
        }
        public static int[] SuffixArray<T>(T[] s)
        {
            return SuffixArray(s, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// 長さnの文字列のsuffixArrayを返す
        /// </summary>
        /// <remarks>
        /// <para>{0,1,2,...,n-1}の順列、s[sa[i],n) < s[sa[i+1],n)</para>
        /// <para>suffixArrayの辞書順</para>
        /// </remarks>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int[] SuffixArray(string s)
        {
            int n = s.Length;
            int[] s2 = new int[n];
            for (int i = 0; i < n; i++)
            {
                s2[i] = s[i];
            }
            return SAIS(s2, 255);
        }

        // O(n^2)
        private static int[] SANative(int[] s)
        {
            int n = s.Length;
            int[] sa = new int[n];
            for (int i = 0; i < n; i++)
            {
                sa[i] = i;
            }
            Array.Sort(sa, (l, r) =>
             {
                 // 同じなら0
                 if (l == r) return 0;
                 // 順番に見る
                 while (l < n && r < n)
                 {

                     if (s[l] != s[r]) return s[l].CompareTo(s[r]);
                     l++;
                     r++;
                 }

                 // indexが大きい方が辞書順で小さい
                 return r.CompareTo(l);
             });
            return sa;
        }
        private static int[] SADoubling(int[] s)
        {
            int n = s.Length;
            int[] sa = new int[n];
            int[] rnk = new int[n];
            int[] tmp = new int[n];
            Array.Copy(s, rnk, n);
            for (int i = 0; i < n; i++)
            {
                sa[i] = i;
            }

            for (int k = 1; k < n; k *= 2)
            {
                Comparison<int> cmp = (int x, int y) =>
                {
                    if (rnk[x] != rnk[y]) return rnk[x].CompareTo(rnk[y]);
                    int rx = x + k < n ? rnk[x + k] : -1;
                    int ry = y + k < n ? rnk[y + k] : -1;
                    return rx.CompareTo(ry);
                };

                Array.Sort(sa, cmp);
                tmp[sa[0]] = 0;
                for (int i = 1; i < n; i++)
                {
                    tmp[sa[i]] = tmp[sa[i - 1]] + (cmp(sa[i - 1], sa[i]) < 0 ? 1 : 0);
                }

                var t = tmp;
                tmp = rnk;
                rnk = t;
            }

            return sa;
        }

        private static int[] SAIS(int[] s, int upper)
        {
            int n = s.Length;
            if (n == 0) return new int[0];
            if (n == 1) return new int[1];
            if (n == 2)
            {
                if (s[0] < s[1])
                {
                    return new int[] { 0, 1 };
                }
                else
                {
                    return new int[] { 1, 0 };
                }
            }

            if (n < ThresholdNative)
            {
                return SANative(s);
            }

            if (n < ThresholdDoubling)
            {
                return SADoubling(s);
            }

            int[] sa = new int[n];
            bool[] ls = new bool[n];

            for (int i = n - 2; i >= 0; i--)
            {
                ls[i] = (s[i] == s[i + 1]) ? ls[i + 1] : (s[i] < s[i + 1]);
            }

            int[] sumL = new int[upper + 1];
            int[] sumS = new int[upper + 1];

            for (int i = 0; i < n; i++)
            {
                if (!ls[i])
                {
                    sumS[s[i]]++;
                }
                else
                {
                    sumL[s[i] + 1]++;
                }
            }

            for (int i = 0; i <= upper; i++)
            {
                sumS[i] += sumL[i];
                if (i < upper) sumL[i + 1] += sumS[i];
            }

            Action<List<int>> induce = (List<int> lms) =>
          {
              for (int i = 0; i < n; i++)
              {
                  sa[i] = -1;
              }

              int[] buf = new int[upper + 1];
              Array.Copy(sumS, buf, upper + 1);

              foreach (var d in lms)
              {
                  if (d == n) continue;
                  sa[buf[s[d]]++] = d;
              }

              Array.Copy(sumL, buf, upper + 1);
              sa[buf[s[n - 1]]++] = n - 1;
              for (int i = 0; i < n; i++)
              {
                  int v = sa[i];
                  if (v >= 1 && !ls[v - 1])
                  {
                      sa[buf[s[v - 1]]++] = v - 1;
                  }
              }

              Array.Copy(sumL, buf, upper + 1);

              for (int i = n - 1; i >= 0; i--)
              {
                  int v = sa[i];
                  if (v >= 1 && ls[v - 1])
                  {
                      sa[--buf[s[v - 1] + 1]] = v - 1;
                  }
              }
          };

            int[] lmsMap = new int[n + 1];
            for (int i = 0; i <= n; i++)
            {
                lmsMap[i] = -1;
            }

            int m = 0;
            for (int i = 1; i < n; i++)
            {
                if (!ls[i - 1] && ls[i])
                {
                    lmsMap[i] = m++;
                }
            }

            List<int> lmsL = new List<int>(m);
            for (int i = 1; i < n; i++)
            {
                if (!ls[i - 1] && ls[i])
                {
                    lmsL.Add(i);
                }
            }

            induce(lmsL);

            if (m != 0)
            {
                List<int> sortedLms = new List<int>(m);
                foreach (var v in sa)
                {
                    if (lmsMap[v] != -1) sortedLms.Add(v);
                }

                int[] recS = new int[m];

                int recUpper = 0;
                recS[lmsMap[sortedLms[0]]] = 0;
                for (int i = 1; i < m; i++)
                {
                    int l = sortedLms[i - 1], r = sortedLms[i];
                    int endL = (lmsMap[l] + 1 < m) ? lmsL[lmsMap[l] + 1] : n;
                    int endR = (lmsMap[r] + 1 < m) ? lmsL[lmsMap[r] + 1] : n;

                    bool same = true;

                    if (endL - l != endR - r)
                    {
                        same = false;
                    }
                    else
                    {
                        while (l < endL)
                        {
                            if (s[l] != s[r])
                            {
                                break;
                            }
                            l++;
                            r++;
                        }

                        if (l == n || s[l] != s[r]) same = false;
                    }

                    if (!same) recUpper++;
                    recS[lmsMap[sortedLms[i]]] = recUpper;
                }

                var recSa = SAIS(recS, recUpper);

                for (int i = 0; i < m; i++)
                {
                    sortedLms[i] = lmsL[recSa[i]];
                }

                induce(sortedLms);
            }
            return sa;
        }
        #endregion // SuffixArray

        #region LCPArray
        /// <summary>
        /// sのLCPArrayとして長さ n-1の配列を返す
        /// </summary>
        /// <remarks>
        /// <para>res[i]はs[sa[i],n), s[sa[i+1],n)のLongest Common Prefixの長さ</para>
        /// <para>O(n)</para>
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <param name="sa"></param>
        /// <param name="cmp"></param>
        /// <returns></returns>
        public static int[] LCPArray<T>(T[] s, int[] sa, Comparison<T> cmp)
        {
            int n = s.Length;
            Debug.Assert(n >= 1);
            int[] rnk = new int[n];
            for (int i = 0; i < n; i++)
            {
                rnk[sa[i]] = i;
            }

            int[] lcp = new int[n - 1];
            int h = 0;
            for (int i = 0; i < n; i++)
            {
                if (h > 0) h--;
                if (rnk[i] == 0) continue;
                int j = sa[rnk[i] - 1];
                for (; j + h < n && i + h < n; h++)
                {
                    if (cmp(s[j + h], s[i + h]) != 0) break;
                }
                lcp[rnk[i] - 1] = h;
            }

            return lcp;
        }

        /// <summary>
        /// sのLCPArrayとして長さ n-1の配列を返す
        /// </summary>
        /// <remarks>
        /// <para>res[i]はs[sa[i],n), s[sa[i+1],n)のLongest Common Prefixの長さ</para>
        /// <para>O(n)</para>
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <param name="sa"></param>
        /// <returns></returns>
        public static int[] LCPArray<T>(T[] s, int[] sa)
        {
            return LCPArray(s, sa, Comparer<T>.Default.Compare);
        }
        /// <summary>
        /// sのLCPArrayとして長さ n-1の配列を返す
        /// </summary>
        /// <remarks>
        /// <para>res[i]はs[sa[i],n), s[sa[i+1],n)のLongest Common Prefixの長さ</para>
        /// <para>O(n)</para>
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <param name="sa"></param>
        /// <returns></returns>
        public static int[] LCPArray(string s, int[] sa)
        {
            //int n = s.Length;
            //int[] s2 = new int[n];
            //for (int i = 0; i < n; i++)
            //{
            //    s2[i] = s[i];
            //}
            return LCPArray(s.ToCharArray(), sa);
        }

        #endregion // LCPArray

        #region ZAlgorithm

        /// <summary>
        /// 長さnの配列sについて sと s[i,n)のLCPの長さを返す
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <param name="eq"></param>
        /// <returns></returns>
        public static int[] ZAlgorithm<T>(T[] s, EqualityComparer<T> eq)
        {
            int n = s.Length;
            if (n == 0) return new int[0];
            int[] z = new int[n];
            z[0] = 0;
            for (int i = 1, j = 0; i < n; i++)
            {
                ref int k = ref z[i];
                k = (j + z[j] <= i) ? 0 : Math.Min(j + z[j] - i, z[i - j]);
                while (i + k < n && eq.Equals(s[k], s[i + k])) k++;
                if (j + z[j] < i + z[i]) j = i;
            }
            z[0] = n;
            return z;
        }

        /// <summary>
        /// 長さnの配列sについて sと s[i,n)のLCPの長さを返す
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int[] ZAlgorithm<T>(T[] s)
        {
            return ZAlgorithm(s, EqualityComparer<T>.Default);
        }

        /// <summary>
        /// 長さnの文字列sについて sと s[i,n)のLCPの長さを返す
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int[] ZAlgorithm(string s)
        {
            return ZAlgorithm(s.ToCharArray());
        }

        #endregion
    }
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
