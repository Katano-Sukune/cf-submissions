using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;

public class Program
{
    int N;
    int T;
    int[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        T = sc.NextInt();
        A = sc.IntArray();

        var dp = new int[N + 1, 301, 301];
        for (int i = 0; i <= N; i++)
        {
            for (int j = 0; j <= 300; j++)
            {
                for (int k = 0; k <= 300; k++)
                {
                    dp[i, j, k] = int.MinValue;
                }
            }
        }
        for (int i = 0; i <= 300; i++)
        {
            dp[0, i, i] = 0;
        }

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j <= 300; j++)
            {
                for (int k = j; k <= 300; k++)
                {
                    int cur = dp[i, j, k];
                    if (cur == int.MinValue) continue;
                    dp[i + 1, j, k] = Math.Max(dp[i + 1, j, k], cur);
                    if (k <= A[i]) dp[i + 1, j, A[i]] = Math.Max(dp[i + 1, j, A[i]], cur + 1);
                }
            }
        }

        int[] a = new int[301];
        for (int i = 0; i <= 300; i++)
        {
            a[i] = int.MinValue;
        }

        a[0] = 0;
        int[,] b = new int[301, 301];
        for (int j = 0; j <= 300; j++)
        {
            for (int k = 0; k <= 300; k++)
            {
                b[j, k] = dp[N, j, k];
            }
        }

        while (T > 0)
        {
            if (T % 2 == 1)
            {
                var tmpA = new int[301];
                Array.Fill(tmpA, int.MinValue);
                for (int j = 0; j <= 300; j++)
                {
                    if (a[j] == int.MinValue) continue;
                    for (int k = j; k <= 300; k++)
                    {
                        if (b[j, k] == int.MinValue) continue;
                        tmpA[k] = Math.Max(tmpA[k], a[j] + b[j, k]);
                    }
                }
                a = tmpA;
            }


            int[,] tmpB = new int[301, 301];
            for (int i = 0; i <= 300; i++)
            {
                for (int j = 0; j <= 300; j++)
                {
                    tmpB[i, j] = int.MinValue;
                }
            }

            for (int i = 0; i <= 300; i++)
            {
                for (int j = i; j <= 300; j++)
                {
                    if (b[i, j] == int.MinValue) continue;
                    for (int k = j; k <= 300; k++)
                    {
                        if (b[j, k] == int.MinValue) continue;
                        tmpB[i, k] = Math.Max(tmpB[i, k], b[i, j] + b[j, k]);
                    }
                }
            }
            b = tmpB;

            T /= 2;
        }

        Console.WriteLine(a.Max());
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
