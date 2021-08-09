using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, B, A;
    int[] S;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        B = sc.NextInt();
        A = sc.NextInt();
        S = sc.IntArray();

        // ロボット Nまで行く
        // 1つ進む
        // Bを消費 Bが1減る
        // Aを消費 S[i]が1ならBが1増える、Aが1減る

        // できるだけBを消費、
        // 充電できるならAを消費、
        // Bが満タンなら充電出来てもBを消費、

        int aa = A;

        for (int i = 0; i < N; i++)
        {
            if (S[i] == 1)
            {
                if (aa < A && B > 0)
                {
                    aa++;
                    B--;
                }
                else if (aa > 0)
                {
                    aa--;
                }
                else
                {
                    Console.WriteLine(i);
                    return;
                }
            }
            else
            {
                if (aa > 0)
                {
                    aa--;
                }
                else if (B > 0)
                {
                    B--;
                }
                else
                {
                    Console.WriteLine(i);
                    return;
                }
            }
        }
        Console.WriteLine(N);
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
