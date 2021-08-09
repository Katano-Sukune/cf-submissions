using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    int[] A, B;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();
        B = sc.IntArray();

        int[] indexOfA = new int[N];
        int[] indexOfB = new int[N];
        for (int i = 0; i < N; i++)
        {
            indexOfA[A[i] - 1] = i;
            indexOfB[B[i] - 1] = i;
        }
        // 順列a,b

        // bをいくつか右に回す a,b一致してる数 最大

        // i回すとき
        int[] cnt = new int[N];

        for (int i = 0; i < N; i++)
        {
            int diff = (indexOfA[i] - indexOfB[i] + N) % N;
            cnt[diff]++;
        }

        Console.WriteLine(cnt.Max());
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
