using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;

public class Program
{
    int N;
    int[] A, B;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = new int[N];
        B = new int[N];
        for (int i = 0; i < N; i++)
        {
            A[i] = sc.NextInt();
            B[i] = sc.NextInt();
        }

        /*
         * i　容量 a_i b_i入ってる
         * 
         * k個のグラスに集める
         * 
         * iからjにx移す
         * 
         * iからx減る
         * jにx/2増える
         * a_j超える分は消える
         * 
         * k個に集める
         * 最大
         */

        var dp = new Array3D(N + 1, N + 1, 10001, int.MinValue);
        dp[0, 0, 0] = 0;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                for (int k = 0; k <= 100 * j; k++)
                {
                    int cur = dp[i, j, k];
                    if (cur == int.MinValue) continue;

                    dp[i + 1, j + 1, k + A[i]] = Math.Max(dp[i + 1, j + 1, k + A[i]], cur + 2 * B[i]);
                    dp[i + 1, j, k] = Math.Max(dp[i + 1, j, k], cur + B[i]);
                }
            }
        }

        double[] ans = new double[N];
        for (int i = 1; i <= N; i++)
        {
            for (int j = 0; j <= 10000; j++)
            {
                int cur = dp[N, i, j];
                if (cur == int.MinValue) continue;

                ans[i - 1] = Math.Max(ans[i - 1], Math.Min((double)cur / 2, j));
            }
        }

        Console.WriteLine(string.Join(" ", ans));
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

class Array3D
{
    readonly int A, B, C;
    int[] T;
    public Array3D(int a, int b, int c, int fill = 0)
    {
        A = a;
        B = b;
        C = c;
        T = new int[A * B * C];
        for (int i = 0; i < A * B * C; i++)
        {
            T[i] = fill;
        }
    }

    public int this[int i, int j, int k]
    {
        set { T[i * B * C + j * C + k] = value; }
        get { return T[i * B * C + j * C + k]; }
    }
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
