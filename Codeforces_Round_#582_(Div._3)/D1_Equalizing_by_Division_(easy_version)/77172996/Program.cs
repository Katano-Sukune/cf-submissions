using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, K;
    int[] A;
    int Ans = int.MaxValue;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        A = sc.IntArray();
        var ls = A.ToList();
        ls.Sort();
        DFS(ls, 1);
        Console.WriteLine(Ans);
    }


    void DFS(List<int> L, int i)
    {
        if (L.Count < K) return;
        int[] cnt = new int[L.Count];
        for (int j = 0; j < L.Count; j++)
        {
            int c = 0;
            int tmp = L[j];
            while (tmp > 0)
            {
                c++;
                tmp /= 2;
            }
            cnt[j] = c;
        }
        {
            int cost = 0;
            for (int j = 0; j < K; j++)
            {
                cost += cnt[j] - i;
            }
            Ans = Math.Min(Ans, cost);
        }
        // i桁以上は一致している
        // 上位i桁を見る
        // 0のやつ1のやつに振り分ける

        List<int> zero = new List<int>();
        List<int> one = new List<int>();
        for (int j = 0; j < L.Count; j++)
        {
            int ll = cnt[j] - i - 1;
            if (ll < 0) continue;
            if ((L[j] & (1 << ll)) == 0) zero.Add(L[j]);
            else one.Add(L[j]);
        }
        DFS(zero, i + 1);
        DFS(one, i + 1);
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
