using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, M, K, Q;
    private int[] X, Y, T;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        K = sc.NextInt();
        Q = sc.NextInt();
        X = new int[Q];
        Y = new int[Q];
        T = new int[Q];
        for (int i = 0; i < Q; i++)
        {
            X[i] = sc.NextInt() - 1;
            Y[i] = sc.NextInt() - 1;
            T[i] = sc.NextInt();
        }

        /*
         * n*mの行列
         * k*kの正方形が壊れたら壊れる
         * q個
         *
         * (x,y)がtに壊れる
         *
         * 壊れた時刻
         */

        int[,] time = new int[N, M];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                time[i, j] = int.MaxValue;
            }
        }

        for (int i = 0; i < Q; i++)
        {
            time[X[i], Y[i]] = T[i];
        }

        int[,] colMax = new int[N - K + 1, M];
        for (int i = 0; i <= N - K; i++)
        {
            for (int j = 0; j < M; j++)
            {
                colMax[i, j] = int.MinValue;
                for (int k = 0; k < K; k++)
                {
                    colMax[i, j] = Math.Max(colMax[i, j], time[i + k, j]);
                }
            }
        }

        int ans = int.MaxValue;
        for (int i = 0; i <= N - K; i++)
        {
            for (int j = 0; j <= M - K; j++)
            {
                int tmp = int.MinValue;
                for (int k = 0; k < K; k++)
                {
                    tmp = Math.Max(tmp, colMax[i, j + k]);
                }

                ans = Math.Min(ans, tmp);
            }
        }

        if (ans == int.MaxValue)
        {
            Console.WriteLine("-1");
            return;
        }
        
        Console.WriteLine(ans);
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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