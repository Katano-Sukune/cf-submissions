using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, X;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        X = sc.NextInt();

        if (N == 1 && X == 1)
        {
            Console.WriteLine("0");
            return;
        }

        int b = -1;
        for (int i = 18 - 1; i >= 0; i--)
        {
            if ((X & (1 << i)) > 0)
            {
                b = i;
                break;
            }
        }

        int max = (1 << N) - 1;
        var ls = new List<int>();
        for (int i = 0; i < N; i++)
        {
            if (b == i) continue;
            int len = ls.Count;
            ls.Add(1 << i);
            for (int j = 0; j < len; j++)
            {
                ls.Add(ls[j]);
            }
        }

        Console.WriteLine(ls.Count);
        Console.WriteLine(string.Join(" ", ls));

        // 1 2 1 4 1 2 1 8  
        // 
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