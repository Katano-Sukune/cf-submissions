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
        int[][] a = new int[n][];
        for (int i = 0; i < n; i++)
        {
            a[i] = sc.IntArray();
        }

        /*
         * 最初スコア0
         * 
         * 各列一番上の1探す a[i][j]
         * 
         * 下k個の1の個数加算
         * 
         * いくつかの1を0に置き換える
         * 
         * 最大スコア
         */

        int[] dp = new int[10001];

        for (int j = 0; j <= 10000; j++)
        {
            dp[j] = int.MinValue;
        }


        dp[0] = 0;

        for (int i = 0; i < m; i++)
        {
            for (int j = 10000; j >= 0; j--)
            {
                if (dp[j] == int.MinValue) continue;

                int cost = 0;
                for (int l = 0; l < n; l++)
                {
                    if (a[l][i] == 1) cost++;
                }
                int cnt = 0;
                for (int l = n - 1; l >= 0; l--)
                {
                    if (l + k < n && a[l + k][i] == 1) cnt--;
                    if (a[l][i] == 0) continue;
                    cnt++;
                    cost--;
                    dp[ j + cost] = Math.Max(dp[ j + cost], dp[ j] + cnt);


                }
            }
        }

        int max = int.MinValue;
        int c = -1;

        for (int i = 0; i <= 10000; i++)
        {
            if (max < dp[ i])
            {
                max = dp[ i];
                c = i;
            }
        }

        Console.WriteLine($"{max} {c}");
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