using System;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private int[] A;
    private int M;
    private int[] P;


    private const int Max = 3000000;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();
        M = sc.NextInt();
        P = sc.IntArray();

        Array.Sort(A);
        int[] cnt = new int[Max + 1];
        foreach (int i in A)
        {
            cnt[i]++;
        }

        // Max未満 iになるパターン数
        long[] t = new long[Max + 1];
        for (int i = 0; i < N;)
        {
            for (int j = 1; A[i] * j <= Max; j++)
            {
                t[A[i] * j] += cnt[A[i]] * cnt[j];
            }

            i += cnt[A[i]];
        }

        for (int i = 0; i < N && (long) A[i] * A[i] <= Max;)
        {
            t[A[i] * A[i]] -= cnt[A[i]];
            i += cnt[A[i]];
        }

        // i未満になるパターン
        long[] t2 = new long[Max + 1];
        for (int i = 0; i < Max; i++)
        {
            t2[i + 1] = t2[i] + t[i];
        }

        long[] ans = new long[M];
        for (int i = 0; i < M; i++)
        {
            ans[i] = (long) N * (N - 1) - t2[P[i]];
        }

        Console.WriteLine(string.Join("\n", ans));
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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