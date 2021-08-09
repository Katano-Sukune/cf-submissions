using System;
using System.Linq;
using CompLib.Collections;
using CompLib.Util;

public class Program
{
    int N;
    long K;
    long[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        A = sc.LongArray();

        var pp = new long[11];
        pp[0] = 1;
        for (int i = 1; i <= 10; i++)
        {
            pp[i] = pp[i - 1] * 10;
            pp[i] %= K;
        }
        // ずらす桁数、余り 数
        var map = new HashMap<long, int>[11];
        for (int i = 0; i <= 10; i++)
        {
            map[i] = new HashMap<long, int>();
        }
        foreach (var i in A)
        {
            long d = 10;
            for (int p = 1; p <= 10; p++)
            {
                map[p][(i * d) % K]++;
                d *= 10;
                d %= K;
            }
        }


        long ans = 0;
        foreach (var i in A)
        {
            string str = i.ToString();
            if (i % K == 0) ans += map[str.Length][0];
            else ans += map[str.Length][K - i % K];
            if ((pp[str.Length] * i + i) % K == 0) ans--;
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
