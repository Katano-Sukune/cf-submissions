using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    int[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();


        int[] cnt = new int[1001];
        foreach (var i in A)
        {
            cnt[i]++;
        }

        // nが偶数
        // 全部4の倍数
        if (N % 2 == 0)
        {

            foreach (var i in cnt)
            {
                if (i % 4 != 0)
                {
                    Console.WriteLine("NO");
                    return;
                }
            }

            int[][] ans = new int[N][];
            for (int i = 0; i < N; i++)
            {
                ans[i] = new int[N];
            }
            int target = 0;
            for (int i = 0; i < N / 2; i++)
            {
                for (int j = 0; j < N / 2; j++)
                {
                    while (cnt[target] == 0) target++;
                    ans[i][j] = target;
                    ans[i][N - 1 - j] = target;
                    ans[N - 1 - i][j] = target;
                    ans[N - 1 - i][N - 1 - j] = target;
                    cnt[target] -= 4;
                }
            }

            Console.WriteLine("YES");
            Console.WriteLine(string.Join("\n", ans.Select(ar => string.Join(" ", ar))));
        }
        else
        {
            // 奇数
            // 真ん中
            // (N-1)個以下2の倍数
            // 残り4の倍数

            int[][] ans = new int[N][];
            for (int i = 0; i < N; i++)
            {
                ans[i] = new int[N];
            }

            int one = 0;
            int two = 0;

            foreach (var i in cnt)
            {
                if (i % 2 == 1) one++;
                if (i % 4 == 2 || i % 4 == 3) two++;
            }

            if (one != 1 || two > N - 1)
            {
                Console.WriteLine("NO");
                return;
            }
            int target = 0;
            for (int i = 0; i < N / 2; i++)
            {
                for (int j = 0; j < N / 2; j++)
                {
                    while (cnt[target] < 4) target++;
                    ans[i][j] = target;
                    ans[i][N - 1 - j] = target;
                    ans[N - 1 - i][j] = target;
                    ans[N - 1 - i][N - 1 - j] = target;
                    cnt[target] -= 4;
                }
            }

            target = 0;
            for (int i = 0; i < N / 2; i++)
            {
                while (cnt[target] < 2) target++;
                ans[i][N / 2] = target;
                ans[N - 1 - i][N / 2] = target;
                cnt[target] -= 2;
            }
            for (int i = 0; i < N / 2; i++)
            {
                while (cnt[target] < 2) target++;
                ans[N / 2][i] = target;
                ans[N / 2][N - i - 1] = target;
                cnt[target] -= 2;
            }

            target = 0;
            while (cnt[target] < 1) target++;
            ans[N / 2][N / 2] = target;
            Console.WriteLine("YES");
            Console.WriteLine(string.Join("\n", ans.Select(ar => string.Join(" ", ar))));
        }



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
