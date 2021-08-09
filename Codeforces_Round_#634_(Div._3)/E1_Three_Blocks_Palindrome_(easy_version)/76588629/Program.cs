using System;
using System.Collections.Generic;
using System.IO;
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
            int n = sc.NextInt();
            int[] a = sc.IntArray();
            sb.AppendLine(Q(n, a));
        }

        Console.Write(sb.ToString());
    }

    string Q(int n, int[] a)
    {
        var sum = new int[27, n + 1];
        var l = new List<int>[27];
        for (int i = 1; i <= 26; i++)
        {
            l[i] = new List<int>();
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 1; j <= 26; j++)
            {
                sum[j, i + 1] = sum[j, i];
            }

            sum[a[i], i + 1]++;
            l[a[i]].Add(i);
        }

        long ans = 0;
        for (int aa = 1; aa <= 26; aa++)
        {
            ans = Math.Max(ans, sum[aa, n]);
            for (int bb = 1; bb <= 26; bb++)
            {
                if (aa == bb) continue;
                for (int i = 0; i < l[aa].Count; i++)
                {
                    int j = l[aa].Count - 1 - i;
                    if (i >= j) break;
                    // [l[i]+1,l[j])

                    ans = Math.Max(ans, (i + 1) * 2 + sum[bb, l[aa][j]] - sum[bb, l[aa][i] + 1]);
                }
            }
        }

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