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
#if !DEBUG
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
#endif
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        int a = sc.NextInt();
        int b = sc.NextInt();

        string s = sc.Next();
        var dp1 = new int[n + 1];
        for (int i = 1; i <= n; i++)
        {
            int max = int.MinValue;
            for (int j = 1; j <= i; j++)
            {
                max = Math.Max(max, dp1[i - j] + (a * j + b));
            }
            dp1[i] = max;
        }

        // i文字見た、保留
        var dp = new int[n + 1, n + 1];
        for (int i = 0; i <= n; i++)
        {
            for (int j = 0; j <= n; j++)
            {
                dp[i, j] = int.MinValue;
            }
        }
        dp[1, 1] = 0;

        for (int i = 1; i <= n; i++)
        {
            for (int j = i; j >= 0; j--)
            {
                if (dp[i, j] == int.MinValue) continue;
                if (j > 0)
                {
                    dp[i, 0] = Math.Max(dp[i, 0], dp[i, j] + dp1[j]);
                    if (i < n && s[i - 1] != s[i])
                    {
                        int tmp = -1;
                        for (int k = i; k < n; k++)
                        {
                            if (s[k] == s[i - 1])
                            {
                                tmp = k;
                                break;
                            }
                        }

                        // [i,tmp)を飛ばす
                        if (tmp != -1)
                        {
                            dp[tmp + 1, j + 1] = Math.Max(dp[tmp + 1, j + 1], dp[i, j] + dp1[tmp - i]);
                        }
                    }
                    if (i < n && s[i - 1] == s[i])
                    {
                        dp[i + 1, j + 1] = Math.Max(dp[i + 1, j + 1], dp[i, j]);
                    }
                }
                else
                {
                    if (i < n)
                    {
                        dp[i + 1, j + 1] = Math.Max(dp[i + 1, j + 1], dp[i, j]);
                    }
                }
            }
        }
        Console.WriteLine(dp[n, 0]);
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
