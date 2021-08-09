using System;
using System.IO;
using System.Text;
using CompLib.Util;

public class Program
{
    private int N;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();

        // + X Y X*Yの長方形の札を得る
        // ? X Y X*Yの財布にいままでの札が全て入るか?

        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});

        int xx = int.MinValue;
        int yy = int.MinValue;
        for (int i = 0; i < N; i++)
        {
            char c = sc.NextChar();
            int x = sc.NextInt();
            int y = sc.NextInt();
            if (x > y)
            {
                x ^= y;
                y ^= x;
                x ^= y;
            }

            if (c == '+')
            {
                xx = Math.Max(xx, x);
                yy = Math.Max(yy, y);
            }
            else if (c == '?')
            {
                if (xx <= x && yy <= y)
                {
                    Console.WriteLine("YES");
                }
                else
                {
                    Console.WriteLine("NO");
                }
            }
        }
        
        Console.Out.Flush();
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
                string s = Console.ReadLine();
                while (s.Length == 0)
                {
                    s = Console.ReadLine();
                }

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