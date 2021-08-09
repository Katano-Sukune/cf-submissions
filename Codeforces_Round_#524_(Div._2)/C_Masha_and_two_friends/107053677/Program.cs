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
        long n = sc.NextInt();
        long m = sc.NextInt();

        long x1 = sc.NextInt();
        long y1 = sc.NextInt();
        long x2 = sc.NextInt() + 1;
        long y2 = sc.NextInt() + 1;

        long x3 = sc.NextInt();
        long y3 = sc.NextInt();
        long x4 = sc.NextInt() + 1;
        long y4 = sc.NextInt() + 1;

        long w, b;
        b = n * m / 2;
        w = n * m - b;

        // 白
        // 範囲内の黒の個数
        long tmp1;
        if ((x1 + y1) % 2 == 0)
        {
            tmp1 = (x2 - x1) * (y2 - y1) / 2;
        }
        else
        {
            tmp1 = (x2 - x1) * (y2 - y1) - (x2 - x1) * (y2 - y1) / 2;
        }

        b -= tmp1;
        w += tmp1;

        // 黒
        // 白がなかったとき
        // 範囲内の白の個数
        long tmp2;
        if ((x3 + y3) % 2 == 0)
        {
            tmp2 = (x4 - x3) * (y4 - y3) - (x4 - x3) * (y4 - y3) / 2;
        }
        else
        {
            tmp2 = (x4 - x3) * (y4 - y3) / 2;
        }


        w -= tmp2;
        b += tmp2;
        // 共通部分

        long x5 = Math.Max(x1, x3);
        long y5 = Math.Max(y1, y3);

        long x6 = Math.Min(x2, x4);
        long y6 = Math.Min(y2, y4);

        if (x5 < x6 && y5 < y6)
        {
            // 範囲内の黒の個数
            long tmp3;
            if ((x5 + y5) % 2 == 0)
            {
                tmp3 = (x6 - x5) * (y6 - y5) / 2;
            }
            else
            {
                tmp3 = (x6 - x5) * (y6 - y5) - (x6 - x5) * (y6 - y5) / 2;
            }

            w -= tmp3;
            b += tmp3;
        }

        Console.WriteLine($"{w} {b}");
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