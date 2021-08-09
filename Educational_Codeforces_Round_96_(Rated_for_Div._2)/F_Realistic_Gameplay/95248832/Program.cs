using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private int K;
    private int[] L, R, A;

    public void Solve()
    {
        checked
        {
            var sc = new Scanner();
            N = sc.NextInt();
            K = sc.NextInt();
            L = new int[N];
            R = new int[N];
            A = new int[N];
            for (int i = 0; i < N; i++)
            {
                L[i] = sc.NextInt();
                R[i] = sc.NextInt();
                A[i] = sc.NextInt();
            }

            /*
             * マガジンサイズ K
             *
             * Nウェーブ
             *
             * ウェーブi
             * A_iモンスター
             * [L_i, R_i]
             *
             * L_iに湧いて R_iまでに倒す
             *
             * 1体倒す 1発
             *
             * リロード 1単位時間
             *
             * 
             */

            // iターン目まで見る , 最後に無駄リロードした

            // 残弾、消費弾
            var dp = new (long m, long c)[N + 1, N + 1];
            for (int i = 0; i <= N; i++)
            {
                for (int j = 0; j <= N; j++)
                {
                    dp[i, j] = (0, long.MaxValue);
                }
            }

            dp[0, 0] = (K, 0);

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    if (dp[i, j] == (0, long.MaxValue)) continue;
                    (long m, long c) = dp[i, j];
                    if (A[i] <= m)
                    {
                        // リロードしない
                        dp[i + 1, j] = (m - A[i], c + A[i]);

                        // する
                        if (i + 1 < N && L[i] < L[i + 1])
                        {
                            long cost = dp[i + 1, j].c + dp[i + 1, j].m;
                            if (cost < dp[i + 1, i + 1].c)
                            {
                                dp[i + 1, i + 1] = (K, cost);
                            }
                        }
                    }
                    else
                    {
                        long t = A[i] - m;

                        // リロード回数
                        long d = (t + K - 1) / K;
                        if (d <= R[i] - L[i])
                        {
                            dp[i + 1, j] = (d * K - t, c + A[i]);
                            if (i + 1 < N && L[i] + d < L[i + 1])
                            {
                                long cost =   dp[i + 1, j].c + dp[i + 1, j].m;
                                if (cost < dp[i + 1, i + 1].c)
                                {
                                    dp[i + 1, i + 1] = (K, cost);
                                }
                            }
                        }
                    }
                }
            }

            long ans = long.MaxValue;
            for (int i = 0; i <= N; i++)
            {
                ans = Math.Min(ans, dp[N, i].c);
            }

            if (ans == long.MaxValue)
            {
                Console.WriteLine("-1");
                return;
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