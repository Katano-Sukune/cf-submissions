using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{
    private int N;
    private int[] A;
    private const int MaxA = 20;
    private const int L = 1 << MaxA;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.Array().Select(s => int.Parse(s) - 1).ToArray();

        // 同じ色のマーブルが連続した位置になるように隣合うマーブルを入れ替える
        // 最小何回か?


        // bit これまでにi個のやつを並べた
        // 並べてない奴の順番 並べた奴の


        // iの以前にあるjの転倒数
        long[,] sum = new long[MaxA, MaxA];
        int[] cnt = new int[MaxA];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < MaxA; j++)
            {
                sum[A[i], j] += cnt[j];
            }

            cnt[A[i]]++;
        }

        var dp = new long[L];
        for (int i = 1; i < L; i++)
        {
            dp[i] = long.MaxValue;
        }

        for (int i = 0; i < L; i++)
        {
            for (int j = 0; j < MaxA; j++)
            {
                int b = 1 << j;
                if ((i & b) > 0) continue;
                long p = 0;
                for (int k = 0; k < MaxA; k++)
                {
                    if(j == k) continue;
                    if ((i & (1 << k)) > 0) continue;
                    p += sum[j, k];
                }

                dp[i | b] = Math.Min(dp[i | b], dp[i] + p);
            }
        }

        Console.WriteLine(dp[L - 1]);
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