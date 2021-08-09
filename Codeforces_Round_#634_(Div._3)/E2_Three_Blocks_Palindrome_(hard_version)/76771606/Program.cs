using System;
using System.Collections.Generic;
using System.Text;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        var sb = new StringBuilder();
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Q(sc.NextInt(), sc.IntArray()));
        }

        Console.Write(sb);
    }

    string Q(int n, int[] a)
    {
        // aの最大値
        int al = 0;
        foreach (int i in a)
        {
            al = Math.Max(al, i);
        }

        var sum = new int[al + 1][];
        List<int>[] ls = new List<int>[al + 1];
        for (int i = 1; i <= al; i++)
        {
            sum[i] = new int[n + 1];
            ls[i] = new List<int>();
        }


        for (int i = 0; i < n; i++)
        {
            for (int j = 1; j <= al; j++)
            {
                sum[j][i + 1] = sum[j][i];
            }

            sum[a[i]][i + 1]++;
            ls[a[i]].Add(i);
        }

        int ans = 0;
        for (int i = 1; i <= al; i++)
        {
            ans = Math.Max(ans, ls[i].Count);
        }

        for (int i = 1; i <= al; i++)
        {
            for (int j = 0; j < ls[i].Count / 2; j++)
            {
                int k = ls[i].Count - 1 - j;
                for (int l = 1; l <= al; l++)
                {
                    ans = Math.Max(ans, (j + 1) * 2 + sum[l][ls[i][k]] - sum[l][ls[i][j] + 1]);
                }
            }
        }

        return ans.ToString();
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