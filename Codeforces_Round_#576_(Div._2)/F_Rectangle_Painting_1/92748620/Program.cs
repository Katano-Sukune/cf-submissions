using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
#if DEBUG
        int n = 50;
        var s = new string[n];
        for (int i = 0; i < n; i++)
        {
            s[i] = new string('#', n);
        }
#else
        int n = sc.NextInt();
        string[] s = new string[n];
        for (int i = 0; i < n; i++)
        {
            s[i] = sc.Next();
        }
#endif
        const int inf = 100;
        var dp = new Array4D(n + 1, n + 1, n + 1, n + 1);
        for (int i = 0; i <= n; i++)
        {
            for (int j = 0; j <= n; j++)
            {
                for (int k = 0; k <= n; k++)
                {
                    for (int l = 0; l <= n; l++)
                    {
                        dp[i, j, k, l] = inf;
                    }
                }
            }
        }

        // int cnt = 0;
        for (int h = 1; h <= n; h++)
        {
            for (int w = 1; w <= n; w++)
            {
                for (int i = 0; i + h <= n; i++)
                {
                    for (int j = 0; j + w <= n; j++)
                    {
                        if (h == 1 && w == 1)
                        {
                            dp[i, j, i + 1, j + 1] = s[i][j] == '#' ? 1 : 0;
                        }
                        else
                        {
                            dp[i, j, i + h, j + w] = Math.Max(h, w);
                            for (int h2 = 1; h2 < h; h2++)
                            {
                                dp[i, j, i + h, j + w] = Math.Min(dp[i, j, i + h, j + w], dp[i, j, i + h2, j + w] + dp[i + h2, j, i + h, j + w]);
                                // cnt++;
                            }

                            for (int w2 = 1; w2 < w; w2++)
                            {
                                dp[i, j, i + h, j + w] = Math.Min(dp[i, j, i + h, j + w], dp[i, j, i + h, j + w2] + dp[i, j + w2, i + h, j + w]);
                                // cnt++;
                            }
                        }
                    }
                }
            }
            // Console.WriteLine(cnt);
        }
        // Console.WriteLine(cnt);
        Console.WriteLine(dp[0, 0, n, n]);
    }


    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

class Array4D
{
    public int[] T;
    int A, B, C, D;
    public Array4D(int a, int b, int c, int d)
    {
        (A, B, C, D) = (a, b, c, d);
        T = new int[A * B * C * D];
    }

    public int this[int i, int j, int k, int l]
    {
        set
        {
            T[i * B * C * D + j * C * D + k * D + l] = value;
        }
        get
        {
            return T[i * B * C * D + j * C * D + k * D + l];
        }
    }
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
