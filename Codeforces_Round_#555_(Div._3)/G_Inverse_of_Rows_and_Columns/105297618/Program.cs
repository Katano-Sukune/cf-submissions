using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;

public class Program
{
    int N, M;
    int[][] A;


    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        A = new int[N][];
        for (int i = 0; i < N; i++)
        {
            A[i] = new int[M];
            for (int j = 0; j < M; j++)
            {
                A[i][j] = 1 - sc.NextInt();
            }
        }

        int[] col, row;
        // 0行目
        for (int j = 0; j < M; j++)
        {
            col = new int[M];
            for (int c = 0; c < M; c++)
            {
                col[c] = A[0][c];
            }

            row = new int[N];
            bool f = true;
            for (int r = 1; f && r < N; r++)
            {
                row[r] = col[0] ^ A[r][0];
                for (int c = 0; c < M; c++)
                {
                    f &= (col[c] ^ A[r][c]) == row[r];
                }
            }
            if (f)
            {
                Console.WriteLine("YES");
                Console.WriteLine(string.Join("", row));
                Console.WriteLine(string.Join("", col));
                return;
            }
            A[0][j] ^= 1;
        }

        // 0行目は全部0になるはず
        // 反転する列確定
        col = new int[M];
        for (int c = 0; c < M; c++)
        {
            if (A[0][c] == 1)
            {
                col[c] = 1;
                for (int r = 0; r < N; r++)
                {
                    A[r][c] ^= 1;
                }
            }
        }

        int[] cntRow = new int[N];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                cntRow[i] += A[i][j];
            }
        }

        for (int i = 1; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                cntRow[i] -= A[i][j];
                A[i][j] ^= 1;
                cntRow[i] += A[i][j];
                bool f = true;
                for (int r = 0; f && r < N; r++)
                {
                    f &= cntRow[r] == 0 || cntRow[r] == M;
                }
                if (f)
                {
                    row = new int[N];
                    for (int r = 0; r < N; r++)
                    {
                        row[r] = cntRow[r] == M ? 1 : 0;
                    }
                    Console.WriteLine("YES");
                    Console.WriteLine(string.Join("", row));
                    Console.WriteLine(string.Join("", col));
                    return;
                }

            }
        }
        Console.WriteLine("NO");
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
