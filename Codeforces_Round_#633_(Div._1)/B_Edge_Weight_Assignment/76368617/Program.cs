using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    private int N;
    private List<int>[] Edges;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        Edges = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            Edges[i] = new List<int>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            int a = sc.NextInt() - 1;
            int b = sc.NextInt() - 1;
            Edges[a].Add(b);
            Edges[b].Add(a);
        }

        // min
        // 距離奇数の葉の対がある 3 無い 1
        int min;
        {
            var q = new Queue<int>();
            int leaf = -1;
            for (int i = 0; i < N; i++)
            {
                if (Edges[i].Count == 1)
                {
                    leaf = i;
                    break;
                }
            }

            int[] dist = new int[N].Select(i => -1).ToArray();
            dist[leaf] = 0;
            q.Enqueue(leaf);
            while (q.Count > 0)
            {
                var d = q.Dequeue();
                foreach (int to in Edges[d])
                {
                    if (dist[to] != -1) continue;
                    dist[to] = dist[d] + 1;
                    q.Enqueue(to);
                }
            }

            bool f = false;
            for (int i = 0; i < N; i++)
            {
                if (Edges[i].Count == 1 && dist[i] % 2 == 1)
                {
                    f = true;
                    break;
                }
            }

            min = f ? 3 : 1;
        }

        // max
        // n-1 - ∑頂点 -> 葉に伸びてるやつ -1
        int max;
        {
            max = N - 1;
            for (int i = 0; i < N; i++)
            {
                int cnt = 0;
                foreach (int to in Edges[i])
                {
                    if (Edges[to].Count == 1)
                    {
                        cnt++;
                    }
                }

                if (cnt >= 2)
                {
                    max -= cnt - 1;
                }
            }
        }
        Console.WriteLine($"{min} {max}");
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