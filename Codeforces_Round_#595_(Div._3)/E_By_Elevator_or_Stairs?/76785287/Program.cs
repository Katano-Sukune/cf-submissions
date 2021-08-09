using System;
using CompLib.Util;

public class Program
{
    private int N, C;
    private int[] A;
    private int[] B;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        C = sc.NextInt();
        A = sc.IntArray();
        B = sc.IntArray();

        long stair = 0;
        long elevator = int.MaxValue;
        long[] ans = new long[N];
        for (int i = 0; i < N - 1; i++)
        {
            ans[i] = Math.Min(stair, elevator);
            long ns = Math.Min(stair, elevator) + A[i];
            long ne = Math.Min(stair + C + B[i], elevator + B[i]);
            stair = ns;
            elevator = ne;
        }

        ans[N - 1] = Math.Min(stair, elevator);
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