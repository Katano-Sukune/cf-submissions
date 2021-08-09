using CompLib.Util;
using System;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int[] a = sc.IntArray();

        // 与えられた要素2つ選んで LCM 
        // 全部のGCD

        // 各素因数2番目
        long ans = 1;


        for (int i = 2; i * i <= Math.Max(a[0], a[1]); i++)
        {
            if (a[0] % i == 0 || a[1] % i == 0)
            {
                int first = int.MaxValue;
                int second = int.MaxValue;
                for (int j = 0; j < n; j++)
                {
                    int c = 0;
                    while (a[j] % i == 0)
                    {
                        c++;
                        a[j] /= i;
                    }

                    if (c < first)
                    {
                        second = first;
                        first = c;
                    }
                    else if (c < second)
                    {
                        second = c;
                    }
                }
                for (int j = 0; j < second; j++)
                {
                    ans *= i;
                }
            }
        }

        if (a[0] != 1)
        {
            int t = a[0];
            int first = int.MaxValue;
            int second = int.MaxValue;
            for (int j = 0; j < n; j++)
            {
                int c = 0;
                while (a[j] % t == 0)
                {
                    c++;
                    a[j] /= t;
                }

                if (c < first)
                {
                    second = first;
                    first = c;
                }
                else if (c < second)
                {
                    second = c;
                }
            }
            for (int j = 0; j < second; j++)
            {
                ans *= t;
            }
        }
        if (a[1] != 1)
        {
            int t = a[1];
            int first = int.MaxValue;
            int second = int.MaxValue;
            for (int j = 0; j < n; j++)
            {
                int c = 0;
                while (a[j] % t == 0)
                {
                    c++;
                    a[j] /= t;
                }

                if (c < first)
                {
                    second = first;
                    first = c;
                }
                else if (c < second)
                {
                    second = c;
                }
            }
            for (int j = 0; j < second; j++)
            {
                ans *= t;
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
