using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Collections;
using CompLib.Util;

public class Program
{
    int N;
    long[] X;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        X = sc.LongArray();
        Array.Sort(X);

        var hs = new HashSet<long>();
        foreach (var i in X)
        {
            hs.Add(i);
        }

        foreach (var i in X)
        {
            for (long b = 1; i + b <= X[N - 1] && i - b >= X[0]; b *= 2)
            {
                if (hs.Contains(i + b) && hs.Contains(i - b))
                {
                    Console.WriteLine(3);
                    Console.WriteLine($"{i - b} {i} {i + b}");
                    return;
                }
            }
        }

        foreach (var i in X)
        {
            for (long b = 1; i - b >= X[0]; b *= 2)
            {
                if (hs.Contains(i - b))
                {
                    Console.WriteLine(2);
                    Console.WriteLine($"{i - b} {i}");
                    return;
                }
            }
        }
        Console.WriteLine("1");
        Console.WriteLine(X[0]);

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
