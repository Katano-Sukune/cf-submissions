using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();

        /*
         * n*nグリッド
         * 蛇
         * 頭、尾が別のセル
         * 
         * 交差しない
         *
         * 左上、右下のセルを聞く
         * 長方形の縁を横切る回数が返る
         *
         * 
         */

        var lsR = new List<int>();
        bool flag = false;
        for (int i = 1; i < N; i++)
        {
            Console.WriteLine($"? 1 1 {i} {N}");
            int ans = sc.NextInt();
            if (flag)
            {
                if (ans % 2 == 0)
                {
                    lsR.Add(i);
                    flag = false;
                }
            }
            else
            {
                if (ans % 2 == 1)
                {
                    lsR.Add(i);
                    flag = true;
                }
            }
        }

        if (flag)
        {
            lsR.Add(N);
            flag = false;
        }

        var lsC = new List<int>();
        for (int i = 1; i < N; i++)
        {
            Console.WriteLine($"? 1 1 {N} {i}");
            int ans = sc.NextInt();
            if (flag)
            {
                if (ans % 2 == 0)
                {
                    flag = false;
                    lsC.Add(i);
                }
            }
            else
            {
                if (ans % 2 == 1)
                {
                    lsC.Add(i);
                    flag = true;
                }
            }
        }

        if (flag)
        {
            lsC.Add(N);
            flag = false;
        }

        if (lsR.Count == 0 && lsC.Count == 0)
        {
            // ない
            return;
        }

        if (lsR.Count == 0)
        {
            // 同じ行にある
            int ok = N;
            int ng = 0;
            while (ok - ng > 1)
            {
                int mid = (ok + ng) / 2;
                Console.WriteLine($"? 1 1 {mid} {lsC[0]}");
                int ans = sc.NextInt();
                if (ans % 2 == 1) ok = mid;
                else ng = mid;
            }

            Console.WriteLine($"! {ok} {lsC[0]} {ok} {lsC[1]}");
        }
        else if (lsC.Count == 0)
        {
            int ok = N;
            int ng = 0;
            while (ok - ng > 1)
            {
                int mid = (ok + ng) / 2;
                Console.WriteLine($"? 1 1 {lsR[0]} {mid}");
                int ans = sc.NextInt();
                if (ans % 2 == 1) ok = mid;
                else ng = mid;
            }

            Console.WriteLine($"! {lsR[0]} {ok} {lsR[1]} {ok}");
        }
        else
        {
            Console.WriteLine($"? {lsR[0]} {lsC[0]} {lsR[0]} {lsC[0]}");
            int ans = sc.NextInt();
            if (ans == 1)
            {
                Console.WriteLine($"! {lsR[0]} {lsC[0]} {lsR[1]} {lsC[1]}");
            }
            else
            {
                Console.WriteLine($"! {lsR[0]} {lsC[1]} {lsR[1]} {lsC[0]}");
            }
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