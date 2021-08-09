using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CompLib.Util;

public class Program
{
    int N;
    int[] A;
    List<int>[] E;

    // 部分木のコスト
    long[] Cost;

    // 部分木のAの総和
    long[] Sum;

    long Ans;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();

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

        /*
         * n頂点 木
         * 
         * 頂点vには a_v
         * 
         * dist(x,y) パス(x,y)の長さ
         * 
         * 木のコスト Σ dist(i,v) * a_i
         * 
         * 任意にvを選ぶ コスト最大化
         */

        Cost = new long[N];
        Sum = new long[N];
        Go(0, -1);

        Ans = long.MinValue;

        DP(0, -1, 0, 0);

        Console.WriteLine(Ans);
    }

    void Go(int cur, int par)
    {
        Sum[cur] = A[cur];
        foreach (var to in E[cur])
        {
            if (to == par) continue;
            Go(to, cur);
            Sum[cur] += Sum[to];
            Cost[cur] += Cost[to] + Sum[to];
        }
    }

    // 
    void DP(int cur, int par, long pSum, long pCost)
    {
        // 現在地のコスト
        Ans = Math.Max(Ans, Cost[cur] + pCost);

        // Console.WriteLine($"{cur} {pSum} {pCost} {Cost[cur]}");

        foreach (var to in E[cur])
        {
            if (to == par) continue;
            // toに進む
            long nextPSum = pSum + Sum[cur] - Sum[to];
            long nextPCost = pCost + pSum + Cost[cur] - Cost[to] + Sum[cur] - 2 * Sum[to];
            DP(to, cur, nextPSum, nextPCost);
        }


    }

    // public static void Main(string[] args) => new Program().Solve();
    public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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
            if (_index >= _line.Length)
            {
                string s;
                do
                {
                    s = Console.ReadLine();
                } while (s.Length == 0);

                _line = s.Split(Separator);
                _index = 0;
            }

            return _line[_index++];
        }

        public string ReadLine()
        {
            _index = _line.Length;
            return Console.ReadLine();
        }

        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public decimal NextDecimal() => decimal.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            string s = Console.ReadLine();
            _line = s.Length == 0 ? new string[0] : s.Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
