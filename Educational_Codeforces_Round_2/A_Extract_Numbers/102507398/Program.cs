using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        string s = sc.Next();

        var ar = s.Split(',', ';');
        List<string> integer = new List<string>();
        List<string> other = new List<string>();

        foreach (var str in ar)
        {
            if (str.Length == 0)
            {
                other.Add(str);
            }
            else if (str == "0")
            {
                integer.Add(str);
            }
            else if (str[0] == '0')
            {
                other.Add(str);
            }
            else
            {
                bool f = true;
                foreach (var c in str)
                {
                    if (c < '0' || c > '9') f = false;
                }
                if (f) integer.Add(str);
                else other.Add(str);
            }
        }
        if (integer.Count > 0) Console.WriteLine($"\"{string.Join(",", integer)}\"");
        else Console.WriteLine("-");

        if (other.Count > 0) Console.WriteLine($"\"{string.Join(",", other)}\"");
        else Console.WriteLine("-");
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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
                        _sum[cur].Add(_sum[cur][cnt - 1] + _sum[last][cnt - 1]);
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

            Num ans = 0;
            if (_depth[v] > _depth[u])
            {
                int diff = _depth[v] - _depth[u];
                // vをdiff上げる
                for (int i = 0; diff > 0; i++, diff /= 2)
                {
                    if (diff % 2 == 1)
                    {
                        ans += _sum[v][i];
                        v = _ancestor[v][i];
                    }
                }
            }

            if (u == v) return ans;

            for (int i = _ancestor[u].Count - 1; i >= 0; i--)
            {
                if (i >= _ancestor[u].Count) continue;
                if (_ancestor[u][i] == _ancestor[v][i]) continue;
                ans += _sum[u][i];
                ans += _sum[v][i];
                u = _ancestor[u][i];
                v = _ancestor[v][i];

            }

            ans += _sum[v][0];
            ans += _sum[u][0];
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
