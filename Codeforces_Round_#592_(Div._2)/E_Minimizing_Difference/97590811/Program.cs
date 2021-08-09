using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private long K;
    private long[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextLong();
        A = sc.LongArray();
        Array.Sort(A);

        long[] back = new long[N + 1];
        for (int i = 0; i < N; i++)
        {
            back[i + 1] = back[i] + A[N - i - 1];
        }

        long ans = long.MaxValue;
        long f = 0;
        for (int i = 0; i < N && f <= K; i++)
        {
            // i未満をiに合わせる
            long ok = A[N - 1];
            long ng = A[i] - 1;

            while (ok - ng > 1)
            {
                long mid = (ok + ng) / 2;
                // 全部mid以下にするコスト
                // mid超過の個数
                int ok2 = N;
                int ng2 = -1;
                while (ok2 - ng2 > 1)
                {
                    int mid2 = (ok2 + ng2) / 2;
                    if (A[mid2] > mid) ok2 = mid2;
                    else ng2 = mid2;
                }

                // 個数 N - ok2

                long cost = back[N - ok2] - mid * (N - ok2);
                if (f + cost <= K) ok = mid;
                else ng = mid;
            }

            ans = Math.Min(ans, ok - A[i]);

            if (i + 1 < N) f += (A[i + 1] - A[i]) * (i + 1);
        }

        long[] front = new long[N + 1];
        for (int i = 0; i < N; i++)
        {
            front[i + 1] = front[i] + A[i];
        }

        
        // Console.WriteLine(ans);
        
        long b = 0;
        for (int i = N - 1; i >= 0 && b <= K; i--)
        {
            long ok = A[0];
            long ng = A[i] + 1;
            while (ng - ok > 1)
            {
                long mid = (ok + ng) / 2;
                // 全部mid以上にする
                // mid未満の個数
                int ok2 = -1;
                int ng2 = N;
                while (ng2 - ok2 > 1)
                {
                    int mid2 = (ok2 + ng2) / 2;
                    if (A[mid2] < mid) ok2 = mid2;
                    else ng2 = mid2;
                }

                long cost = mid * (ok2 + 1) - front[ok2 + 1];
                if (b + cost <= K) ok = mid;
                else ng = mid;
            }

            ans = Math.Min(ans, A[i] - ok);
            if (i - 1 >= 0) b += (A[i] - A[i - 1]) * (N - i);
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