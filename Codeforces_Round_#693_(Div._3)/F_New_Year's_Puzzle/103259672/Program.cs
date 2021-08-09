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
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        int m = sc.NextInt();
        (int r, int c)[] block = new (int r, int c)[m];
        for (int i = 0; i < m; i++)
        {
            block[i] = (sc.NextInt() - 1, sc.NextInt() - 1);
        }

        Array.Sort(block, (l, r) => l.c.CompareTo(r.c));

        List<(int c, int s)> ls = new List<(int c, int s)>(m);
        for (int i = 0; i < m; i++)
        {
            if (i + 1 < m && block[i].c == block[i + 1].c)
            {
                ls.Add((block[i].c, 3));
                i++;
            }
            else
            {
                ls.Add((block[i].c, 1 << block[i].r));
            }
        }

        int s = 3;
        int p = -1;
        for (int i = 0; i < ls.Count; i++)
        {
            if (s == 3)
            {
                s = ls[i].s;
                p = ls[i].c;
            }
            else
            {
                int d = ls[i].c - p;
                if (d % 2 == 0)
                {
                    if(((s & ls[i].s) != 0) || ((s | ls[i].s) != 3))
                    {
                        Console.WriteLine("NO");
                        return;
                    }
                    s = 3;
                }
                else
                {
                    if(s != ls[i].s)
                    {
                        Console.WriteLine("NO");
                        return;
                    }
                    s = 3;
                }
                p = ls[i].c;
            }
        }
        if (s != 3)
        {
            Console.WriteLine("NO");
            return;
        }

        Console.WriteLine("YES");
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
