using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Collections;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int I = sc.NextInt();
        int[] a = sc.IntArray();

        // K種類ある
        // 圧縮して各数 k = ceil(log K)
        // 合計 nk bit

        // すべての数を [l,r]に収める

        // 種類数最大

        int k = (I * 8) / n;

        // Iが大きい
        if (k > 25)
        {
            Console.WriteLine("0");
            return;
        }

        int K = 1 << k;

        var map = new HashMap<int, int>();
        foreach (int i in a)
        {
            map[i]++;
        }

        var ar = map.ToArray();
        if (K >= ar.Length)
        {
            Console.WriteLine("0");
            return;
        }

        Array.Sort(ar, (l, r) => l.Key.CompareTo(r.Key));

        long[] sum = new long[ar.Length + 1];
        for (int i = 0; i < ar.Length; i++)
        {
            sum[i + 1] = sum[i] + ar[i].Value;
        }

        long ans = long.MaxValue;
        for (int l = 0; l + K <= ar.Length; l++)
        {
            int r = l + K;
            long cost = sum[l] + (sum[ar.Length] - sum[r]);
            ans = Math.Min(ans, cost);
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