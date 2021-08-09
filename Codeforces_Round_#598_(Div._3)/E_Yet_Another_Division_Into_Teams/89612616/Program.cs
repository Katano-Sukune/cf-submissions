using System;
using CompLib.Util;

public class Program
{
    private int N;
    private int[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();

        // N人学生
        // 学生iのスキル A_i

        // チーム
        // 最低3人 
        // 学生は1つのチームに属する
        // 多様性
        // 属する学生のA_iの最大 - 最小

        // 多様性の総和が最小になるようにチーム分けする

        /*
         * 全員でチーム
         *
         * max(A) - min(A)
         * 
         * Aで降順ソート
         *
         * dp[][] iまで j人チーム
         *
         * 
         */

        var sorted = new int[N];
        for (int i = 0; i < N; i++)
        {
            sorted[i] = i;
        }

        Array.Sort(sorted, (l, r) => A[r].CompareTo(A[l]));

        // 境目を選ぶ
        // 0,Nは強制
        // i-1,i-2が選ばれているとiは選べない
        // iを選ぶと -A[i-1] + A[i]減る

        var dp = new long[N + 1, 3];
        for (int i = 0; i <= N; i++)
        {
            for (int j = 0; j <= 2; j++)
            {
                dp[i, j] = long.MaxValue;
            }
        }

        dp[0, 0] = A[sorted[0]];
        for (int i = 1; i < N; i++)
        {
            for (int j = 0; j <= 2; j++)
            {
                // 選ばない
                if (dp[i - 1, j] == long.MaxValue) continue;
                dp[i, Math.Min(j + 1, 2)] = Math.Min(dp[i, Math.Min(j + 1, 2)], dp[i - 1, j]);
            }

            // 選ぶ
            if (dp[i - 1, 2] != long.MaxValue)
            {
                dp[i, 0] = Math.Min(dp[i, 0], dp[i - 1, 2] - A[sorted[i - 1]] + A[sorted[i]]);
            }
        }

        // Nは選ぶ
        dp[N, 0] = dp[N - 1, 2] - A[sorted[N - 1]];
        long res = dp[N, 0];
        int k = 0;
        int[] ans = new int[N];

        // 復元
        int p = 0;
        for (int i = N; i >= 1; i--)
        {
            if (i < N) ans[sorted[i]] = k;
            if (p == 0)
            {
                // 遷移元2
                p = 2;
                k++;
            }
            else if (p == 1)
            {
                // 遷移元 0
                p = 0;
            }
            else if (p == 2)
            {
                // 遷移元 1 or 2
                if (dp[i - 1, 2] != dp[i, 2]) p = 1;
            }
        }
        ans[sorted[0]] = k;
        Console.WriteLine($"{res} {k}");
        Console.WriteLine(string.Join(" ",ans));
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