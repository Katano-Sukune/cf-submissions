using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n, q;
        n = sc.NextInt();
        q = sc.NextInt();

        var child = new List<int>[n];
        for (int i = 0; i < n; i++)
        {
            child[i] = new List<int>();
        }
        for (int i = 1; i < n; i++)
        {
            child[sc.NextInt() - 1].Add(i);
        }

        var tour = new List<int>();
        int[] size = new int[n]; // 部分木のサイズ

        tour.Add(0);
        {
            int[] stack = new int[n];
            int[] index = new int[n];
            int ptr = 0;
            while (ptr >= 0)
            {
                int cur = stack[ptr];
                if (index[ptr] < child[cur].Count)
                {
                    stack[ptr + 1] = child[cur][index[ptr]];
                    tour.Add(stack[ptr + 1]);
                    index[ptr + 1] = 0;
                    index[ptr]++;
                    ptr++;
                }
                else
                {
                    size[cur] = 1;
                    foreach (var to in child[cur])
                    {
                        size[cur] += size[to];
                    }
                    ptr--;
                }
            }
        }
        var index2 = new int[n];
        for (int i = 0; i < n; i++)
        {
            index2[tour[i]] = i;
        }
        var sb = new StringBuilder();

        for (int i = 0; i < q; i++)
        {
            int u = sc.NextInt() - 1;
            int k = sc.NextInt();
            var ii = index2[u];
            if (k <= size[u])
            {
                sb.AppendLine((tour[ii + k - 1] + 1).ToString());
            }
            else
            {
                sb.AppendLine("-1");
            }
        }
        Console.Write(sb);
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
