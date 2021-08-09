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
        int m = sc.NextInt();
        int[] a = sc.IntArray();
        int[] b = sc.IntArray();

        /*
         * n個
         * 
         * iはa_i
         * 
         * b_i = 1通常
         *     = 2重要
         *     
         * i_1, i_2 ...を閉じる
         * 
         * aの合計m以上
         * bの合計最小
         */

        List<int> one = new List<int>();
        List<int> two = new List<int>();
        for (int i = 0; i < n; i++)
        {
            if (b[i] == 1) one.Add(a[i]);
            else two.Add(a[i]);
        }

        one.Sort((l, r) => r.CompareTo(l));
        two.Sort((l, r) => r.CompareTo(l));

        var sumOne = new long[one.Count + 1];
        for (int i = 0; i < one.Count; i++)
        {
            sumOne[i + 1] = sumOne[i] + one[i];
        }

        long ans = long.MaxValue;

        long sum2 = 0;
        for (int t = 0; t <= two.Count; t++)
        {
            int ng = -1;
            int ok = one.Count + 1;
            while (ok - ng > 1)
            {
                int mid = (ok + ng) / 2;
                if (sum2 + sumOne[mid] >= m) ok = mid;
                else ng = mid;
            }

            if (ok <= one.Count) ans = Math.Min(ans, t * 2 + ok);
            if (t < two.Count) sum2 += two[t];
        }

        if (ans == long.MaxValue)
        {
            Console.WriteLine("-1");
        }
        else
        {
            Console.WriteLine(ans);
        }
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
