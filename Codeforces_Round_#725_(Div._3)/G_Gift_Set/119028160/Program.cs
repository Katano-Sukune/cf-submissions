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
#if !DEBUG
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
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
        long a = sc.NextInt();
        long b = sc.NextInt();

        if (a > b) (a, b) = (b, a);

        // a*x1 + b*y1 <= x
        // a*y1 + b*x1 <= y

        long ok = 0;
        long ng = x + y;
        while (ng - ok > 1)
        {
            long mid = (ok + ng) / 2;

            // a * x + b * (mid - x) <= x
            // a * (mid - x) + b * x <= y
            // a * mid + b * mid <= (x + y)

            if (a * mid > Math.Min(x, y))
            {
                ng = mid;
                continue;
            }
            long ng1 = -1;
            long ok1 = mid;
            while (ok1 - ng1 > 1)
            {
                long mid1 = (ok1 + ng1) / 2;
                if (a * mid1 + b * (mid - mid1) <= x) ok1 = mid1;
                else ng1 = mid1;
            }

            // ok1以上ならx以下になる

            long ng2 = mid + 1;
            long ok2 = 0;
            while (ng2 - ok2 > 1)
            {
                long mid2 = (ok2 + ng2) / 2;
                if (a * (mid - mid2) + b * mid2 <= y) ok2 = mid2;
                else ng2 = mid2;
            }

            // ok2以下ならy以下になる
            if (ok1 <= ok2) ok = mid;
            else ng = mid;

            // Console.WriteLine($"{mid} {ok1}:{a * ok + b * (mid - ok)} {ok2}");
        }

        Console.WriteLine(ok);
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