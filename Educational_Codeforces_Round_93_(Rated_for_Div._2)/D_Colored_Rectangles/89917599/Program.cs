using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{

    int R, G, B;
    int[] r;
    int[] g;
    int[] b;
    public void Solve()
    {
        var sc = new Scanner();
        R = sc.NextInt();
        G = sc.NextInt();
        B = sc.NextInt();
        r = sc.IntArray();
        g = sc.IntArray();
        b = sc.IntArray();


        /*
         * 色棒
         * 赤 i 長さr_i
         * 緑
         * 青
         * 
         * 長方形
         * 
         * ある色の棒のペアを取る
         * 上と違う色の棒のペアを取る
         * 
         * たて2本、横のペア同色
         * 
         * ペアは最大1回まで使える
         * 
         * 作った長方形の面積最大
         */

        /*
         * r,g,bのペアリスト
         * 
         *
         */

        Array.Sort(r, (l, r) => r.CompareTo(l));
        Array.Sort(g, (l, r) => r.CompareTo(l));
        Array.Sort(b, (l, r) => r.CompareTo(l));
        var dp = new long[r.Length + 1, g.Length + 1, b.Length + 1];
        for (int i = 0; i <= r.Length; i++)
        {
            for (int j = 0; j <= g.Length; j++)
            {
                for (int k = 0; k < b.Length; k++)
                {
                    dp[i, j, k] = long.MinValue;
                }
            }
        }
        long ans = long.MinValue;
        dp[0, 0, 0] = 0;
        for (int i = 0; i <= r.Length; i++)
        {
            for (int j = 0; j <= g.Length; j++)
            {
                for (int k = 0; k <= b.Length; k++)
                {
                    if (dp[i, j, k] == long.MinValue) continue;
                    long cur = dp[i, j, k];
                    if (i < r.Length && j < g.Length)
                    {
                        ref var t = ref dp[i + 1, j + 1, k];
                        t = Math.Max(t, cur + r[i] * g[j]);
                    }

                    if (i < r.Length && k < b.Length)
                    {
                        ref var t = ref dp[i + 1, j, k + 1];
                        t = Math.Max(t, cur + r[i] * b[k]);
                    }

                    if (j < g.Length && k < b.Length)
                    {
                        ref var t = ref dp[i, j + 1, k + 1];
                        t = Math.Max(t, cur + g[j] * b[k]);
                    }

                    ans = Math.Max(ans, cur);
                }
            }
        }

        Console.WriteLine(ans);
    }

    int[] F(int[] s)
    {
        Array.Sort(s, (l, r) => r.CompareTo(l));
        var res = new List<int>();
        for (int i = 0; i < s.Length;)
        {
            if (i + 1 < s.Length && s[i] == s[i + 2])
            {
                res.Add(s[i]);
                i += 2;
            }
            else
            {
                i++;
            }
        }

        return res.ToArray();
    }

    public static void Main(string[] args) => new Program().Solve();
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
