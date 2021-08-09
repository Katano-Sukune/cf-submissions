using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
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
        int[] b = sc.IntArray();

        /*
         * 1~2n
         * n個ペアにする
         * 
         * x個 小さい方 n-x個 大きい方
         * 
         * 
         * 
         */
        bool[] flag = new bool[2 * n + 1];
        foreach (var i in b)
        {
            flag[i] = true;
        }

        var ls = new List<int>(n);
        for (int i = 1; i <= 2 * n; i++)
        {
            if (!flag[i]) ls.Add(i);
        }

        bool[] ff = new bool[n + 1];
        ff[0] = true;

        int ptr = 0;
        for (int i = 0; i < n; i++)
        {
            // iまでは小さい方を取る
            while (ptr < n && ls[ptr] < b[i]) ptr++;
            if (ptr < n)
            {
                ff[i + 1] = true;
                ptr++;
            }
            else break;
        }

        bool[] bb = new bool[n + 1];
        bb[n] = true;
        ptr = n - 1;
        for (int i = n - 1; i >= 0; i--)
        {
            while (ptr >= 0 && ls[ptr] > b[i]) ptr--;
            if (ptr >= 0)
            {
                bb[i] = true;
                ptr--;
            }
            else break;
        }

        int ans = 0;
        for (int x = 0; x <= n; x++)
        {
            if(ff[x] && bb[x])
            {
                ans++;
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
