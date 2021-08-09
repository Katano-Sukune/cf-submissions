using CompLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
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
            sb.AppendLine(Q(sc.NextInt(), sc.NextInt(), sc.IntArray()));
        }
        Console.Write(sb);
    }

    string Q(int n, int k, int[] a)
    {
        // i回操作 [l,r] の中央値で置き換える
        // 全部 kにできるか?

        // kと隣り合うkより大きい kにできる

        // kと隣り合うkより小さい kのグループが2以上 できる
        // kより小さいグループ > 隣り合うk以上のグループ

        // kと隣り合うkより小さい

        // kが存在する
        // kとk以上　隣接　or k k未満 k以上の順で並ぶ

        if (n == 1)
        {
            return a[0] == k ? "yes" : "no";
        }

        var list = new List<int>();
        int cnt = 1;
        if (a[0] >= k) list.Add(0);
        for (int i = 1; i < n; i++)
        {
            if (list.Count % 2 == 0)
            {
                if (a[i] >= k)
                {
                    list.Add(cnt);
                    cnt = 0;
                }
            }
            else
            {
                if (a[i] < k)
                {
                    list.Add(cnt);
                    cnt = 0;
                }
            }
            cnt++;
        }
        list.Add(cnt);

        // kが存在する
        // 2以上のk以上のグループがある
        bool f1 = a.Any(i => i == k);
        bool f2 = false;

        for (int i = 1; i < list.Count; i += 2)
        {
            f2 |= list[i] >= 2;
        }

        // 2つのk以上の挟まれたk未満がある
        for(int i = 1;i < n - 1; i++)
        {
            if (a[i] < k && a[i + 1] >= k && a[i - 1] >= k) f2 = true;
        }
        return f1 && f2 ? "yes" : "no";
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
