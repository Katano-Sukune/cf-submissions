using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    int[] A;
    List<int>[] edges;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();
        edges = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            edges[i] = new List<int>();
        }
        for (int i = 0; i < N - 1; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            edges[u].Add(v);
            edges[v].Add(u);
        }
        int r = 0;
        int b = 0;
        for (int i = 0; i < N; i++)
        {
            if (A[i] == 1) r++;
            if (A[i] == 2) b++;
        }
        // 部分木iにある色
        int[] red = new int[N];
        int[] blue = new int[N];
        int ans = 0;
        {
            int[] stack = new int[N];
            int[] index = new int[N];
            int ptr = 0;
            while (ptr >= 0)
            {
                int cur = stack[ptr];
                if (index[ptr] < edges[cur].Count)
                {
                    if (ptr > 0 && edges[cur][index[ptr]] == stack[ptr - 1])
                    {
                        index[ptr]++;
                        continue;
                    }
                    stack[ptr + 1] = edges[cur][index[ptr]];
                    index[ptr + 1] = 0;
                    index[ptr]++;
                    ptr++;
                }
                else
                {
                    if (A[cur] == 1) red[cur]++;
                    if (A[cur] == 2) blue[cur]++;
                    foreach (int to in edges[cur])
                    {
                        red[cur] += red[to];
                        blue[cur] += blue[to];

                        if(red[to] == 0 || blue[to] == 0)
                        {
                            int rr = r - red[to];
                            int bb = b - blue[to];
                            if(rr == 0 || bb == 0)
                            {
                                ans++;
                            }
                        }
                    }
                    ptr--;
                }
            }
        }
  
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
