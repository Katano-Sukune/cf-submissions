using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();

        int[,] parity = new int[n, n];
        int cntOdd = (n * n + 1) / 2;

        // 1
        for (int i = 0; i < n; i++)
        {
            parity[i, n / 2] = 1;
            parity[n / 2, i] = 1;
        }

        cntOdd -= (n + n - 1);

        // 8
        for (int i = 0; i < n / 2; i++)
        {
            for (int j = i + 1; j < n / 2; j++)
            {
                if (cntOdd >= 8)
                {
                    parity[i, j] = 1;
                    parity[j, i] = 1;

                    parity[n - i - 1, j] = 1;
                    parity[j, n - i - 1] = 1;

                    parity[i, n - j - 1] = 1;
                    parity[n - j - 1, i] = 1;

                    parity[n - i - 1, n - j - 1] = 1;
                    parity[n - j - 1, n - i - 1] = 1;
                    cntOdd -= 8;
                }
            }
        }

        // 4
        for (int i = 0; i < n / 2; i++)
        {
            if (cntOdd >= 4)
            {
                parity[i, i] = 1;
                parity[n - i - 1, i] = 1;
                parity[i, n - i - 1] = 1;
                parity[n - i - 1, n - i - 1] = 1;
                cntOdd -= 4;
            }
        }


        // 奇数 (n^2+1)/2個

        // 行、列に奇数個ずつ

        int odd = 1;
        int even = 2;
        int[][] ans = new int[n][];
        for (int i = 0; i < n; i++)
        {
            ans[i] = new int[n];
            for (int j = 0; j < n; j++)
            {
                if (parity[i, j] == 1)
                {
                    ans[i][j] = odd;
                    odd += 2;
                }
                else
                {
                    ans[i][j] = even;
                    even += 2;
                }
            }
        }

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine(string.Join(" ", ans[i]));
        }

        /*
         * 
         *
         *
         * 
         */
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