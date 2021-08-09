using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.Collections.Generic;

public class Program
{
    int A, N, M;
    int[] L, R;

    int[] X;
    long[] P;
    public void Solve()
    {
        var sc = new Scanner();
        A = sc.NextInt();
        N = sc.NextInt();
        M = sc.NextInt();

        L = new int[N];
        R = new int[N];
        for (int i = 0; i < N; i++)
        {
            L[i] = sc.NextInt();
            R[i] = sc.NextInt();
        }

        X = new int[M];
        P = new long[M];
        for (int i = 0; i < M; i++)
        {
            X[i] = sc.NextInt();
            P[i] = sc.NextInt();
        }

        int[] u = new int[A + 1];
        for (int i = 0; i <= A; i++)
        {
            u[i] = -1;
        }

        for (int i = 0; i < M; i++)
        {
            if (u[X[i]] == -1)
            {
                u[X[i]] = i;
            }
            else
            {
                if (P[i] < P[u[X[i]]])
                {
                    u[X[i]] = i;
                }
            }
        }

        int[] imos = new int[A + 1];
        for (int i = 0; i < N; i++)
        {
            imos[L[i]]++;
            imos[R[i]]--;
        }

        for (int i = 0; i < A; i++)
        {
            imos[i + 1] += imos[i];
        }

        /*
         * x = 0にいる
         * 
         * Aに行く
         * 
         * 1s +1移動できる
         * 
         * [l,r]は雨
         * 
         * M本傘
         * Xに重さP
         * 
         * 
         */

        // 現在地、持ってる傘
        var dp = new long[A + 1, M + 1];
        for (int i = 0; i <= A; i++)
        {
            for (int j = 0; j <= M; j++)
            {
                dp[i, j] = long.MaxValue;
            }
        }

        dp[0, M] = 0;

        for (int i = 0; i < A; i++)
        {
            // 今持ってる傘
            for (int j = 0; j < M; j++)
            {
                if (dp[i, j] == long.MaxValue) continue;
                dp[i + 1, j] = Math.Min(dp[i + 1, j], dp[i, j] + P[j]);
            }

            // 傘なし
            if (imos[i] == 0)
            {
                for (int j = 0; j <= M; j++)
                {
                    if (dp[i, j] == long.MaxValue) continue;
                    dp[i + 1, M] = Math.Min(dp[i + 1, M], dp[i, j]);
                }
            }

            // 持ち替え
            for (int j = 0; j <= M; j++)
            {
                if (dp[i, j] == long.MaxValue) continue;
                if (u[i] == -1) continue;
                dp[i + 1, u[i]] = Math.Min(dp[i + 1, u[i]], dp[i, j] + P[u[i]]);
            }
        }

        long ans = long.MaxValue;
        for (int i = 0; i <= M; i++)
        {
            ans = Math.Min(ans, dp[A, i]);
        }

        if (ans == long.MaxValue)
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
