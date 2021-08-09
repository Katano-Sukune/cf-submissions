using CompLib.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        var sb = new StringBuilder();
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Q(sc.NextInt(), sc.NextInt()));
        }

        Console.Write(sb.ToString());
    }

    private string Q(long n, long m)
    {
        /*
         * f(x) xの二進 1を含む部分文字列の数
         * 
         * 長さn 1はm個 のx f(x) の最大値
         * 
         * 0のみの部分文字列を最小化
         * 
         * 
         */

        long all = (long)(n + 1) * (n) / 2;

        long zero = n - m;

        long z = long.MaxValue;
        {
            // 端が0
            long d = zero / (m + 1);
            long mo = zero % (m + 1);
            // d+1が mo個 dがm+1-mo個
            long a = (d + 1) * (d + 2) / 2 * mo + d * (d + 1) / 2 * (m + 1 - mo);
            z = Math.Min(a, z);


        }
        if (m >= 1)
        {
            // 端　片方0 
            long d = zero / m;
            long mo = zero % m;
            long b = (d + 1) * (d + 2) / 2 * mo + d * (d + 1) / 2 * (m + 1 - mo);
            z = Math.Min(b, z);
        }
        if (m - 1 >= 1)
        {
            long d = zero / (m - 1);
            long mo = zero % (m - 1);
            // d+1が mo個 dがm+1-mo個
            long c = (d + 1) * (d + 2) / 2 * mo + d * (d + 1) / 2 * (m + 1 - mo);
            z = Math.Min(c, z);
        }

        return (all - z).ToString();
    }

    public static void Main(string[] args) => new Program().Solve();
}

namespace CompLib.Util
{
    using System;
    using System.Linq;
    class Scanner
    {
        private int _index;
        private string[] _line;
        const char separator = ' ';

        public Scanner()
        {
            _index = 0;
            _line = new string[0];
        }

        public string Next()
        {
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(separator);
                _index = 0;
            }
            return _line[_index++];
        }
        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(separator);
            _index = _line.Length;
            return _line;
        }
        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
    }
}