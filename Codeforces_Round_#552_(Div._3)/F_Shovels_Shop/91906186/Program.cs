using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int m = sc.NextInt();
        int k = sc.NextInt();

        int[] a = sc.IntArray();

        int[] max = new int[k + 1];
        for (int i = 0; i < m; i++)
        {
            int x = sc.NextInt();
            int y = sc.NextInt();
            if (x > k) continue;
            max[x] = Math.Max(max[x], y);
        }

        Array.Sort(a);
        /*
         * n個スコップ
         * 
         * iはa_iB
         * 
         * k個スコップ買う
         * 
         * x_j個買ったら下位y_j個無料になる
         */

        /*
         * 
         */

        long[] table = new long[k + 1];
        for (int i = 0; i < k; i++)
        {
            table[i + 1] = table[i] + a[i];
        }

        long[] dp = new long[k + 1];
        for (int i = 1; i <= k; i++)
        {
            dp[i] = long.MaxValue;
        }

        for (int i = 0; i < k; i++)
        {
            for (int x = 1; x + i <= k; x++)
            {
                // コスト
                long cost = table[x + i] - table[i + max[x]];

                dp[i+x] = Math.Min(dp[i+x], dp[i] + cost);
            }
        }

        Console.WriteLine(dp[k]);
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
