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

        /*
         * n-順列p
         * 
         * i,jを聞く p_i % p_jが返る
         * 
         * 2n回まで
         * 
         * 
         * 
         */

        /*
         * 隣り合う2つ聞く
         * 
         * x % y 
         * y % x
         * 
         * 
         * 大きい方が正しい値
         * 
         * 
         */

        // 確定してないindex
        int[] ar = new int[n];
        for (int i = 0; i < n; i++)
        {
            ar[i] = i;
        }

        int[] ans = new int[n];
        bool[] f = new bool[n];
        bool[] f2 = new bool[n + 1];
        while (ar.Length > 0)
        {
            if (ar.Length == 1)
            {
                for (int i = 1; i <= n; i++)
                {
                    if (f2[i]) continue;
                    ans[ar[0]] = i;
                    break;
                }
                break;
            }
            for (int i = 0; i + 1 < ar.Length; i += 2)
            {
                Console.WriteLine($"? {ar[i] + 1} {ar[i + 1] + 1}");
                int s = sc.NextInt();
                Console.WriteLine($"? {ar[i + 1] + 1} {ar[i] + 1}");
                int t = sc.NextInt();

                if (s > t)
                {
                    ans[ar[i]] = s;
                    f2[s] = true;
                    f[ar[i]] = true;
                }
                else
                {
                    ans[ar[i + 1]] = t;
                    f2[t] = true;
                    f[ar[i + 1]] = true;
                }
            }

            var next = new List<int>();
            for (int i = 0; i < ar.Length; i++)
            {
                if (f[ar[i]]) continue;
                next.Add(ar[i]);
            }

            ar = next.ToArray();
        }
        Console.WriteLine($"! {string.Join(" ", ans)}");
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
