using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, R;
    private int[] A, B;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        R = sc.NextInt();
        A = new int[N];
        B = new int[N];
        for (int i = 0; i < N; i++)
        {
            A[i] = sc.NextInt();
            B[i] = sc.NextInt();
        }

        List<(int a, int b)> plus = new List<(int a, int b)>();
        List<(int a, int b)> minus = new List<(int a, int b)>();
        for (int i = 0; i < N; i++)
        {
            if (B[i] >= 0)
            {
                plus.Add((A[i], B[i]));
            }
            else
            {
                minus.Add((A[i], B[i]));
            }
        }

        int ans = 0;
        plus.Sort((l, r) => l.a.CompareTo(r.a));
        foreach ((int a, int b) in plus)
        {
            if (R >= a)
            {
                ans++;
                R += b;
            }
        }


        minus.Sort((l, r) => (r.b + r.a).CompareTo(l.b + l.a));

        int[,] dp = new int[minus.Count + 1, R + 1];
        for (int i = 0; i <= minus.Count; i++)
        {
            for (int j = 0; j <= R; j++)
            {
                dp[i, j] = int.MinValue;
            }
        }

        dp[0, R] = 0;
        for (int i = 0; i < minus.Count; i++)
        {
            (int a, int b) = minus[i];
            for (int j = 0; j <= R; j++)
            {
                if (dp[i, j] == int.MinValue) continue;
                dp[i + 1, j] = Math.Max(dp[i + 1, j], dp[i, j]);
                if (j >= a && j + b >= 0) dp[i + 1, j + b] = Math.Max(dp[i + 1, j + b], dp[i, j] + 1);
            }
        }

        int mx = 0;
        for (int i = 0; i <= R; i++)
        {
            mx = Math.Max(mx, dp[minus.Count, i]);
        }

        ans += mx;

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