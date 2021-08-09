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
        int h = sc.NextInt();
        int m = sc.NextInt();

        int hh, mm;
        var t = sc.Next().Split(':');
        hh = int.Parse(t[0]);
        mm = int.Parse(t[1]);

        int time = hh * m + mm;

        int[] mirror = new int[] {0, 1, 5, -1, -1, 2, -1, -1, 8, -1};
        for (; time < h * m; time++)
        {
            string h2 = string.Format("{0:D2}", time / m);
            string m2 = string.Format("{0:D2}", time % m);

            if (mirror[h2[0] - '0'] == -1 || mirror[h2[1] - '0'] == -1) continue;
            if (mirror[m2[0] - '0'] == -1 || mirror[m2[1] - '0'] == -1) continue;

            int h3 = int.Parse($"{mirror[m2[1] - '0']}{mirror[m2[0] - '0']}");
            int m3 = int.Parse($"{mirror[h2[1] - '0']}{mirror[h2[0] - '0']}");

            int time2 = h3 * m + m3;
            
            if (time2 < h * m && h3 < h && m3 < m)
            {
                Console.WriteLine($"{h2:D2}:{m2:D2}");
                return;
            }
        }


        Console.WriteLine($"00:00");
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