using System;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
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
        int[][] a = new int[3][];
        for (int i = 0; i < 3; i++)
        {
            a[i] = sc.IntArray();
        }

        var dp = new bool[n, 3, 3];
        for (int i = 0; i < 3; i++)
        {
            dp[0, i, i] = true;
        }

        for (int i = 1; i < n; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                // 先頭
                for (int k = 0; k < 3; k++)
                {
                    // 1つ前
                    if (!dp[i - 1, j, k]) continue;
                    for (int l = 0; l < 3; l++)
                    {
                        // 現在
                        if (a[k][i - 1] == a[l][i]) continue;
                        dp[i, j, l] = true;
                    }
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (a[i][0] == a[j][n - 1])
                {
                    dp[n - 1, i, j] = false;
                }
            }
        }

        int f = -1;
        int c = -1;
        for (int i = 0; i < 3 && f == -1; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (dp[n - 1, i, j])
                {
                    f = i;
                    c = j;
                    break;
                }
            }
        }

        int[] p = new int[n];
        p[n - 1] = a[c][n - 1];
        for (int i = n - 2; i >= 0; i--)
        {
            for (int j = 0; j < 3; j++)
            {
                if (a[c][i + 1] == a[j][i]) continue;
                if (dp[i, f, j])
                {
                    c = j;
                    break;
                }
            }

            p[i] = a[c][i];
        }

        Console.WriteLine(string.Join(" ", p));
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