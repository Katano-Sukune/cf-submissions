using System;
using System.IO;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        var t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        int a = sc.NextInt();
        int c = sc.NextInt();
        int b = sc.NextInt();

        string s = sc.Next();

        int w = 0;
        char[] ans = new char[n];
        for (int i = 0; i < n; i++)
        {
            if (a > 0 && s[i] == 'S')
            {
                w++;
                ans[i] = 'R';
                a--;
            }
            else if (b > 0 && s[i] == 'P')
            {
                w++;
                ans[i] = 'S';
                b--;
            }
            else if (c > 0 && s[i] == 'R')
            {
                w++;
                ans[i] = 'P';
                c--;
            }
        }

        if (w < (n + 1) / 2)
        {
            Console.WriteLine("NO");
            return;
        }

        for (int i = 0; i < n; i++)
        {
            if (ans[i] == default(char))
            {
                if (a > 0)
                {
                    ans[i] = 'R';
                    a--;
                }
                else if (b > 0)
                {
                    ans[i] = 'S';
                    b--;
                }
                else if (c > 0)
                {
                    ans[i] = 'P';
                    c--;
                }
            }
        }

        Console.WriteLine("YES");
        Console.WriteLine(new string(ans));
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