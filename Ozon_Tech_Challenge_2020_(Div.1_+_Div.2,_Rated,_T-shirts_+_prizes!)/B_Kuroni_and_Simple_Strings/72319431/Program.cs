using System;
using System.Collections.Generic;
using System.Text;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        Console.WriteLine(Q(sc.Next()));
    }

    string Q(string s)
    {
        int l = s.Length;

        var o = new List<int>();
        var e = new List<int>();

        for (int i = 0; i < l; i++)
        {
            if (s[i] == '(') o.Add(i);
            if (s[l - i - 1] == ')') e.Add(l - i - 1);
        }

        int k = 1;
        int index = 0;
        List<int> ans = new List<int>();
        while (index < Math.Min(o.Count, e.Count) && o[index] < e[index])
        {
            ans.Add(o[index] + 1);
            ans.Add(e[index] + 1);
            index++;
        }

        ans.Sort();

        if (ans.Count == 0) return "0";
        
        return $"{k}\n{ans.Count}\n{string.Join(" ", ans)}";
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