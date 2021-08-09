using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        char[] a = sc.NextCharArray();
        char[] b = sc.NextCharArray();

        /*
         * aの桁並び替え bを超えない最大
         */

        if (b.Length > a.Length)
        {
            Array.Sort(a, (l, r) => r.CompareTo(l));
            Console.WriteLine(new string(a));
            return;
        }
        int n = a.Length;
        // 使った桁、未満が確定したか?
        var dp = new long[(1 << n), 2];
        for (int i = 0; i < (1 << n); i++)
        {
            dp[i, 0] = int.MinValue;
            dp[i, 1] = int.MinValue;
        }

        dp[0, 0] = 0;
        for (int i = 0; i < (1 << n); i++)
        {
            // i個bit立ってる
            int cnt = 0;
            for (int j = 0; j < n; j++)
            {
                // a[j]を使う
                if ((i & (1 << j)) > 0) cnt++;
            }

            for (int j = 0; j < n; j++)
            {
                if ((i & (1 << j)) > 0) continue;
                if (cnt == 0 && a[j] == '0') continue;
                // a[j]を使う
                int next = i | (1 << j);
                for (int k = 0; k < 2; k++)
                {
                    if (dp[i, k] == int.MinValue) continue;
                    if (a[j] < b[cnt])
                    {
                        dp[next, 1] = Math.Max(dp[next, 1], dp[i, k] * 10 + (a[j] - '0'));
                    }
                    else if (a[j] == b[cnt] || k == 1)
                    {
                        dp[next, k] = Math.Max(dp[next, k], dp[i, k] * 10 + (a[j] - '0'));
                    }
                }
            }


        }

        Console.WriteLine(Math.Max(dp[(1 << n) - 1, 0], dp[(1 << n) - 1, 1]));
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
