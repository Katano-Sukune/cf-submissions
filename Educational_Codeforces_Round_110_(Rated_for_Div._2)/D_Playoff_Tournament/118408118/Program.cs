using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int K;
    char[] S;
    int Q;
    int[] P;
    char[] C;
    public void Solve()
    {
        var sc = new Scanner();
        K = sc.NextInt();
        S = sc.NextCharArray();
        int Q = sc.NextInt();
        P = new int[Q];
        C = new char[Q];
        for (int i = 0; i < Q; i++)
        {
            P[i] = sc.NextInt() - 1;
            C[i] = sc.NextChar();
        }
        int n = 1 << K;
        int[] dp = new int[2 * n];
        for (int i = 0; i < n; i++)
        {
            dp[i + n] = 1;
        }

        for (int i = 0; i < n - 1; i++)
        {
            int j = n - i - 1;
            if (S[i] == '?')
            {
                dp[j] = dp[j * 2] + dp[j * 2 + 1];
            }
            else
            {
                dp[j] = dp[2 * j + 1 - (S[i] - '0')];
            }
        }

#if !DEBUG
        System.Console.SetOut(new System.IO.StreamWriter(System.Console.OpenStandardOutput()) { AutoFlush = false});
#endif

        for (int t = 0; t < Q; t++)
        {
            S[P[t]] = C[t];
            int p = n - P[t] - 1;
            while (p >= 1)
            {
                int q = n - p - 1;
                if (S[q] == '?')
                {
                    dp[p] = dp[p * 2] + dp[p * 2 + 1];
                }
                else
                {
                    dp[p] = dp[2 * p + 1 - (S[q] - '0')];
                }
                p /= 2;
            }

            Console.WriteLine(dp[1]);
        }

        System.Console.Out.Flush();

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
