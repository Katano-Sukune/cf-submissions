using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    int N, K;
    int[] C;
    int[] F;
    int[] H;
    const int Max = 100000;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        C = sc.IntArray();
        F = sc.IntArray();
        H = new int[K + 1];
        for (int i = 1; i <= K; i++)
        {
            H[i] = sc.NextInt();
        }
        /*
         * n人
         * K*Nカード C_i
         * 
         * k枚ずつ配る
         * 
         * jがf_jをT枚持っているとき 幸福度 h_t
         * 
         * 総和最大
         */

        /*
         * 欲しい人が1人 その人に全部渡す
         * 
         * 複数人 総和が最大になるように
         */

        int[] fCnt = new int[Max + 1];
        int[] cCnt = new int[Max + 1];
        foreach (var i in C)
        {
            cCnt[i]++;
        }

        foreach (var i in F)
        {
            fCnt[i]++;
        }

        long ans = 0;
        for (int num = 1; num <= Max; num++)
        {
            // cCnt[i] を fCnt[i]人で h総和が最大になるように分ける
            int f = fCnt[num];
            int c = cCnt[num];

            var dp = new long[c + 1];
            for (int i = 1; i <= c; i++)
            {
                dp[i] = long.MinValue;
            }

            for (int i = 0; i < f; i++)
            {
                for (int j = c; j >= 0; j--)
                {
                    if (dp[j] == long.MinValue) continue;
                    for (int k = 0; k <= K; k++)
                    {
                        if (j + k > c) continue;
                        dp[j + k] = Math.Max(dp[j + k], dp[j] + H[k]);
                    }
                }
            }

            ans += dp.Max();
        }

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
