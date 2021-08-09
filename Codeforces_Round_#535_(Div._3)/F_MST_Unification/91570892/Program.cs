using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using CompLib.Graph;
using CompLib.Util;

public class Program
{
    int N, M;
    Edge[] E;

    List<Edge> Tree;
    List<Edge> UnUse;
    public void Solve()
    {
        try
        {
            var sc = new Scanner();
            N = sc.NextInt();
            M = sc.NextInt();
            E = new Edge[M];
            for (int i = 0; i < M; i++)
            {
                E[i] = new Edge(sc.NextInt() - 1, sc.NextInt() - 1, sc.NextInt());
            }

            Array.Sort(E, (l, r) => l.W.CompareTo(r.W));
            Tree = new List<Edge>();
            UnUse = new List<Edge>();
            // コスト
            var uf = new UnionFind(N);
            for (int i = 0; i < M; i++)
            {
                if (uf.Connect(E[i].U, E[i].V))
                {
                    Tree.Add(E[i]);
                }
                else
                {
                    UnUse.Add(E[i]);
                }
            }

            /*
             * 最小全域木
             * 
             * 使われなかった辺について
             * 
             * パス(U,V)間の辺の最大より大きくなるまで増やす
             * 
             * 
             */

            var tree = new Tree(N, Tree);

            long ans = 0;
            foreach (var e in UnUse)
            {
                var max = tree.GetMaxW(e.U, e.V);

                // Console.WriteLine($"{e.U} {e.V} {max} {tree.LCA(e.U, e.V)}");
                // Console.WriteLine($"{tree.GetMaxW2(e.V, 0)}");
                ans += Math.Max(0, max - e.W + 1);
            }
            Console.WriteLine(ans);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public static void Main(string[] args) => new Thread(new Program().Solve, 150000000).Start();
}

class Tree
{
    int N;
    List<(int to, int w)>[] Edge;
    int[] Depth;

    List<int>[] Ancestor;
    List<int>[] MaxW;
    public Tree(int n, List<Edge> ls)
    {
        N = n;
        Edge = new List<(int to, int w)>[N];
        Ancestor = new List<int>[N];
        MaxW = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            Edge[i] = new List<(int to, int w)>();
            Ancestor[i] = new List<int>();
            MaxW[i] = new List<int>();
        }

        foreach (var e in ls)
        {
            Edge[e.U].Add((e.V, e.W));
            Edge[e.V].Add((e.U, e.W));
        }

        var q = new Queue<int>();
        Depth = new int[N];
        for (int i = 0; i < N; i++)
        {
            Depth[i] = -1;
        }

        q.Enqueue(0);
        Depth[0] = 0;
        while (q.Count > 0)
        {
            var d = q.Dequeue();
            foreach (var t in Edge[d])
            {
                if (Depth[t.to] != -1) continue;
                Depth[t.to] = Depth[d] + 1;
                q.Enqueue(t.to);
                Ancestor[t.to].Add(d);
                MaxW[t.to].Add(t.w);
            }

            if (Depth[d] > 0)
            {
                for (int i = 0; i < Ancestor[Ancestor[d][i]].Count; i++)
                {
                    Ancestor[d].Add(Ancestor[Ancestor[d][i]][i]);
                    MaxW[d].Add(Math.Max(MaxW[Ancestor[d][i]][i], MaxW[d][i]));
                }
            }
        }
    }



    // u,v間の最大
    public int GetMaxW(int u, int v)
    {
        var lca = LCA(u, v);
        return Math.Max(GetMaxW2(u, lca), GetMaxW2(v, lca));
    }

    public int GetMaxW2(int u, int a)
    {
        int result = int.MinValue;
        for (int i = 20; i >= 0; i--)
        {
            if ((1 << i) <= Depth[u] - Depth[a])
            {
                result = Math.Max(result, MaxW[u][i]);
                u = Ancestor[u][i];
            }
        }
        return result;
    }

    public int LCA(int u, int v)
    {
        if (Depth[u] > Depth[v])
        {
            return LCA(v, u);
        }

        v = GetAncestor(v, Depth[v] - Depth[u]);

        if (u == v) return u;


        for (int i = 20; i >= 0; i--)
        {
            if (i >= Ancestor[u].Count) continue;
            if (Ancestor[u][i] == Ancestor[v][i]) continue;
            u = Ancestor[u][i];
            v = Ancestor[v][i];
        }

        return Ancestor[u][0];
    }

    // vのh世代上
    public int GetAncestor(int v, int h)
    {
        for (int i = 20; i >= 0; i--)
        {
            if ((1 << i) <= h)
            {
                v = Ancestor[v][i];
                h -= (1 << i);
            }
        }

        return v;
    }
}

namespace CompLib.Graph
{
    class UnionFind
    {
        private readonly int[] _parent, _size;

        public UnionFind(int size)
        {
            _parent = new int[size];
            _size = new int[size];
            for (int i = 0; i < size; i++)
            {
                _parent[i] = i;
                _size[i] = 1;
            }
        }

        /// <summary>
        /// iが含まれる木の根を調べる
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int Find(int i) => _parent[i] == i ? i : Find(_parent[i]);

        /// <summary>
        /// x,yが同じグループに含まれるか?
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Same(int x, int y) => Find(x) == Find(y);

        /// <summary>
        /// xとyを同じグループにする 元々同じグループならfalseを返す
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Connect(int x, int y)
        {
            x = Find(x);
            y = Find(y);
            if (x == y) return false;

            // データ構造をマージする一般的なテク
            if (_size[x] > _size[y])
            {
                _parent[y] = x;
                _size[x] += _size[y];
            }
            else
            {
                _parent[x] = y;
                _size[y] += _size[x];
            }

            return true;
        }

        /// <summary>
        /// iが含まれるグループのサイズ
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int GetSize(int i) => _size[Find(i)];
    }
}

struct Edge
{
    public int U, V, W;
    public Edge(int u, int v, int w)
    {
        U = u;
        V = v;
        W = w;
    }
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
