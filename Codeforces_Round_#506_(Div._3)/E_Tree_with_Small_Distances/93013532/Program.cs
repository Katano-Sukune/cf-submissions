using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.Collections.Generic;

public class Program
{
    int N;
    List<int>[] E;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        /*
         * N頂点　木
         * 
         * 1から各点までの距離が最大2になるように
         */

        E = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<int>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            (int u, int v) = (sc.NextInt() - 1, sc.NextInt() - 1);
            E[u].Add(v);
            E[v].Add(u);
        }

        var queue = new int[2 * N];
        var begin = 0;
        var end = 0;

        int[] dist = new int[N];
        int[] par = new int[N];
        for (int i = 0; i < N; i++)
        {
            dist[i] = -1;
        }
        dist[0] = 0;
        queue[end++] = 0;
        while (end > begin)
        {
            var d = queue[begin++];
            foreach (var to in E[d])
            {
                if (dist[to] != -1) continue;
                dist[to] = dist[d] + 1;
                par[to] = d;
                queue[end++] = to;
            }
        }

        bool[] f = new bool[N];
        var ls = new List<int>();
        for (int i = 0; i < N; i++)
        {
            if (dist[i] <= 2)
            {
                f[i] = true;
            }
            else
            {
                ls.Add(i);
            }
        }

        ls.Sort((l, r) => dist[r].CompareTo(dist[l]));

        int ans = 0;
        foreach (var i in ls)
        {
            if (f[i]) continue;
            ans++;
            foreach (var to in E[par[i]])
            {
                f[to] = true;
            }
            f[par[i]] = true;
        }

        Console.WriteLine(ans);
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
