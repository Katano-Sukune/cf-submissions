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
        A = sc.IntArray();
        B = sc.IntArray();


        long[] ta = new long[N + N - 1];
        long[] tb = new long[N + N - 1];
        for (int i = 0; i < N; i++)
        {
            ta[2 * i] = A[i];
            tb[2 * i] = B[i];
        }

        long sum = 0;
        for (int i = 0; i < N; i++)
        {
            sum += ta[2 * i] * tb[2 * i];
        }

        long ans = sum;

        for (int m = 0; m < N + N - 1; m++)
        {
            // 中央
            long tmp = sum;

            for (int i = 1; m - i >= 0 && m + i < N + N - 1; i++)
            {
                if ((m + i) % 2 == 1) continue;
                tmp -= ta[m - i] * tb[m - i];
                tmp -= ta[m + i] * tb[m + i];

                tmp += ta[m - i] * tb[m + i];
                tmp += ta[m + i] * tb[m - i];

                ans = Math.Max(ans, tmp);
            }
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
