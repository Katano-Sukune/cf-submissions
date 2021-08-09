using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, K;
    private int[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        A = sc.IntArray();

        Array.Sort(A);

        var fCnt = new Dictionary<int, int>(N);
        var fSum = new Dictionary<int, long>(N);

        long sum = 0;
        for (int i = 0; i < N; i++)
        {
            if (i == 0 || A[i - 1] < A[i])
            {
                fCnt[A[i]] = i;
                fSum[A[i]] = sum;
            }

            sum += A[i];
        }

        sum = 0;
        var bCnt = new Dictionary<int, int>(N);
        var bSum = new Dictionary<int, long>(N);
        for (int i = N - 1; i >= 0; i--)
        {
            if (i == N - 1 || A[i + 1] > A[i])
            {
                bCnt[A[i]] = N - i - 1;
                bSum[A[i]] = sum;
            }

            sum += A[i];
        }

        long ans = long.MaxValue;

        foreach (int i in A)
        {
            int fc = fCnt[i];
            int bc = bCnt[i];
            // iの個数
            int cntI = N - fc - bc;

            int r = K - cntI;
            if (r <= 0)
            {
                Console.WriteLine(0);
                return;
            }

            long costF = (long) (i - 1) * fc - fSum[i];
            long costB = bSum[i] - (long) (i + 1) * bc;
            if (r <= fc)
            {
                ans = Math.Min(ans, costF + r);
            }

            if (r <= bc)
            {
                ans = Math.Min(ans, costB + r);
            }

            ans = Math.Min(ans, costF + costB + r);
        }

        Console.WriteLine(ans);
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