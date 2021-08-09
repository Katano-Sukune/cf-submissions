using System;
using System.Collections.Generic;
using System.Text;
using CompLib.Algorithm;
using CompLib.Util;
using System.Linq;

public class Program
{
    private int N, M;
    private List<int>[] Edge;
    private int[][] V;
    private int[] K;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        Edge = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            Edge[i] = new List<int>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            int a = sc.NextInt() - 1;
            int b = sc.NextInt() - 1;
            Edge[a].Add(b);
            Edge[b].Add(a);
        }

        K = new int[M];
        V = new int[M][];
        for (int i = 0; i < M; i++)
        {
            K[i] = sc.NextInt();
            V[i] = new int[K[i]];
            for (int j = 0; j < K[i]; j++)
            {
                V[i][j] = sc.NextInt() - 1;
            }
        }

        var tree = new TreeEx(Edge, 0);
        var sb = new StringBuilder();
        for (int i = 0; i < M; i++)
        {
            if (K[i] == 1)
            {
                sb.AppendLine("YES");
                continue;
            }

            Array.Sort(V[i], (l, r) => tree.Depth(l).CompareTo(tree.Depth(r)));
            bool f = true;
            for (int j = K[i] - 2; j >= 0; j--)
            {
                int a = tree.LowestCommonAncestor(V[i][K[i] - 1], V[i][j]);
                if (a != V[i][j] && a != tree.Parent(V[i][j]))
                {
                    f = false;
                    break;
                }
            }

            sb.AppendLine(f ? "YES" : "NO");
        }

        Console.Write(sb.ToString());
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
            return Depth(a) + Depth(b) - Depth(LowestCommonAncestor(a, b));
        }
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