using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Collections;
using CompLib.Util;

public class Program
{
    private int N;
    private long K;
    private int[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextLong();
        A = sc.IntArray();

        int[] cnt;
        long[] ar;
        {
            var map = new HashMap<int, int>();
            foreach (int i in A)
            {
                map[i]++;
            }

            var kv = map.ToArray();
            Array.Sort(kv, (l, r) => l.Key.CompareTo(r.Key));
            cnt = new int[kv.Length];
            ar = new long[kv.Length];
            for (int i = 0; i < kv.Length; i++)
            {
                cnt[i] = kv[i].Value;
                ar[i] = kv[i].Key;
            }
        }
        int left = 0;
        int right = ar.Length - 1;

        while (left != right)
        {
            int ll = cnt[left];
            int rr = cnt[right];

            // Console.WriteLine($"{K} {dq.First()} {map[dq.First()]} {dq.Last()} {map[dq.Last()]}");
            if (ll < rr)
            {
                long d = ar[left];
                // 次
                long next = ar[left + 1];
                long dist = next - d;
                // 移動可能最大
                long max = K / ll;
                // Console.WriteLine($"d = {d} {dq.First()} {dist} {max}");

                if (dist <= max)
                {
                    cnt[left] -= ll;
                    cnt[left + 1] += ll;
                    left++;
                    K -= dist * ll;
                }
                else
                {
                    ar[left] = d + max;
                    K -= max * ll;
                }

                if (max == 0) break;
            }
            else
            {
                long d = ar[right];
                long prev = ar[right - 1];
                long dist = d - prev;
                long max = K / rr;

                if (dist <= max)
                {
                    cnt[right] -= rr;
                    cnt[right - 1] += rr;
                    right--;
                    K -= dist * rr;
                }
                else
                {
                    ar[right] = d - max;
                    K -= max * rr;
                }

                if (max == 0) break;
            }
        }

        Console.WriteLine(ar[right] - ar[left]);
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
            if (_index >= _line.Length)
            {
                string s = Console.ReadLine();
                while (s.Length == 0)
                {
                    s = Console.ReadLine();
                }

                _line = s.Split(Separator);
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