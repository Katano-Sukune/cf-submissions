using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.Collections.Generic;
using System.Text;

public class Program
{
    int N, M, K;
    int[] A, B;

    List<int>[] E, L;
    int T;
    StringBuilder Sb;
    int[] Ar;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        K = sc.NextInt();

        A = new int[M];
        B = new int[M];
        for (int i = 0; i < M; i++)
        {
            A[i] = sc.NextInt() - 1;
            B[i] = sc.NextInt() - 1;
        }

        /*
         * N個都市
         * 
         * M本道路候補
         * 
         * N-1本つなぐ
         * 
         * 1から各都市への距離の総和が最小になるつなげ方
         * 
         * K個
         * 足りないならすべて
         */

        // BFSする
        // 各距離iの頂点からi-1へのパス 積が

        E = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<int>();
        }

        for (int i = 0; i < M; i++)
        {
            E[A[i]].Add(B[i]);
            E[B[i]].Add(A[i]);
        }

        int[] dist = new int[N];
        for (int i = 0; i < N; i++)
        {
            dist[i] = int.MaxValue;
        }


        int[] queue = new int[N];
        int begin = 0;
        int end = 0;
        queue[end++] = 0;
        dist[0] = 0;
        while (end > begin)
        {
            int d = queue[begin++];
            foreach (var to in E[d])
            {
                if (dist[to] == int.MaxValue)
                {
                    dist[to] = dist[d] + 1;
                    queue[end++] = to;
                }
            }
        }

        L = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            L[i] = new List<int>();
        }

        for (int i = 0; i < M; i++)
        {
            if (dist[A[i]] + 1 == dist[B[i]])
            {
                L[B[i]].Add(i);
            }
            if (dist[B[i]] + 1 == dist[A[i]])
            {
                L[A[i]].Add(i);
            }
        }

        T = 0;
        Sb = new StringBuilder();
        Ar = new int[M];
        Go(1);
        Console.WriteLine(T);
        Console.Write(Sb);
    }

    void Go(int i)
    {
        if (i >= N)
        {
            Sb.AppendLine(string.Join("", Ar));
            T++;
            return;
        }
        foreach (var e in L[i])
        {
            if (T >= K) break;
            Ar[e] = 1;
            Go(i + 1);
            Ar[e] = 0;
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
