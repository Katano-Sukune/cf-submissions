using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, K;
    private int[] R;
    private int[] X, Y;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        R = sc.IntArray();

        X = new int[K];
        Y = new int[K];
        for (int i = 0; i < K; i++)
        {
            X[i] = sc.NextInt() - 1;
            Y[i] = sc.NextInt() - 1;
        }

        var sorted = new int[N];
        for (int i = 0; i < N; i++)
        {
            sorted[i] = i;
        }

        Array.Sort(sorted, (l, r) => R[l].CompareTo(R[r]));

        int[] cnt = new int[N];
        for (int i = 0; i < N;)
        {
            int j = i;
            for (; j < N && R[sorted[i]] == R[sorted[j]]; j++)
            {
                cnt[sorted[j]] = i;
            }

            i = j;
        }

        for (int i = 0; i < K; i++)
        {
            if (R[X[i]] > R[Y[i]]) cnt[X[i]]--;
            if (R[X[i]] < R[Y[i]]) cnt[Y[i]]--;
        }

        Console.WriteLine(string.Join(" ", cnt));
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