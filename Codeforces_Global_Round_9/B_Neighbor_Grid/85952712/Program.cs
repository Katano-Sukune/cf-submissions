using System;
using System.IO;
using CompLib.Util;

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

        int[][] ans = new int[n][];
        for (int i = 0; i < n; i++)
        {
            ans[i] = new int[m];
            for (int j = 0; j < m; j++)
            {
                if (i == 0 || i == n - 1)
                {
                    if (j == 0 || j == m - 1)
                    {
                        ans[i][j] = 2;
                    }
                    else
                    {
                        ans[i][j] = 3;
                    }
                }
                else
                {
                    if (j == 0 || j == m - 1)
                    {
                        ans[i][j] = 3;
                    }
                    else
                    {
                        ans[i][j] = 4;
                    }
                }

                if (a[i][j] > ans[i][j])
                {
                    Console.WriteLine("NO");
                    return;
                }
            }
        }

        Console.WriteLine("YES");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine(string.Join(" ", ans[i]));
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