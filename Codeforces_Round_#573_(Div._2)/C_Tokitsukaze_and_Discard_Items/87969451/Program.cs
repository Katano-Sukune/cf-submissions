using System;
using System.Collections.Generic;
using CompLib.Util;

public class Program
{
    private long N, K;
    private int M;

    private long[] P;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextLong();
        M = sc.NextInt();
        K = sc.NextLong();
        P = sc.LongArray();

        var q = new Queue<long>();
        foreach (long l in P)
        {
            q.Enqueue(l - 1);
        }
        /*
         * N個アイテム
         * 最初 iページ目
         * i*K+1 ~ (i+1)KのK個ある
         *
         * 操作
         * P_jがある一番小さいページのP_jを消す
         *
         * 空いたところを詰める
         *
         * 何回操作するか?
         */

        // ずれ
        int c = 0;
        int ans = 0;
        while (q.Count > 0)
        {
            ans++;
            var top = q.Dequeue();
            // topと同じページにあるやつ
            int cnt = 1;
            while (q.Count > 0 && (q.Peek() - c) / K == (top - c) / K)
            {
                q.Dequeue();
                cnt++;
            }
            c += cnt;
        }

        Console.WriteLine(ans);
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