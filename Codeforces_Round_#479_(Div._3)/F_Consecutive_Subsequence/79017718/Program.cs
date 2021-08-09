using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Collections;
using CompLib.Util;

public class Program
{
    int N;
    int[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        int[] A = sc.IntArray();

        var map = new HashMap<int, int>();
        foreach (var i in A)
        {
            map[i] = Math.Max(map[i], map[i - 1] + 1);
        }

        int max = 0;
        int p = 0;
        foreach (var pair in map)
        {
            if (max < pair.Value)
            {
                max = pair.Value;
                p = pair.Key;
            }
        }
        Console.WriteLine(max);
        var l = new List<int>();
        int target = p - max + 1;
        for (int i = 0; i < N; i++)
        {
            if (A[i] == target)
            {
                l.Add(i + 1);
                target++;
            }
        }
        Console.WriteLine(string.Join(" ", l));
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
