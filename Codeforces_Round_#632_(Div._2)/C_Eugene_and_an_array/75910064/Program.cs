using System;
using System.Collections.Generic;
using CompLib.Collections;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int[] a = sc.IntArray();

        int[] r = new int[n + 1];
        r[n] = n + 1;
        var map = new Dictionary<long, int>();
        map[0] = n;
        long sum = 0;
        for (int i = n - 1; i >= 0; i--)
        {
            sum += a[i];
            r[i] = r[i + 1];
            if (map.TryGetValue(sum, out int o))
            {
                r[i] = Math.Min(r[i], o);
            }

            map[sum] = i;
        }

        long cnt = 0;
        for (int i = 0; i < n; i++)
        {
            cnt += n - r[i] + 1;
        }

        Console.WriteLine((long) (n + 1) * n / 2 - cnt);
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