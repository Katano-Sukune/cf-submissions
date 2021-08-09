using System;
using System.IO;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        for (int i = 0; i < t; i++)
        {
            Console.WriteLine(Q(sc.NextInt(), sc.IntArray(), sc.IntArray()));
        }

        Console.Out.Flush();
    }

    string Q(int n, int[] a, int[] b)
    {
        int cnt = 0;
        bool flag = false;
        bool flag2 = false;
        int d = -1;
        for (int i = 0; i < n; i++)
        {
            if (a[i] != b[i])
            {
                if (!flag)
                {
                    if (flag2) return "NO";
                    flag = true;
                    d = b[i] - a[i];
                    if (d < 0) return "NO";
                }
                else
                {
                    if (d != b[i] - a[i]) return "NO";
                }
            }
            else
            {
                if (flag)
                {
                    flag = false;
                    flag2 = true;
                }
            }
        }

        return "YES";
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