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
        string s = sc.Next();
        int n = sc.NextInt();

        int l = s.Length;
        var dp = new bool[l + 1, n + 1];
        dp[0, 0] = true;
        for (int i = 0; i < l;)
        {
            if (i + 1 < l && s[i + 1] == '?')
            {
                for (int j = 0; j <= n; j++)
                {
                    if (!dp[i, j]) continue;
                    dp[i + 2, j] = true;
                    if (j + 1 <= n) dp[i + 2, j + 1] = true;
                }
                i += 2;
            }
            else if (i + 1 < l && s[i + 1] == '*')
            {
                for (int j = 0; j <= n; j++)
                {
                    if (!dp[i, j]) continue;
                    for (int k = j; k <= n; k++) dp[i + 2, k] = true;
                    break;
                }
                i += 2;
            }
            else
            {
                for (int j = 0; j <= n; j++)
                {
                    if (!dp[i, j]) continue;
                    if (j + 1 <= n) dp[i + 1, j + 1] = true;
                }
                i++;
            }
        }

        if (!dp[l, n])
        {
            Console.WriteLine("Impossible");
            return;
        }

        char[] ans = new char[n];
        int ptr = n;
        for (int i = l - 1; i >= 0;)
        {
            if (s[i] == '?')
            {
                if (ptr > 0 && dp[i - 1, ptr - 1])
                {
                    ans[--ptr] = s[i - 1];
                }
                i -= 2;
            }
            else if (s[i] == '*')
            {
                int j;
                for (j = ptr; j >= 0; j--)
                {
                    if (dp[i - 1, j]) break;
                }
                for (int k = j; k < ptr; k++) ans[k] = s[i - 1];
                ptr = j;
                i -= 2;
            }
            else
            {
                ans[--ptr] = s[i];
                i--;
            }
        }

        Console.WriteLine(new string(ans));
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
