using System;
using CompLib.Collections;
using CompLib.Util;

public class Program
{
    int N, M;
    long K;
    long[][] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        K = sc.NextLong();
        A = new long[N][];
        for (int i = 0; i < N; i++)
        {
            A[i] = sc.LongArray();
        }

        // 0,0 -> n-1,m-1に最短で移動

        // 通ったところのxorはkに等しい
        // 何通りか?

        int s = ((N - 1) + (M - 1)) / 2;
        var fMap = new HashMap<long, long>[N, M];
        var bMap = new HashMap<long, long>[N, M];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                fMap[i, j] = new HashMap<long, long>();
                bMap[i, j] = new HashMap<long, long>();
            }
        }
        fMap[0, 0][A[0][0]] = 1;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                if (i + j >= s) continue;
                foreach (var pair in fMap[i, j])
                {
                    //  Console.WriteLine($"{pair.Key} {A[i+1][j]}");
                    if (i + 1 < N) fMap[i + 1, j][pair.Key ^ A[i + 1][j]] += pair.Value;
                    if (j + 1 < M) fMap[i, j + 1][pair.Key ^ A[i][j + 1]] += pair.Value;
                }
            }
        }
        bMap[N - 1, M - 1][0] = 1;
        for (int i = N - 1; i >= 0; i--)
        {
            for (int j = M - 1; j >= 0; j--)
            {
                if (i + j <= s) continue;
                foreach (var pair in bMap[i, j])
                {
                    if (0 <= i - 1) bMap[i - 1, j][pair.Key ^ A[i][j]] += pair.Value;
                    if (0 <= j - 1) bMap[i, j - 1][pair.Key ^ A[i][j]] += pair.Value;
                }
            }
        }
        long ans = 0;
        for (int i = 0; i < N; i++)
        {
            int j = s - i;
            // Console.WriteLine($"{i} {j} {j < 0 || M >= j}");
            if (j < 0 || M <= j) continue;

            // 0,0 -> i,jに行くパターン
            // i,j -> n-1,m-1に行くパターン
            foreach (var pair in fMap[i, j])
            {
                // Console.WriteLine($"{i} {j} {pair.Key} {pair.Key ^ K} {bMap[i, j][pair.Key ^ K]}");
                ans += pair.Value * bMap[i, j][pair.Key ^ K];
            }
        }
        Console.WriteLine(ans);
    }



    public static void Main(string[] args) => new Program().Solve();
}


namespace CompLib.Collections
{
    using System.Collections.Generic;
    public class HashMap<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public new TValue this[TKey key]
        {
            get
            {
                TValue o;
                return TryGetValue(key, out o) ? o : default(TValue);
            }
            set { base[key] = value; }
        }
    }
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
