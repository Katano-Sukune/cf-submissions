using System;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Collections;

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
        int[] p = sc.IntArray();

        var map = new HashMap<int, int>();
        foreach (int i in p)
        {
            map[i]++;
        }

        var ar = map.ToArray();
        Array.Sort(ar, (l, r) => -l.Key.CompareTo(r.Key));

        int sum = 0;
        for (int i = 0; i < ar.Length; i++)
        {
            if (sum + ar[i].Value > n / 2)
            {
                // 前i個
                if (i < 3)
                {
                    Console.WriteLine("0 0 0");
                    return;
                }

                int g = ar[0].Value;
                int ptr = 1;
                int s = 0;
                while (ptr < i && s <= g)
                {
                    s += ar[ptr++].Value;
                }

                int b = 0;
                for (; ptr < i; ptr++)
                {
                    b += ar[ptr].Value;
                }

                if (b <= g)
                {
                    Console.WriteLine("0 0 0");
                    return;
                }

                Console.WriteLine($"{g} {s} {b}");
                return;
            }

            sum += ar[i].Value;
        }

        Console.WriteLine("0 0 0");
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

namespace CompLib.Collections
{
    using System.Collections.Generic;

    public class HashMap<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public new TValue this[TKey key]
        {
            get
            {
                TValue o;
                return TryGetValue(key, out o) ? o : default(TValue);
            }
            set { base[key] = value; }
        }
    }
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