using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private int[] A;

    private const int MaxA = 20;

    private long[,] Cost;

    private long[] DP;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();
        for (int i = 0; i < N; i++)
        {
            A[i]--;
        }

        // 最終的にiの後ろにjがいるとき転倒数への寄与
        Cost = new long[MaxA, MaxA];

        long[] cnt = new long[MaxA];
        foreach (int i in A)
        {
            for (int j = 0; j < MaxA; j++)
            {
                if (i == j) continue;
                Cost[i, j] += cnt[j];
            }

            cnt[i]++;
        }

        DP = new long[1 << MaxA];
        Array.Fill(DP, long.MaxValue);

        Console.WriteLine(Go((1 << MaxA) - 1));
    }

    long Go(long t)
    {
        if (t == 0) return 0;
        if (DP[t] != long.MaxValue) return DP[t];

        long res = long.MaxValue;
        for (int i = 0; i < MaxA; i++)
        {
            int b = 1 << i;
            if ((t & b) == 0) continue;
            long tmp = Go(t ^ b);
            for (int j = 0; j < MaxA; j++)
            {
                if (i == j || (t & (1 << j)) == 0) continue;
                tmp += Cost[j, i];
            }

            res = Math.Min(res, tmp);
        }

        DP[t] = res;
        return res;
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