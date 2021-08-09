using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, M;

    List<int>[] E;
    List<int>[] Rev;

    int K;
    int[] P;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        E = new List<int>[N];
        Rev = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<int>();
            Rev[i] = new List<int>();
        }

        for (int i = 0; i < M; i++)
        {
            int s = sc.NextInt() - 1;
            int t = sc.NextInt() - 1;
            Rev[t].Add(s);
            E[s].Add(t);
        }

        K = sc.NextInt();
        P = sc.IntArray().Select(i => i - 1).ToArray();

        // P_k-1からの最短距離

        var dist = new int[N].Select(i => int.MaxValue).ToArray();
        dist[P[K - 1]] = 0;
        var q = new Queue<int>();
        q.Enqueue(P[K - 1]);

        while (q.Count > 0)
        {
            var deq = q.Dequeue();
            foreach (var from in Rev[deq])
            {
                if (dist[from] != int.MaxValue) continue;
                dist[from] = dist[deq] + 1;
                q.Enqueue(from);
            }
        }


        // K個の交差点 p_0....p_{k-1}の順で通る

        // カーナビは予め用意した最短経路以外だとルートを再構築する

        // 再構築、最小、最大

        int min = 0;
        int max = 0;
        for (int i = 0; i < K - 1; i++)
        {
            // minへの移動か?

            // 最短経路が2つあるか?
            int mn = int.MaxValue;
            int cnt = 1;

            foreach (int to in E[P[i]])
            {
                if (dist[to] < mn)
                {
                    mn = dist[to];
                    cnt = 1;
                }
                else if (dist[to] == mn)
                {
                    cnt++;
                }
            }

            if (dist[P[i + 1]] != mn)
            {
                min++;
                max++;
            }
            else
            {
                if (cnt >= 2)
                {
                    max++;
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
