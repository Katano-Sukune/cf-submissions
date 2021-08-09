using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, K;
    private string S;
    private const string Sente = "tokitsukaze";
    private const string Gote = "quailty";
    private const string OnceAgain = "once again";

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        S = sc.Next();

        /*
         * n枚カード
         *
         * 連続したk枚同じ向きにする
         *
         * 全部同じ向きにできれば勝ち
         *
         * 先手 tokitsukaze
         *
         * 
         */

        // i以上の位置にある
        List<int>[] idx = new List<int>[2];
        idx[0] = new List<int>();
        idx[1] = new List<int>();
        for (int i = 0; i < N; i++)
        {
            idx[S[i] - '0'].Add(i);
        }

        // 初手でクリア
        if (idx[0].Count == 0 || idx[1].Count == 0 ||
            Math.Min(idx[0][^1] - idx[0][0] + 1, idx[1][^1] - idx[1][0] + 1) <= K)
        {
            Console.WriteLine(Sente);
            return;
        }

        // 先手の操作にかかわらず後手がクリア
        bool f = true;
        for (int l = 0; f && l + K <= N; l++)
        {
            // 0
            {
                int tl0 = Math.Min(idx[0][0], l);
                int tr0 = Math.Max(idx[0][^1], l + K - 1);
                // Console.WriteLine($"{l} {l + K - 1} {0} {Length(l, l + K - 1, idx[1])}");
                f &= tr0 - tl0 + 1 <= K || Length(l, l + K - 1, idx[1]) <= K;
            }
            {
                int tl1 = Math.Min(idx[1][0], l);
                int tr1 = Math.Max(idx[1][^1], l + K - 1);
                f &= tr1 - tl1 + 1 <= K || Length(l, l + K - 1, idx[0]) <= K;
            }
        }

        if (f)
        {
            Console.WriteLine(Gote);
            return;
        }

        Console.WriteLine(OnceAgain);
    }

    int Length(int l, int r, List<int> ls)
    {
        int left;
        if (ls[0] < l)
        {
            left = ls[0];
        }
        else
        {
            int ng = -1;
            int ok = ls.Count;
            while (ok - ng > 1)
            {
                int mid = (ok + ng) / 2;
                if (ls[mid] > r) ok = mid;
                else ng = mid;
            }

            left = ls[ok];
        }

        int right;
        if (r < ls[^1])
        {
            right = ls[^1];
        }
        else
        {
            int ok = -1;
            int ng = ls.Count - 1;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                if (ls[mid] < l) ok = mid;
                else ng = mid;
            }

            right = ls[ok];
        }

        return right - left + 1;
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