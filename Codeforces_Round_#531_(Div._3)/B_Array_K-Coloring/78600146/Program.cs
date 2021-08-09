using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int k = sc.NextInt();
        int[] a = sc.IntArray();

        // k色で塗る
        // i色で塗られたやつ全部値違う

        int[] index = new int[n];
        for (int i = 0; i < n; i++)
        {
            index[i] = i;
        }

        Array.Sort(index, (l, r) => a[l].CompareTo(a[r]));

        var hs = new HashSet<int>[k];
        for (int i = 0; i < k; i++)
        {
            hs[i] = new HashSet<int>();
        }
        int[] ans = new int[n];
        for (int i = 0; i < n; i++)
        {
            int t = a[index[i]];
            ans[index[i]] = (i % k) + 1;
            if (!hs[i % k].Add(t))
            {
                Console.WriteLine("NO");
                return;
            }
        }
        Console.WriteLine("YES");
        Console.WriteLine(string.Join(" ", ans));

    }

    public static void Main(string[] args) => new Program().Solve();
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
