using System;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Q(sc.NextInt(), sc.Next()));
        }

        Console.Write(sb.ToString());
    }

    string Q(int n, string x)
    {
        char[] a = new char[n];
        char[] b = new char[n];
        bool f = false;
        for (int i = 0; i < n; i++)
        {
            switch (x[i])
            {
                case '0':
                    a[i] = b[i] = '0';
                    break;
                case '1':
                    if (f)
                    {
                        a[i] = '0';
                        b[i] = '1';
                    }
                    else
                    {
                        a[i] = '1';
                        b[i] = '0';
                        f = true;
                    }

                    break;
                case '2':
                    if (f)
                    {
                        a[i] = '0';
                        b[i] = '2';
                    }
                    else
                    {
                        a[i] = b[i] = '1';
                    }

                    break;
            }
        }

        return $"{new string(a)}\n{new string(b)}";
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