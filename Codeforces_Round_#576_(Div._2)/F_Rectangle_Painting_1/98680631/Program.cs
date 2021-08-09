using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private Array4D DP;
    private string[] S;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        S = new string[N];
        for (int i = 0; i < N; i++)
        {
            S[i] = sc.Next();
        }

        DP = new Array4D(N, N, N, N);
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                for (int k = 0; k < N; k++)
                {
                    for (int l = 0; l < N; l++)
                    {
                        DP[i, j, k, l] = -1;
                    }
                }
            }
        }

        Console.WriteLine(Go(0, 0, N - 1, N - 1));
    }

    int Go(int r1, int c1, int r2, int c2)
    {
        if (DP[r1, c1, r2, c2] != -1)
        {
            return DP[r1, c1, r2, c2];
        }

        if (r1 == r2 && c1 == c2)
        {
            return S[r1][c1] == '#' ? 1 : 0;
        }

        int ans = Math.Max(r2 - r1 + 1, c2 - c1 + 1);
        if (r2 > r1)
        {
            for (int r3 = r1; r3 < r2; r3++)
            {
                ans = Math.Min(ans, Go(r1, c1, r3, c2) + Go(r3 + 1, c1, r2, c2));
            }
        }

        if (c2 > c1)
        {
            for (int c3 = c1; c3 < c2; c3++)
            {
                ans = Math.Min(ans, Go(r1, c1, r2, c3) + Go(r1, c3 + 1, r2, c2));
            }
        }

        DP[r1, c1, r2, c2] = ans;
        return ans;
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

class Array4D
{
    private readonly int X, Y, Z, U;

    private int[] T;

    public Array4D(int x, int y, int z, int u)
    {
        X = x;
        Y = y;
        Z = z;
        U = u;
        T = new int[x * y * z * u];
    }

    public new int this[int i, int j, int k, int l]
    {
        set { T[i * Y * Z * U + j * Z * U + k * U + l] = value; }
        get { return T[i * Y * Z * U + j * Z * U + k * U + l]; }
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