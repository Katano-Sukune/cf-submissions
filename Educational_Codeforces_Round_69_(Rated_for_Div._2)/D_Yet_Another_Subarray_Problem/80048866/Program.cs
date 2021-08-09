using System;
using CompLib.Util;

public class Program
{
    int N, M;
    long K;
    long[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        K = sc.NextLong();
        A = sc.LongArray();

        // r-l+1をmで割った余りで分ける

        // rについて lをmで割った余りで分ける
        long ans = 0;

        var min = new long[M];
        for (int i = 0; i < M; i++)
        {
            min[i] = long.MaxValue / 2;
        }
        long sum = 0;
        min[0] = K;
        for (int i = 1; i <= N; i++)
        {
            sum += A[i - 1];
            long tmp = long.MaxValue;
            for (int j = 0; j < M; j++)
            {
                tmp = Math.Min(tmp, min[j]);
            }
            ans = Math.Max(ans, sum - tmp);
            int m = i % M;
            min[m] = Math.Min(min[m] + K, sum + K);
        }
        Console.WriteLine(ans);
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
