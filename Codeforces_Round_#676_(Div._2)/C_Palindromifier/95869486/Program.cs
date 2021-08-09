using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private string S;
    private int N;

    public void Solve()
    {
        var sc = new Scanner();
        S = sc.Next();
        N = S.Length;
        /*
         * iを選んで S[2,i]を反転、Sの先頭に
         *
         * iを選んで S[i,N-1]を反転 Sの末尾に
         *
         * abcd
         * babcd
         * babcdc
         * babcdcdcba
         * babcdcdcbab
         *
         * abc
         * babc
         * babcb
         * babcbcba
         * babcbcbab
         *
         * abac
         * babac
         * babaca
         * 
         */

        Console.WriteLine(4);
        Console.WriteLine("L 2");
        N++;
        Console.WriteLine($"R {N - 1}");
        N++;
        Console.WriteLine("R 2");
        N += N - 2;
        Console.WriteLine($"R {N - 1}");
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