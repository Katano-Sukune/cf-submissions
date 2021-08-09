using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Q(sc.NextInt()));
        }
        Console.Write(sb);
    }

    string Q(int n)
    {
        // 隣接要素の差が [2,4]な長さnの順列
        if (n == 2 || n == 3) return "-1";

        // n奇数
        // 奇数、偶数に分ける
        // 1, 3 .... n, n-3, n-1,n-5,n-7...4,2 
        var ans = new List<int>();
        if (n % 2 == 1)
        {
            for (int i = 1; i <= n; i += 2) ans.Add(i);
            ans.Add(n - 3);
            ans.Add(n - 1);
            for (int i = n - 5; i > 0; i -= 2) ans.Add(i);
        }
        // n偶数
        else
        {
            // 2,4,...n,n-3,n-1,n-5,n-7...3,1
            for (int i = 2; i <= n; i += 2) ans.Add(i);
            ans.Add(n - 3);
            ans.Add(n - 1);
            for (int i = n - 5; i > 0; i -= 2) ans.Add(i);
        }
        return string.Join(" ", ans);
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
