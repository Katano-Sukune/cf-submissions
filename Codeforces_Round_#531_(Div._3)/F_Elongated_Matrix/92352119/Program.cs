using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, M;
    int[][] A;
    private List<(int next, int min)>[] L1;

    private int[,,] dp;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        A = new int[N][];
        for (int i = 0; i < N; i++)
        {
            A[i] = sc.IntArray();
        }

        if (N == 1)
        {
            int t = int.MaxValue;
            for (int i = 0; i < M - 1; i++)
            {
                t = Math.Min(t, Math.Abs(A[0][i + 1] - A[0][i]));
            }

            Console.WriteLine(t);
            return;
        }

        /*
         * 行列a
         * 
         * 行を並び替える
         * 
         * 最初の列を上から下まで走査
         * 
         * 2番目...
         * 
         * 順に書いていく
         * 
         * |s_i - s_{i+1}| >= kなら k-acceptable
         * 
         * 最大k
         */

        // 次に置ける列
        L1 = new List<(int next, int min)>[N];

        for (int i = 0; i < N; i++)
        {
            L1[i] = new List<(int next, int min)>();
            for (int j = 0; j < N; j++)
            {
                if (i == j) continue;
                int min = int.MaxValue;
                for (int l = 0; l < M; l++)
                {
                    min = Math.Min(min, Math.Abs(A[i][l] - A[j][l]));
                }
                L1[i].Add((j, min));
            }
            L1[i].Sort((l, r) => r.min.CompareTo(l.min));
        }

        int len = 1 << N;
        // 使ったところ、一番下,一番上のとき、k最小の最大
        dp = new int[len, N, N];
        for (int i = 0; i < len; i++)
        {
            for (int j = 0; j < N; j++)
            {
                for (int k = 0; k < N; k++)
                {
                    dp[i, j, k] = int.MinValue;
                }
            }
        }

        for (int i = 0; i < N; i++)
        {
            dp[(1 << i), i, i] = int.MaxValue;
        }

        for (int i = 0; i < len; i++)
        {
            for (int j = 0; j < N; j++)
            {
                for (int k = 0; k < N; k++)
                {
                    if (dp[i, j, k] == int.MinValue) continue;
                    foreach (var t in L1[j])
                    {
                        int b = 1 << (t.next);
                        if ((i & b) > 0) continue;
                        dp[i | b, t.next, k] = Math.Max(dp[i | b, t.next, k], Math.Min(dp[i, j, k], t.min));
                    }
                }
            }
        }

        int ans = int.MinValue;
        for (int j = 0; j < N; j++)
        {
            for (int k = 0; k < N; k++)
            {
                if (j == k) continue;
                int min = dp[len - 1, j, k];
                for (int l = 0; l < M - 1; l++)
                {
                    min = Math.Min(min, Math.Abs(A[j][l] - A[k][l + 1]));
                }
                ans = Math.Max(ans, min);
            }
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
