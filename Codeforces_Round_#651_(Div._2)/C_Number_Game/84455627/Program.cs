using System;
using System.Collections.Generic;
using System.IO;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }


    private const string Sente = "Ashishgup";
    private const string Gote = "FastestFinger";

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        if (n == 1)
        {
            Console.WriteLine(Gote);
            return;
        }

        if (n == 2)
        {
            Console.WriteLine(Sente);
            return;
        }
        // nを3以上の奇数で割る
        // nから1引く、
        // 1にされたら負け

        // 1 負け
        // 2 勝ち
        // 奇数 勝ち

        // 奇素因数が無い 負け

        int cnt2 = 0;
        while (n % 2 == 0)
        {
            cnt2++;
            n /= 2;
        }

        int cntOdd = 0;
        for (int i = 3; i * i <= n; i += 2)
        {
            while (n % i == 0)
            {
                cntOdd++;
                n /= i;
            }
        }

        if (n != 1) cntOdd++;

        if (cnt2 == 0)
        {
            Console.WriteLine(Sente);
            return;
        }

        if (cnt2 != 1)
        {
            Console.WriteLine(cntOdd == 0 ? Gote : Sente);
            return;
        }

        // 2* 
        if (cntOdd == 1)
        {
            Console.WriteLine(Gote);
            return;
        }

        Console.WriteLine(Sente);
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