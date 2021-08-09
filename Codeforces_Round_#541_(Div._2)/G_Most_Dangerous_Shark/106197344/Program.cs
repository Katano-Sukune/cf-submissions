using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;

public class Program
{
    int N, M;
    int[] H;
    long[] Cost;


    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();

        {
            int[] k = new int[N];
            int[][] a = new int[N][];
            long[][] c = new long[N][];
            for (int i = 0; i < N; i++)
            {
                k[i] = sc.NextInt();
                a[i] = sc.IntArray();
                c[i] = sc.LongArray();
            }

            int q = sc.NextInt();
            H = new int[M];
            Cost = new long[M];
            int ptr = 0;
            for (int i = 0; i < q; i++)
            {
                int id = sc.NextInt() - 1;
                int mul = sc.NextInt();
                for (int j = 0; j < k[id]; j++)
                {
                    H[ptr] = a[id][j];
                    Cost[ptr] = c[id][j] * mul;
                    ptr++;
                }
            }
        }

        // iを左に倒す ll[i]まで倒れる
        int[] ll = new int[M];
        {
            // Fを倒せれば Sまで倒せる
            var stack = new St[M];
            int ptr = 0;
            for (int i = 0; i < M; i++)
            {
                int min = Math.Max(0, i - H[i] + 1);
                while (ptr > 0 && i - stack[ptr - 1].F < H[i])
                {
                    min = Math.Min(min, stack[ptr - 1].S);
                    ptr--;
                }

                ll[i] = min;
                stack[ptr++] = new St(i, min);
            }
        }
        int[] rr = new int[M];
        {
            var stack = new St[M];
            int ptr = 0;
            for (int i = M - 1; i >= 0; i--)
            {
                int max = Math.Min(M - 1, i + H[i] - 1);
                while (ptr > 0 && stack[ptr - 1].F - i < H[i])
                {
                    max = Math.Max(max, stack[ptr - 1].S);
                    ptr--;
                }

                rr[i] = max;
                stack[ptr++] = new St(i, max);
            }
        }

        // Console.WriteLine(string.Join(" ", ll));
        // Console.WriteLine(string.Join(" ", rr));

        // sまで倒すコストs
        {
            var st = new St2[M];
            int ptr = 0;
            long[] dp = new long[M + 1];
            Array.Fill(dp, long.MaxValue);
            dp[0] = 0;

            for (int i = 0; i < M; i++)
            {
                // iを左に倒す
                dp[i + 1] = dp[ll[i]] + Cost[i];

                long min = long.MaxValue;
                // 右に倒す
                while (ptr > 0 && st[ptr - 1].F < i) ptr--;
                if (ptr > 0) dp[i + 1] = Math.Min(dp[i + 1], st[ptr - 1].S);
                if (ptr == 0 || dp[i] + Cost[i] < st[ptr - 1].S) st[ptr++] = new St2(rr[i], dp[i] + Cost[i]);
            }
            Console.WriteLine(dp[M]);
        }
    }

    struct St
    {
        public int F, S;
        public St(int f, int s)
        {
            F = f;
            S = s;
        }
    }

    struct St2
    {
        public int F;
        public long S;
        public St2(int f, long s)
        {
            F = f;
            S = s;
        }
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
