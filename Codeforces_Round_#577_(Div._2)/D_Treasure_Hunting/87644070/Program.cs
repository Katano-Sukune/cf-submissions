using System;
using System.Collections.Generic;
using System.Diagnostics;
using CompLib.Collections;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int m = sc.NextInt();
        int k = sc.NextInt();
        int q = sc.NextInt();

        int highest = -1;
        var rr = new long[n + 1];
        var ll = new long[n + 1];
        for (int i = 1; i <= n; i++)
        {
            rr[i] = long.MinValue;
            ll[i] = long.MaxValue;
        }

        for (int i = 0; i < k; i++)
        {
            int r = sc.NextInt();
            int c = sc.NextInt();
            highest = Math.Max(highest, r);

            rr[r] = Math.Max(rr[r], c);
            ll[r] = Math.Min(ll[r], c);
        }

        long o;
        long[] b = sc.LongArray();

        Array.Sort(b);
        var ls = new List<(long col, long cost)>();
        ls.Add((1, 0));
        for (int r = 1; r <= highest; r++)
        {
            var next = new List<(long col, long cost)>();
            foreach (var pair in ls)
            {
                if (rr[r] == long.MinValue)
                {
                    int ok = q;
                    int ng = -1;
                    while (ok - ng > 1)
                    {
                        int mid = (ok + ng) / 2;
                        if (pair.col <= b[mid]) ok = mid;
                        else ng = mid;
                    }

                    if (ok < q)
                    {
                        long key = b[ok];
                        long value = pair.cost + Math.Abs(b[ok] - pair.col);
                        next.Add((key, value));
                    }

                    if (!(ok < q && b[ok] == pair.col) && 0 <= ng)
                    {
                        long key = b[ng];
                        long value = pair.cost + Math.Abs(pair.col - b[ng]);
                        next.Add((key, value));
                    }

                    Debug.Assert(highest > r);
                }
                else
                {
                    {
                        // left -> right
                        int ok = q;
                        int ng = -1;
                        while (ok - ng > 1)
                        {
                            int mid = (ok + ng) / 2;
                            if (rr[r] <= b[mid]) ok = mid;
                            else ng = mid;
                        }

                        if (ok < q)
                        {
                            long key = r == highest ? rr[r] : b[ok];
                            long value = pair.cost + Math.Abs(pair.col - ll[r]) + Math.Abs(rr[r] - ll[r]) +
                                         Math.Abs(r == highest ? 0 : b[ok] - rr[r]);

                            next.Add((key, value));
                        }

                        if (!(ok < q && b[ok] == rr[r]) && 0 <= ng)
                        {
                            long key = r == highest ? rr[r] : b[ng];
                            long value = pair.cost + Math.Abs(pair.col - ll[r]) + Math.Abs(rr[r] - ll[r]) +
                                         Math.Abs(r == highest ? 0 : rr[r] - b[ng]);
                            next.Add((key, value));
                        }
                    }

                    {
                        // left -> right
                        int ok = q;
                        int ng = -1;
                        while (ok - ng > 1)
                        {
                            int mid = (ok + ng) / 2;
                            if (ll[r] <= b[mid]) ok = mid;
                            else ng = mid;
                        }

                        if (ok < q)
                        {
                            long key = r == highest ? ll[r] : b[ok];
                            long value = pair.cost + Math.Abs(pair.col - rr[r]) + Math.Abs(rr[r] - ll[r]) +
                                         Math.Abs(r == highest ? 0 : b[ok] - ll[r]);
                            next.Add((key, value));
                        }

                        if (!(ok < q && b[ok] == ll[r]) && 0 <= ng)
                        {
                            long key = r == highest ? ll[r] : b[ng];
                            long value = pair.cost + Math.Abs(pair.col - rr[r]) + Math.Abs(rr[r] - ll[r]) +
                                         Math.Abs(r == highest ? 0 : ll[r] - b[ng]);
                            next.Add((key, value));
                        }
                    }
                }
            }

            ls = new List<(long col, long cost)>();
            next.Sort((x, y) => x.col == y.col ? x.cost.CompareTo(y.cost) : x.col.CompareTo(y.col));
            for (int i = 0; i < next.Count; i++)
            {
                if (i == 0 || next[i - 1].col != next[i].col) ls.Add(next[i]);
            }

            //
            // Console.WriteLine("------");
            // Console.WriteLine(r);
            //
            // foreach (var p in map)
            // {
            //     Console.WriteLine($"{p.Key} {p.Value}");
            // }
        }

        long ans = long.MaxValue;
        foreach (var pair in ls)
        {
            ans = Math.Min(ans, pair.cost);
        }

        Console.WriteLine(ans + highest - 1);
    }

    public static void Main(string[] args) => new Program().Solve();
}

namespace CompLib.Collections
{
    using System.Collections.Generic;

    public class HashMap : Dictionary<int, long>
    {
        public new long this[int key]
        {
            get
            {
                long o;
                return TryGetValue(key, out o) ? o : long.MaxValue;
            }
            set { base[key] = value; }
        }
    }
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