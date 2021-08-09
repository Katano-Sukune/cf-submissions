using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        int m = sc.NextInt();
        List<int>[] e = new List<int>[n];
        for (int i = 0; i < n; i++)
        {
            e[i] = new List<int>();
        }

        for (int i = 0; i < m; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            e[u].Add(v);
            e[v].Add(u);
        }


        /*
         * n頂点 m辺　グラフ
         * 
         * 頂点を選ぶ
         * 選んでない頂点が選んだ頂点の少なくとも1つに隣接している
         * 
         * floor(n/2)以下頂点選ぶ
         */

        /*
         * 二部グラフ
         * 少ない方選ぶ
         * 
         * 
         */

        int[] parity = new int[n];
        var q = new Queue<int>();
        parity[0] = 1;
        q.Enqueue(0);

        while (q.Count > 0)
        {
            var d = q.Dequeue();
            foreach (var to in e[d])
            {
                if (parity[to] != 0) continue;
                parity[to] = -parity[d];
                q.Enqueue(to);
            }
        }

        int b = parity.Count(num => num > 0);
        int w = n - b;

        var ls = new List<int>();
        if (b < w)
        {
            for (int i = 0; i < n; i++)
            {
                if (parity[i] > 0) ls.Add(i + 1);
            }

            Console.WriteLine(b);
        }
        else
        {
            for (int i = 0; i < n; i++)
            {
                if (parity[i] < 0) ls.Add(i + 1);
            }

            Console.WriteLine(w);
        }

        Console.WriteLine(string.Join(" ", ls));
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
