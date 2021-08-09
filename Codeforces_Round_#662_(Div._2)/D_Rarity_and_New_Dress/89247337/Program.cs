using System;
using CompLib.Util;

public class Program
{
    private int N, M;
    private char[][] C;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        C = new char[N][];
        for (int i = 0; i < N; i++)
        {
            C[i] = sc.NextCharArray();
        }

        /*
         * 全部同じ文字
         * 斜め45度の正方形
         *
         * いくつあるか?
         */

        int[,] l = new int[N, M];
        int[,] r = new int[N, M];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                int k = M - j - 1;
                if (j == 0 || C[i][j - 1] != C[i][j])
                {
                    l[i, j] = 0;
                }
                else
                {
                    l[i, j] = l[i, j - 1] + 1;
                }

                if (j == 0 || C[i][k + 1] != C[i][k])
                {
                    r[i, k] = 0;
                }
                else
                {
                    r[i, k] = r[i, k + 1] + 1;
                }
            }
        }

        int[,] u = new int[N, M];
        int[,] d = new int[N, M];
        for (int i = 0; i < N; i++)
        {
            int k = N - i - 1;
            for (int j = 0; j < M; j++)
            {
                if (i == 0)
                {
                    u[i, j] = 1;
                }
                else
                {
                    // 左右の同色の個数
                    int lr = Math.Min(l[i, j], r[i, j]);
                    // 上同色何段できるか?
                    int uu = C[i - 1][j] == C[i][j] ? u[i - 1, j] : 0;
                    u[i, j] = Math.Min(lr, uu) + 1;
                }

                if (i == 0)
                {
                    d[k, j] = 1;
                }
                else
                {
                    // 左右の同色の個数
                    int lr = Math.Min(l[k, j], r[k, j]);
                    // 上同色何段できるか?
                    int dd = C[k + 1][j] == C[k][j] ? d[k + 1, j] : 0;
                    d[k, j] = Math.Min(lr, dd) + 1;
                }
            }
        }

        long ans = 0;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                // 中心にできるひし形最大
                int max = Math.Min(u[i, j], d[i, j]);
                ans += max;
            }
        }

        Console.WriteLine(ans);
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