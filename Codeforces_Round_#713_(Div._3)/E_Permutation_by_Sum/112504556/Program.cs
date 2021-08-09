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
        int l = sc.NextInt();
        int r = sc.NextInt();
        int s = sc.NextInt();

        // n順列 p

        // sum(a[l:r]) = s
        int len = r - l + 1;

        int min = (1 + len) * len / 2;
        int max = (n - len + 1 + n) * len / 2;

        if (s < min || max < s)
        {
            Console.WriteLine("-1");
            return;
        }

        int diff = s - min;
        int d = diff / len;
        int m = diff % len;

        bool[] flag = new bool[n + 1];
        var ans = new int[n];
        for (int i = 0; i < len; i++)
        {
            int num = 1 + d + i;
            if (len - i <= m) num++;
            // Console.WriteLine($"{i+l-1} {num} {d}");
            flag[num] = true;
            ans[i + l - 1] = num;
        }

        int p = 1;
        for (int i = 0; i < n; i++)
        {
            if (ans[i] == 0)
            {
                while (flag[p]) p++;
                ans[i] = p;
                flag[p] = true;
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