using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;

public class Program
{
    int N;
    int[] A;
    public void Solve()
    {
        checked
        {
            var sc = new Scanner();
            N = sc.NextInt();
            A = sc.IntArray();

            long ans = (long)N * N * (N + 1) / 2;
            for (int i = 0; i < N; i++)
            {
                // iを含まないl,rの選び方
                // A_i未満
                ans -= (long)(A[i] - 1) * (A[i]) / 2;
                // A_i超過
                ans -= (long)(N - A[i]) * (N - A[i] + 1) / 2;
            }

            for (int i = 0; i < N - 1; i++)
            {
                // A_i, A_{i+1}をつなげるl,rの選び方
                int min = Math.Min(A[i], A[i + 1]);
                int max = Math.Max(A[i], A[i + 1]);

                ans -= (long)min * (N - max + 1);
            }

            Console.WriteLine(ans);
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
