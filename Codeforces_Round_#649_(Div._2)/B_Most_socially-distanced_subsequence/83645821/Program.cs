using System;
using System.Collections.Generic;
using System.IO;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();

        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        int[] p = sc.IntArray();

        // 長さn 順列p

        // 長さ2以上部分列 s
        // |s_0 - s_1| + .... |s_k-2 - s_k-1| が最大になるs
        // 複数あるなら長さ最小

        List<int> ans = new List<int>();
        ans.Add(p[0]);
        bool f = p[0] < p[1];
        for (int i = 1; i < n - 1; i++)
        {
            if (f ^ (p[i] < p[i + 1]))
            {
                ans.Add(p[i]);
                f = !f;
            }
        }

        ans.Add(p[n - 1]);
        // long t = 0;
        // for (int i = 0; i < ans.Count - 1; i++)
        // {
        //     t += Math.Abs(ans[i] - ans[i + 1]);
        // }

        Console.WriteLine(ans.Count);
        Console.WriteLine(string.Join(" ", ans));
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