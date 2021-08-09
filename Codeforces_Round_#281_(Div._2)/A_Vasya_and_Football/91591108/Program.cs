using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        string home = sc.Next();
        string away = sc.Next();
        int n = sc.NextInt();

        int[] cntHome = new int[100];
        int[] cntAway = new int[100];
        bool[] fHome = new bool[100];
        bool[] fAway = new bool[100];


        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < n; i++)
        {
            int t = sc.NextInt();
            char c = sc.NextChar();
            int m = sc.NextInt();
            char col = sc.NextChar();
            if (c == 'a')
            {
                cntAway[m] += col == 'y' ? 1 : 2;
                if (!fAway[m] && cntAway[m] >= 2)
                {
                    fAway[m] = true;
                    Console.WriteLine($"{away} {m} {t}");
                }
            }
            else
            {
                cntHome[m] += col == 'y' ? 1 : 2;
                if (!fHome[m] && cntHome[m] >= 2)
                {
                    fHome[m] = true;
                    Console.WriteLine($"{home} {m} {t}");
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
