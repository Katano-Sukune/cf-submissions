using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CompLib.Util;

public class Program
{
    int N;
    List<int>[] E;

    int[] P;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        E = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<int>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            int a = sc.NextInt() - 1;
            int b = sc.NextInt() - 1;
            E[a].Add(b);
            E[b].Add(a);
        }

        P = new int[N];
        var s = Go(0, -1);
        int u = s.v;


        var t = Go(u, -1);
        int v = t.v;

        // 直径 u-v
        var dist = new int[N];
        for (int i = 0; i < N; i++)
        {
            dist[i] = int.MaxValue;
        }
        var q = new Queue<int>();
        int cur = v;
        while (true)
        {
            q.Enqueue(cur);
            dist[cur] = 0;
            if (cur == u) break;
            cur = P[cur];
        }

        while (q.Count > 0)
        {
            var d = q.Dequeue();
            foreach (var to in E[d])
            {
                if (dist[to] <= dist[d] + 1) continue;
                dist[to] = dist[d] + 1;
                q.Enqueue(to);
            }
        }

        int c = -1;
        int max = -1;
        for (int i = 0; i < N; i++)
        {
            if (i == u || i == v) continue;
            if (max < dist[i])
            {
                max = dist[i];
                c = i;
            }
        }

        Console.WriteLine(t.dist + max);
        Console.WriteLine($"{u + 1} {v + 1} {c + 1}");
    }

    (int dist, int v) Go(int cur, int par)
    {
        (int dist, int v) ans = (0, cur);
        foreach (var to in E[cur])
        {
            if (to == par) continue;
            var t = Go(to, cur);
            if (ans.dist < t.dist + 1)
            {
                ans = (t.dist + 1, t.v);
            }
        }
        P[cur] = par;
        return ans;
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
