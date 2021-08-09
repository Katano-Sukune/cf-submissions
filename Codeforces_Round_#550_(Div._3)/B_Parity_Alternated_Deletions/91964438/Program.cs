using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        var a = sc.IntArray();

        /*
         * 最初　任意の要素消す
         * 
         * 次以降
         * 前が奇数なら偶数
         * 偶数なら奇数
         * 
         * 残った要素和最小
         */

        var lsO = new List<int>();
        var lsE = new List<int>();

        for (int i = 0; i < n; i++)
        {
            if (a[i] % 2 == 0)
            {
                lsE.Add(a[i]);
            }
            else
            {
                lsO.Add(a[i]);
            }
        }

        if (Math.Abs(lsO.Count - lsE.Count) <= 1)
        {
            Console.WriteLine("0");
            return;
        }

        // 多い方
        // 少ない方+1個消せる
        long ans = 0;
        if (lsO.Count > lsE.Count)
        {
            lsO.Sort();
            for (int i = 0; i < lsO.Count - (lsE.Count + 1); i++)
            {
                ans += lsO[i];
            }
        }
        else
        {
            lsE.Sort();
            for (int i = 0; i < lsE.Count - (lsO.Count + 1); i++)
            {
                ans += lsE[i];
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
