using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private string N;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.Next();
        int[] cnt = new int[10];
        foreach (char c in N)
        {
            cnt[c - '0']++;
        }

        long[,] bi = new long[30, 30];
        for (int i = 0; i < 30; i++)
        {
            bi[i, 0] = bi[0, i] = 1;
        }

        for (int i = 1; i < 30; i++)
        {
            for (int j = 1; j < 30; j++)
            {
                bi[i, j] = bi[i - 1, j] + bi[i, j - 1];
            }
        }

        long[,] dp = new long[11, 30];
        dp[10, 0] = 1;
        for (int d = 9; d > 0; d--)
        {
            if (cnt[d] == 0)
            {
                for (int i = 0; i < 30; i++)
                {
                    dp[d, i] = dp[d + 1, i];
                }
            }
            else
            {
                for (int i = 0; i < 30; i++)
                {
                    long prev = dp[d + 1, i];
                    if (prev == 0) continue;
                    for (int j = 1; j <= cnt[d]; j++)
                    {
                        // Console.WriteLine($"{d} {i+j} {j}");
                        dp[d, i + j] += prev * bi[i, j];
                    }
                }
            }
        }

        if (cnt[0] == 0)
        {
            for (int i = 0; i < 30; i++)
            {
                dp[0, i] = dp[1, i];
            }
        }
        else
        {
            for (int i = 0; i < 30; i++)
            {
                long prev = dp[1, i];
                if (prev == 0) continue;
                for (int j = 1; j <= cnt[0]; j++)
                {
                    dp[0, i + j] += prev * bi[i - 1, j];
                }
            }
        }

        long ans = 0;
        for (int i = 0; i < 30; i++)
        {
            ans += dp[0, i];
        }
        //
        // for (int i = 0; i <= 18; i++)
        // {
        //     Console.Write($"{dp[9, i]} ");
        // }
        //
        // Console.WriteLine();

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