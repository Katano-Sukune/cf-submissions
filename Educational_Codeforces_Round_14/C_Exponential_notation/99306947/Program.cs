using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        var x = sc.Next().Split('.');

        string f, g;
        if (x.Length == 1)
        {
            f = x[0].TrimStart('0');
            g = "";
        }
        else
        {
            f = x[0].TrimStart('0');

            g = x[1].TrimEnd('0');
        }

        if (g == "")
        {
            // 整数
            if (f == "")
            {
                Console.WriteLine("0");
                return;
            }

            if (f.Length == 1)
            {
                Console.WriteLine(f);
                return;
            }

            int b = f.Length - 1;
            f = f.TrimEnd('0');

            var f0 = f[0];
            var f1 = f.Substring(1);
            if (f1.Length == 0)
            {
                Console.WriteLine($"{f0}E{b}");
            }
            else
            {
                Console.WriteLine($"{f0}.{f1}E{b}");
            }
        }
        else
        {
            if (f == "")
            {
                int mb = 0;
                for (int i = 0; i < g.Length; i++)
                {
                    if (g[i] != '0')
                    {
                        mb = i + 1;
                        break;
                    }
                }

                g = g.TrimStart('0');
                if (g.Length == 1)
                {
                    Console.WriteLine($"{g}E-{mb}");
                }
                else
                {
                    var g0 = g[0];
                    var g1 = g.Substring(1);
                    Console.WriteLine($"{g0}.{g1}E-{mb}");
                }
            }
            else if (f.Length == 1)
            {
                Console.WriteLine($"{f}.{g}");
            }
            else
            {
                var f0 = f[0];
                var f1 = f.Substring(1);
                Console.WriteLine($"{f0}.{f1}{g}E{f1.Length}");
            }
        }
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