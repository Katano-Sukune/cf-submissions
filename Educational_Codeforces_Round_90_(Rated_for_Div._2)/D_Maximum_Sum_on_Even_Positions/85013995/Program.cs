using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        var sc = new Scanner();
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
        int[] a = sc.IntArray();

        // 連続部分列を反転させる
        // 1回
        // 操作後 偶数番目の要素の和の最大

        var odd = new List<long>();
        odd.Add(0);
        for (int i = 0; i + 1 < n; i += 2)
        {
            odd.Add(odd[odd.Count - 1] + a[i + 1] - a[i]);
        }

        var even = new List<long>();
        even.Add(0);
        for (int i = 1; i + 1 < n; i += 2)
        {
            even.Add(even[even.Count - 1] + a[i] - a[i + 1]);
        }

        long max = 0;

        long tmp = 0;
        for (int i = 0; i < odd.Count; i++)
        {
            max = Math.Max(max, odd[i] - tmp);
            tmp = Math.Min(tmp, odd[i]);
        }

        tmp = 0;
        for (int i = 0; i < even.Count; i++)
        {
            max = Math.Max(max, even[i] - tmp);
            tmp = Math.Min(tmp, even[i]);
        }

        long ans = 0;
        for (int i = 0; i < n; i += 2)
        {
            ans += a[i];
        }

        ans += max;

        Console.WriteLine(ans);
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
                string s = Console.ReadLine();
                while (s.Length == 0)
                {
                    s = Console.ReadLine();
                }

                _line = s.Split(Separator);
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