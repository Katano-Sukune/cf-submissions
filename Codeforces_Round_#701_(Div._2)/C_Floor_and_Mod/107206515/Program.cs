using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
#if !DEBUG
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
#endif
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        long x = sc.NextInt();
        long y = sc.NextInt();

        // 1 <= a <= x
        // 1 <= b <= y
        // floor(a/b) = a%b
        // を満たすx,yの個数

        // a = d*b+m
        // d = m

        long ans = 0;
        for (long d = 1; d * d <= x; d++)
        {
            long d2 = x / d;
            if (d < y + 1)
            {
                long ok = d;
                long ng = y + 1;
                while (ng - ok > 1)
                {
                    long mid = (ok + ng) / 2;
                    long a = d * mid + d;
                    if (a <= x) ok = mid;
                    else ng = mid;
                }

                ans += ok - d;
            }

            if (d2 < y + 1)
            {
                long ok = d2;
                long ng = y + 1;
                while (ng - ok > 1)
                {
                    long mid = (ok + ng) / 2;
                    long a = d2 * mid + d2;
                    if (a <= x) ok = mid;
                    else ng = mid;
                }

                ans += ok - d2;
            }
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