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
        int q = sc.NextInt();
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < q; i++)
        {
            sb.AppendLine(Query(sc.NextInt(), sc.NextInt(), sc.IntArray()));

        }
        Console.Write(sb);
    }

    string Query(int n, int k, int[] a)
    {
        int odd = 0;
        foreach (var i in a)
        {
            if (i % 2 != 0) odd++;
        }

        if (odd < k) return "NO";
        if ((odd - k) % 2 != 0) return "NO";

        int d = odd - k;
        List<int> ans = new List<int>();
        for (int i = 0; i < n; i++)
        {
            if (a[i] % 2 != 0)
            {
                if (d == 0) ans.Add(i + 1);
                else d--;
            }
        }
        ans[ans.Count - 1] = n;
        return $"YES\n{string.Join(" ", ans) }";
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
