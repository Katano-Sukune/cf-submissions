using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private int[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();
        /*
         * 隣り合う2つ消す
         * 和 正負反転したのを同じ位置に戻す
         *
         * 最大
         */

        /*
         * + 1
         * -- -2
         * ++-, -++ +1
         * --+- +--- ++++,-+--,---+ -2,4
         * ++-+- -+++- ----- --+++ +++-- +-++- +--++ ++--+ -+-++
         *
         * --+-+- +---+- +++++- ++---- ++-+++
         * 展開していく
         *
         * 
         */
        if (N == 1)
        {
            Console.WriteLine(A[0]);
            return;
        }
        long min = 1 - (N - 1) * 3;
        long max = 1 + (N - 1) * 3;

        // min, max6刻み

        // iまで見る、mod, 最後のやつ, 同じやつ2回連続
        var dp = new long[N, 6, 2];
        var dp2 = new long[N, 6];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    dp[i, j, k] = long.MinValue;
                }

                dp2[i, j] = long.MinValue;
            }
        }

        dp[0, 1, 1] = A[0];
        dp[0, 5, 0] = -A[0];

        for (int i = 1; i < N; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    long cur = dp[i - 1, j, k];
                    if (cur == long.MinValue) continue;
                    // +にする
                    {
                        if (k == 1)
                        {
                            ref long to = ref dp2[i, (j + 1) % 6];
                            to = Math.Max(to, cur + A[i]);
                        }
                        else
                        {
                            ref long to = ref dp[i, (j + 1) % 6, 1];
                            to = Math.Max(to, cur + A[i]);
                        }
                    }

                    {
                        if (k == 0)
                        {
                            ref long to = ref dp2[i, (j + 5) % 6];
                            to = Math.Max(to, cur - A[i]);
                        }
                        else
                        {
                            ref long to = ref dp[i, (j + 5) % 6, 0];
                            to = Math.Max(to, cur - A[i]);
                        }
                    }
                }

                long cur2 = dp2[i - 1, j];
                if (cur2 == long.MinValue) continue;
                
                {
                    ref long to = ref dp2[i, (j + 1) % 6];
                    to = Math.Max(to, cur2 + A[i]);
                }
                {
                    ref long to = ref dp2[i, (j + 5) % 6];
                    to = Math.Max(to, cur2 - A[i]);
                }
            }
        }

        Console.WriteLine(dp2[N - 1, (N % 2 == 1 ? 1 : 4)]);
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