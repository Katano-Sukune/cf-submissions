using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int L;
    private string N;

    public void Solve()
    {
        var sc = new Scanner();
        L = sc.NextInt();
        N = sc.Next();

        List<int>[] d = new List<int>[L + 1];
        for (int i = 0; i <= L; i++)
        {
            d[i] = new List<int>();
        }

        int min = int.MaxValue;
        for (int i = 1; i < L; i++)
        {
            if (N[i] == '0') continue;
            int len = Math.Max(i, L - i);
            d[len].Add(i);
            min = Math.Min(min, len);
        }

        BigInteger ans = -1;
        foreach (int i in d[min])
        {
            string f = N.Substring(0, i);
            string b = N.Substring(i, L - i);
            BigInteger sum = BigInteger.Parse(f) + BigInteger.Parse(b);
            if (ans == -1 || sum < ans)
            {
                ans = sum;
            }
        }

        Console.WriteLine(ans);
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