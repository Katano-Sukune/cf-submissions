using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, M, S, T;

    List<int>[] E;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        S = sc.NextInt() - 1;
        T = sc.NextInt() - 1;
        E = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<int>();
        }

        for (int i = 0; i < M; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            E[u].Add(v);
            E[v].Add(u);
        }

        /*
         * n頂点m無向辺
         * 自己辺　多重辺なし
         * 連結
         * 
         * 辺を1本追加
         * 
         * s-tの距離が減らないように
         * 
         */

        /*
         * s,tまでの距離で分類
         * 
         * a-bをつなぐ
         * (s,t) <= (s,a)+1+(b,t), (s,b)+1+(a,t)
         */

        int[] distS = C(S);
        int[] distT = C(T);

        int st = distS[T];
        int ans = 0;
        for (int a = 0; a < N; a++)
        {
            for (int b = a+1; b < N; b++)
            {
                
                if (st <= distS[a] + 1 + distT[b] && st <= distS[b] + 1 + distT[a])
                {
                    // Console.WriteLine($"{ a} {b}");
                    ans++;
                }

            }
        }

        ans -= M;
        Console.WriteLine(ans);

    }

    int[] C(int source)
    {
        var dist = new int[N];
        for (int i = 0; i < N; i++)
        {
            dist[i] = int.MaxValue;
        }

        dist[source] = 0;
        var q = new Queue<int>();
        q.Enqueue(source);
        while (q.Count > 0)
        {
            var d = q.Dequeue();
            foreach (var to in E[d])
            {
                if (dist[to] <= dist[d] + 1) continue;
                dist[to] = dist[d] + 1;
                q.Enqueue(to);
            }
        }

        return dist;
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
