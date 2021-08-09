using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private int[] B;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        B = sc.IntArray();

        if (N <= 3)
        {
            Console.WriteLine("1");
            return;
        }

        var sorted = new int[N];
        for (int i = 0; i < N; i++)
        {
            sorted[i] = i;
        }

        Array.Sort(sorted, (l, r) => B[l].CompareTo(B[r]));

        Array.Sort(B);
        {
            int diff = B[1] - B[0];
            int prev = B[1];
            int cnt = 0;
            int idx = -1;
            for (int i = 2; i < N; i++)
            {
                if (B[i] - prev == diff)
                {
                    prev = B[i];
                }
                else
                {
                    cnt++;
                    idx = i;
                }
            }

            if (cnt == 0)
            {
                Console.WriteLine(sorted[0] + 1);
                return;
            }

            if (cnt == 1)
            {
                Console.WriteLine(sorted[idx] + 1);
                return;
            }
        }

        {
            int diff = B[2] - B[1];
            bool f = true;
            for (int i = 3; i < N && f; i++)
            {
                f &= B[i] - B[i - 1] == diff;
            }

            if (f)
            {
                Console.WriteLine(sorted[0] + 1);
                return;
            }
        }

        {
            int diff = B[2] - B[0];
            bool f = true;
            for (int i = 3; i < N && f; i++)
            {
                f &= B[i] - B[i - 1] == diff;
            }

            if (f)
            {
                Console.WriteLine(sorted[1] + 1);
                return;
            }
        }

        Console.WriteLine("-1");
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