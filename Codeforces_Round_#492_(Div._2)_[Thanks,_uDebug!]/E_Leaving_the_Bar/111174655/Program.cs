using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        var dq = new LinkedList<(long x, long y, List<int> ls)>();
        for (int i = 0; i < n; i++)
        {
            dq.AddLast((sc.NextLong(), sc.NextLong(), new List<int>() {i + 1}));
        }

        while (dq.Count > 2)
        {
            var t = new (long x, long y, List<int> ls)[3];
            for (int i = 0; i < 3; i++)
            {
                t[i] = dq.First();
                dq.RemoveFirst();
            }

            bool f = true;
            for (int i = 0; f && i < 3; i++)
            {
                for (int j = i + 1; f && j < 3; j++)
                {
                    long x1 = t[i].x + t[j].x;
                    long y1 = t[i].y + t[j].y;

                    long x2 = -t[i].x + t[j].x;
                    long y2 = -t[i].y + t[j].y;

                    long d1 = x1 * x1 + y1 * y1;
                    long d2 = x2 * x2 + y2 * y2;

                    if (d1 <= (long) 1e12)
                    {
                        var l = t[j].ls;
                        foreach (int k in t[i].ls)
                        {
                            l.Add(k);
                        }

                        dq.AddFirst(t[3 - (i + j)]);
                        dq.AddLast((x1, y1, l));
                        f = false;
                    }
                    else if (d2 <= (long) 1e12)
                    {
                        var l = t[j].ls;
                        foreach (int k in t[i].ls)
                        {
                            l.Add(-k);
                        }

                        dq.AddFirst(t[3 - (i + j)]);
                        dq.AddLast((x2, y2, l));
                        f = false;
                    }
                }
            }
        }

        if (dq.Count > 1)
        {
            var t = new (long x, long y, List<int> ls)[2];
            for (int k = 0; k < 2; k++)
            {
                t[k] = dq.First();
                dq.RemoveFirst();
            }

            int i = 0;
            int j = 1;
            long x1 = t[i].x + t[j].x;
            long y1 = t[i].y + t[j].y;

            long x2 = -t[i].x + t[j].x;
            long y2 = -t[i].y + t[j].y;

            long d1 = x1 * x1 + y1 * y1;
            long d2 = x2 * x2 + y2 * y2;

            if (d1 <= d2)
            {
                var l = t[j].ls;
                foreach (int k in t[i].ls)
                {
                    l.Add(k);
                }

                dq.AddLast((x1, y1, l));
            }
            else
            {
                var l = t[j].ls;
                foreach (int k in t[i].ls)
                {
                    l.Add(-k);
                }

                dq.AddLast((x2, y2, l));
            }
        }

        // Console.WriteLine($"{dq.First().x} {dq.First().y}");
        var ans = dq.First().ls;

        int[] c = new int[n];
        foreach (int i in ans)
        {
            c[Math.Abs(i) - 1] = Math.Sign(i);
        }

        Console.WriteLine(string.Join(" ", c));
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