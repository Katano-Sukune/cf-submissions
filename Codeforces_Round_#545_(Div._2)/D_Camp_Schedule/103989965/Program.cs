using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Text;
using CompLib.String;

public class Program
{
    string S, T;
    public void Solve()
    {
        var sc = new Scanner();
        S = sc.Next();
        T = sc.Next();

        int[] cnt = new int[2];
        for (int i = 0; i < S.Length; i++)
        {
            cnt[S[i] - '0']++;
        }
        int l = 0;
        var z = StringACL.ZAlgorithm(T);
        for (int i = 1; i < T.Length; i++)
        {
            if(z[i] == T.Length - i)
            {
                l = T.Length - i;
                break;
            }
        }

        int[] cnt2 = new int[2];
        for (int i = l; i < T.Length; i++)
        {
            cnt2[T[i] - '0']++;
        }

        int[] cnt3 = new int[2];
        for (int i = 0; i < T.Length; i++)
        {
            cnt3[T[i] - '0']++;
        }
        if (cnt3[0] > cnt[0] || cnt3[1] > cnt[1])
        {
            Console.WriteLine(S);
            return;
        }
        var subStr = T.Substring(l);
        var sb = new StringBuilder();
        sb.Append(T);
        cnt[0] -= cnt3[0];
        cnt[1] -= cnt3[1];
        while (cnt2[0] <= cnt[0] && cnt2[1] <= cnt[1])
        {
            sb.Append(subStr);
            cnt[0] -= cnt2[0];
            cnt[1] -= cnt2[1];
        }

        sb.Append('0', cnt[0]);
        sb.Append('1', cnt[1]);
        Console.WriteLine(sb);
    }

    int[] Table()
    {
        var res = new int[T.Length + 1];
        res[0] = 0;
        int j = 0;
        for (int i = 1; i < T.Length; i++)
        {
            if (T[i] == T[j])
            {
                res[i] = j++;
            }
            else
            {
                res[i] = j;
                j = 0;
            }
        }
        res[T.Length] = j;
        return res;
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

            // C++?????????????????????????????????????????????????????????????????????????
            // ????????????????????? #if????????????
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
        /// ??????n???????????????suffixArray?????????
        /// </summary>
        /// <remarks>
        /// <para>{0,1,2,...,n-1}????????????s[sa[i],n) < s[sa[i+1],n)</para>
        /// <para>suffixArray????????????</para>
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
                 // ????????????0
                 if (l == r) return 0;
                 // ???????????????
                 while (l < n && r < n)
                 {

                     if (s[l] != s[r]) return s[l].CompareTo(s[r]);
                     l++;
                     r++;
                 }

                 // index???????????????????????????????????????
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
        /// s???LCPArray??????????????? n-1??????????????????
        /// </summary>
        /// <remarks>
        /// <para>res[i]???s[sa[i],n), s[sa[i+1],n)???Longest Common Prefix?????????</para>
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
        /// s???LCPArray??????????????? n-1??????????????????
        /// </summary>
        /// <remarks>
        /// <para>res[i]???s[sa[i],n), s[sa[i+1],n)???Longest Common Prefix?????????</para>
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
        /// s???LCPArray??????????????? n-1??????????????????
        /// </summary>
        /// <remarks>
        /// <para>res[i]???s[sa[i],n), s[sa[i+1],n)???Longest Common Prefix?????????</para>
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
        /// ??????n?????????s???????????? s??? s[i,n)???LCP??????????????????
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
        /// ??????n?????????s???????????? s??? s[i,n)???LCP??????????????????
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int[] ZAlgorithm<T>(T[] s)
        {
            return ZAlgorithm(s, EqualityComparer<T>.Default);
        }

        /// <summary>
        /// ??????n????????????s???????????? s??? s[i,n)???LCP??????????????????
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
