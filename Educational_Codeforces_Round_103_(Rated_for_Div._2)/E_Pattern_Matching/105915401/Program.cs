using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using CompLib.Graph;

public class Program
{
    int N, M, K;
    string[] P;

    int T;
    int[] Map;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        K = sc.NextInt();

        P = new string[N];
        for (int i = 0; i < N; i++)
        {
            P[i] = sc.Next();
        }

        T = 1;
        for (int i = 0; i < K; i++)
        {
            T *= 27;
        }
        Map = new int[T];
        Array.Fill(Map, -1);
        for (int i = 0; i < N; i++)
        {
            int num = 0;
            for (int j = 0; j < K; j++)
            {
                num *= 27;
                if (P[i][j] == '_') num += 26;
                else num += P[i][j] - 'a';
            }
            Map[num] = i;
        }

        var scc = new SCC(N);

        for (int i = 0; i < M; i++)
        {
            string s = sc.Next();
            int mt = sc.NextInt() - 1;
            // sと一致するやつ
            var ls = new List<int>();
            bool flag = false;
            for (int j = 0; j < (1 << K); j++)
            {
                int num = 0;
                for (int k = 0; k < K; k++)
                {
                    num *= 27;
                    if ((j & (1 << k)) > 0) num += 26;
                    else num += s[k] - 'a';
                }
                if (Map[num] != -1)
                {
                    ls.Add(Map[num]);
                    if (mt == Map[num]) flag = true;
                }
            }

            if (!flag)
            {
                Console.WriteLine("NO");
                return;
            }
            foreach (int j in ls)
            {
                if (j == mt) continue;
                scc.AddEdge(mt, j);
            }
        }

        var grp = scc.GetSCCs();

        int[] ans = new int[N];
        int ptr = 0;
        foreach (var ls in grp)
        {
            if (ls.Count >= 2)
            {
                Console.WriteLine("NO");
                return;
            }
            ans[ptr++] = ls[0] + 1;
        }
        Console.WriteLine("YES");
        Console.WriteLine(string.Join(" ", ans));

        /*
         * n個の文字列p
         * m個　　　　s
         * 
         * 各k文字
         * 
         * S_jが最初に一致するパターンがP_{MT_j}になるように並び替える
         * 
         */

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
