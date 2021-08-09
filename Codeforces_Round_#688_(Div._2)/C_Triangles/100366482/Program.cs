using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
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
        string[] s = new string[n];
        for (int i = 0; i < n; i++)
        {
            s[i] = sc.Next();
        }

        // i行目にあるn
        var row = new List<int>[n, 10];
        var col = new List<int>[n, 10];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                row[i, j] = new List<int>();
                col[i, j] = new List<int>();
            }
        }

        int[] u = new int[10];
        int[] d = new int[10];
        int[] l = new int[10];
        int[] r = new int[10];

        for (int i = 0; i < 10; i++)
        {
            u[i] = int.MaxValue;
            d[i] = int.MinValue;
            l[i] = int.MaxValue;
            r[i] = int.MinValue;
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                int num = s[i][j] - '0';
                u[num] = Math.Min(u[num], i);
                d[num] = Math.Max(d[num], i);
                l[num] = Math.Min(l[num], j);
                r[num] = Math.Max(r[num], j);

                row[i, num].Add(j);
                col[j, num].Add(i);
            }
        }

        int[] ans = new int[10];
        for (int num = 0; num <= 9; num++)
        {
            for (int i = 0; i < n; i++)
            {
                // i行目
                // もともとある2つ
                if (row[i, num].Count >= 2)
                {
                    int t1 = row[i, num][^1] - row[i, num][0];
                    int t2 = Math.Max(i, n - 1 - i);
                    ans[num] = Math.Max(ans[num], t1 * t2);
                }

                // 行に追加
                if (row[i, num].Count >= 1)
                {
                    int t1 = Math.Max(n - 1 - row[i, num][0], row[i, num][^1]);
                    int t2 = Math.Max(i - u[num], d[num] - i);
                    ans[num] = Math.Max(ans[num], t1 * t2);
                }

                if (col[i, num].Count >= 2)
                {
                    int t1 = col[i, num][^1] - col[i, num][0];
                    int t2 = Math.Max(i, n - 1 - i);
                    ans[num] = Math.Max(ans[num], t1 * t2);
                }

                if (col[i, num].Count >= 1)
                {
                    int t1 = Math.Max(n - 1 - col[i, num][0], col[i, num][^1]);
                    int t2 = Math.Max(i - l[num], r[num] - i);
                    ans[num] = Math.Max(ans[num], t1 * t2);
                }
            }
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