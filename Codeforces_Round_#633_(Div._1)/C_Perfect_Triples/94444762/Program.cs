using System;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        // 0, 1, 2, 3
        // 4 8 12,| 4+1, 8+2, 12+3, | 4+2, 8+3, 12+1,| 4+3,8+1,12+2
        // 16,32,48,

        // t_i, 2t_i, 3t_i, |  |
        // 
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        Console.WriteLine(F(sc.NextLong()));
    }

    long F(long n)
    {
        if (n < 4)
        {
            // 1,2,3
            return n;
        }

        int b = 1;
        for (b = 1; (1L << (2 * (b + 1))) <= n; b++) ;

        long tmp = (n - (1L << (2 * b))) / 3;
        long p = 1L << (2 * b);
        long m = (n - 1) % 3;

        long ans = p * (m + 1);
        for (int i = b - 1; i >= 0; i--)
        {
            p /= 4;
            long d = tmp / p;
            int[] ar;
            switch (d)
            {
                case 0:
                    ar = new int[3];
                    break;
                case 1:
                    // 1,2,3
                    ar = new int[] {1, 2, 3};
                    break;
                case 2:
                    // 2,3,1
                    ar = new int[] {2, 3, 1};
                    break;
                case 3:
                    // 3,1,2
                    ar = new int[] {3, 1, 2};
                    break;
                default:
                    throw new Exception();
            }

            ans += p * ar[m];
            tmp %= p;
        }

        return ans;
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