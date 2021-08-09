using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    string S;
    public void Solve()
    {
        var sc = new Scanner();
        S = sc.Next();

        // Sから 一つ選んで取り除く

        // 2つになったら終わり

        // 先手は最小化、後手は最大化する
        // ?を決めた時 ありえる値昇順

        // 先手 1を選ぶ 全部消せる 00

        // 後手 0を選ぶ　全部消せる 11

        // 10 or 01

        // 偶数個 1,0の数同じ
        // 奇数個 1の数 = 0の数+1

        // 両方とも先頭から取っていく

        int n = S.Length;

        List<int> list1 = new List<int>();
        List<int> list0 = new List<int>();
        List<int> listq = new List<int>();

        for (int i = 0; i < n; i++)
        {
            var c = S[i];
            if (c == '1') list1.Add(i);
            if (c == '0') list0.Add(i);
            if (c == '?') listq.Add(i);
        }
        // 先手の手数 (n-1)/2
        // 後手 (n-2)/2

        // 00
        if (list1.Count < list0.Count + listq.Count) Console.WriteLine("00");
        // 01 or 10
        if (list1.Count <= (n + 1) / 2 && list0.Count <= n / 2)
        {
            int qTo0 = n / 2 - list0.Count;
            int qTo1 = (n + 1) / 2 - list1.Count;

            if (S[n - 1] == '1') Console.WriteLine("01");
            else if (S[n - 1] == '0') Console.WriteLine("10");
            else if (S[n - 1] == '?')
            {
                if (qTo1 > 0) Console.WriteLine("01");
                if (qTo0 > 0) Console.WriteLine("10");
            }
        }
        // 11
        if (list1.Count + listq.Count > list0.Count + 1) Console.WriteLine("11");
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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
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
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
