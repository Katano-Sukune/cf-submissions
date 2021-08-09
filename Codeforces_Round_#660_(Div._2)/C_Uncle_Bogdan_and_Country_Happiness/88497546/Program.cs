using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
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

    private int N, M;
    private int[] P;
    private int[] H;
    private List<int>[] E;

    void Q(Scanner sc)
    {
        N = sc.NextInt();
        M = sc.NextInt();
        P = sc.IntArray();
        H = sc.IntArray();
        E = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<int>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            int x = sc.NextInt() - 1;
            int y = sc.NextInt() - 1;
            E[x].Add(y);
            E[y].Add(x);
        }

        /* n頂点木 1が首都
         * m人
         * iにはP_i人住んでる
         *
         * 1から住んでる都市に最短で帰る
         *
         * 都市iに帰る最初or途中で機嫌が悪くなる
         * 悪くなったら良くならない
         *
         * H_i iを通った人 良い人数 - 悪い人数
         *
         * H_iが正しいか?
         */
        var res = Go(0, -1);
        Console.WriteLine(res.f ? "YES" : "NO");
    }

    (int g, int b, bool f) Go(int cur, int par)
    {
        int good = 0;
        int bad = 0;
        int cnt = P[cur];
        foreach (var to in E[cur])
        {
            if (to == par) continue;
            var r = Go(to, cur);
            if (!r.f) return (-1, -1, false);
            good += r.g;
            bad += r.b;
            cnt += r.g + r.b;
        }

        // 良い人
        // g + b = cnt
        // g-b = H

        if ((cnt + H[cur]) % 2 != 0) return (-1, -1, false);
        // 良い,悪いカウント
        int g = (cnt + H[cur]) / 2;
        int b = cnt - g;

        // 良くはならない
        if (g < good) return (-1, -1, false);
        // 
        if (g < 0 || b < 0) return (-1, -1, false);


        return (g, b, true);
    }

    public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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