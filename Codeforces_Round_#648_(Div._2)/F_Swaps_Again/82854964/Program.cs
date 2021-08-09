using System;
using System.Linq;
using System.Text;
using CompLib.Collections;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Q(sc.NextInt(), sc.IntArray(), sc.IntArray()));
        }

        Console.Write(sb);
    }

    string Q(int n, int[] a, int[] b)
    {
        // 移動の和が0なやつにできる

        if (n % 2 == 1)
        {
            if (a[n / 2] != b[n / 2]) return "No";
        }

        var map = new HashMap<(int, int), int>();
        for (int i = 0; i < n / 2; i++)
        {
            int minA = Math.Min(a[i], a[n - 1 - i]);
            int maxA = Math.Max(a[i], a[n - 1 - i]);
            map[(minA, maxA)]++;


            int minB = Math.Min(b[i], b[n - 1 - i]);
            int maxB = Math.Max(b[i], b[n - 1 - i]);
            map[(minB, maxB)]--;
        }
        foreach (var p in map)
        {
            if (p.Value != 0) return "No";
        }

        return "Yes";
    }

    public static void Main(string[] args) => new Program().Solve();
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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
                _index = 0;
            }

            return _line[_index++];
        }

        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public decimal NextDecimal() => decimal.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
