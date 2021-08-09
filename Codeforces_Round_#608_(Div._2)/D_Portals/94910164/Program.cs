using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, M, K;
    private int[] A, B, C;
    private int[] U, V;

    public void Solve()
    {
        checked
        {
            var sc = new Scanner();
            N = sc.NextInt();
            M = sc.NextInt();
            K = sc.NextInt();

            A = new int[N];
            B = new int[N];
            C = new int[N];
            for (int i = 0; i < N; i++)
            {
                A[i] = sc.NextInt();
                B[i] = sc.NextInt();
                C[i] = sc.NextInt();
            }

            U = new int[M];
            V = new int[M];
            for (int i = 0; i < M; i++)
            {
                U[i] = sc.NextInt() - 1;
                V[i] = sc.NextInt() - 1;
            }

            List<int>[] tmpE = new List<int>[N];
            for (int i = 0; i < N; i++)
            {
                tmpE[i] = new List<int>();
                tmpE[i].Add(i);
            }

            for (int i = 0; i < M; i++)
            {
                tmpE[U[i]].Add(V[i]);
            }

            // 送れる一番後ろの城から
            var e = new List<int>[N];
            for (int i = 0; i < N; i++)
            {
                e[i] = new List<int>();
            }

            var flag = new bool[N];
            for (int u = N - 1; u >= 0; u--)
            {
                foreach (var v in tmpE[u])
                {
                    if (!flag[v]) e[u].Add(v);
                    flag[v] = true;
                }

                e[u].Sort((l, r) => C[r].CompareTo(C[l]));
            }
            /*
             * n個城
             *
             * k人
             * iをたおす A_iいる
             *
             * B_i 新規加入
             *
             * 1人残して防衛
             *
             * 防衛した城 C_iスコア
             *
             * iにいるときに残す
             *
             * 城 u_jにいるとき、城 v_jに1人送れる
             *
             * 
             */

            // iまで見る、j個防衛
            var dp = new long[N + 1, N + 1];
            for (int i = 0; i <= N; i++)
            {
                for (int j = 0; j <= N; j++)
                {
                    dp[i, j] = long.MinValue;
                }
            }

            dp[0, 0] = 0;

            long tmpK = K;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j <= i && tmpK - j >= A[i]; j++)
                {
                    if (dp[i, j] == long.MinValue) continue;

                    // 倒せる
                    // 残さない
                    dp[i + 1, j] = Math.Max(dp[i + 1, j], dp[i, j]);

                    long s = 0;
                    for (int k = 1; k <= e[i].Count && tmpK + B[i] - (j + k) >= 0; k++)
                    {
                        s += C[e[i][k - 1]];
                        dp[i + 1, j + k] = Math.Max(dp[i + 1, j + k], dp[i, j] + s);
                    }
                }

                tmpK += B[i];
            }

            long ans = -1;
            for (int i = 0; i <= N && tmpK - i >= 0; i++)
            {
                ans = Math.Max(ans, dp[N, i]);
            }

            Console.WriteLine(ans);
        }
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