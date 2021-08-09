using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private const string Bicarp = "Bicarp";
    private const string Monocarp = "Monocarp";
    private int N;
    private string S;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        S = sc.Next();

        /*
         * 偶数桁消えてる
         *
         * 先頭和 = 末尾和 なら　Bicarp
         *
         * 先手 Monocarp
         */

        int f = 0;
        int b = 0;

        int fq = 0;
        int bq = 0;
        for (int i = 0; i < N / 2; i++)
        {
            if (S[i] == '?')
            {
                fq++;
            }
            else
            {
                f += S[i] - '0';
            }

            if (S[i + N / 2] == '?')
            {
                bq++;
            }
            else
            {
                b += S[i + N / 2] - '0';
            }
        }

        if (fq > bq)
        {
            int d = f - b;
            int dq = bq - fq;
            Console.WriteLine(d == dq / 2 * 9 ? Bicarp : Monocarp);
        }
        else
        {
            int d = b - f;
            int dq = fq - bq;
            Console.WriteLine(d == dq / 2 * 9 ? Bicarp : Monocarp);
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