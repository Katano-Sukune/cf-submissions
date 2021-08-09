using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, M;
    List<E>[] Edges;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        Edges = new List<E>[N];
        for (int i = 0; i < N; i++)
        {
            Edges[i] = new List<E>();
        }
        for (int i = 0; i < M; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            Edges[u].Add(new E(v, i));
        }

        // 有向グラフ
        // サークルはすべて同じ色ではないように辺に色を塗る

        // 色　最小

        {
            // 1色でできるか?
            bool[] ans = new bool[M];
            int[] stack = new int[N];
            int[] index = new int[N];
            bool[] flag = new bool[N];
            bool[] flag2 = new bool[N];
            bool tmp = true;
            for (int i = 0; i < N; i++)
            {
                if (flag2[i]) continue;
                int ptr = 0;
                stack[0] = i;
                index[0] = 0;
                flag[i] = true;
                while (ptr >= 0)
                {
                    int cur = stack[ptr];
                    if (index[ptr] < Edges[cur].Count)
                    {
                        if (flag[Edges[cur][index[ptr]].To])
                        {
                            tmp = false;
                            ans[Edges[cur][index[ptr]].I] = true;
                            index[ptr]++;
                            continue;
                        }

                        if (flag2[Edges[cur][index[ptr]].To])
                        {
                            index[ptr]++;
                            continue;
                        }
                        stack[ptr + 1] = Edges[cur][index[ptr]].To;
                        flag[stack[ptr + 1]] = true;
                        index[ptr + 1] = 0;
                        index[ptr]++;
                        ptr++;
                    }
                    else
                    {
                        flag[cur] = false;
                        flag2[cur] = true;
                        ptr--;
                    }
                }
            }

            if (tmp)
            {
                Console.WriteLine(1);
            }
            else
            {
                Console.WriteLine(2);
            }

            Console.WriteLine(string.Join(" ", ans.Select(b => b ? 2 : 1)));
        }
    }

    public static void Main(string[] args) => new Program().Solve();
}

struct E
{
    public int To, I;
    public E(int t, int i)
    {
        To = t;
        I = i;
    }
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
