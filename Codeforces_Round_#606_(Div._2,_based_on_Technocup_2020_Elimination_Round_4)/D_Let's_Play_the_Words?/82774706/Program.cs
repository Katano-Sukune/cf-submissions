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
            int n = sc.NextInt();
            string[] s = new string[n];
            for (int j = 0; j < n; j++)
            {
                s[j] = sc.Next();
            }
            sb.AppendLine(Q(n, s));
        }
        Console.Write(sb);
    }

    string Q(int n, string[] s)
    {
        // 各文字列　しりとりになってる
        // 反転させる
        HashSet<string> hs = new HashSet<string>(StringComparer.Ordinal);
        foreach (var ss in s)
        {
            hs.Add(ss);
        }

        List<int>[] ls = new List<int>[4];
        int[] cnt = new int[4];
        for (int i = 0; i < 4; i++)
        {
            ls[i] = new List<int>();
        }
        for (int i = 0; i < n; i++)
        {

            string ss = s[i];
            string rev = new string(ss.Reverse().ToArray());
            int b = ss[0] - '0';
            int e = ss[ss.Length - 1] - '0';
            if (!hs.Contains(rev)) ls[b * 2 + e].Add(i + 1);
            cnt[b * 2 + e]++;
        }

        // 00 01 11 10 01 10...

        // 01 - 10 = 1

        // 10 - 01 = 1,0

        // 11 10 01 10 01 10 00 01 10 01

        if (cnt[0] > 0 && cnt[3] > 0 && (cnt[1] + cnt[2]) == 0) return "-1";

        List<int> a = new List<int>();

        // 01 10の数を揃える
        if (ls[1].Count < ls[2].Count)
        {
            int c = (cnt[2] - cnt[1]) / 2;
            if (c > ls[2].Count) return "-1";
            for (int i = 0; i < c; i++)
            {
                a.Add(ls[2][i]);
            }
        }
        else
        {
            int c = (cnt[1] - cnt[2]) / 2;
            if (c > ls[1].Count) return "-1";
            for (int i = 0; i < c; i++)
            {
                a.Add(ls[1][i]);
            }
        }


        return $"{a.Count}\n{string.Join(" ", a)}";



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
