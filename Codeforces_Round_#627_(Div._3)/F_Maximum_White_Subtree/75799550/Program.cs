using System;
using System.Collections.Generic;
using CompLib.Util;

public class Program
{
    private int N;
    private int[] A;
    private List<int>[] Edge;

    private int[] Size;

    private int[] Memo;

    private int[] Ans;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();
        Edge = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            Edge[i] = new List<int>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            int a = sc.NextInt() - 1;
            int b = sc.NextInt() - 1;
            Edge[a].Add(b);
            Edge[b].Add(a);
        }

        Size = new int[N];
        Memo = new int[N];
        DFS1();
        Ans = new int[N];
        DFS2();
        Console.WriteLine(string.Join(" ", Ans));
    }

    void DFS1()
    {
        var ind = new int[N];
        var stack = new int[N];
        int ptr = 0;
        stack[0] = 0;
        while (ptr >= 0)
        {
            if (ind[ptr] < Edge[stack[ptr]].Count)
            {
                int to = Edge[stack[ptr]][ind[ptr]];
                if (ptr > 0 && to == stack[ptr - 1])
                {
                    ind[ptr]++;
                    continue;
                }

                stack[ptr + 1] = Edge[stack[ptr]][ind[ptr]];
                ind[ptr + 1] = 0;
                ind[ptr]++;
                ptr++;
            }
            else
            {
                Size[stack[ptr]] = 1;
                Memo[stack[ptr]] = A[stack[ptr]] == 1 ? 1 : -1;
                foreach (int to in Edge[stack[ptr]])
                {
                    if (ptr > 0 && to == stack[ptr - 1]) continue;
                    Size[stack[ptr]] += Size[to];
                    Memo[stack[ptr]] += Math.Max(0, Memo[to]);
                }

                ptr--;
            }
        }
    }

    void DFS2()
    {
        var ind = new int[N];
        var stack = new int[N];
        var pp = new int[N];
        int ptr = 0;
        stack[0] = 0;
        while (ptr >= 0)
        {
            int cur = stack[ptr];
            if (ind[ptr] < Edge[cur].Count)
            {
                int to = Edge[cur][ind[ptr]];
                if (ptr > 0 && to == stack[ptr - 1])
                {
                    ind[ptr]++;
                    continue;
                }

                pp[ptr + 1] = Math.Max(0, pp[ptr]) + Memo[stack[ptr]] - Math.Max(0, Memo[to]);
                stack[ptr + 1] = to;
                ind[ptr + 1] = 0;
                ind[ptr]++;
                ptr++;
            }
            else
            {
                Ans[stack[ptr]] = Memo[stack[ptr]] + Math.Max(pp[ptr], 0);
                ptr--;
            }
        }
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