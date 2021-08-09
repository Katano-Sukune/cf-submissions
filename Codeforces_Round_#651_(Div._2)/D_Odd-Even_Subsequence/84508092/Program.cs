using System;
using CompLib.Util;

public class Program
{
    private int N;
    private int K;
    private int[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        A = sc.IntArray();

        // Aの部分配列 長さK S

        // Sの奇数番目最大、偶数番目最大
        // の最小 を最小化


        var sorted = new int[N];
        for (int i = 0; i < N; i++)
        {
            sorted[i] = i;
        }

        Array.Sort(sorted, (l, r) => A[l].CompareTo(A[r]));
        int ans1 = int.MaxValue, ans2 = int.MaxValue;
        if (K % 2 == 1)
        {
            int ng = -1;
            int ok = N;
            // 偶数番目
            while (ok - ng > 1)
            {
                int mid = (ok + ng) / 2;
                bool[] f = new bool[N];
                int cnt = 0;
                for (int i = 1; i < N - 1; i++)
                {
                    if (A[i] <= A[sorted[mid]] && !f[i - 1])
                    {
                        cnt++;
                        f[i] = true;
                    }
                }

                if (cnt >= K / 2)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }

            if (ok < N)
            {
                ans1 = A[sorted[ok]];
            }

            ng = -1;
            ok = N;
            // 偶数番目
            while (ok - ng > 1)
            {
                int mid = (ok + ng) / 2;
                bool[] f = new bool[N];
                int cnt = 0;
                for (int i = 0; i < N; i++)
                {
                    if (A[i] <= A[sorted[mid]] && (i == 0 || !f[i - 1]))
                    {
                        cnt++;
                        f[i] = true;
                    }
                }

                if (cnt >= K - (K / 2))
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }

            if (ok < N)
            {
                ans2 = A[sorted[ok]];
            }
        }
        else
        {
            int ng = -1;
            int ok = N;
            // 偶数番目
            while (ok - ng > 1)
            {
                int mid = (ok + ng) / 2;
                bool[] f = new bool[N];
                int cnt = 0;
                for (int i = 1; i < N; i++)
                {
                    if (A[i] <= A[sorted[mid]] && !f[i - 1])
                    {
                        cnt++;
                        f[i] = true;
                    }
                }

                if (cnt >= K / 2)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }

            if (ok < N)
            {
                ans1 = A[sorted[ok]];
            }

            ng = -1;
            ok = N;
            // 偶数番目
            while (ok - ng > 1)
            {
                int mid = (ok + ng) / 2;
                bool[] f = new bool[N];
                int cnt = 0;
                for (int i = 0; i < N - 1; i++)
                {
                    if (A[i] <= A[sorted[mid]] && (i == 0 || !f[i - 1]))
                    {
                        cnt++;
                        f[i] = true;
                    }
                }

                if (cnt >= (K / 2))
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }

            if (ok < N)
            {
                ans2 = A[sorted[ok]];
            }
        }

        Console.WriteLine(Math.Min(ans1, ans2));
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
                string s = Console.ReadLine();
                while (s.Length == 0)
                {
                    s = Console.ReadLine();
                }

                _line = s.Split(Separator);
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