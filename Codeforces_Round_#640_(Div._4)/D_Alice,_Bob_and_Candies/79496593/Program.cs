using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Q(sc.NextInt(), sc.IntArray()));
        }
        Console.Write(sb);
    }

    string Q(int n, int[] a)
    {
        // alice 先手 bob後手
        // alice 左から食べる bob右から食べる

        // 食べた合計が相手が前の手で食べた合計より大きくなるまで食べる
        // 双方が食べた合計

        long alice = 0;
        long bob = 0;

        long pBob = 0;
        long pAlice = 0;

        int l = 0;
        int r = n - 1;

        int cnt = 0;
        for (int i = 0; l <= r; i++)
        {
            if (i % 2 == 0)
            {
                pAlice = 0;
                while (pAlice <= pBob && l <= r)
                {
                    pAlice += a[l++];
                }

                alice += pAlice;
            }
            else
            {
                pBob = 0;
                while (pBob <= pAlice && l <= r)
                {
                    pBob += a[r--];
                }
                bob += pBob;
            }
            cnt++;
        }

        return $"{cnt} {alice} {bob}";
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
