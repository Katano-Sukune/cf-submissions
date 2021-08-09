using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, W;
    private int[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        W = sc.NextInt();
        A = sc.IntArray();

        var sorted = new int[N];
        for (int i = 0; i < N; i++)
        {
            sorted[i] = i;
        }

        Array.Sort(sorted, (l, r) => A[l].CompareTo(A[r]));

        long s = 0;
        long last = 0;
        long[] ans = new long[N];
        foreach (int i in sorted)
        {
            // 入れる
            long t = Math.Max(last, (A[i] + 1) / 2);
            s += t;
            ans[i] = t;
            last = t;
        }

        if (s > W)
        {
            Console.WriteLine("-1");
            return;
        }

        long m = W - s;
        long last2 = long.MaxValue;
        for (int i = N - 1; i >= 0; i--)
        {
            int j = sorted[i];
            // lastまで
            // mまで
            // A[j]まで
            long t = Math.Min(A[j] - ans[j], Math.Min(m, last2 - ans[j]));
            m -= t;
            ans[j] += t;
            last2 = ans[j];
        }

        if (m > 0)
        {
            Console.WriteLine("-1");
            return;
        }

        Console.WriteLine(string.Join(" ", ans));
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