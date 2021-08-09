using System;
using System.Collections;
using System.Collections.Generic;
using CompLib.Util;

public class Program
{
    private int N, M, K;

    private List<(int to, int w)>[] E;

    private bool[][] F1;
    private bool[][][][] F2;

    private int[] C;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        K = sc.NextInt();

        E = new List<(int to, int w)>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<(int to, int w)>();
        }

        for (int i = 0; i < M; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            int w = sc.NextInt();
            E[u].Add((v, w));
        }

        // (i, c_i)は使えない 2つある頂点がある
        F1 = new bool[K + 1][];

        // (i, c_iを使うと) (i', c'_i)は使えない 
        F2 = new bool[K + 1][][][];
        for (int i = 1; i <= K; i++)
        {
            F1[i] = new bool[i];
            F2[i] = new bool[i][][];
            for (int j = 0; j < i; j++)
            {
                F2[i][j] = new bool[K + 1][];
                for (int k = 1; k <= K; k++)
                {
                    F2[i][j][k] = new bool[k];
                }
            }
        }

        var ls = new HashSet<(int i, int ci)>[N];
        for (int i = 0; i < N; i++)
        {
            ls[i] = new HashSet<(int i, int ci)>();
        }

        for (int i = 0; i < N; i++)
        {
            E[i].Sort((l, r) => l.w.CompareTo(r.w));
            for (int j = 0; j < E[i].Count; j++)
            {
                int to = E[i][j].to;
                if (ls[to].Add((E[i].Count, j)))
                {
                    foreach (var t in ls[to])
                    {
                        F2[E[i].Count][j][t.i][t.ci] = true;
                        F2[t.i][t.ci][E[i].Count][j] = true;
                    }
                }
                else
                {
                    F1[E[i].Count][j] = true;
                }
            }
        }

        C = new int[K + 1];

        Console.WriteLine(Go(K));
    }

    int Go(int i)
    {
        if (i == 0)
        {
            return 1;
        }

        int res = 0;
        for (int ci = 0; ci < i; ci++)
        {
            if (F1[i][ci]) continue;
            bool f = true;
            for (int i2 = i + 1; i2 <= K && f; i2++)
            {
                f &= !F2[i][ci][i2][C[i2]];
            }

            if (!f) continue;
            C[i] = ci;
            res += Go(i - 1);
        }

        return res;
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