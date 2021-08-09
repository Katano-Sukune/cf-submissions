using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        List<int>[] e = new List<int>[n];
        for (int i = 0; i < n; i++)
        {
            e[i] = new List<int>();
        }

        for (int i = 0; i < n - 1; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            e[u].Add(v);
            e[v].Add(u);
        }

        // 辺を1つ外して 1追加
        // 重心が一意になるようにする

        // 奇数 ok

        // 偶数
        // ちょうど1/2の部分木が存在する
        // 片方の重心にもう片方の葉をつなげる

        if (n % 2 == 1)
        {
            var t = e[0][0] + 1;
            Console.WriteLine($"1 {t}");
            Console.WriteLine($"1 {t}");

            return;
        }

        int[] w = new int[n];

        Go(0, -1, e, w);

        // wにn/2がある
        int ce = -1;
        for (int i = 0; i < n; i++)
        {
            if (w[i] == n / 2)
            {
                ce = i;
                break;
            }
        }

        // ceが無い
        // 重心1つかつ0が重心
        if (ce == -1)
        {
            var t = e[0][0] + 1;
            Console.WriteLine($"1 {t}");
            Console.WriteLine($"1 {t}");
            return;
        }

        int[] w2 = new int[n];
        Go(ce, -1, e, w2);

        int ce2 = -1;
        foreach (var to in e[ce])
        {
            if (w2[to] == n / 2)
            {
                ce2 = to;
                break;
            }
        }

        // 重心1つ
        if (ce2 == -1)
        {
            var t = e[0][0] + 1;
            Console.WriteLine($"1 {t}");
            Console.WriteLine($"1 {t}");
            return;
        }

        // 2つ
        int[] f = new int[n];
        Go(ce, ce2, e, f);

        for (int i = 0; i < n; i++)
        {
            if (f[i] == 0 && e[i].Count == 1)
            {
                var t = e[i][0];
                Console.WriteLine($"{i + 1} {t + 1}");
                Console.WriteLine($"{i + 1} {ce + 1}");
                return;
            }
        }
    }

    void Go(int cur, int par, List<int>[] e, int[] w)
    {
        w[cur] = 1;
        foreach (var to in e[cur])
        {
            if (par == to) continue;
            Go(to, cur, e, w);
            w[cur] += w[to];
        }
    }

    // public static void Main(string[] args) => new Program().Solve();
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
