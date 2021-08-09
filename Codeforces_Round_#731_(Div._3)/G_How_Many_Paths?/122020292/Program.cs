using System;
using CompLib.Util;
using CompLib.Graph;
using System.Collections.Generic;
using System.IO;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
#if !DEBUG
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
#endif
        int t = sc.NextInt();
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
        int[] a = new int[m];
        int[] b = new int[m];
        bool[] f = new bool[n];
        for (int i = 0; i < m; i++)
        {
            a[i] = sc.NextInt() - 1;
            b[i] = sc.NextInt() - 1;
            if (a[i] == b[i]) f[a[i]] = true;
        }


        var e = new List<int>[n];
        for (int i = 0; i < n; i++)
        {
            e[i] = new List<int>();
        }
        var scc = new SCC(n);
        for (int i = 0; i < m; i++)
        {
            if (a[i] == b[i]) continue;
            e[a[i]].Add(b[i]);
            scc.AddEdge(a[i], b[i]);
        }

        int[] ans = new int[n];
        var q = new Queue<int>(n);
        q.Enqueue(0);
        ans[0] = 1;
        while (q.Count > 0)
        {
            int dq = q.Dequeue();
            foreach (int to in e[dq])
            {
                if (ans[to] == 0) q.Enqueue(to);
                ans[to]++;
            }
        }

        var tmp = scc.GetSCCs();
        foreach (var ls in tmp)
        {

            if (ls.Count > 1 || f[ls[0]])
            {
                if (ans[ls[0]] == 0) continue;
                ans[ls[0]] = -1;
                q.Enqueue(ls[0]);
            }
        }

        while (q.Count > 0)
        {
            int dq = q.Dequeue();
            foreach (int to in e[dq])
            {
                if (ans[to] == -1) continue;
                ans[to] = -1;
                q.Enqueue(to);
            }
        }

        for (int i = 0; i < n; i++)
        {
            if (ans[i] >= 2)
            {
                ans[i] = 2;
                q.Enqueue(i);
            }
        }

        while (q.Count > 0)
        {
            int dq = q.Dequeue();
            foreach (int to in e[dq])
            {
                if (ans[to] == -1 || ans[to] == 2) continue;
                ans[to] = 2;
                q.Enqueue(to);
            }
        }

        Console.WriteLine(string.Join(" ", ans));

    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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