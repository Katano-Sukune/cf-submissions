using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
#if !DEBUG
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
#endif
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        int m = sc.NextInt();

        long[] a = sc.LongArray();
        long[] x = sc.LongArray();

        /*
         * xを受け取る
         * 配列の最初に置く
         *
         * 
         */

        long sum = 0;
        foreach (long l in a)
        {
            sum += l;
        }

        long[] sum2 = new long[n + 1];
        for (int i = 0; i < n; i++)
        {
            sum2[i + 1] = sum2[i] + a[i];
        }

        List<(long num, int idx)> ls = new List<(long num, int idx)>();
        ls.Add((long.MinValue, 0));

        for (int i = 1; i <= n; i++)
        {
            if (ls[^1].num < sum2[i])
            {
                ls.Add((sum2[i], i));
            }
        }

        long[] ans = new long[m];
        for (int q = 0; q < m; q++)
        {
            if (x[q] <= ls[^1].num)
            {
                // 1週目でできる
                int ng = 0;
                int ok = ls.Count - 1;
                while (ok - ng > 1)
                {
                    int mid = (ok + ng) / 2;
                    if (x[q] <= ls[mid].num) ok = mid;
                    else ng = mid;
                }

                ans[q] = ls[ok].idx - 1;
                continue;
            }

            if (sum <= 0)
            {
                ans[q] = -1;
                continue;
            }

            // r週する
            long r = (x[q] - ls[^1].num + sum - 1) / sum;
            if (sum * r >= x[q])
            {
                ans[q] = r * n - 1;
                continue;
            }

            int ng2 = 0;
            int ok2 = ls.Count - 1;
            while (ok2 - ng2 > 1)
            {
                int mid = (ok2 + ng2) / 2;
                if (x[q] - sum * r <= ls[mid].num) ok2 = mid;
                else ng2 = mid;
            }

            ans[q] = r * n + ls[ok2].idx - 1;
        }

        Console.WriteLine(string.Join(" ", ans));
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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