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
        int l = sc.NextInt();
        var a = sc.IntArray();
        int[] rev = new int[n];
        for (int i = 0; i < n; i++)
        {
            rev[i] = l - a[n - i - 1];
        }

        // 現在地
        double f = 0;
        double s = 0;

        int fIdx = 0;
        int sIdx = 0;
        double time = 0;
        while (true)
        {
            double ft = fIdx < n ? (a[fIdx] - f) / (fIdx + 1) : Double.MaxValue;
            double st = sIdx < n ? (rev[sIdx] - s) / (sIdx + 1) : Double.MaxValue;

            if (ft < st)
            {
                if (f + ft * (fIdx + 1) + s + ft * (sIdx + 1) < l)
                {
                    f += ft * (fIdx + 1);
                    s += ft * (sIdx + 1);
                    time += ft;
                    fIdx++;
                }
                else
                {
                    time += (l - (s + f)) / (sIdx + fIdx + 2);
                    break;
                }
            }
            else
            {
                if (f + st * (fIdx + 1) + s + st * (sIdx + 1) < l)
                {
                    f += st * (fIdx + 1);
                    s += st * (sIdx + 1);
                    time += st;
                    sIdx++;
                }
                else
                {
                    
                    time += (l - (s + f)) / (sIdx + fIdx + 2);
                    break;
                }
            }
        }

        Console.WriteLine(time);
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