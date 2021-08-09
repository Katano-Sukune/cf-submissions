using System;
using System.Collections.Generic;
using CompLib.Util;

public class Program
{
    private int N, P;
    private int[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        P = sc.NextInt();
        A = sc.IntArray();

        /* 敵iはa_i個キャンディー持ってる
         *
         * 順列Pを決める 
         *
         * 順列の前から
         * 持ってるキャンディー >= a[P_i]なら勝ち
         * キャンディー1増える
         *
         * f(x) 最初x個キャンディー持ってる
         *
         * 全部勝てる順列の順列の個数
         *
         * f(x)がpで割り切れないxすべて
         */

        Array.Sort(A);
        List<int> ans = new List<int>();
        for (int x = 1; x <= 2000; x++)
        {
            // 個数
            long t = 1;

            int index = 0;
            for (int i = 0; i < N; i++)
            {
                for (; index < N && A[index] <= x + i; index++) ;
                t *= Math.Max(0, index - i);
                t %= P;
            }

            if (t != 0)
            {
                ans.Add(x);
            }
        }

        Console.WriteLine(ans.Count);
        Console.WriteLine(string.Join(" ", ans));
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