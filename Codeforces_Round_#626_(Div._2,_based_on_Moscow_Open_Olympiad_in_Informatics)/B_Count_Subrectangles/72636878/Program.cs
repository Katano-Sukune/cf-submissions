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
        int m = sc.NextInt();
        int k = sc.NextInt();

        int[] a = sc.IntArray();
        int[] b = sc.IntArray();

        var mapA = new HashMap<int, int>();
        {
            int cnt = 0;
            for (int i = 0; i < n; i++)
            {
                if (a[i] == 1)
                {
                    cnt++;
                }
                else if (cnt > 0)
                {
                    mapA[cnt]++;
                    cnt = 0;
                }
            }

            if (cnt > 0)
            {
                mapA[cnt]++;
            }
        }

        var mapB = new HashMap<int, int>();
        {
            int cnt = 0;
            for (int i = 0; i < m; i++)
            {
                if (b[i] == 1)
                {
                    cnt++;
                }
                else if (cnt > 0)
                {
                    mapB[cnt]++;
                    cnt = 0;
                }
            }

            if (cnt > 0)
            {
                mapB[cnt]++;
            }
        }

        List<Tuple<int, int>> p = new List<Tuple<int, int>>();
        for (int i = 1; i * i <= k; i++)
        {
            if (k % i == 0)
            {
                if (i * i != k)
                {
                    p.Add(new Tuple<int, int>(k / i, i));
                }

                p.Add(new Tuple<int, int>(i, k / i));
            }
        }

        long ans = 0;
        foreach (Tuple<int, int> tuple in p)
        {
            foreach (KeyValuePair<int, int> i in mapA)
            {
                if (i.Key < tuple.Item1) continue;
                foreach (KeyValuePair<int, int> j in mapB)
                {
                    if (j.Key < tuple.Item2) continue;
                    ans += (long)(i.Key - tuple.Item1 + 1) * ( j.Key - tuple.Item2 + 1) * i.Value * j.Value;
                }
            }
        }

        Console.WriteLine(ans);
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