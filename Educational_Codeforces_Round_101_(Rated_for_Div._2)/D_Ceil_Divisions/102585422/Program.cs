using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
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
        /*
         * n配列 a
         * 
         * a_i = i
         * 
         * x,y選ぶ
         * 
         * a_x = ceil(a_x / a_y)
         * 
         * n-1個 1
         * 1個 2
         * にする
         * 
         * n+5回以下
         * 
         * 3~n-1, n
         * 
         * 
         */
        List<(int x, int y)> ls = new List<(int x, int y)>();
        int t = n;
        for (int i = n - 1; i >= 2; i--)
        {
            var nC1 = (t + (i - 2)) / (i - 1);
            var nC2 = (nC1 + (i - 2)) / (i - 1);
            if (i == 2 || nC2 > 1)
            {
                ls.Add((t, i));
                ls.Add((t, i));
                t = i;
            }
            else
            {
                ls.Add((i, t));
            }
        }
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        Console.WriteLine($"{ls.Count}");
        foreach ((int x, int y) in ls)
        {
            Console.WriteLine($"{x} {y}");
        }
        Console.Out.Flush();

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
