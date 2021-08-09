using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int[] a = sc.IntArray();

        Array.Sort(a, (l, r) => r.CompareTo(l));

        var dp = new long[n, n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                for (int k = 0; k < n; k++)
                {
                    dp[i, j, k] = long.MaxValue;
                }
            }
        }
        dp[0, 0, 0] = 0;
        for (int i = 1; i < n; i++)
        {
            for (int j = 0; j < i; j++)
            {
                for (int k = 0; k < i; k++)
                {
                    long cur = dp[i - 1, j, k];
                    if (cur == long.MaxValue) continue;
                    if (i == n - 1)
                    {
                        dp[i, i, i] = Math.Min(dp[i, i, i], Math.Max(cur, Math.Max(a[j] - a[i], a[k] - a[i])));
                    }
                    else
                    {
                        dp[i, i, k] = Math.Min(dp[i, i, k], Math.Max(cur, a[j] - a[i]));
                        dp[i, j, i] = Math.Min(dp[i, j, i], Math.Max(cur, a[k] - a[i]));
                    }
                }
            }
        }

        var l = new List<int>();
        var r = new List<int>();
        int ll = n - 1;
        int rr = n - 1;

        for (int i = n - 1; i >= 0; i--)
        {
            if (i == n - 1)
            {
                bool flag = true;
                for (int j = 0; flag && j < i; j++)
                {
                    for (int k = 0; flag && k < i; k++)
                    {
                        long tmp = dp[i - 1, j, k];
                        if (tmp == long.MaxValue) continue;
                        if (Math.Max(tmp, Math.Max(a[j] - a[i], a[k] - a[i])) == dp[i, ll, rr])
                        {
                            ll = j;
                            rr = k;
                            l.Add(a[i]);
                            flag = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                if (ll == i)
                {
                    for (int j = 0; j < i; j++)
                    {
                        long tmp = dp[i - 1, j, rr];
                        if (tmp == long.MaxValue) continue;
                        if (Math.Max(tmp, a[j] - a[i]) == dp[i, ll, rr])
                        {
                            l.Add(a[i]);
                            ll = j;
                            break;
                        }
                    }
                }
                else
                {
                    for (int k = 0; k < i; k++)
                    {
                        long tmp = dp[i - 1, ll, k];
                        if (tmp == long.MaxValue) continue;
                        if (Math.Max(tmp, a[k] - a[i]) == dp[i, ll, rr])
                        {
                            r.Add(a[i]);
                            rr = k;
                            break;
                        }
                    }
                }
            }
        }
        r.Add(a[0]);
        var ans = new List<int>();
        ans.AddRange(l);
        r.Reverse();
        ans.AddRange(r);
        Console.WriteLine(string.Join(" ", ans));
        // Console.WriteLine(dp[n - 1, n - 1, n - 1]);
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
