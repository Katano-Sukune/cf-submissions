using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{
    int N;
    int[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();

        Array.Sort(A);

        int ng = -1;
        int ok = (int)1e9;
        while (ok - ng > 1)
        {
            int mid = (ok + ng) / 2;
            if (F(mid)) ok = mid;
            else ng = mid;
        }

        // Console.WriteLine(ok);
        W(ok);
    }

    bool F(int n)
    {
        bool[,,] dp = new bool[N, N, N];
        dp[0, 0, 0] = true;
        for (int i = 1; i < N - 1; i++)
        {
            for (int j = 0; j < i; j++)
            {
                for (int k = 0; k < i; k++)
                {
                    if (!dp[i - 1, j, k]) continue;
                    if (A[i] - A[j] <= n) dp[i, i, k] = true;
                    if (A[i] - A[k] <= n) dp[i, j, i] = true;
                }
            }
        }

        for (int j = 0; j < N - 1; j++)
        {
            for (int k = 0; k < N - 1; k++)
            {
                if (!dp[N - 2, j, k]) continue;
                if (A[N - 1] - A[j] <= n && A[N - 1] - A[k] <= n) return true;
            }
        }
        return false;
    }

    void W(int n)
    {
        bool[,,] dp = new bool[N, N, N];
        dp[0, 0, 0] = true;
        for (int i = 1; i < N - 1; i++)
        {
            for (int j = 0; j < i; j++)
            {
                for (int k = 0; k < i; k++)
                {
                    if (!dp[i - 1, j, k]) continue;
                    if (A[i] - A[j] <= n) dp[i, i, k] = true;
                    if (A[i] - A[k] <= n) dp[i, j, i] = true;
                }
            }
        }

        var l = new List<int>();
        var r = new List<int>();
        l.Add(A[N - 1]);

        int ll = -1;
        int rr = -1;

        for (int j = 0; j < N - 1 && ll == -1; j++)
        {
            for (int k = 0; k < N - 1 && ll == -1; k++)
            {
                if (dp[N - 2, j, k] && A[N - 1] - A[j] <= n && A[N - 1] - A[k] <= n)
                {
                    ll = j;
                    rr = k;
                }
            }
        }

        for (int i = N - 2; i >= 1; i--)
        {
            bool f = true;
            for (int j = ll - 1; j >= 0 && f; j--)
            {
                if (dp[i - 1, j, rr] && A[ll] - A[j] <= n)
                {
                    f = false;
                    l.Add(A[ll]);
                    ll = j;
                    break;
                }
            }

            if (f)
            {
                for (int k = rr - 1; k >= 0; k--)
                {
                    if (dp[i - 1, ll, k] && A[rr] - A[k] <= n)
                    {
                        r.Add(A[rr]);
                        rr = k;
                        break;
                    }
                }
            }
        }

        r.Add(A[0]);

        var ans = new List<int>();
        ans.AddRange(l);
        r.Reverse();
        ans.AddRange(r);
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
