using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    long[] A;
    List<int>[] E;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.LongArray();
        E = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<int>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            E[u].Add(v);
            E[v].Add(u);
        }
        long all = 0;
        foreach (var i in A)
        {
            all += i;
        }
        // 部分木iのAの合計
        long[] sum = new long[N];
        // 部分木iのコスト
        long[] cost = new long[N];
        {
            int[] stack = new int[N];
            int[] index = new int[N];
            int ptr = 0;
            while (ptr >= 0)
            {
                int cur = stack[ptr];
                if (index[ptr] < E[cur].Count)
                {
                    if (ptr > 0 && stack[ptr - 1] == E[cur][index[ptr]])
                    {
                        index[ptr]++;
                        continue;
                    }
                    stack[ptr + 1] = E[cur][index[ptr]];
                    index[ptr + 1] = 0;
                    index[ptr]++;
                    ptr++;
                }
                else
                {
                    foreach (int to in E[cur])
                    {
                        if (ptr > 0 && to == stack[ptr - 1]) continue;
                        cost[cur] += cost[to] + sum[to];
                        sum[cur] += sum[to];
                    }
                    sum[cur] += A[cur];
                    ptr--;
                }
            }
        }
        long ans = 0;
        {
            int[] stack = new int[N];
            int[] index = new int[N];

            // 親側のコスト
            long[] pp = new long[N];

            // 親側の合計
            long[] pSum = new long[N];
            int ptr = 0;
            while (ptr >= 0)
            {
                int cur = stack[ptr];
                if (index[ptr] < E[cur].Count)
                {
                    if (ptr > 0 && stack[ptr - 1] == E[cur][index[ptr]])
                    {
                        index[ptr]++;
                        continue;
                    }
                    stack[ptr + 1] = E[cur][index[ptr]];
                    index[ptr + 1] = 0;
                    pSum[ptr + 1] = all - sum[stack[ptr + 1]];
                    pp[ptr + 1] = pp[ptr] + pSum[ptr + 1] + cost[cur] - (cost[stack[ptr + 1]] + sum[stack[ptr + 1]]);
                    index[ptr]++;
                    ptr++;
                }
                else
                {
                    ans = Math.Max(ans, cost[cur] + pp[ptr]);
                    // Console.WriteLine($"{cur} {cost[cur]} {pp[ptr]} {pSum[ptr]}");
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
