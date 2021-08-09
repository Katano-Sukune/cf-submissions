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
            sb.AppendLine(Q(sc.NextInt(), sc.NextInt()));
        }
        Console.Write(sb);
    }

    string Q(int n, int k)
    {
        if (n % 2 == 1 && k % 2 == 0) return "NO";

        if (n < k) return "NO";
        var list = new List<int>();
        if (n % 2 == k % 2)
        {
            for (int i = 0; i < k - 1; i++)
            {
                list.Add(1);
            }
            list.Add(n - k + 1);
        }
        else
        {
            // 全部偶数
            if (n < 2 * k) return "NO";
            for (int i = 0; i < k - 1; i++)
            {
                list.Add(2);
            }
            list.Add(n - 2 * (k - 1));
        }
        return $"YES\n{string.Join(" ", list)}";
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
