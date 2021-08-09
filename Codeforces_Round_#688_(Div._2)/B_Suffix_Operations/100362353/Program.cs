using System;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
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
        int[] a = sc.IntArray();

        // 先頭iまでiに揃える,消したか?
        var f = new long[n, 2];
        for (int i = 0; i < n; i++)
        {
            if (i == 0)
            {
                f[0, 0] = 0;
                f[0, 1] = 0;
            }
            else if (i == 1)
            {
                f[1, 0] = f[0, 0] + Math.Abs(a[1] - a[0]);
                f[1, 1] = 0;
            }
            else
            {
                f[i, 0] = f[i - 1, 0] + Math.Abs(a[i] - a[i - 1]);
                f[i, 1] = Math.Min(f[i - 1, 1] + Math.Abs(a[i] - a[i - 1]), f[i - 2, 0] + Math.Abs(a[i] - a[i - 2]));
            }
        }

        var b = new long[n, 2];
        for (int i = n - 1; i >= 0; i--)
        {
            if (i == n - 1)
            {
                b[n - 1, 0] = 0;
                b[n - 1, 1] = 0;
            }
            else if (i == n - 2)
            {
                b[n - 2, 0] = b[i + 1, 0] + Math.Abs(a[i] - a[i + 1]);
                b[n - 2, 1] = 0;
            }
            else
            {
                b[i, 0] = b[i + 1, 0] + Math.Abs(a[i] - a[i + 1]);
                b[i, 1] = Math.Min(b[i + 1, 1] + Math.Abs(a[i] - a[i + 1]), b[i + 2, 0] + Math.Abs(a[i] - a[i + 2]));
            }
        }

        long ans = long.MaxValue;
        for (int i = 0; i < n; i++)
        {
            ans = Math.Min(ans, f[i, 0] + b[i, 0]);
            ans = Math.Min(ans, f[i, 1] + b[i, 0]);
            ans = Math.Min(ans, f[i, 0] + b[i, 1]);
        }

        Console.WriteLine(ans);
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