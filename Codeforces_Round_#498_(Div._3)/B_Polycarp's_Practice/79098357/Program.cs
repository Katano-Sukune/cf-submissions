using System;
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
        var index = new int[n];
        for (int i = 0; i < n; i++)
        {
            index[i] = i;
        }

        Array.Sort(index, (l, r) => a[r].CompareTo(a[l]));

        var f = new bool[n];

        long p = 0;
        for (int i = 0; i < k; i++)
        {
            p += a[index[i]];
            f[index[i]] = true;
        }

        int[] ans = new int[k];
        int cnt = 0;
        int ii = 0;
        for (int i = 0; i < n; i++)
        {
            cnt++;
            if (f[i])
            {
                ans[ii++] = cnt;
                cnt = 0;
            }
        }
        ans[k - 1] += cnt;
        Console.WriteLine(p);
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
