using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
#if !DEBUG
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
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
        int k = sc.NextInt();
        string s = sc.Next();

        int first = s.IndexOf('*');
        int last = s.LastIndexOf('*');
        int[,] dp = new int[n, k];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < k; j++)
            {
                dp[i, j] = int.MaxValue;
            }
        }

        dp[first, 0] = 1;
        for (int i = first + 1; i <= last; i++)
        {
            if (s[i] == '*')
            {
                for (int j = 0; j < k; j++)
                {
                    if (dp[i - 1, j] == int.MaxValue) continue;
                    dp[i, 0] = Math.Min(dp[i, 0], dp[i - 1, j] + 1);
                }
            }

            for (int j = 1; j < k; j++)
            {
                if (dp[i - 1, j - 1] == int.MaxValue) continue;
                dp[i, j] = Math.Min(dp[i, j], dp[i - 1, j - 1]);
            }
        }

        Console.WriteLine(dp[last, 0]);
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