using System;
using System.IO;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        string s = sc.Next();

        // c-good
        // sが c 1文字
        // sの前半がcのみ 後半が (c+1)-good

        // 後半がcのみ 前半が(c+1)-good

        // sをa-goodにするための変更する文字数
        int[][] a = new int[n + 1][];
        a[0] = new int[26];
        for (int i = 0; i < n; i++)
        {
            int c = s[i] - 'a';
            a[i + 1] = new int[26];
            Array.Copy(a[i], a[i + 1], 26);
            a[i + 1][c]++;
        }

        Console.WriteLine(F(a, 0, n, 0));
    }

    // s[l:r)をt-goodにする
    int F(int[][] a, int l, int r, int t)
    {
        if (r - l == 1)
        {
            return 1 - (a[r][t] - a[l][t]);
        }

        // 前半をt埋め
        int mid = (l + r) / 2;
        int aa = (mid - l - (a[mid][t] - a[l][t])) + F(a, mid, r, t + 1);
        int bb = F(a, l, mid, t + 1) + (r - mid - (a[r][t] - a[mid][t]));

        // Console.WriteLine($"{l} {r} {t} {Math.Min(aa, bb)}");
        return Math.Min(aa, bb);
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