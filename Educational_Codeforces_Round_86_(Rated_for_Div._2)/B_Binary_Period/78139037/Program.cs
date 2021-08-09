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
        int n = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < n; i++)
        {
            sb.AppendLine(Q(sc.Next()));
        }
        Console.Write(sb);
    }

    string Q(string t)
    {
        // sは　0,1のみ
        // |s| <= 2|t|
        // t ∈ s

        // sは周期 kをもつ k最小

        var f = t[0];
        bool flag = true;
        for (int i = 0; i < t.Length; i++)
        {
            if (t[i] != f)
            {
                flag = false;
                break;
            }
        }

        if (flag) return t;

        List<char> ans = new List<char>();
        ans.Add(f);
        for (int i = 1; i < t.Length; i++)
        {
            if (ans[ans.Count - 1] == t[i])
            {
                ans.Add(t[i] == '0' ? '1' : '0');
            }
            ans.Add(t[i]);
        }
        return new string(ans.ToArray());
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
