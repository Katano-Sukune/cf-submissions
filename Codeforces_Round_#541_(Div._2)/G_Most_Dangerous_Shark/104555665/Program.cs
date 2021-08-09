using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{
    int N, M;
    int[] K;
    int[][] A, C;

    int[] Domino;
    long[] Cost;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        K = new int[N];
        A = new int[N][];
        C = new int[N][];
        for (int i = 0; i < N; i++)
        {
            K[i] = sc.NextInt();
            A[i] = sc.IntArray();
            C[i] = sc.IntArray();
        }

        Domino = new int[M];
        Cost = new long[M];
        {
            int ptr2 = 0;
            int q = sc.NextInt();
            for (int i = 0; i < q; i++)
            {
                int id = sc.NextInt() - 1;
                int mul = sc.NextInt();
                for (int j = 0; j < K[id]; j++)
                {
                    Domino[ptr2] = A[id][j];
                    Cost[ptr2] = (long)C[id][j] * mul;
                    ptr2++;
                }
            }
        }
        var st = new PairII[M + 1];
        int ptr = 0;
        // iを左、右に倒したとき、倒れる最小、最大
        int[] ll = new int[M];
        {
            // iを倒すとlまで倒れる

            for (int i = 0; i < M; i++)
            {
                // iが倒せる左端
                int t = Math.Max(0, i - Domino[i] + 1);
                while (ptr > 0 && st[ptr - 1].F >= t)
                {
                    t = Math.Min(t, st[--ptr].S);
                }

                ll[i] = t;
                st[ptr++] = new PairII(i, t);
            }
        }
        ptr = 0;
        int[] rr = new int[M];
        {

            for (int i = M - 1; i >= 0; i--)
            {
                int t = Math.Min(M - 1, i + Domino[i] - 1);
                while (ptr > 0 && st[ptr - 1].F <= t)
                {
                    t = Math.Max(t, st[--ptr].S);
                }
                rr[i] = t;
                st[ptr++] = new PairII(i, t);
            }
        }

        //Console.WriteLine(string.Join(" ", ll));
        //Console.WriteLine(string.Join(" ", rr));

        // 先頭i個倒すコスト
        var dp = new long[M + 1];
        //for (int i = 1; i <= M; i++)
        //{
        //    dp[i] = long.MaxValue;
        //}

        // 先頭iまで倒すとコストc
        var st2 = new PairIL[M];
        ptr = 0;
        // st2.Push((0, 0));
        for (int i = 0; i < M; i++)
        {
            // iを左に倒す
            dp[i + 1] = dp[ll[i]] + Cost[i];

            // 右に倒す

            // iを連鎖で倒せない pop
            while (ptr > 0 && st2[ptr - 1].F < i) ptr--;

            // 連鎖で倒せるやつある
            if (ptr > 0) dp[i + 1] = Math.Min(dp[i + 1], st2[ptr - 1].S);

            if (ptr == 0 || dp[i] + Cost[i] < st2[ptr-1].S) st2[ptr++] = new PairIL(rr[i], dp[i] + Cost[i]);
        }

        Console.WriteLine(dp[M]);
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

struct PairII
{
    public int F, S;
    public PairII(int f, int s)
    {
        F = f;
        S = s;
    }
}

struct PairIL
{
    public int F;
    public long S;
    public PairIL(int f, long s)
    {
        F = f;
        S = s;
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
            if (_index >= _line.Length)
            {
                string s;
                do
                {
                    s = Console.ReadLine();
                } while (s.Length == 0);

                _line = s.Split(Separator);
                _index = 0;
            }

            return _line[_index++];
        }

        public string ReadLine()
        {
            _index = _line.Length;
            return Console.ReadLine();
        }

        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public decimal NextDecimal() => decimal.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            string s = Console.ReadLine();
            _line = s.Length == 0 ? new string[0] : s.Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
