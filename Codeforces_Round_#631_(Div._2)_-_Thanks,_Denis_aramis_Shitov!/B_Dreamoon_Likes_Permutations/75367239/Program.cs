using System;
using System.Collections.Generic;
using System.Text;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Q(sc.NextInt(), sc.IntArray()));
        }

        Console.Write(sb.ToString());
    }

    string Q(int n, int[] a)
    {
        bool[] front = new bool[n + 1];
        front[0] = true;
        bool[] ff = new bool[n];
        int max = 0;
        for (int i = 0; i < n; i++)
        {
            if (ff[a[i]]) break;
            ff[a[i]] = true;
            max = Math.Max(max, a[i]);
            if (max == i + 1) front[i + 1] = true;
        }

        bool[] back = new bool[n + 1];
        bool[] bf = new bool[n];
        max = 0;
        for (int i = n - 1; i >= 0; i--)
        {
            if (bf[a[i]]) break;
            bf[a[i]] = true;
            max = Math.Max(max, a[i]);
            if (max == n - i) back[i] = true;
        }

        List<string> ans = new List<string>();
        for (int i = 1; i <= n - 1; i++)
        {
            if (front[i] && back[i])
            {
                ans.Add($"{i} {n - i}");
            }
        }

//Console.WriteLine(string.Join(" ",front));
//Console.Write(string.Join(" ",back));
        if (ans.Count == 0) return "0";
        return $"{ans.Count}\n{string.Join("\n", ans)}";
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