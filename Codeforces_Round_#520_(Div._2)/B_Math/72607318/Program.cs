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

        // x掛ける
        // nが平方数のとき平方根にする
        // 最小値
        // 達成するための最小操作回数


        List<int> e = new List<int>();
        List<int> p = new List<int>();
        for (int i = 2; i * i <= n; i++)
        {
            if (n % i == 0)
            {
                int cnt = 0;
                p.Add(i);
                while (n % i == 0)
                {
                    cnt++;
                    n /= i;
                }

                e.Add(cnt);
            }
        }

        if (n > 1)
        {
            p.Add(n);
            e.Add(1);
        }

        int ansN = 1;
        foreach (int i in p)
        {
            ansN *= i;
        }

        bool flag = false;
        int ee = 0;
        for (int i = 1; i < e.Count; i++)
        {
            if (e[i - 1] != e[i]) flag = true;
        }

        for (int i = 0; i < e.Count; i++)
        {
            int log = 0;
            int j = 1;
            for (j = 1; j < e[i]; j *= 2) log++;
            ee = Math.Max(ee, log);
            if (j != e[i]) flag = true;
        }
        
        Console.WriteLine($"{ansN} {ee + (flag?1:0)}");
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