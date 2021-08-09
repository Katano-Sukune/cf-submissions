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
        int q = sc.NextInt();
        string s = sc.Next();
        for (int i = 0; i < q; i++)
        {
            int l = sc.NextInt() - 1;
            int r = sc.NextInt();

            var span = s.AsSpan(l, r - l);
            int[] f = new int[n];
            int[] b = new int[n];
            for (int j = 0; j < n - 1; j++)
            {
                if (f[j] < span.Length && s[j] == span[f[j]])
                {
                    f[j + 1] = f[j] + 1;
                }
                else
                {
                    f[j + 1] = f[j];
                }
            }

            for (int j = n - 1; j >= 1; j--)
            {
                if (b[j] < span.Length && s[j] == span[span.Length - 1 - b[j]])
                {
                    b[j - 1] = b[j] + 1;
                }
                else
                {
                    b[j - 1] = b[j];
                }
            }

            bool flag = true;
            for (int j = 0; flag && j < n; j++)
            {
                if (f[j] + b[j] >= span.Length && f[j] > 0 && b[j] > 0)
                {
                    Console.WriteLine("YES");
                    flag = false;
                }
            }

            if (flag) Console.WriteLine("NO");
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