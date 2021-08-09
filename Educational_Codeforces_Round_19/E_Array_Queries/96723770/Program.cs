using System;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private int[] A;
    private int Q;
    private const int T = 350;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();
        Q = sc.NextInt();

        var dp = new int[N, T + 1];
        for (int i = N - 1; i >= 0; i--)
        {
            for (int k = 1; k <= T; k++)
            {
                int next = i + A[i] + k;
                if (next >= N) dp[i, k] = 1;
                else dp[i, k] = dp[next, k] + 1;
            }
        }

        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        for (int i = 0; i < Q; i++)
        {
            int p = sc.NextInt() - 1;
            int k = sc.NextInt();

            if (k <= T)
            {
                Console.WriteLine(dp[p, k]);
                continue;
            }

            int ans = 0;
            while (p < N)
            {
                ans++;
                p += A[p] + k;
            }

            Console.WriteLine(ans);
        }

        Console.Out.Flush();
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