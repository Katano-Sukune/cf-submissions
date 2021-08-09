using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    long K;
    string S;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextLong();
        S = sc.Next();


        // iまで見た、スキップj個 最後文字
        var dp = new long[N + 1, N + 1, 27];
        dp[0, 0, 26] = 1;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j <= i; j++)
            {

                for (int c = 0; c <= 26; c++)
                {
                    // s_iを使う
                    dp[i + 1, j, S[i] - 'a'] += dp[i, j, c];


                    // 使わない
                    if (S[i] - 'a' != c) dp[i + 1, j + 1, c] += dp[i, j, c];
                }
            }
        }

        long[] cnt = new long[N + 1];
        for (int i = 0; i <= N; i++)
        {
            for (int c = 0; c <= 26; c++)
            {
                cnt[i] += dp[N, i, c];
            }
        }

        long ans = 0;
        for (int i = 0; i <= N && K > 0; i++)
        {
            // Console.WriteLine($"{i} {cnt[i]} {K}");
            if (cnt[i] < K)
            {
                ans += cnt[i] * i;
                K -= cnt[i];
            }
            else
            {
                ans += i * K;
                K = 0;
            }
        }

        if (K > 0)
        {
            Console.WriteLine("-1");
            return;
        }

        Console.WriteLine(ans);
    }

    public static void Main(string[] args) => new Program().Solve();
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
