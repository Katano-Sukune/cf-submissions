using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Algorithm;
using CompLib.Graph;
using CompLib.Util;

public class Program
{
    private int N;
    private int M;
    private int K;

    private TreeEx T;

    private List<E> UnUsed;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        K = sc.NextInt();

        var uf = new UnionFind(N);
        var ls = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            ls[i] = new List<int>();
        }

        UnUsed = new List<E>();

        for (int i = 0; i < M; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            if (uf.Unite(u, v))
            {
                ls[u].Add(v);
                ls[v].Add(u);
            }
            else
            {
                UnUsed.Add(new E(u, v));
            }
        }

        T = new TreeEx(ls);

        if (M == N - 1)
        {
            // 木
            Tree();
        }
        else
        {
            NonTree();
        }
    }

    void Tree()
    {
        // 二分グラフ
        List<int> b = new List<int>();
        List<int> w = new List<int>();
        for (int i = 0; i < N; i++)
        {
            if (T.Depth(i) % 2 == 0) b.Add(i);
            else w.Add(i);
        }

        int t = (K + 1) / 2;

        var l = b.Count > w.Count ? b : w;
        var ans = new List<int>();
        for (int i = 0; i < t; i++)
        {
            ans.Add(l[i] + 1);
        }

        Console.WriteLine(1);
        Console.WriteLine(string.Join(" ", ans));
    }

    void NonTree()
    {
        // 小さいサークル
        foreach (E e in UnUsed)
        {
            var a = T.LowestCommonAncestor(e.S, e.T);
            int dist = T.Depth(e.S) + T.Depth(e.T) - 2 * T.Depth(a);
            if (dist + 1 <= K)
            {
                Console.WriteLine(2);
                var ls = Circle(e.S, a, e.T);
                Console.WriteLine(ls.Count);
                Console.WriteLine(string.Join(" ", ls));
                return;
            }
        }

        // K以下のループが無い

        var aa = T.LowestCommonAncestor(UnUsed[0].S, UnUsed[0].T);
        var c = Circle(UnUsed[0].S, aa, UnUsed[0].T);
        // cを一つおきに
        var t = (K + 1) / 2;
        var ans = new List<int>();
        for (int i = 0; i < t; i++)
        {
            ans.Add(c[i * 2]);
        }

        Console.WriteLine(1);
        Console.WriteLine(string.Join(" ", ans));
    }

    List<int> Circle(int s, int a, int t)
    {
        var sToa = new List<int>();
        var tToa = new List<int>();
        int cur = s;
        while (cur != a)
        {
            sToa.Add(cur + 1);
            cur = T.Parent(cur);
        }

        cur = t;
        while (cur != a)
        {
            tToa.Add(cur + 1);
            cur = T.Parent(cur);
        }

        sToa.Add(a + 1);
        for (int i = tToa.Count() - 1; i >= 0; i--)
        {
            sToa.Add(tToa[i]);
        }

        return sToa;
    }

    public static void Main(string[] args) => new Program().Solve();
}

struct E
{
    public int S, T;

    public E(int s, int t)
    {
        S = s;
        T = t;
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
        public bool Unite(int x, int y)
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

namespace CompLib.Algorithm
{
    public class Tree
    {
        private readonly List<int>[] _edge;
        protected List<int>[] _child;
        protected readonly int _root;
        protected readonly int _size;

        private readonly int[] _depth;
        private readonly int[] _parent;

        public Tree(List<int>[] edge, int root = 0)
        {
            _root = root;
            _size = edge.Length;
            _edge = edge;
            _child = new List<int>[_size];
            for (int i = 0; i < _size; i++)
            {
                _child[i] = new List<int>();
            }

            _depth = new int[_size].Select(i => -1).ToArray();
            _parent = new int[_size];

            _depth[_root] = 0;
            _parent[_root] = -1;
            var q = new Queue<int>();
            q.Enqueue(_root);

            while (q.Count > 0)
            {
                var d = q.Dequeue();
                foreach (int i in _edge[d])
                {
                    if (_depth[i] != -1) continue;
                    _child[d].Add(i);
                    _depth[i] = _depth[d] + 1;
                    _parent[i] = d;
                    q.Enqueue(i);
                }
            }
        }

        /// <summary>
        /// 各頂点の深さ
        /// </summary>
        /// <returns></returns>
        public int[] DepthAll()
        {
            return _depth;
        }

        /// <summary>
        /// 頂点nの深さ
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int Depth(int n)
        {
            return _depth[n];
        }

        /// <summary>
        /// 各頂点の親 根の親は-1
        /// </summary>
        /// <returns></returns>
        public int[] ParentAll()
        {
            return _parent;
        }

        /// <summary>
        /// 頂点nの親 nが根なら-1を返す
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int Parent(int n)
        {
            return _parent[n];
        }

        /// <summary>
        /// 根がnの部分木の頂点集合
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int[] SubTreeNodes(int n)
        {
            var l = new List<int>();
            SearchNodes(n, l);
            return l.ToArray();
        }

        private void SearchNodes(int r, List<int> l)
        {
            l.Add(r);
            foreach (int i in _child[r])
            {
                SearchNodes(i, l);
            }
        }


        public int DeepestSubtreeNode(int r, out int node)
        {
            var l = SubTreeNodes(r);

            int result = Depth(r);
            node = r;

            foreach (int i in l)
            {
                if (Depth(i) > result)
                {
                    result = Depth(i);
                    node = i;
                }
            }

            return result;
        }
    }
}

namespace CompLib.Algorithm
{
    /// <summary>
    /// Treeに機能追加
    /// </summary>
    public class TreeEx : Tree
    {
        private readonly List<int>[] _ancestor;

        public TreeEx(List<int>[] tree, int root = 0) : base(tree, root)
        {
            _ancestor = new List<int>[_size];
            for (int i = 0; i < _size; i++)
            {
                _ancestor[i] = new List<int>();
            }

            var q = new Queue<int>();
            q.Enqueue(_root);
            while (q.Count > 0)
            {
                var d = q.Dequeue();
                if (Depth(d) > 0)
                {
                    _ancestor[d].Add(Parent(d));
                    for (int i = 0; i < _ancestor[_ancestor[d][i]].Count; i++)
                    {
                        _ancestor[d].Add(_ancestor[_ancestor[d][i]][i]);
                    }
                }

                foreach (int i in _child[d])
                {
                    q.Enqueue(i);
                }
            }
        }

        /// <summary>
        /// 頂点nのi世代上
        /// </summary>
        /// <param name="n"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public int Ancestor(int n, int i)
        {
            if (i == 0)
            {
                return n;
            }

            for (int t = 0;; t++)
            {
                if (((1 << t) & i) > 0)
                {
                    return Ancestor(_ancestor[n][t], i - (1 << t));
                }
            }
        }

        public int LowestCommonAncestor(int a, int b)
        {
            if (Depth(b) < Depth(a))
            {
                return LowestCommonAncestor(b, a);
            }

            // depth(a) <= depth(b)
            b = Ancestor(b, Depth(b) - Depth(a));
            if (a == b) return a;

            int ok = -1;
            int ng = _ancestor[a].Count;
            while (ng - ok > 1)
            {
                int med = (ok + ng) / 2;
                if (_ancestor[a][med] == _ancestor[b][med])
                {
                    ng = med;
                }
                else
                {
                    ok = med;
                }
            }

            if (ok == -1)
            {
                return Parent(a);
            }

            return LowestCommonAncestor(_ancestor[a][ok], _ancestor[b][ok]);
        }

        public int Dist(int a, int b)
        {
            return Depth(a) + Depth(b) - 2 * Depth(LowestCommonAncestor(a, b));
        }
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