using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();

        int[] a = sc.IntArray();

        int[] cnt = new int[1001];
        foreach (var i in a)
        {
            cnt[i]++;
        }

        int[][] ans = new int[n][];
        for (int i = 0; i < n; i++)
        {
            ans[i] = new int[n];
        }

        int t = 0;
        for (int i = 1; i <= 1000 && t < (n / 2) * (n / 2); i++)
        {
            while (t < (n / 2) * (n / 2) && cnt[i] >= 4)
            {
                int r = t / (n / 2);
                int c = t % (n / 2);
                ans[r][c] = i;
                ans[n - r - 1][c] = i;
                ans[r][n - c - 1] = i;
                ans[n - r - 1][n - c - 1] = i;
                t++;
                cnt[i] -= 4;
            }
        }

        if (n % 2 == 1)
        {
            t = 0;
            for (int i = 1; i <= 1000 && t < (n / 2); i++)
            {
                while (t < (n / 2) && cnt[i] >= 2)
                {
                    int r = n / 2;
                    int c = t;
                    ans[r][c] = i;
                    ans[r][n - c - 1] = i;
                    t++;
                    cnt[i] -= 2;
                }
            }

            t = 0;
            for (int i = 1; i <= 1000 && t < (n / 2); i++)
            {
                while (t < (n / 2) && cnt[i] >= 2)
                {
                    int r = t;
                    int c = n / 2;
                    ans[r][c] = i;
                    ans[n - r - 1][c] = i;
                    t++;
                    cnt[i] -= 2;
                }
            }

            t = 0;
            for (int i = 1; i <= 1000 && t < 1; i++)
            {
                if (cnt[i] > 0)
                {
                    ans[n / 2][n / 2] = i;
                    t++;
                    cnt[i]--;
                }
            }
        }

        for (int i = 1; i <= 1000; i++)
        {
            if (cnt[i] > 0)
            {
                Console.WriteLine("NO");
                return;
            }
        }

        Console.WriteLine("YES");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine(string.Join(" ", ans[i]));
        }
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
