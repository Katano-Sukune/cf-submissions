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
        long sum = 0;
        int[] index = new int[n];
        for (int i = 0; i < n; i++)
        {
            index[i] = i;
        }
        int[] a = sc.IntArray();
        Array.Sort(index, (l, r) => a[r].CompareTo(a[l]));
        foreach (var i in a)
        {
            sum += i;
        }
        var ans = new List<int>();
        // maxを削除
        if (sum - a[index[0]] - a[index[1]] == a[index[1]])
        {
            for (int i = 0; i < n; i++)
            {
                if (a[i] == a[index[0]]) ans.Add(i + 1);
            }
        }

        // max以外を削除
        for(int i = 0; i < n; i++)
        {
            if (a[i] == a[index[0]]) continue;
            if (sum - a[index[0]] - a[i] == a[index[0]]) ans.Add(i + 1);
        }
        Console.WriteLine(ans.Count);
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
