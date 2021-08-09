using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        (int x, int y)[] a = new (int x, int y)[4];
        for (int i = 0; i < 4; i++)
        {
            a[i] = (sc.NextInt(), sc.NextInt());
        }

        (int x, int y)[] b = new (int x, int y)[4];
        for (int i = 0; i < 4; i++)
        {
            b[i] = (sc.NextInt(), sc.NextInt());
        }

        for (int i = -100; i <= 100; i++)
        {
            for (int j = -100; j <= 100; j++)
            {
                bool f1;
                var p1 = a[0];
                var p2 = a[2];
                f1 = F(p1.x, p2.x, i) && F(p1.y, p2.y, j);

                bool f2;


                if (b[0].x == b[2].x)
                {
                    int d = Math.Min(Math.Abs(b[0].y - j), Math.Abs(b[2].y - j));
                    f2 = F(b[0].y, b[2].y, j) && F(b[0].x - d, b[0].x + d, i);
                }
                else
                {
                    int d = Math.Min(Math.Abs(b[0].x - i), Math.Abs(b[2].x - i));
                    f2 = F(b[0].x, b[2].x, i) && F(b[0].y - d, b[0].y + d, j);
                }

                if (f1 && f2)
                {
                    Console.WriteLine("YES");
                    return;
                }
            }
        }

        Console.WriteLine("NO");
    }

    bool F(int a, int b, int c)
    {
        return (a <= c && c <= b) || (b <= c && c <= a);
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