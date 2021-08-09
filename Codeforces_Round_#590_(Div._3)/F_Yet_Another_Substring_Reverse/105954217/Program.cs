using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using CompLib.Algorithm;

public class Program
{
    string S;
    int N;
    bool[] F;

    int[] Memo;

    public void Solve()
    {
        var sc = new Scanner();
        S = sc.Next();
        N = S.Length;

        // a~tからなる文字列S

        // 1回
        // Sの部分列　S[l,r]を反転
        // 各文字1回ずつの部分文字列最大長

        F = new bool[1 << 20];
        F[0] = true;
        for (int i = 0; i < S.Length; i++)
        {
            int tmp = 0;
            for (int j = i; j >= 0; j--)
            {
                int num = S[j] - 'a';
                if ((tmp & (1 << num)) > 0) break;
                tmp |= (1 << num);
                F[tmp] = true;
            }
        }
        Memo = new int[1 << 20];
        Array.Fill(Memo, -1);
        int ans = int.MinValue;
        for (int i = 0; i < (1 << 20); i++)
        {
            if (F[i])
            {
                ans = Math.Max(ans, Algorithm.BitCount(i) + Go(((1 << 20) - 1) ^ i));
            }
        }

        Console.WriteLine(ans);
    }

    int Go(int bit)
    {
        if (Memo[bit] != -1) return Memo[bit];
        if (F[bit])
        {
            Memo[bit] = Algorithm.BitCount(bit);
        }
        else
        {
            for (int i = 0; i < 20; i++)
            {
                if ((bit & (1 << i)) > 0) Memo[bit] = Math.Max(Memo[bit], Go(bit ^ (1 << i)));
            }
        }

        return Memo[bit];
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}


namespace CompLib.Algorithm
{
    static class Algorithm
    {
        /// <summary>
        /// nの立っているbitの数
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
       　public static int BitCount(long n)
        {
            n = (n & 0x5555555555555555) + (n >> 1 & 0x5555555555555555);
            n = (n & 0x3333333333333333) + (n >> 2 & 0x3333333333333333);
            n = (n & 0x0f0f0f0f0f0f0f0f) + (n >> 4 & 0x0f0f0f0f0f0f0f0f);
            n = (n & 0x00ff00ff00ff00ff) + (n >> 8 & 0x00ff00ff00ff00ff);
            n = (n & 0x0000ffff0000ffff) + (n >> 16 & 0x0000ffff0000ffff);
            return (int)((n & 0x00000000ffffffff) + (n >> 32 & 0x00000000ffffffff));
        }
    }
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
