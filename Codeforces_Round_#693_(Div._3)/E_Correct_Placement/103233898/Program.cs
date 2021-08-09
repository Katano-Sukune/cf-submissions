using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;

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
        (int h, int w, int idx)[] f = new (int h, int w, int idx)[n];
        for (int i = 0; i < n; i++)
        {
            int h = sc.NextInt();
            int w = sc.NextInt();
            if (h > w)
            {
                (h, w) = (w, h);
            }
            f[i] = (h, w, i);
        }

        Array.Sort(f, (l, r) => l.w == r.w ? l.h.CompareTo(r.h) : l.w.CompareTo(r.w));

        int[] ans = new int[n];

        int minH = int.MaxValue;
        int idx = -1;

        int tmpMinH = int.MaxValue;
        int tmpIdx = -1;

        for (int i = 0; i < n; i++)
        {
            if (i > 0 && f[i - 1].w != f[i].w)
            {
                minH = tmpMinH;
                idx = tmpIdx;
            }

            if (minH < f[i].h)
            {
                ans[f[i].idx] = idx + 1;
            }
            else
            {
                ans[f[i].idx] = -1;
            }
            
            if (f[i].h < tmpMinH)
            {
                ans[f[i].idx] = -1;
                tmpMinH = f[i].h;
                tmpIdx = f[i].idx;
            }
        }

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
