using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Text;

public class Program
{
    int N, A, B;
    string S;
    public void Solve()
    {
        var sc = new Scanner();

        var rnd = new Random();
#if DEBUG
        N = 5000;
        A = 1;
        B = 1;
        {
            var tmp = new char[N];
            for (int i = 0; i < N; i++)
            {
                tmp[i] = (char)rnd.Next('a', 'z' + 1);
            }
            S = new string(tmp);
        }
#else
        N = sc.NextInt();
        A = sc.NextInt();
        B = sc.NextInt();
        S = sc.Next();
#endif

        // i文字目まで見る
        // コスト最小
        long[] dp = new long[N + 1];
        Array.Fill(dp, long.MaxValue);
        dp[0] = 0;

        // iからj文字先まで コストbで行ける
        var ls = new List<int>[N + 1];
        for (int i = 0; i <= N; i++)
        {
            ls[i] = new List<int>();
        }

        for (int len = 1; 2 * len <= N; len++)
        {
            var mod1 = rnd.Next();
            var mod2 = rnd.Next();

            var hs = new HashSet<(long h1, long h2)>();

            long t1 = 0;
            long t2 = 0;

            long h1 = 0;
            long h2 = 0;

            long p1 = 1;
            long p2 = 1;

            for (int i = len; i < len + len - 1; i++)
            {
                t1 *= 256;
                t2 *= 256;
                t1 += S[i - len];
                t2 += S[i - len];
                t1 %= mod1;
                t2 %= mod2;

                h1 *= 256;
                h2 *= 256;
                h1 += S[i];
                h2 += S[i];
                h1 %= mod1;
                h2 %= mod2;

                p1 *= 256;
                p2 *= 256;
                p1 %= mod1;
                p2 %= mod2;
            }

            for (int i = len + len - 1; i < N; i++)
            {
                t1 *= 256;
                t2 *= 256;
                t1 += S[i - len];
                t2 += S[i - len];
                t1 %= mod1;
                t2 %= mod2;

                h1 *= 256;
                h2 *= 256;
                h1 += S[i];
                h2 += S[i];
                h1 %= mod1;
                h2 %= mod2;

                hs.Add((t1, t2));

                if (hs.Contains((h1, h2))) ls[i - len + 1].Add(len);

                t1 -= p1 * S[i - (len + len - 1)];
                t2 -= p2 * S[i - (len + len - 1)];
                t1 %= mod1;
                t2 %= mod2;
                if (t1 < 0) t1 += mod1;
                if (t2 < 0) t2 += mod2;

                h1 -= p1 * S[i - (len - 1)];
                h2 -= p2 * S[i - (len - 1)];
                h1 %= mod1;
                h2 %= mod2;
                if (h1 < 0) h1 += mod1;
                if (h2 < 0) h2 += mod2;
            }
        }

        for (int i = 0; i < N; i++)
        {
            dp[i + 1] = Math.Min(dp[i + 1], dp[i] + A);
            foreach (var to in ls[i])
            {
                dp[i + to] = Math.Min(dp[i + to], dp[i] + B);
            }
        }

        Console.WriteLine(dp[N]);
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
