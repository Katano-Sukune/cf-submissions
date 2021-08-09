using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        /*
         * n*n
         * 
         * (1,1)から(n,n)に移動
         * 
         * (x,y) に A_x_y < 1e16を入れる
         * 
         * 移動経路のA_x_yの総和が返る
         * 
         * 移動経路
         */

        // 移動パターン
        // Rを0Dを1として2進数

        long[][] grid = new long[n][];
        for (int i = 0; i < n; i++)
        {
            grid[i] = new long[n];
            for (int j = 0; j < n; j++)
            {
                grid[i][j] = -1;
            }
        }
        grid[0][0] = 0;
        for (int i = 1; i < n; i++)
        {
            grid[0][i] = 1L << (i - 1);
            grid[i][n - 1] = 0;
        }

        for (int i = 1; i < n; i++)
        {
            for (int j = n - 2; j >= 0; j--)
            {
                long d = 1L << (i + j - 1);
                grid[i][j] = grid[i - 1][j + 1] + d;
            }
        }
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine(string.Join(" ", grid[i]));
        }
        // 1は右、0は下

        int q = sc.NextInt();
        for (int i = 0; i < q; i++)
        {
            long k = sc.NextLong();
            long cp = k;
            var sb = new StringBuilder();
            sb.AppendLine("1 1");
            int x = 1;
            int y = 1;

            long s = 0;
            for (int j = 0; j < 2 * (n - 1); j++)
            {
                if (k % 2 == 0)
                {
                    y++;
                }
                else
                {
                    x++;
                }

                sb.AppendLine($"{y} {x}");
                s += grid[y - 1][x - 1];
                k /= 2;
            }

            Debug.Assert(cp == s);

            Console.Write(sb);
        }
    }

    public static void Main(string[] args) => new Program().Solve();
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
