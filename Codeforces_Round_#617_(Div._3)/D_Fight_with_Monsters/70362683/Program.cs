using CompLib.Util;
using System;

public class Program
{
    int N, A, B, K;
    int[] H;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.NextInt();
        B = sc.NextInt();
        K = sc.NextInt();
        H = sc.IntArray();

        var cnt = new long[N];
        for (int i = 0; i < N; i++)
        {
            // 1 <= h <= A 0
            // h <= A+B 1
            // 
            // 1 <= h%(A+B) <= A 0
            // else 1
            // 7 10 50 12 1 8
            // 2 0 0 2 1 3

            int m = (H[i] - 1) % (A + B);
            if (m >= A)
            {
                cnt[i] = m / A;
            }
        }

        Array.Sort(cnt);

        int ans = 0;
        long sum = 0;
        for (int i = 0; i < N; i++)
        {
            sum += cnt[i];
            if (sum <= K)
            {
                ans = i + 1;
            }
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
        private int _index;
        private string[] _line;
        const char separator = ' ';

        public Scanner()
        {
            _index = 0;
            _line = new string[0];
        }

        public string Next()
        {
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(separator);
                _index = 0;
            }
            return _line[_index++];
        }
        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(separator);
            _index = _line.Length;
            return _line;
        }
        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
    }
}