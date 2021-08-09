using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        string s = sc.Next();

        /*
         * 文字列s
         * 1 <= i <= |s|選ぶ
         *
         * sのi文字目削除、
         * sの先頭から連続グループ消す
         *
         * 操作回数最大
         */

        List<int> ls = new List<int>();

        for (int i = 0; i < n; i++)
        {
            if (i == 0 || s[i - 1] != s[i]) ls.Add(0);
            ls[^1]++;
        }

        /*
         * 先頭2個以上 先頭
         *
         * 2個以上グループ
         *
         * 端のやつ
         */

        int ans = 0;
        int ptr = 0;
        int ptr2 = 0;
        while (ptr2 < ls.Count)
        {
            while (ptr < ls.Count && ls[ptr] <= 1) ptr++;
            if (ptr < ls.Count)
            {
                ls[ptr]--;
                ans++;
            }
            else
            {
                while (ptr2 < ls.Count && ls[ptr2] <= 0) ptr2++;
                if (ptr2 >= ls.Count)
                {
                    break;
                }

                ls[ptr2] = 0;
                ans++;
            }

            while (ptr2 < ls.Count && ls[ptr2] <= 0) ptr2++;
            if (ptr2 < ls.Count)
            {
                ls[ptr2] = 0;
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