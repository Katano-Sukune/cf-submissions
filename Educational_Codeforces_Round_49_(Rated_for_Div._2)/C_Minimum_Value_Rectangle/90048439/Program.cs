using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Collections;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        int[] a = sc.IntArray();

        /*
         * 棒n本
         * 
         * 4本選んで長方形作る
         * 
         * Sを面積、Pを周長
         * 
         * P^2/Sが最小な長方形
         */

        /*
         * P = 2(x+y)
         * S = x*y
         * 
         * P^2/S = 4(x+y)^2/(x*y)
         * 
         * 
         */

        Array.Sort(a);
        var ls = new List<long>();
        int cnt = 0;
        for(int i = 0;i < n; i++)
        {
            if((i > 0 && a[i-1] != a[i]))
            {
                cnt = 0;
            }
            cnt++;
            if (cnt >= 2)
            {
                ls.Add(a[i]);
                cnt -= 2;
            }
        }

        long x = ls[0];
        long y = ls[1];
        for(int i = 1;i < ls.Count - 1; i++)
        {
            long pa2 = 4 * (x + y) * (x + y);
            long sa = x * y;

            long pb2 = 4 * (ls[i] + ls[i + 1]) * (ls[i] + ls[i + 1]);
            long sb = ls[i] * ls[i + 1];

            // pa/sa > pb / sb
            // pa * sb > pb*sa

            if(pb2 * sa < pa2 * sb)
            {
                x = ls[i];
                y = ls[i + 1];
            }
        }

        Console.WriteLine($"{x} {x} {y} {y}");
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
