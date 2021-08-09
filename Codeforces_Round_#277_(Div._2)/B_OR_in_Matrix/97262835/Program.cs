using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int m = sc.NextInt();
        int n = sc.NextInt();

        int[][] b = new int[m][];
        for (int i = 0; i < m; i++)
        {
            b[i] = sc.IntArray();
        }

        /*
         * m行n列行列 A
         *
         * 
         */

        int[][] a = new int[m][];
        for (int i = 0; i < m; i++)
        {
            a[i] = new int[n];
            for (int j = 0; j < n; j++)
            {
                a[i][j] = 1;
            }
        }

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (b[i][j] == 0)
                {
                    for (int k = 0; k < n; k++)
                    {
                        a[i][k] = 0;
                    }

                    for (int k = 0; k < m; k++)
                    {
                        a[k][j] = 0;
                    }
                }
            }
        }

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                int tmp = 0;
                for (int k = 0; k < n; k++)
                {
                    tmp |= a[i][k];
                }

                for (int k = 0; k < m; k++)
                {
                    tmp |= a[k][j];
                }

                if (b[i][j] != tmp)
                {
                    Console.WriteLine("NO");
                    return;
                }
            }
        }

        Console.WriteLine("YES");
        for (int i = 0; i < m; i++)
        {
            Console.WriteLine(string.Join(" ",a[i]));
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