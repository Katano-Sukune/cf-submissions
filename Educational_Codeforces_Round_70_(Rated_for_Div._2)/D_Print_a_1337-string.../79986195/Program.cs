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
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < t; i++)
        {
            Console.WriteLine(Q(sc.NextInt()));
        }
        Console.Out.Flush();
    }

    string Q(long n)
    {
        // 部分文字列1337がn個ある文字列

        // 111111...333333....77777
        // 1の数 * 3の数 C 2 * 7の数

        // 一番うしろに7
        var tmp = new List<int>();
        for (int i = 100000; i >= 2; i--)
        {
            long l = (long)(i - 1) * i / 2;
            while (n >= l)
            {
                tmp.Add(i);
                n -= l;
            }
        }

        var ans = new StringBuilder();
        ans.Append('1');
        for (int i = 0; i < tmp.Count - 1; i++)
        {
            ans.Append('3', tmp[i] - tmp[i + 1]);
            ans.Append('1');
        }
        ans.Append('3', tmp[tmp.Count - 1]);
        ans.Append('7');
        return ans.ToString();
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
