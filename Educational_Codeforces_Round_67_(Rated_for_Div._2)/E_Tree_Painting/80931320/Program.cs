using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    List<int>[] E;
    public void Solve()
    {
        var sc = new Scanner();

        // 頂点iを選ぶ
        // iからの距離+1の総和を最大にする

        N = sc.NextInt();
        E = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<int>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            int a = sc.NextInt() - 1;
            int b = sc.NextInt() - 1;
            E[a].Add(b);
            E[b].Add(a);
        }

        // 0が根
        // 部分木iのスコア
        long[] score = new long[N];

        // 部分木iの大きさ
        int[] size = new int[N];
        {
            int[] stack = new int[N];
            int[] index = new int[N];
            stack[0] = 0;
            index[0] = 0;
            int ptr = 0;
            while (ptr >= 0)
            {
                int cur = stack[ptr];
                ref int i = ref index[ptr];
                if (i < E[cur].Count)
                {
                    if (ptr > 0 && stack[ptr - 1] == E[cur][i])
                    {
                        i++;
                        continue;
                    }
                    stack[ptr + 1] = E[cur][i];
                    index[ptr + 1] = 0;
                    i++;
                    ptr++;
                }
                else
                {
                    size[cur] = 1;
                    score[cur] = 1;
                    foreach (var to in E[cur])
                    {
                        size[cur] += size[to];
                        score[cur] += score[to] + size[to];
                    }
                    ptr--;
                }
            }
        }

        long ans = score[0];
        {
            int[] stack = new int[N];
            int[] index = new int[N];

            // 親側のスコア
            long[] par = new long[N];
            int ptr = 0;
            while (ptr >= 0)
            {
                int cur = stack[ptr];
                ref int i = ref index[ptr];
                if (i < E[cur].Count)
                {
                    if (ptr > 0 && stack[ptr - 1] == E[cur][i])
                    {
                        i++;
                        continue;
                    }
                    stack[ptr + 1] = E[cur][i];
                    index[ptr + 1] = 0;
                    // 親側のスコア 
                    // Console.WriteLine($"{score[cur]} {score[stack[ptr + 1]]} {size[stack[ptr + 1]]}");
                    par[ptr + 1] = par[ptr] + N + score[cur] - score[stack[ptr + 1]] - 2 * size[stack[ptr + 1]];
                    i++;
                    ptr++;
                }
                else
                {
                    // Console.WriteLine($"aaa {cur} {score[cur]} {par[ptr]}");
                    ans = Math.Max(ans, score[cur] + par[ptr]);
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
