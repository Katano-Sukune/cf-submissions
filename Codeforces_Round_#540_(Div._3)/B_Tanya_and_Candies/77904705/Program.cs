using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    int[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();

        // N-1個
        // iを除くN-1個

        // 奇数番目、偶数番目の和が同じになるのはいくつ?

        // [0,N)の奇数、偶数番目の和
        long[] odd = new long[N + 1];
        long[] even = new long[N + 1];
        for (int i = 0; i < N; i++)
        {
            odd[i + 1] = odd[i];
            even[i + 1] = even[i];
            if (i % 2 == 0) even[i + 1] += A[i];
            else odd[i + 1] += A[i];
        }
        int ans = 0;
        for (int i = 0; i < N; i++)
        {
            long o, e;

            o = odd[i] + even[N] - even[i + 1];
            e = even[i] + odd[N] - odd[i + 1];

            if (o == e) ans++;
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
