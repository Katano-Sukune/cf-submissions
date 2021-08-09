using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;

public class Program
{
    int N;
    string[] S;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        S = new string[N];
        for (int i = 0; i < N; i++)
        {
            S[i] = sc.Next();
        }

        for (char c = 'a'; c <= 'z'; c++)
        {
            bool flag = true;
            foreach (var str in S)
            {
                foreach (char si in str)
                {
                    flag &= si != c;
                }
            }
            if (flag)
            {
                Console.WriteLine(c);
                return;
            }
        }

        for (char c = 'a'; c <= 'z'; c++)
        {
            for (char d = 'a'; d <= 'z'; d++)
            {
                bool flag = true;
                foreach (var str in S)
                {
                    for (int i = 0; i < str.Length - 1; i++)
                    {
                        flag &= !(str[i] == c && str[i + 1] == d);
                    }
                }
                if (flag)
                {
                    Console.WriteLine($"{c}{d}");
                    return;
                }
            }
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
