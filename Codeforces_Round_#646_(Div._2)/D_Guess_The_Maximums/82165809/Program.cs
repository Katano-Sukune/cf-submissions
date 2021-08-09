using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    Scanner Sc;
    public void Solve()
    {
        Sc = new Scanner();
        int t = Sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            int n = Sc.NextInt();
            int k = Sc.NextInt();
            var c = new int[k];
            var s = new int[k][];
            for (int j = 0; j < k; j++)
            {
                c[j] = Sc.NextInt();
                s[j] = new int[c[j]];
                for (int l = 0; l < c[j]; l++)
                {
                    s[j][l] = Sc.NextInt();
                }
            }
            Q(n, k, c, s);
        }

    }

    void Q(int n, int k, int[] c, int[][] s)
    {
        // パスワードP 長さk 配列 1 <= P_i <= n

        // 配列A 長さn 1 <= A_i <= n



        // 全体の最大値聞く

        // sの左半分の最大値聞く

        // 一緒? sの右:左半分 全部 全体の最大

        // 残り半分 聞く

        int max;
        {
            int[] qq = new int[n].Select((i, index) => index + 1).ToArray();
            max = Q(qq);
        }

        int left = 0;
        int right = k;
        int[] ans = new int[k];
        while (right - left > 1)
        {

            // sの[l,mid)を聞く

            // max ... [l,r)に最大値がある 以外 全部max

            // それ以外に最大値がある [l,r) 全部max

            int mid = (left + right) / 2;
            var ls = new List<int>();
            for (int i = left; i < mid; i++)
            {
                ls.AddRange(s[i]);
            }

            int res = Q(ls.ToArray());

            if (res == max)
            {
                for (int i = mid; i < right; i++)
                {
                    ans[i] = max;
                }
                right = mid;
            }
            else
            {
                for (int i = left; i < mid; i++)
                {
                    ans[i] = max;
                }
                left = mid;
            }

        }

        // 最大値があるSが分かった

        // S[l] 以外聞く

        var ls2 = new List<int>();
        for (int i = 1; i <= n; i++)
        {
            if (s[left].Contains(i)) continue;
            ls2.Add(i);
        }

        ans[left] = Q(ls2.ToArray());

        Console.WriteLine($"! {string.Join(" ", ans)}");
        Sc.Next();
    }

    int[] Test = new int[] { 1, 2, 3, 4 };

    int Q(int[] q)
    {
#if DEBUG
        Console.WriteLine($"[DEBUG] ? {q.Length} {string.Join(" ", q)}");
        int ans = int.MinValue;
        for (int i = 0; i < q.Length; i++)
        {
            ans = Math.Max(ans, Test[q[i] - 1]);
        }
        Console.WriteLine($"[DEBUG] {ans}");
        return ans;
#else
        Console.WriteLine($"? {q.Length} {string.Join(" ", q)}");
        return Sc.NextInt();
#endif
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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
                _index = 0;
            }

            return _line[_index++];
        }

        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public decimal NextDecimal() => decimal.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
