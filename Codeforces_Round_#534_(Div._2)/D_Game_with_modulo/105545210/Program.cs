using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

public class Program
{

    public void Solve()
    {
        var rnd = new Random();
        while (G() == "start")
        {
            // ? x y

            // x < y かつ x%a >= y%bな x,yを探す
            long x, y;
            while (true)
            {
                x = rnd.Next(1, (int)2e9);
                y = rnd.Next(1, (int)2e9);
                if (x == y) continue;
                if (x > y) (x, y) = (y, x);
                if (Query(x, y) == 'x') break;
            }

            while (y - x > 1)
            {
                long mid = (x + y) / 2;
                if (Query(x, mid) == 'x')
                {
                    y = mid;
                }
                else
                {
                    x = mid;
                }
            }
            // y%a = 0
            var ls = new List<(long p, int cnt)>();
            long tmp = y;
            for (long i = 2; i * i <= tmp; i++)
            {
                if (tmp % i == 0)
                {
                    int cnt = 0;
                    while (tmp % i == 0)
                    {
                        tmp /= i;
                        cnt++;
                    }
                    ls.Add((i, cnt));
                }
            }
            if (tmp > 1) ls.Add((tmp, 1));
            long ans = y;
            foreach ((long p, int cnt) in ls)
            {
                var ar = new long[cnt + 1];
                ar[0] = 1;
                for (int i = 1; i <= cnt; i++) ar[i] = ar[i - 1] * p;
                int ok = 0;
                int ng = cnt + 1;
                while (ng - ok > 1)
                {
                    int mid = (ok + ng) / 2;
                    if (Query(0, ans / ar[mid]) == 'x')
                    {
                        ok = mid;
                    }
                    else
                    {
                        ng = mid;
                    }
                }
                ans /= ar[ok];
            }

            Console.WriteLine($"! {ans}");
            ptr++;
        }
    }

    int[] ans = { 1, 2, 3, 65535 };
    int ptr = 0;
    string G()
    {
#if DEBUG
        if (ptr >= ans.Length) return "end";
        else return "start";
#else
        return Console.ReadLine();
#endif
    }

    char Query(long x, long y)
    {
#if DEBUG
        Console.WriteLine($"{x}%{ans[ptr]}={x % ans[ptr]} {y}%{ans[ptr]}={y % ans[ptr]}");
        if (x % ans[ptr] >= y % ans[ptr]) return 'x';
        else return 'y';
#else
        Console.WriteLine($"? {x} {y}");
        return Console.ReadLine()[0];
#endif
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
