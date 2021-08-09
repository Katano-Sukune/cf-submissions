using System;
using System.Collections.Generic;
using System.Linq;
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
        int a = sc.NextInt() - 1;
        int b = sc.NextInt() - 1;
        int da = sc.NextInt();
        int db = sc.NextInt();

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

        /*
         * n頂点木
         * 
         * aはaにbはbにいる
         * aが先手
         * 
         * aは距離daまで,bはdbまでジャンプできる
         * 
         * aがbと同じ頂点にいるならaが勝ち
         * 
         */

        /*
         * aがいる頂点
         * 
         * bが移動できる範囲
         */

        if (da > db)
        {
            Console.WriteLine("Alice");
            return;
        }
        // 初手で取れる
        int[] sDist = Calc(a, n, e);

        if (sDist[b] <= da)
        {
            Console.WriteLine("Alice");
            return;
        }

        int sMax = int.MinValue;
        int max = -1;
        for (int i = 0; i < n; i++)
        {
            if (sMax < sDist[i])
            {
                sMax = sDist[i];
                max = i;
            }
        }

        int[] dist2 = Calc(max, n, e);

        int l = dist2.Max();

        if(da*2 >= l)
        {
            Console.WriteLine("Alice");
            return;
        }

        if(da * 2 < db)
        {
            Console.WriteLine("Bob");
            return;
        }

        Console.WriteLine("Alice");
    }

    int[] Calc(int s, int n, List<int>[] e)
    {
        int[] res = new int[n];
        for (int i = 0; i < n; i++)
        {
            res[i] = int.MaxValue;
        }

        var q = new Queue<int>();
        res[s] = 0;
        q.Enqueue(s);

        while (q.Count > 0)
        {
            var d = q.Dequeue();
            foreach (var to in e[d])
            {
                if (res[to] != int.MaxValue) continue;
                res[to] = res[d] + 1;
                q.Enqueue(to);
            }
        }

        return res;
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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
