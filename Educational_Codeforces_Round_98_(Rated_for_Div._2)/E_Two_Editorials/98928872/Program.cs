using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, M, K;
    private int[] L, R;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        K = sc.NextInt();
        L = new int[M];
        R = new int[M];
        for (int i = 0; i < M; i++)
        {
            L[i] = sc.NextInt() - 1;
            R[i] = sc.NextInt() - 1;
        }

        /*
         * 1~m
         * m人
         *
         * 1~n
         * n問
         *
         * 人i
         * [l,r]の解説聞きたい
         *
         * 2人で解説
         * 連続k問
         * 重なってても良い
         *
         * 聞きたい解説を多くする方だけ聞く
         * 
         */

        int[,] imos = new int[N + 1, N + 1];
        for (int i = 0; i < M; i++)
        {
            for (int j = 1; j <= K && j <= R[i] - L[i] + 1; j++)
            {
                // j個重なる長方形
                // 右上
                int mn = Math.Max(0, L[i] - (K - j));
                int mx = Math.Min(N - 1, R[i] - (j - 1));
                imos[0, mn]++;
                imos[0, mx + 1]--;
                imos[N, mn]--;
                imos[N, mx + 1]++;

                imos[mn, 0]++;
                imos[mx + 1, 0]--;
                imos[mn, N]--;
                imos[mx + 1, N]++;
                imos[mx + 1, N]++;

                imos[mn, mn]--;
                imos[mn, mx + 1]++;
                imos[mx + 1, mn]++;
                imos[mx + 1, mx + 1]--;
            }
        }

        for (int i = 0; i <= N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                imos[i, j + 1] += imos[i, j];
            }
        }

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j <= N; j++)
            {
                imos[i + 1, j] += imos[i, j];
            }
        }

        long ans = 0;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                // Console.Write($"{imos[i, j]} ");
                ans = Math.Max(ans, imos[i, j]);
            }

            // Console.WriteLine();
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