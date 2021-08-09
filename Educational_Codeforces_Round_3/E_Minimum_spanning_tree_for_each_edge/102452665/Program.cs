using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using CompLib.DataStructure;
using System.Collections.Generic;
using CompLib.Graph;

public class Program
{
    int N, M;
    int[] U, V, W;
    public void Solve()
    {
        var sc = new Scanner();
        //#if DEBUG
        //        N = 200000;
        //        M = 200000;
        //        U = new int[M];
        //        V = new int[M];
        //        W = new int[M];
        //        for (int i = 0; i < M; i++)
        //        {
        //            U[i] = i;
        //            V[i] = (i + 1) % N;
        //            W[i] = 1;
        //        }
        // #else
        N = sc.NextInt();
        M = sc.NextInt();

        U = new int[M];
        V = new int[M];
        W = new int[M];
        for (int i = 0; i < M; i++)
        {
            U[i] = sc.NextInt() - 1;
            V[i] = sc.NextInt() - 1;
            W[i] = sc.NextInt();
        }
        // #endif

        var sorted = new int[M];
        for (int i = 0; i < M; i++)
        {
            sorted[i] = i;
        }

        Array.Sort(sorted, (l, r) => W[l].CompareTo(W[r]));

        long sum = 0;
        var uf = new UnionFind(N);

        var lca = new LowestCommonAncestor(N);


        foreach (var i in sorted)
        {
            if (uf.Connect(U[i], V[i]))
            {
                lca.AddEdge(U[i], V[i], W[i]);
                sum += W[i];
            }
        }
        lca.Build();
        long[] ans = new long[M];
        for (int i = 0; i < M; i++)
        {
            // Console.WriteLine($"{U[i]} {V[i]} {lca.LCA(U[i], V[i])}");
            ans[i] = sum + W[i] - lca.Dist(U[i], V[i]);
        }


        Console.WriteLine(string.Join("\n", ans));
    }

    // public static void Main(string[] args) => new Program().Solve();
    public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}


namespace CompLib.DataStructure
{
    using System.Collections.Generic;
    using System.Linq;
    class UnionFind
    {
        private readonly int _n;
        private readonly int[] _parent, _size;

        /// <summary>
        /// n頂点の無向グラフに 1.辺を追加, 2.2頂点が同じ連結成分に属するか判定 ができるデータ構造
        /// </summary>
        /// <param name="n">頂点の個数</param>
        public UnionFind(int n)
        {
            _n = n;
            _parent = new int[_n];
            _size = new int[_n];
            for (int i = 0; i < _n; i++)
            {
                _parent[i] = i;
                _size[i] = 1;
            }
        }

        /// <summary>
        /// iがいる連結成分の代表値
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int Find(int i) => _parent[i] == i ? i : Find(_parent[i]);

        /// <summary>
        /// x,yが同じ連結成分にいるか?
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Same(int x, int y) => Find(x) == Find(y);

        /// <summary>
        /// (x, y)に辺を追加する
        /// </summary>
        /// <remarks>
        /// ACLでは連結された代表値を返しますが、ここでは連結できたか?を返します
        /// </remarks>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>x,yが違う連結成分だったならtrueを返す</returns>
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
        /// iが含まれる成分のサイズ
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int GetSize(int i) => _size[Find(i)];

        /// <summary>
        /// 連結成分のリスト
        /// </summary>
        /// <returns></returns>
        List<int>[] Groups()
        {
            var leaderBuf = new int[_n];
            var groupSize = new int[_n];
            for (int i = 0; i < _n; i++)
            {
                leaderBuf[i] = Find(i);
                groupSize[leaderBuf[i]]++;
            }

            var result = new List<int>[_n];
            for (int i = 0; i < _n; i++)
            {
                result[i] = new List<int>(groupSize[i]);
            }

            for (int i = 0; i < _n; i++)
            {
                result[leaderBuf[i]].Add(i);
            }

            return result.Where(ls => ls.Count > 0).ToArray();
        }
    }
}


namespace CompLib.Graph
{

    using Num = System.Int64;
    using System.Collections.Generic;

    class LowestCommonAncestor
    {
        public readonly int N;

        private List<S>[] E;

        private int[] _depth;
        private List<int>[] _ancestor;
        private List<Num>[] _sum;

        public LowestCommonAncestor(int n)
        {
            N = n;
            E = new List<S>[N];
            for (int i = 0; i < N; i++)
            {
                E[i] = new List<S>();
            }
        }

        /// <summary>
        /// 辺を追加
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="w"></param>
        public void AddEdge(int u, int v, Num w)
        {
            E[u].Add(new S(v, w));
            E[v].Add(new S(u, w));
        }

        public void Build(int root = 0)
        {


            _depth = new int[N];
            _ancestor = new List<int>[N];
            _sum = new List<Num>[N];
            for (int i = 0; i < N; i++)
            {
                _ancestor[i] = new List<int>();
                _sum[i] = new List<Num>();
            }

            var q = new Queue<int>();
            q.Enqueue(root);
            for (int i = 0; i < N; i++)
            {
                _depth[i] = -1;
            }
            _depth[root] = 0;
            while (q.Count > 0)
            {
                var cur = q.Dequeue();
                if (cur != root)
                {
                    while (_ancestor[_ancestor[cur][_ancestor[cur].Count - 1]].Count >= _ancestor[cur].Count)
                    {
                        int cnt = _ancestor[cur].Count;
                        int last = _ancestor[cur][cnt - 1];
                        _ancestor[cur].Add(_ancestor[last][cnt - 1]);
                        _sum[cur].Add(Math.Max(_sum[cur][cnt - 1], _sum[last][cnt - 1]));
                    }
                }
                foreach (var t in E[cur])
                {
                    if (_depth[t.To] != -1) continue;
                    q.Enqueue(t.To);
                    _depth[t.To] = _depth[cur] + 1;
                    _ancestor[t.To].Add(cur);
                    _sum[t.To].Add(t.W);
                }
            }

        }

        /// <summary>
        /// u,vの距離
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public Num Dist(int u, int v)
        {
            if (_depth[u] > _depth[v])
            {
                (u, v) = (v, u);
            }

            Num ans = Num.MinValue;
            if (_depth[v] > _depth[u])
            {
                int diff = _depth[v] - _depth[u];
                // vをdiff上げる
                for (int i = 0; diff > 0; i++, diff /= 2)
                {
                    if (diff % 2 == 1)
                    {
                        ans = Math.Max(ans, _sum[v][i]);
                        v = _ancestor[v][i];
                    }
                }
            }

            if (u == v) return ans;

            for (int i = _ancestor[u].Count - 1; i >= 0; i--)
            {
                if (i >= _ancestor[u].Count) continue;
                if (_ancestor[u][i] == _ancestor[v][i]) continue;
                ans = Math.Max(ans, _sum[u][i]);
                ans = Math.Max(ans, _sum[v][i]);
                u = _ancestor[u][i];
                v = _ancestor[v][i];

            }

            ans = Math.Max(ans, _sum[u][0]);
            ans = Math.Max(ans, _sum[v][0]);
            return ans;
        }

        /// <summary>
        /// u,vの最小共通祖先
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public int LCA(int u, int v)
        {

            if (_depth[u] > _depth[v])
            {
                (u, v) = (v, u);
            }

            if (_depth[v] > _depth[u])
            {
                int diff = _depth[v] - _depth[u];
                // vをdiff上げる
                for (int i = 0; diff > 0; i++, diff >>= 1)
                {
                    if ((diff & 1) == 1) v = _ancestor[v][i];
                }
            }

            if (u == v) return u;

            for (int i = _ancestor[u].Count - 1; i >= 0; i--)
            {
                if (i >= _ancestor[u].Count) continue;
                if (_ancestor[u][i] == _ancestor[v][i]) continue;
                u = _ancestor[u][i];
                v = _ancestor[v][i];
            }

            return _ancestor[u][0];
        }

        struct S
        {
            public int To;
            public Num W;
            public S(int to, Num w)
            {
                To = to;
                W = w;
            }
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
