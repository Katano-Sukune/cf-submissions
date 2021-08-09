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
        var q = new Queue<(long x, long y, List<int> ls)>[2];
        q[0] = new Queue<(long x, long y, List<int> ls)>();
        q[1] = new Queue<(long x, long y, List<int> ls)>();
        for (int i = 0; i < n; i++)
        {
            long x = sc.NextLong();
            long y = sc.NextLong();
            q[T(x, y)].Enqueue((x, y, new List<int>() {i + 1}));
        }

        while (q[0].Count > 1 || q[1].Count > 1)
        {
            for (int t = 0; t < 2; t++)
            {
                if (q[t].Count > 1)
                {
                    var t1 = q[t].Dequeue();
                    var t2 = q[t].Dequeue();
                    if (t1.ls.Count < t2.ls.Count)
                    {
                        var tmp = t1;
                        t1 = t2;
                        t2 = tmp;
                    }

                    long x1 = t1.x + t2.x;
                    long y1 = t1.y + t2.y;


                    long x2 = t1.x - t2.x;
                    long y2 = t1.y - t2.y;

                    if (x1 * x1 + y1 * y1 <= x2 * x2 + y2 * y2)
                    {
                        var ls = t1.ls;
                        foreach (int i in t2.ls)
                        {
                            ls.Add(i);
                        }

                        q[T(x1, y1)].Enqueue((x1, y1, ls));
                    }
                    else
                    {
                        var ls = t1.ls;
                        foreach (int i in t2.ls)
                        {
                            ls.Add(-i);
                        }

                        q[T(x2, y2)].Enqueue((x2, y2, ls));
                    }
                }
            }
        }

        if (q[0].Count == 1 && q[1].Count == 1)
        {
            var t1 = q[0].Dequeue();
            var t2 = q[1].Dequeue();
            if (t1.ls.Count < t2.ls.Count)
            {
                var tmp = t1;
                t1 = t2;
                t2 = tmp;
            }

            long x1 = t1.x + t2.x;
            long y1 = t1.y + t2.y;


            long x2 = t1.x - t2.x;
            long y2 = t1.y - t2.y;

            if (x1 * x1 + y1 * y1 <= x2 * x2 + y2 * y2)
            {
                var ls = t1.ls;
                foreach (int i in t2.ls)
                {
                    ls.Add(i);
                }

                q[T(x1, y1)].Enqueue((x1, y1, ls));
            }
            else
            {
                var ls = t1.ls;
                foreach (int i in t2.ls)
                {
                    ls.Add(-i);
                }

                q[T(x2, y2)].Enqueue((x2, y2, ls));
            }
        }

        int[] c = new int[n];
        if (q[0].Count == 0)
        {
            foreach (var i in q[1].Peek().ls)
            {
                c[Math.Abs(i) - 1] = Math.Sign(i);
            }
        }
        else if (q[1].Count == 0)
        {
            foreach (var i in q[0].Peek().ls)
            {
                c[Math.Abs(i) - 1] = Math.Sign(i);
            }
        }

        Console.WriteLine(string.Join(" ", c));
    }

    int T(long x, long y)
    {
        return Math.Abs(x) >= Math.Abs(y) ? 0 : 1;
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