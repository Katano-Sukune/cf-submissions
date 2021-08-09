using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int a = sc.NextInt();
        int b = sc.NextInt();
        int c = sc.NextInt();

        int w = Math.Min(a / 3, Math.Min(b / 2, c / 2));

        a -= w * 3;
        b -= w * 2;
        c -= w * 2;
        int max = int.MinValue;
        for (int be = 0; be < 7; be++)
        {
            int ta = a;
            int tb = b;
            int tc = c;
            int cnt = 0;
            bool f = true;
            for (int i = 0; f && i < 7; i++)
            {
                switch ((i + be) % 7)
                {
                    case 0:
                    case 3:
                    case 6:
                        if (ta == 0)
                        {
                            f = false;
                            break;
                        }
                        ta--;
                        cnt++;
                        break;
                    case 1:
                    case 5:
                        if (tb == 0)
                        {
                            f = false;
                            break;
                        }
                        tb--;
                        cnt++;
                        break;
                    case 2:
                    case 4:
                        if (tc == 0)
                        {
                            f = false;
                            break;
                        }
                        tc--;
                        cnt++;
                        break;
                }
            }


            max = Math.Max(max, cnt);
        }

        Console.WriteLine(w * 7 + max);
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
