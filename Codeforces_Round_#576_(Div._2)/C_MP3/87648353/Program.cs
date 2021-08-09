using System;
using System.Collections.Generic;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int I = sc.NextInt();
        int[] a = sc.IntArray();

        // aの種類がKなら コスト ceil(lg(k))*n bit

        // コストをIbyte以下にしたい

        // [l:r]
        // l未満の値は lに r強はrにする
        // 変更された値の個数最小
// i * 8 / n 
        if (I * 8 / n >= 25)
        {
            Console.WriteLine(0);
            return;
        }

        int maxK = 1 << (I * 8 / n);
        Array.Sort(a);
        var cnt = new List<int>();
        cnt.Add(1);
        for (int i = 1; i < n; i++)
        {
            if (a[i] != a[i - 1])
            {
                cnt.Add(0);
            }

            cnt[cnt.Count - 1]++;
        }

        if (cnt.Count <= maxK)
        {
            Console.WriteLine(0);
            return;
        }

        int max = int.MinValue;
        int tmp = 0;
        for (int i = 0; i < maxK - 1; i++)
        {
            tmp += cnt[i];
        }

        for (int i = maxK - 1; i < cnt.Count; i++)
        {
            tmp += cnt[i];
            max = Math.Max(max, tmp);
            tmp -= cnt[i - (maxK - 1)];
        }

        Console.WriteLine(n - max);
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