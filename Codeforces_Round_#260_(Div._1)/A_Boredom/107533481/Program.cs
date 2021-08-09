using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int[] a = sc.IntArray();

        // a_kを選ぶ
        // a_k, a_k+-1と等しい要素を消す
        // a_kポイント

        // スコア最大

        const int maxA = 100000;

        int[] cnt = new int[maxA + 2];
        foreach (int i in a)
        {
            cnt[i]++;
        }

        var dp = new long[maxA + 3];
        Array.Fill(dp, long.MinValue);
        dp[1] = 0;
        for (long i = 1; i <= maxA + 1; i++)
        {
            if (cnt[i] > 0)
            {
                // Console.WriteLine($"{i} {dp[i]} {}");
                dp[i + 2] = Math.Max(dp[i + 2], dp[i] + i * cnt[i]);
            }

            dp[i + 1] = Math.Max(dp[i + 1], dp[i]);
        }

        Console.WriteLine(dp[maxA + 2]);
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