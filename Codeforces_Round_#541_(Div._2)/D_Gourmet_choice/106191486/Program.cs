using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using CompLib.Graph;
using System.Collections.Generic;
using CompLib.DataStructure;

public class Program
{
    int N, M;
    string[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        A = new string[N];
        for (int i = 0; i < N; i++)
        {
            A[i] = sc.Next();
        }



        var scc = new SCC(N + M);
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                switch (A[i][j])
                {
                    case '>':
                        scc.AddEdge(N + j, i);
                        break;
                    case '<':
                        scc.AddEdge(i, N + j);
                        break;
                    case '=':
                        scc.AddEdge(i, N + j);
                        scc.AddEdge(N + j, i);
                        break;
                }
            }
        }

        var grp = scc.GetSCCs();
        var idx = new int[N + M];
        for (int i = 0; i < grp.Length; i++)
        {
            foreach (var j in grp[i])
            {
                idx[j] = i;
            }
        }

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                if (A[i][j] == '=')
                {
                    if (idx[i] != idx[j + N])
                    {
                        Console.WriteLine("No");
                        return;
                    }
                }
                else if (A[i][j] == '>')
                {
                    if (idx[i] <= idx[N + j])
                    {
                        Console.WriteLine("No");
                        return;
                    }
                }
                else if (A[i][j] == '<')
                {
                    if (idx[i] >= idx[N + j])
                    {
                        Console.WriteLine("No");
                        return;
                    }
                }
            }
        }

        int[] r = new int[N];
        int[] c = new int[M];
        Array.Fill(r, -1);
        Array.Fill(c, -1);

        var uf = new UnionFind(N + M);
        foreach (var ls in grp)
        {
            foreach (var j in ls)
            {
                uf.Connect(j, ls[0]);
            }
        }

        var e = new List<int>[N + M];
        for (int i = 0; i < N + M; i++)
        {
            e[i] = new List<int>();
        }

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                if (A[i][j] == '=') continue;
                if (A[i][j] == '<')
                {
                    e[uf.Find(j + N)].Add(uf.Find(i));
                }
                else
                {
                    e[uf.Find(i)].Add(uf.Find(j + N));
                }
            }
        }

        foreach (var ls in grp)
        {
            int max = 0;
            var lead = uf.Find(ls[0]);
            foreach (var to in e[lead])
            {
                if (to < N) max = Math.Max(max, r[to]);
                else max = Math.Max(max, c[to - N]);
            }

            foreach (var i in ls)
            {
                if (i < N) r[i] = max + 1;
                else c[i - N] = max + 1;
            }
        }

        Console.WriteLine("YES");
        Console.WriteLine(string.Join(" ", r));
        Console.WriteLine(string.Join(" ", c));
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    class SCC
    {
        private readonly int _n;
        private readonly List<(int f, int t)> _edges;

        // start[i+1] - start[i] iから生える辺の本数
        // eList[start[i]] ~ eList[start[i+1]-1]がiから生える辺の行き先 
        private int[] _start;
        private int[] _eList;


        // 行きがけ順現在地, 強連結成分の個数
        private int _nowOrd, _groupNum;
        List<int> _visited;

        // iから行ける頂点のordの最小値 ,行きがけ順, iが含まれるトポロジカル順序
        int[] _low, _ord, _ids;

        public SCC(int n)
        {
            _n = n;
            _edges = new List<(int f, int t)>();
        }

        /// <summary>
        /// fromからtoへ有向辺を追加します
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void AddEdge(int from, int to)
        {
            Debug.Assert(0 <= from && from < _n);
            Debug.Assert(0 <= to && to < _n);
            _edges.Add((from, to));
        }

        /// <summary>
        /// 強連結成分分解して、トポロジカル順序で返します
        /// </summary>
        /// <remarks>内部でExecute()します</remarks>
        /// <returns></returns>
        public List<int>[] GetSCCs()
        {
            Execute();

            // ACLだとvec.reserveしてるけどいる?
            List<int>[] groups = new List<int>[_groupNum];
            for (int i = 0; i < _groupNum; i++)
            {
                groups[i] = new List<int>();
            }

            for (int i = 0; i < _n; i++)
            {
                groups[_ids[i]].Add(i);
            }

            return groups;
        }

        /// <summary>
        /// 強連結成分分解をします
        /// </summary>
        public void Execute()
        {
            Build();
            _nowOrd = 0;
            _groupNum = 0;
            _visited = new List<int>(_n);
            _low = new int[_n];
            _ord = new int[_n];

            // CodeforcesだとArrayにFill()が無い
            for (int i = 0; i < _n; i++)
            {
                _ord[i] = -1;
            }
            _ids = new int[_n];

            for (int i = 0; i < _n; i++)
            {
                if (_ord[i] == -1) Go(i);
            }

            for (int i = 0; i < _n; i++)
            {
                _ids[i] = _groupNum - 1 - _ids[i];
            }
        }

        /// <summary>
        /// 強連結成分の個数
        /// </summary>
        /// <remarks>
        /// Execute()してから呼んでください
        /// </remarks>
        public int GroupsCount
        {
            get
            {
                return _groupNum;
            }
        }

        /// <summary>
        /// vが含まれる強連結成分のトポロジカル順序
        /// </summary>
        /// <param name="v"></param>
        /// <remarks>
        /// Execute()してから呼んでください
        /// </remarks>
        /// <returns></returns>
        public int GetId(int v)
        {
            return _ids[v];
        }

        void Go(int v)
        {
            _low[v] = _ord[v] = _nowOrd++;
            _visited.Add(v);
            for (int i = _start[v]; i < _start[v + 1]; i++)
            {
                int to = _eList[i];
                if (_ord[to] == -1)
                {
                    Go(to);
                    _low[v] = Math.Min(_low[v], _low[to]);
                }
                else
                {
                    _low[v] = Math.Min(_low[v], _ord[to]);
                }
            }

            if (_low[v] == _ord[v])
            {
                while (true)
                {
                    int u = _visited[_visited.Count - 1];
                    _visited.RemoveAt(_visited.Count - 1);
                    _ord[u] = _n;
                    _ids[u] = _groupNum;
                    if (u == v) break;
                }
                _groupNum++;
            }
        }

        private void Build()
        {
            _start = new int[_n + 1];
            _eList = new int[_edges.Count];

            foreach (var e in _edges)
            {
                _start[e.f + 1]++;
            }

            for (int i = 1; i <= _n; i++)
            {
                _start[i] += _start[i - 1];
            }

            var counter = new int[_n + 1];
            Array.Copy(_start, counter, _n + 1);

            foreach (var e in _edges)
            {
                _eList[counter[e.f]++] = e.t;
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
