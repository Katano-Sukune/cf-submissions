using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int k = sc.NextInt();

        char[][] ans = new char[4][];
        for (int i = 0; i < 4; i++)
        {
            ans[i] = new char[n];
            Array.Fill(ans[i], '.');
        }

        if (k % 2 == 0)
        {
            for (int i = 0; i < k; i++)
            {
                ans[i % 2 + 1][i / 2 + 1] = '#';
            }
        }
        else
        {
            if (k > n - 2)
            {
                for (int i = 1; i < n - 1; i++)
                {
                    ans[1][i] = '#';
                }

                for (int i = 0; i < (k - (n - 2)) / 2; i++)
                {
                    ans[2][i + 1] = '#';
                    ans[2][n - i - 2] = '#';
                }
            }
            else
            {
                for (int i = 0; i < k; i++)
                {
                    ans[1][i + (n - k) / 2] = '#';
                }
            }
        }

        Console.WriteLine("YES");
        for (int i = 0; i < 4; i++)
        {
            Console.WriteLine(new string(ans[i]));
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