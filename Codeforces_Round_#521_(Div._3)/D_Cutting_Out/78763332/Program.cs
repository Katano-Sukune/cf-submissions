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
        int k = sc.NextInt();
        int[] s = sc.IntArray();


        int[] cnt = new int[200001];
        cnt[0] = int.MinValue;
        foreach (var i in s)
        {
            cnt[i]++;
        }

        int[] index = new int[200001];
        for (int i = 0; i <= 200000; i++)
        {
            index[i] = i;
        }
        Array.Sort(index, (l, r) => cnt[r].CompareTo(cnt[l]));
        int ok = 0;
        int ng = n + 1;
        while (ng - ok > 1)
        {
            int mid = (ok + ng) / 2;
            // mid個できるか?
            int tmp = 0;
            for (int i = 0; i < 200000; i++)
            {
                tmp += cnt[index[i]] / mid;
                if (cnt[index[i]] < mid) break;
            }
            if (tmp >= k) ok = mid;
            else ng = mid;
        }

        List<int> ans = new List<int>();
        int t = 0;
        for (int i = 0; i < 200000 && t < k; i++)
        {
            int tt = cnt[index[i]] / ok;
            int cc = Math.Min(tt, k - t);
            for (int j = 0; j < cc; j++)
            {
                ans.Add(index[i]);
            }
            t += cc;
        }
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
