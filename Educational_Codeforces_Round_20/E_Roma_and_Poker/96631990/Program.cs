using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, K;
    private string S;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        S = sc.Next();

        bool[,] dp = new bool[N + 1, 2 * N + 1];
        dp[0, N] = true;
        for (int i = 0; i < N; i++)
        {
            for (int j = -N; j <= N; j++)
            {
                if (!dp[i, j + N]) continue;
                if (S[i] == 'W' || S[i] == '?')
                {
                    if (j + 1 <= N && (i == N - 1 || Math.Abs(j + 1) != K))
                    {
                        dp[i + 1, j + 1 + N] = true;
                    }
                }

                if (S[i] == 'D' || S[i] == '?')
                {
                    if (i == N - 1 || Math.Abs(j) != K)
                    {
                        dp[i + 1, j + N] = true;
                    }
                }

                if (S[i] == 'L' || S[i] == '?')
                {
                    if (j - 1 >= -N && (i == N - 1 || Math.Abs(j - 1) != K))
                    {
                        dp[i + 1, j - 1 + N] = true;
                    }
                }
            }
        }

        int cur = int.MinValue;
        if (dp[N, -K + N]) cur = -K + N;
        if (dp[N, K + N]) cur = K + N;
        if (cur == int.MinValue)
        {
            Console.WriteLine("NO");
            return;
        }

        char[] ans = new char[N];
        for (int i = N - 1; i >= 0; i--)
        {
            switch (S[i])
            {
                case 'W':
                    ans[i] = 'W';
                    cur--;
                    break;
                case 'L':
                    ans[i] = 'L';
                    cur++;
                    break;
                case 'D':
                    ans[i] = 'D';
                    break;
                case '?':

                    // W
                    if (cur - 1 >= 0 && dp[i, cur - 1])
                    {
                        ans[i] = 'W';
                        cur--;
                    }

                    // D
                    else if (dp[i, cur])
                    {
                        ans[i] = 'D';
                    }

                    // L
                    else if (cur + 1 <= 2 * N && dp[i, cur + 1])
                    {
                        ans[i] = 'L';
                        cur++;
                    }

                    break;
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