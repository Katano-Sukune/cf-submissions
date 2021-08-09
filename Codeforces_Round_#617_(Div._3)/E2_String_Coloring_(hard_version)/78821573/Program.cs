using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    char[] S;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        S = sc.NextCharArray();

        // Sを色塗る　最小

        // 違う色で塗られた隣接する文字を入れ替える
        // Sをソート

        // 同じ色なら並び替えできない

        // 最小の増加列に分ける

        var list = new List<List<int>>();
        for (int i = 0; i < N; i++)
        {
            int ng = -1;
            int ok = list.Count;
            while (ok - ng > 1)
            {
                int mid = (ok + ng) / 2;
                var l = list[mid];
                if (S[l[l.Count - 1]] <= S[i]) ok = mid;
                else ng = mid;
            }
            if (ok == list.Count) list.Add(new List<int>());
            list[ok].Add(i);
        }
        int res = list.Count;
        var ans = new int[N];
        for (int i = 0; i < list.Count; i++)
        {
            foreach (var j in list[i])
            {
                ans[j] = i + 1;
            }
        }
        Console.WriteLine(res);
        Console.WriteLine(string.Join(" ", ans));
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
