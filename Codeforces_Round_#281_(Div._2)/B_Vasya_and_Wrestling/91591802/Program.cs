using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        string first = "first";
        string second = "second";
        int n = sc.NextInt();
        int[] a = new int[n];

        var l = new List<int>();
        var r = new List<int>();

        for (int i = 0; i < n; i++)
        {
            a[i] = sc.NextInt();
        }

        long sum = 0;
        foreach (var i in a)
        {
            if (i > 0) l.Add(i);
            else r.Add(-i);
            sum += i;
        }

        if (sum > 0)
        {
            Console.WriteLine(first);
            return;
        }
        if (sum < 0)
        {
            Console.WriteLine(second);
            return;
        }

        for (int i = 0; i < Math.Min(l.Count, r.Count); i++)
        {
            if (l[i] > r[i])
            {
                Console.WriteLine(first);
                return;
            }
            if (l[i] < r[i])
            {
                Console.WriteLine(second);
                return;
            }
        }
        if (l.Count > r.Count)
        {
            Console.WriteLine(first);
            return;
        }
        if (l.Count < r.Count)
        {
            Console.WriteLine(second);
            return;
        }

        Console.WriteLine(a[n - 1] > 0 ? first : second);

    }

    public static void Main(string[] args) => new Program().Solve();
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
