using System;
using System.Collections.Generic;
using System.IO;
using CompLib.Util;

public class Program
{
    private int[] Sieve;
    private int N;
    private int[] A;
    private const int MaxA = 10000000;

    public void Solve()
    {
        Sieve = new int[MaxA + 1];
        for (int i = 2; i * i <= MaxA; i++)
        {
            if (Sieve[i] == 0)
            {
                for (int j = i; j <= MaxA; j += i)
                {
                    Sieve[j] = i;
                }
            }
        }

        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();
        int[] d1 = new int[N];
        int[] d2 = new int[N];
        for (int i = 0; i < N; i++)
        {
            int tmp = A[i];
            int p = Sieve[tmp];
            if (p == 0)
            {
                d1[i] = -1;
                d2[i] = -1;
                continue;
            }

            d1[i] = tmp;
            d2[i] = 1;
            while (d1[i] % p == 0)
            {
                d2[i] *= p;
                d1[i] /= p;
            }

            if (d1[i] == 1)
            {
                d1[i] = -1;
                d2[i] = -1;
                continue;
            }
        }

        Console.WriteLine(string.Join(" ", d1));
        Console.WriteLine(string.Join(" ", d2));
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