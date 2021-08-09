using System;
using System.Linq;
using CompLib.Util;
using System.Collections.Generic;
using CompLib.Algorithm;
using System.Text;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        var edge = new List<int>[n];
        for (int i = 0; i < n; i++)
        {
            edge[i] = new List<int>();
        }

        for (int i = 0; i < n - 1; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            edge[u].Add(v);
            edge[v].Add(u);
        }
        var tree = new TreeEx(edge, 0);

        int q = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < q; i++)
        {
            int x = sc.NextInt() - 1;
            int y = sc.NextInt() - 1;
            // x->yの距離

            int a = sc.NextInt() - 1;
            int b = sc.NextInt() - 1;

            int k = sc.NextInt();

            int dist = tree.Dist(a, b);
      
            if (dist <= k && (dist % 2 == k % 2))
            {
                sb.AppendLine("YES");
                continue;
            }

            int dist2 = tree.Dist(a, x) + 1 + tree.Dist(y, b);
            if (dist2 <= k && (dist2 % 2 == k % 2))
            {
                sb.AppendLine("YES");
                continue;
            }

            int dist3 = tree.Dist(a, y) + 1 + tree.Dist(x, b);
            if (dist3 <= k && (dist3 % 2 == k % 2))
            {
                sb.AppendLine("YES");
                continue;
            }
            // Console.WriteLine($"{i} {dist} {tree.Dist(a, x)}+1+{tree.Dist(y, b)}={dist2} {dist3}");
            sb.AppendLine("NO");
        }

        // Console.WriteLine(tree.Dist(2, 1));

        Console.Write(sb);

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

            for (int t = 0; ; t++)
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
            return Depth(a) + Depth(b) - 2* Depth(LowestCommonAncestor(a, b));
        }
    }
}