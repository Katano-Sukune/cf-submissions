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
        decimal c = sc.NextDecimal();
        decimal m = sc.NextDecimal();
        decimal p = sc.NextDecimal();
        decimal v = sc.NextDecimal();
        Console.WriteLine(Go(c, m, p, v));
    }

    decimal Go(decimal c, decimal m, decimal p, decimal v)
    {
        // pを選ぶ 0回
        decimal pp = 0;

        // mを選ぶ
        decimal mm = 0;
        if (m > 0)
        {
            if (m <= v)
            {
                if (c > 0)
                {
                    mm = Go(c + m / 2, 0, p + m / 2, v) * m;
                }
                else
                {
                    mm = Go(0, 0, 1, v) * m;
                }
            }
            else
            {
                if (c > 0)
                {
                    mm = Go(c + v / 2, m - v, p + v / 2, v) * m;
                }
                else
                {
                    mm = Go(0, m - v, p + v, v) * m;
                }
            }
        }

        decimal cc = 0;
        if (c > 0)
        {
            if (c <= v)
            {
                if (m > 0)
                {
                    cc = Go(0, m + c / 2, p + c / 2, v) * c;
                }
                else
                {
                    cc = Go(0, 0, 1, v) * c;
                }
            }
            else
            {
                if (m > 0)
                {
                    cc = Go(c - v, m + v / 2, p + v / 2, v) * c;
                }
                else
                {
                    cc = Go(c - v, 0, c + v, v) * c;
                }
            }
        }

        return pp + mm + cc + 1;
    }

    // public static void Main(string[] args) => new Program().Solve();
    public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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
