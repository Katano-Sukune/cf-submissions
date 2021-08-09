using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < t; i++)
        {
            Console.WriteLine(Q(sc.NextInt(), sc.NextInt(), sc.IntArray()));
        }
        Console.Out.Flush();
    }

    string Q(int n, int x, int[] a)
    {
        var f = new bool[n + 1, x + 1, 2];
        f[0, 0, 0] = true;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    if (j + 1 <= x) f[i + 1, j + 1, (a[i] + k) % 2] |= f[i, j, k];
                    if (j <= x) f[i + 1, j, k] |= f[i, j, k];
                }
            }
        }

        return f[n, x, 1] ? "Yes" : "No";
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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
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
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
