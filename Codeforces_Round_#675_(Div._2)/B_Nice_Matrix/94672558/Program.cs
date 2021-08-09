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
        int n = sc.NextInt();
        int m = sc.NextInt();

        int[][] a = new int[n][];
        for (int i = 0; i < n; i++)
        {
            a[i] = sc.IntArray();
        }

        /*
         * 行、列が全て回文ならnice
         *
         * 1つ選んで+-1できる
         *
         * 
         */

        long ans = 0;
        for (int i = 0; i < n / 2; i++)
        {
            for (int j = 0; j < m / 2; j++)
            {
                var tmp = new int[4];
                tmp[0] = a[i][j];
                tmp[1] = a[n - i - 1][j];
                tmp[2] = a[i][m - j - 1];
                tmp[3] = a[n - i - 1][m - j - 1];
                Array.Sort(tmp);
                for (int k = 0; k < 4; k++)
                {
                    ans += Math.Abs(tmp[k] - tmp[1]);
                }
            }
        }

        if (n % 2 == 1)
        {
            for (int j = 0; j < m / 2; j++)
            {
                var tmp1 = a[n / 2][j];
                var tmp2 = a[n / 2][m - j - 1];
                ans += Math.Abs(tmp1 - tmp2);
            }
        }

        if (m % 2 == 1)
        {
            for (int i = 0; i < n / 2; i++)
            {
                var tmp1 = a[i][m / 2];
                var tmp2 = a[n - i - 1][m / 2];
                ans += Math.Abs(tmp1 - tmp2);
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