using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    private int N, H, L, R;
    private int[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        H = sc.NextInt();
        L = sc.NextInt();
        R = sc.NextInt();
        A = sc.IntArray();

        /*
         * n回寝る
         * i回目 起きてからa[i]時間後寝る
         *
         * 寝るのは H時間 (1日)
         *
         * [L,R] の間に入眠すれば 良い
         *
         * 良い回数
         */
        
        var dp = new int[H].Select(i => -1).ToArray();
        dp[0] = 0;
        for (int i = 0; i < N; i++)
        {
            var next = new int[H].Select(ii => -1).ToArray();
            for (int j = 0; j < H; j++)
            {
                if (dp[j] == -1) continue;
                {
                    int a = (j + A[i]) % H;
                    int t = dp[j] + (L <= a && a <= R ? 1 : 0);
                    next[a] = Math.Max(next[a], t);

                    int b = (j + A[i] + H - 1) % H;
                    int tt = dp[j] + (L <= b && b <= R ? 1 : 0);
                    next[b] = Math.Max(next[b], tt);
                }
            }

            dp = next;
        }

        Console.WriteLine(dp.Max());
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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
                _index = 0;
            }

            return _line[_index++];
        }

        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public decimal NextDecimal() => decimal.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}