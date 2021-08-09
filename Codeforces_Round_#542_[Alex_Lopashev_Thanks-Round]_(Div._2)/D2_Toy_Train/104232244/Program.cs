using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;

public class Program
{
    int N, M;
    int[] A, B;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        A = new int[M];
        B = new int[M];
        for (int i = 0; i < M; i++)
        {
            A[i] = sc.NextInt() - 1;
            B[i] = sc.NextInt() - 1;
        }


        int[] cnt = new int[N];

        // iで乗せる 下ろす先最短距離
        int[] t = new int[N];
        Array.Fill(t, int.MaxValue);
        for (int i = 0; i < M; i++)
        {
            cnt[A[i]]++;
            if (A[i] < B[i]) t[A[i]] = Math.Min(t[A[i]], B[i] - A[i]);
            else t[A[i]] = Math.Min(t[A[i]], N - (A[i] - B[i]));
        }

        long[] ans = new long[N];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                int to = (i + j) % N;
                if (cnt[to] == 0) continue;
                ans[i] = Math.Max(ans[i], j + N * (cnt[to] - 1) + t[to]);
            }
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
