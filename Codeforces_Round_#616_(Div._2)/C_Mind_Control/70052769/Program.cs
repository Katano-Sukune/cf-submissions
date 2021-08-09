using CompLib.Util;
using System;
using System.Collections.Generic;
using System.Text;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Q(sc.NextInt(), sc.NextInt(), sc.NextInt(), sc.IntArray()));
        }
        Console.Write(sb.ToString());
    }

    private string Q(int n, int m, int k, int[] a)
    {
        // 1分ごとに先頭にいる人が最初 or 最後の要素を取る
        // 列から抜ける
        // あなたはm番目 k人までどっちを取らせるか説得できる
        // あなたが最悪取れる最大の要素
        int ans = int.MinValue;
        k = Math.Min(m - 1, k);
        for (int i = 0; i <= k; i++)
        {
            // i人最初の取らせる
            int left = i;
            int right = n - 1 - (k - i);

            int tmp = int.MaxValue;
            int nokori = m - 1 - k;
            for (int j = 0; j <= m - 1 - k; j++)
            {
                // 説得できなかった人j人最初
                int ll = left + j;
                int rr = right - (nokori - j);
                tmp = Math.Min(tmp, Math.Max(a[ll], a[rr]));
            }
            ans = Math.Max(ans, tmp);
        }
        return ans.ToString();
    }

    public static void Main(string[] args) => new Program().Solve();
}

namespace CompLib.Util
{
    using System;
    using System.Linq;
    class Scanner
    {
        private int _index;
        private string[] _line;
        const char separator = ' ';

        public Scanner()
        {
            _index = 0;
            _line = new string[0];
        }

        public string Next()
        {
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(separator);
                _index = 0;
            }
            return _line[_index++];
        }
        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(separator);
            _index = _line.Length;
            return _line;
        }
        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
    }
}