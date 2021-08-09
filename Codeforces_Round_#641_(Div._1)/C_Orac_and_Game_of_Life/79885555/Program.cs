using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, M, T;
    string[] S;
    int[] I, J;
    long[] P;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        T = sc.NextInt();
        S = new string[N];
        for (int i = 0; i < N; i++)
        {
            S[i] = sc.Next();
        }

        I = new int[T];
        J = new int[T];
        P = new long[T];
        for (int i = 0; i < T; i++)
        {
            I[i] = sc.NextInt() - 1;
            J[i] = sc.NextInt() - 1;
            P[i] = sc.NextLong();
        }
        // 同じ色の隣り合っているマスがある
        // 次色変わる
        // else 変わらない

        // 最初色変わるやつ 交互 
        // 変わらないやつ 一番近い変わるやつ... 交互
        int[] dx = new int[] { 1, 0, -1, 0 };
        int[] dy = new int[] { 0, 1, 0, -1 };
        long[,] d = new long[N, M];
        var yq = new Queue<int>();
        var xq = new Queue<int>();
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {

                bool f = false;
                for (int k = 0; k < 4; k++)
                {
                    int yy = i + dy[k];
                    int xx = j + dx[k];
                    if (yy < 0 || yy >= N) continue;
                    if (xx < 0 || xx >= M) continue;
                    f |= S[yy][xx] == S[i][j];
                }
                if (f)
                {
                    yq.Enqueue(i);
                    xq.Enqueue(j);
                    d[i, j] = 0;
                }
                else
                {
                    d[i, j] = long.MaxValue;
                }
            }
        }

        while (yq.Count > 0)
        {
            int yy = yq.Dequeue();
            int xx = xq.Dequeue();
            for (int k = 0; k < 4; k++)
            {
                int ny = yy + dy[k];
                int nx = xx + dx[k];
                if (ny < 0 || ny >= N) continue;
                if (nx < 0 || nx >= M) continue;
                if (d[ny, nx] != long.MaxValue) continue;
                d[ny, nx] = d[yy, xx] + 1;
                yq.Enqueue(ny);
                xq.Enqueue(nx);
            }
        }

        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < T; i++)
        {
            int ii = S[I[i]][J[i]] - '0';
            if (P[i] >= d[I[i], J[i]])
            {
                Console.WriteLine((ii + (P[i] - d[I[i], J[i]]) % 2) % 2);
            }
            else
            {
                Console.WriteLine(ii);
            }
        }
        Console.Out.Flush();
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
