using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    int[] K;

    int[][] C, D;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = new int[N];

        Tuple<int, long>[][] card = new Tuple<int, long>[N][];
        for (int i = 0; i < N; i++)
        {
            K[i] = sc.NextInt();
            card[i] = new Tuple<int, long>[K[i]];
            for (int j = 0; j < K[i]; j++)
            {
                card[i][j] = new Tuple<int, long>(sc.NextInt(), sc.NextInt());

            }

            Array.Sort(card[i], (l, r) => l.Item2.CompareTo(r.Item2));
        }


        // %10枚使ったときの最大
        var ans = new long[N + 1, 10];
        for (int i = 0; i <= N; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                ans[i, j] = long.MinValue;
            }
        }

        ans[0, 0] = 0;

        // i枚使ってコストjのとき最大
        long[,] dp = new long[4, 4];

        // i枚使ってコストjD最大1枚は倍のとき最大
        long[,] dp2 = new long[4, 4];

        for (int i = 0; i < N; i++)
        {

            for (int j = 0; j <= 3; j++)
            {
                for (int k = 0; k <= 3; k++)
                {
                    dp[j, k] = long.MinValue;
                    dp2[j, k] = long.MinValue;
                }
            }

            dp[0, 0] = 0;

            for (int j = 0; j < K[i]; j++)
            {
                for (int l = 3; l >= 0; l--)
                {
                    if (l + card[i][j].Item1 > 3) continue;
                    for (int k = 2; k >= 0; k--)
                    {
                        if (dp[k, l] == long.MinValue) continue;
                        dp[k + 1, l + card[i][j].Item1] = Math.Max(dp[k + 1, l + card[i][j].Item1], dp[k, l] + card[i][j].Item2);
                        dp2[k + 1, l + card[i][j].Item1] = Math.Max(dp2[k + 1, l + card[i][j].Item1], dp[k, l] + 2 * card[i][j].Item2);
                    }
                }
            }

            for (int j = 0; j < 10; j++)
            {
                ans[i + 1, j] = ans[i, j];
            }
            for (int j = 0; j < 10; j++)
            {
                if (ans[i, j] == long.MinValue) continue;
                for (int k = 0; k <= 3; k++)
                {
                    for (int l = 0; l <= 3; l++)
                    {
                        
                        if (j + k >= 10 && dp2[k,l] != long.MinValue)
                        {
                            ans[i + 1, j + k - 10] = Math.Max(ans[i + 1, j + k - 10], ans[i, j] + dp2[k, l]);
                        }
                        else if(dp[k,l] != long.MinValue)
                        {
                            ans[i + 1, j + k] = Math.Max(ans[i + 1, j + k], ans[i, j] + dp[k, l]);
                        }
                    }
                }
            }
        }

        long max = long.MinValue;
        for (int i = 0; i < 10; i++)
        {
            max = Math.Max(max, ans[N, i]);
        }

        Console.WriteLine(max);
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
