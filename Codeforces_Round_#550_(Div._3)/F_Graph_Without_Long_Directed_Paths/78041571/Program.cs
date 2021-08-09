using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, M;
    List<int>[] Edges;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();

        Edges = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            Edges[i] = new List<int>();
        }
        int[] U = new int[M];
        int[] V = new int[M];
        for (int i = 0; i < M; i++)
        {
            U[i] = sc.NextInt() - 1;
            V[i] = sc.NextInt() - 1;
            Edges[U[i]].Add(V[i]);
            Edges[V[i]].Add(U[i]);
        }

        // 2部グラフか?

        var q = new Queue<int>();
        int[] f = new int[N];
        q.Enqueue(0);
        f[0] = 1;
        while (q.Count > 0)
        {
            var d = q.Dequeue();
            foreach (var to in Edges[d])
            {
                if (f[to] == 0)
                {
                    q.Enqueue(to);
                    f[to] = f[d] * -1;
                }
                else
                {
                    if (f[to] == f[d])
                    {
                        Console.WriteLine("NO");
                        return;
                    }
                }
            }
        }

        char[] ans = new char[M];
        for (int i = 0; i < M; i++)
        {
            // U <- Vなら1
            // U -> Vなら0

            ans[i] = f[U[i]] == 1 ? '1' : '0';
        }
        Console.WriteLine("YES");
        Console.WriteLine(new string(ans));
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
