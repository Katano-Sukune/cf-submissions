using System;
using System.Linq;
using System.Text;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private int[] A;
    private int M;
    private int[] K, Pos;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();
        M = sc.NextInt();
        K = new int[M];
        Pos = new int[M];
        for (int i = 0; i < M; i++)
        {
            K[i] = sc.NextInt();
            Pos[i] = sc.NextInt();
        }

        /*
         * 配列Aがある
         * M個クエリ
         * 長さKの部分列 総和最大、辞書順最小
         * Pos番目
         */

        // iとそれ以降選ぶ、j個選ぶ (総和最大、前に選んだやつ)
        (long sum, int pi)[,] dp = new (long sum, int pi)[N + 1, N + 1];
        for (int i = 0; i <= N; i++)
        {
            for (int j = 0; j <= N; j++)
            {
                dp[i, j] = (long.MinValue, -1);
            }
        }

        dp[N, 0] = (0, 0);
        for (int i = N - 1; i >= 0; i--)
        {
            for (int j = 1; j <= N - i; j++)
            {
                ref var cur = ref dp[i, j];
                for (int k = i + 1; k <= N; k++)
                {
                    (long sum, int pi) = dp[k, j - 1];
                    if (sum == long.MinValue) continue;
                    if (sum + A[i] > cur.sum)
                    {
                        cur = (sum + A[i], k);
                    }
                    else if (sum + A[i] == cur.sum && A[k] < A[cur.pi])
                    {
                        cur = (sum + A[i], k);
                    }
                }
            }
        }

        int[] f = new int[N + 1];
        for (int i = 1; i <= N; i++)
        {
            for (int j = 1; j + i <= N; j++)
            {
                if (dp[j, i].sum > dp[f[i], i].sum)
                {
                    f[i] = j;
                }
                else if (dp[j, i].sum == dp[f[i], i].sum && A[j] < A[f[i]])
                {
                    f[i] = j;
                }
            }
        }

        var sb = new StringBuilder();
        for (int i = 0; i < M; i++)
        {
            int cur = f[K[i]];
            for (int j = 0; j < Pos[i] - 1; j++)
            {
                cur = dp[cur, K[i] - j].pi;
            }

            sb.AppendLine($"{A[cur]}");
        }

        Console.Write(sb);
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