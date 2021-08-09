using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    private int N;
    private int[,] S;
    private int M;
    private int[] P;

    public void Solve()
    {
        var sc = new Scanner();

        N = sc.NextInt();
        S = new int[N, N];
        for (int i = 0; i < N; i++)
        {
            var ln = sc.Next();
            for (int j = 0; j < N; j++)
            {
                S[i, j] = ln[j] == '1' ? 1 : int.MaxValue / 2;
            }

            S[i, i] = 0;
        }


        for (int k = 0; k < N; k++)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    S[i, j] = Math.Min(S[i, j], S[i, k] + S[k, j]);
                }
            }
        }

        M = sc.NextInt();
        P = sc.IntArray().Select(i => i - 1).ToArray();

        // Sのi行j列が1なら i->jに辺がある

        // M個の頂点のパスがある
        // Pの部分配列 V
        // PがVを通る最短パスの1つならVは良い

        // 最短の良いV


        // iまで見る
        List<int> v = new List<int>();
        v.Add(P[0]);
        for (int i = 2; i < M; i++)
        {
            if (S[v[v.Count - 1], P[i]] != S[v[v.Count - 1], P[i - 1]] + S[P[i - 1], P[i]])
            {
                v.Add(P[i - 1]);
            }
        }

        v.Add(P[M - 1]);

        Console.WriteLine(v.Count);
        Console.WriteLine(string.Join(" ", v.Select(i => i + 1)));
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