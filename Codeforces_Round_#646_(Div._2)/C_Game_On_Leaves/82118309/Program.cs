using System;
using System.Collections.Generic;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < t; i++)
        {
            int n = sc.NextInt();
            int x = sc.NextInt() - 1;
            var l = new List<int>[n];
            for (int j = 0; j < n; j++)
            {
                l[j] = new List<int>();

            }
            for (int j = 0; j < n - 1; j++)
            {
                int u = sc.NextInt() - 1;
                int v = sc.NextInt() - 1;
                l[u].Add(v);
                l[v].Add(u);
            }
            Console.WriteLine(Q(n, x, l));
        }
        Console.Out.Flush();
    }

    string Q(int n, int x, List<int>[] e)
    {
        // n頂点の木

        // 葉を選んで消す
        // xを消したら勝ち
        // 先手はAyush
        // 後手Ashish

        // xを消せる
        // xの子を一つ残して消した　相手

        if (e[x].Count <= 1)
        {
            return "Ayush";
        }

        return n % 2 == 0 ? "Ayush" : "Ashish";
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
