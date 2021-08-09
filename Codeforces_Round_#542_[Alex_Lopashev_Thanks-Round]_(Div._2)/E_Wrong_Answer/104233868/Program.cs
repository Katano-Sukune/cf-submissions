using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{
    int K;
    public void Solve()
    {
        var sc = new Scanner();
        K = sc.NextInt();
        const int N = 2000;
        const int MaxA = 1000000;

        if (K <= MaxA / 3)
        {
            if (K % 2 == 0)
            {
                Console.WriteLine("2");
                Console.WriteLine($"{(K - MaxA) / 2} {MaxA}");
            }
            else
            {
                Console.WriteLine("2");
                Console.WriteLine($"{(K - (MaxA - 1)) / 2} {MaxA - 1}");
            }
            return;
        }

        for (int t = MaxA; t >= 1; t--)
        {
            int a = t - K % t;
            int l = K / t + 1;
            if (a < l) continue;
            if (t + a > MaxA) continue;
            if (t + a <= MaxA)
            {
                var ls = new List<int>();

                for (int i = 0; i < l - 1; i++)
                {
                    ls.Add(-1);
                }
                ls.Add(-(a - (l - 1)));
                ls.Add(a + t);
                Console.WriteLine(ls.Count);
                Console.WriteLine(string.Join(" ", ls));
                return;
            }
        }
        throw new Exception();
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
