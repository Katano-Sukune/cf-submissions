using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int[] l = new int[n];
        int[] r = new int[n];
        for (int i = 0; i < n; i++)
        {
            l[i] = sc.NextInt();
            r[i] = sc.NextInt() + 1;
        }

        var ls = new List<int>();
        foreach (int i in l)
        {
            ls.Add(i);
        }

        foreach (int i in r)
        {
            ls.Add(i);
        }

        ls.Sort();

        var map = new Dictionary<int, int>();
        int k = 0;
        for (int i = 0; i < ls.Count; i++)
        {
            if (i == 0 || ls[i - 1] != ls[i])
            {
                map[ls[i]] = k++;
            }
        }

        var imos = new int[k];
        foreach (int i in l)
        {
            imos[map[i]]++;
        }

        foreach (int i in r)
        {
            imos[map[i]]--;
        }

        for (int i = 1; i < k; i++)
        {
            imos[i] += imos[i - 1];
        }

        foreach (int i in imos)
        {
            if (i > 2)
            {
                Console.WriteLine("NO");
                return;
            }
        }

        Console.WriteLine("YES");
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