using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int[] a = sc.IntArray();

        var dp = new bool[n, 5];
        for (int j = 0; j < 5; j++)
        {
            dp[0, j] = true;
        }

        for (int i = 1; i < n; i++)
        {
            if (a[i - 1] < a[i])
            {
                for (int j = 0; j < 5; j++)
                {
                    if (!dp[i - 1, j]) continue;
                    for (int k = j + 1; k < 5; k++)
                    {
                        dp[i, k] = true;
                    }
                }
            }
            else if (a[i - 1] > a[i])
            {
                for (int j = 5 - 1; j >= 0; j--)
                {
                    if (!dp[i - 1, j]) continue;
                    for (int k = 0; k < j; k++)
                    {
                        dp[i, k] = true;
                    }

                    break;
                }
            }
            else
            {
                for (int j = 0; j < 5; j++)
                {
                    if (!dp[i - 1, j]) continue;
                    for (int k = 0; k < 5; k++)
                    {
                        if (j == k) continue;
                        dp[i, k] = true;
                    }
                }
            }
        }

        int p = -1;
        for (int i = 0; i < 5; i++)
        {
            if (dp[n - 1, i])
            {
                p = i;
                break;
            }
        }

        if (p == -1)
        {
            Console.WriteLine("-1");
            return;
        }

        int[] ans = new int[n];
        ans[n - 1] = p + 1;
        for (int i = n - 2; i >= 0; i--)
        {
            if (a[i] < a[i + 1])
            {
                for (int j = 0; j < p; j++)
                {
                    if (dp[i, j])
                    {
                        ans[i] = j + 1;
                        p = j;
                        break;
                    }
                }
            }
            else if (a[i] > a[i + 1])
            {
                for (int j = p + 1; j < 5; j++)
                {
                    if (dp[i, j])
                    {
                        ans[i] = j + 1;
                        p = j;
                        break;
                    }
                }
            }
            else
            {
                for (int j = 0; j < 5; j++)
                {
                    if (p != j && dp[i, j])
                    {
                        ans[i] = j + 1;
                        p = j;
                        break;
                    }
                }
            }
        }

        Console.WriteLine(string.Join(" ", ans));
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