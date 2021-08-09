using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Collections;

public class Program
{
    private int N, Y1;
    private long[] A;
    private int M, Y2;
    private long[] B;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        Y1 = sc.NextInt();
        A = sc.LongArray();

        M = sc.NextInt();
        Y2 = sc.NextInt();
        B = sc.LongArray();

        // A1 - B1 = 1,2,4,8,16,

        int ans = 0;
        for (int d = 0; d <= 40; d++)
        {
            long p = 1L << d;
            var cnt = new HashMap<long, int>();
            foreach (long i in A)
            {
                long m = i % (2 * p);
                cnt[m]++;
            }

            foreach (long i in B)
            {
                long m = (i + p) % (2 * p);
                cnt[m]++;
            }

            foreach (KeyValuePair<long, int> pair in cnt)
            {
                ans = Math.Max(ans, pair.Value);
            }
        }

        var cnt2 = new HashMap<long, int>();
        foreach (long l in A)
        {
            cnt2[l]++;
        }

        foreach (long l in B)
        {
            cnt2[l]++;
        }

        foreach (KeyValuePair<long,int> pair in cnt2)
        {
            ans = Math.Max(ans, pair.Value);
        }
        
        Console.WriteLine(ans);
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