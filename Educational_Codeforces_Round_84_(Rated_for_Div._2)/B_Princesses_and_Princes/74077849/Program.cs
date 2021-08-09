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
            int n = sc.NextInt();
            int[] k = new int[n];
            int[][] g = new int[n][];
            for (int j = 0; j < n; j++)
            {
                k[j] = sc.NextInt();
                g[j] = new int[k[j]];
                for (int l = 0; l < k[j]; l++)
                {
                    g[j][l] = sc.NextInt() - 1;
                }
            }

            sb.AppendLine(Q(n, k, g));
        }

        Console.Write(sb.ToString());
    }

    string Q(int n, int[] k, int[][] g)
    {
        bool[] usedF = new bool[n];
        bool[] usedM = new bool[n];

        for (int i = 0; i < n; i++)
        {
            foreach (int j in g[i])
            {
                if (!usedM[j])
                {
                    usedM[j] = true;
                    usedF[i] = true;
                    break;
                }
            }
        }

        int a = -1;
        int b = -1;
        for (int i = 0; i < n; i++)
        {
            if (!usedF[i]) a = i + 1;
            if (!usedM[i]) b = i + 1;
        }

        if (a == -1)
        {
            return "OPTIMAL";
        }

        return $"IMPROVE\n{a} {b}";
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