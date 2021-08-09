using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;

public class Program
{
    int N;
    int[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();

        // iまで見る
        // i番目が減少列にある?

        // 減少列の最大
        // 増加列最小
        var dp = new int[N, 2];
        for (int i = 0; i < N; i++)
        {
            dp[i, 1] = int.MaxValue;
            dp[i, 0] = int.MinValue;
        }

        // A_0を減少列に入れる
        dp[0, 1] = int.MinValue;
        // 増加列
        dp[0, 0] = int.MaxValue;

        for (int i = 1; i < N; i++)
        {
            // i-1が増加列
            // iも増加列
            if (A[i - 1] < A[i] && dp[i - 1, 0] != int.MinValue)
            {
                dp[i, 0] = Math.Max(dp[i, 0], dp[i - 1, 0]);
            }
            // i-1は減少列
            // iは増加列
            if (dp[i - 1, 1] < A[i])
            {
                dp[i, 0] = Math.Max(dp[i, 0], A[i - 1]);
            }

            // i-1, iが減少列
            if (A[i - 1] > A[i] && dp[i - 1, 1] != int.MaxValue)
            {
                dp[i, 1] = Math.Min(dp[i, 1], dp[i - 1, 1]);
            }

            // i-1は増加列
            // iは減少列
            if (dp[i - 1, 0] > A[i])
            {
                dp[i, 1] = Math.Min(dp[i, 1], A[i - 1]);
            }
        }

        int[] ans = new int[N];
        if (dp[N - 1, 0] != int.MinValue)
        {
            ans[N - 1] = 0;
        }
        else if (dp[N - 1, 1] != int.MaxValue)
        {
            ans[N - 1] = 1;
        }
        else
        {
            Console.WriteLine("NO");
            return;
        }
        for (int i = N - 2; i >= 0; i--)
        {
            // iが増加列
            if (dp[i, 0] != int.MinValue)
            {
                if (ans[i + 1] == 0 && A[i] < A[i + 1])
                {
                    // 次も増加列
                    ans[i] = 0;
                    continue;
                }

                if (ans[i + 1] == 1 && dp[i + 1, 1] == A[i])
                {
                    // 次は減少列
                    ans[i] = 0;
                    continue;
                }
            }
            if (dp[i, 1] != int.MaxValue)
            {
                if (ans[i + 1] == 1 && A[i] > A[i + 1])
                {
                    ans[i] = 1;
                    continue;
                }

                if (ans[i + 1] == 0 && dp[i + 1, 0] == A[i])
                {
                    ans[i] = 1;
                    continue;
                }
            }
        }

        Console.WriteLine("YES");
        Console.WriteLine(string.Join(" ", ans));
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
