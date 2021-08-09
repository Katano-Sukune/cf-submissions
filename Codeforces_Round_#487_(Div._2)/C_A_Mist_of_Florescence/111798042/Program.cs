using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int A, B, C, D;

    public void Solve()
    {
        var sc = new Scanner();
        A = sc.NextInt();
        B = sc.NextInt();
        C = sc.NextInt();
        D = sc.NextInt();

        // 1個以上ある2色 25 * 50置く

        // 

        const int N = 50;
        const int M = 50;

        var ans = new char[N][];
        for (int i = 0; i < N; i++)
        {
            ans[i] = new char[N];
        }

        for (int i = 0; i < N / 2; i++)
        {
            for (int j = 0; j < M; j++)
            {
                ans[i][j] = 'A';
                ans[i + N / 2][j] = 'B';
            }
        }

        A--;
        B--;

        for (int i = 0; i < N / 2; i += 2)
        {
            for (int j = 0; j < M; j += 2)
            {
                if (B > 0)
                {
                    ans[i][j] = 'B';
                    B--;
                }
                else if (C > 0)
                {
                    ans[i][j] = 'C';
                    C--;
                }

                if (A > 0)
                {
                    ans[N - i - 1][j] = 'A';
                    A--;
                }
                else if (D > 0)
                {
                    ans[N - i - 1][j] = 'D';
                    D--;
                }
            }
        }

        Console.WriteLine($"{N} {M}");
        for (int i = 0; i < N; i++)
        {
            Console.WriteLine(new string(ans[i]));
        }
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