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
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
    }

    private int N;
    private List<int>[] E;

    void Q(Scanner sc)
    {
        N = sc.NextInt();
        E = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<int>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            E[u].Add(v);
            E[v].Add(u);
        }

        // 木
        // 2つnode選ばれる s,t

        // クエリ 11回まで ノードの集合aを聞く
        // 集合の内、dist(s, a_i) + dist(t, a_i) が最小になるnode,和を返す

        // s,tはどれか?

        // path s-t上を選ぶ 
        // dist(s, t)


        // 適当な葉を聞く

        // 全部聞く
        // dist(s, t), s-t上の頂点
        int[] all = new int[N];
        for (int i = 0; i < N; i++)
        {
            all[i] = i;
        }

        int root, dist;
        (root, dist) = Query(all, sc);

        // rootを根にして、木をつくる

        // D 根からの距離、ノードのリスト
        var d1 = new List<List<int>>();

        Go(root, -1, 0, d1);

        int ok = (dist + 1) / 2;
        int ng = d1.Count;

        int d = ok;
        int s;
        (s, _) = Query(d1[ok].ToArray(), sc);
        while (ng - ok > 1)
        {
            int mid = (ok + ng) / 2;
            int node, sum;
            (node, sum) = Query(d1[mid].ToArray(), sc);
            if (sum == dist)
            {
                ok = mid;
                if (d < mid)
                {
                    d = mid;
                    s = node;
                }
            }
            else
            {
                ng = mid;
            }
        }


        // sが確定した
        // sから距離distの頂点

        List<List<int>> d2 = new List<List<int>>();
        Go(s, -1, 0, d2);

        int f;
        (f, _) = Query(d2[dist].ToArray(), sc);

        Console.WriteLine($"! {s + 1} {f + 1}");
        sc.Next();
    }


    void Go(int cur, int par, int depth, List<List<int>> d)
    {
        if (d.Count <= depth)
        {
            d.Add(new List<int>());
        }

        d[depth].Add(cur);
        foreach (int to in E[cur])
        {
            if (to == par) continue;
            Go(to, cur, depth + 1, d);
        }
    }


    (int node, int dist) Query(int[] ar, Scanner sc)
    {
        Console.WriteLine($"? {ar.Length} {string.Join(" ", ar.Select(i => i + 1))}");
        return (sc.NextInt() - 1, sc.NextInt());
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
                string s = Console.ReadLine();
                while (s.Length == 0)
                {
                    s = Console.ReadLine();
                }

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