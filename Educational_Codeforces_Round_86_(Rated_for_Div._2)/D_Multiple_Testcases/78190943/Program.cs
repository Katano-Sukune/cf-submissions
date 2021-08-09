using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{
    int N, K;
    int[] M;
    int[] C;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        M = sc.IntArray();
        C = new int[K + 1];
        Array.Sort(M, (l, r) => r.CompareTo(l));
        for (int i = 1; i <= K; i++)
        {
            C[i] = sc.NextInt();
        }
        // 初期配列
        // i番目のサイズ M_i
        // M_i <= K

        // 各テストケースに 初期配列を振り分ける

        // サイズ1以上の配列がc_1個より多く ... i以上の配列 c_i個含まれてちゃだめ

        List<List<int>> ans = new List<List<int>>();
        ans.Add(new List<int>());

        foreach (var i in M)
        {
            int ng = -1;
            int ok = ans.Count;
            while (ok - ng > 1)
            {
                int mid = (ok + ng) / 2;
                if (ans[mid].Count >= C[i]) ng = mid;
                else ok = mid;
            }
            if (ok == ans.Count)
            {
                ans.Add(new List<int>());
            }
            ans[ok].Add(i);
        }

        var sb = new StringBuilder();
        sb.AppendLine(ans.Count.ToString());
        for (int i = 0; i < ans.Count; i++)
        {
            sb.AppendLine($"{ans[i].Count} {string.Join(" ", ans[i])}");
        }
        Console.Write(sb);
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
