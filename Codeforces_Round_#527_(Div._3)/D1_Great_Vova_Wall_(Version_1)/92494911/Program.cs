using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    int[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();

        // 同じ高さのところに横に置いてそれぞれ1増やす
        // 縦に置いて 2増やす

        // 全部同じ高さにする 個数

        // 全部highestに揃える
        // 空き奇数できない
        // N奇数NO

        int highest = A.Max();

        // highestに揃える
        {
            int black = 0;
            int white = 0;
            for (int i = 0; i < N; i++)
            {
                int cnt = highest - A[i];
                int b = cnt / 2;
                int w = cnt - b;

                if (i % 2 == 0)
                {
                    black += b;
                    white += w;
                }
                else
                {
                    black += w;
                    white += b;
                }
            }

            if (black == white)
            {
                Console.WriteLine("YES");
                return;
            }
        }

        // highest + 1に揃える
        {
            int black = 0;
            int white = 0;
            for (int i = 0; i < N; i++)
            {
                int cnt = highest - A[i] + 1;
                int b = cnt / 2;
                int w = cnt - b;

                if (i % 2 == 0)
                {
                    black += b;
                    white += w;
                }
                else
                {
                    black += w;
                    white += b;
                }
            }

            if (black == white)
            {
                Console.WriteLine("YES");
                return;
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
