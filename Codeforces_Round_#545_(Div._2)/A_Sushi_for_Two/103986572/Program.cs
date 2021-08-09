using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int[] a = sc.IntArray();

        int[,] f = new int[n + 1, 2];
        for (int i = 0; i < n; i++)
        {
            if (a[i] == 1)
            {
                f[i + 1, 0] = f[i, 0] + 1;
                f[i + 1, 1] = 0;
            }
            else
            {
                f[i + 1, 0] = 0;
                f[i + 1, 1] = f[i, 1] + 1;
            }
        }

        int[,] b = new int[n + 1, 2];
        for (int i = n - 1; i >= 0; i--)
        {
            if (a[i] == 1)
            {
                b[i, 0] = b[i + 1, 0] + 1;
                b[i, 1] = 0;
            }
            else
            {
                b[i, 0] = 0;
                b[i, 1] = b[i + 1, 1] + 1;
            }
        }

        int ans = int.MinValue;
        for (int i = 0; i <= n; i++)
        {
            ans = Math.Max(ans, Math.Min(f[i, 0], b[i, 1]));
            ans = Math.Max(ans, Math.Min(f[i, 1], b[i, 0]));
        }
        Console.WriteLine(ans * 2);
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
