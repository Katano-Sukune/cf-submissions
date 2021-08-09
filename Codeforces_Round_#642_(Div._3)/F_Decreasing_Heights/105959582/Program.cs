using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    int N, M;
    long[][] A;
    void Q(Scanner sc)
    {
        N = sc.NextInt();
        M = sc.NextInt();
        A = new long[N][];
        for (int i = 0; i < N; i++)
        {
            A[i] = sc.LongArray();
        }

        // i,jにいる
        // 基準高さk,l
        // コスト最小
        var dp = new long[N, M][,];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                dp[i, j] = new long[i + 1, j + 1];
                for (int k = 0; k <= i; k++)
                {
                    for (int l = 0; l <= j; l++)
                    {
                        dp[i, j][k, l] = long.MaxValue;
                    }
                }
            }
        }

        dp[0, 0][0, 0] = 0;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                for (int k = 0; k <= i; k++)
                {
                    for (int l = 0; l <= j; l++)
                    {
                        long cur = dp[i, j][k, l];
                        if (cur == long.MaxValue) continue;
                        // 現在地の高さ
                        long h = A[k][l] + (i - k) + (j - l);
                        if (i + 1 < N)
                        {
                            // 下に移動
                            if (A[i + 1][j] >= h + 1)
                            {
                                ref long to = ref dp[i + 1, j][k, l];
                                long cost = cur + A[i + 1][j] - (h + 1);
                                to = Math.Min(to, cost);
                            }
                            else
                            {
                                ref long to = ref dp[i + 1, j][i + 1, j];
                                long cost = cur + (i + j + 1) * (h + 1 - A[i + 1][j]);
                                to = Math.Min(to, cost);
                            }
                        }

                        if (j + 1 < M)
                        {
                            if (A[i][j + 1] >= h + 1)
                            {
                                ref long to = ref dp[i, j + 1][k, l];
                                long cost = cur + A[i][j + 1] - (h + 1);
                                to = Math.Min(to, cost);
                            }
                            else
                            {
                                ref long to = ref dp[i, j + 1][i, j + 1];
                                long cost = cur + (i + j + 1) * (h + 1 - A[i][j + 1]);
                                to = Math.Min(to, cost);
                            }
                        }
                    }
                }
            }
        }

        long ans = long.MaxValue;
        for (int k = 0; k < N; k++)
        {
            for (int l = 0; l < M; l++)
            {
                ans = Math.Min(ans, dp[N - 1, M - 1][k, l]);
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
