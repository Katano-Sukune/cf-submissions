using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int m = sc.NextInt();
        int[] a = new int[m];
        int[] b = new int[m];
        for (int i = 0; i < m; i++)
        {
            a[i] = sc.NextInt();
            b[i] = sc.NextInt();
            if (a[i] > b[i]) (a[i], b[i]) = (b[i], a[i]);
        }

        {
            int x = a[0];
            int[] cnt = new int[n + 1];
            int t = 0;
            for (int i = 0; i < m; i++)
            {
                if (a[i] == x || b[i] == x) continue;
                cnt[a[i]]++;
                cnt[b[i]]++;
                t++;
            }

            if (cnt.Contains(t))
            {
                Console.WriteLine("YES");
                return;
            }
        }
        {
            int x = b[0];
            int[] cnt = new int[n + 1];
            int t = 0;
            for (int i = 0; i < m; i++)
            {
                if (a[i] == x || b[i] == x) continue;
                cnt[a[i]]++;
                cnt[b[i]]++;
                t++;
            }

            if (cnt.Contains(t))
            {
                Console.WriteLine("YES");
                return;
            }
        }

        Console.WriteLine("NO");


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
