using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int a = sc.NextInt();
        int b = sc.NextInt();

        int[] x = new int[n];
        int[] y = new int[n];
        for (int i = 0; i < n; i++)
        {
            x[i] = sc.NextInt();
            y[i] = sc.NextInt();
        }

        int ans = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (F(a, b, x[i], y[i], x[j], y[j]))
                {
                    ans = Math.Max(ans, x[i] * y[i] + x[j] * y[j]);
                }
            }
        }

        Console.WriteLine(ans);
    }

    public bool F(int a, int b, int x1, int y1, int x2, int y2)
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    int xx1;
                    int yy1;

                    int xx2;
                    int yy2;
                    if (i == 0)
                    {
                        xx1 = x1;
                        yy1 = y1;
                    }
                    else
                    {
                        xx1 = y1;
                        yy1 = x1;
                    }

                    if (j == 0)
                    {
                        xx2 = x2;
                        yy2 = y2;
                    }
                    else
                    {
                        xx2 = y2;
                        yy2 = x2;
                    }

                    int xx;
                    int yy;
                    if (k == 0)
                    {
                        xx = xx1 + xx2;
                        yy = Math.Max(yy1, yy2);
                    }
                    else
                    {
                        xx = Math.Max(xx1, xx2);
                        yy = yy1 + yy2;
                    }

                    if (xx <= a && yy <= b)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
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