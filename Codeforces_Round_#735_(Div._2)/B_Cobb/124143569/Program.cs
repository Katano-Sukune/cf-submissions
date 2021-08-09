using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
#if !DEBUG
        System.Console.SetOut(new System.IO.StreamWriter(System.Console.OpenStandardOutput()) { AutoFlush = false });
#endif
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        System.Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        int k = sc.NextInt();
        long[] a = sc.LongArray();


        long ans = long.MinValue;
        for (long i = Math.Max(0, n - 5000); i < n; i++)
        {
            for (long j = i + 1; j < n; j++)
            {
                ans = Math.Max(ans, (i + 1) * (j + 1) - k * (a[i] | a[j]));
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
