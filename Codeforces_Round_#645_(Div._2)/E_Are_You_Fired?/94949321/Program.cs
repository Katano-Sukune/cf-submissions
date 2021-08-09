using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private long[] A;
    private long X;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.LongArray();
        X = sc.NextLong();

        var sum = new long[N / 2 + 1];
        for (int i = 0; i < N / 2; i++)
        {
            sum[i + 1] = sum[i] + X - A[i];
        }

        var min = new long[N / 2 + 1];
        for (int i = 1; i <= N / 2; i++)
        {
            min[i] = Math.Min(min[i - 1], sum[i]);
        }

        long tmp = 0;
        foreach (long l in A)
        {
            tmp += l;
        }

        for (int k = A.Length; k <= N; k++)
        {
            if (tmp + min[N - k] > 0)
            {
                Console.WriteLine(k);
                return;
            }

            if (k + 1 <= N) tmp += X;
        }

        Console.WriteLine("-1");
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