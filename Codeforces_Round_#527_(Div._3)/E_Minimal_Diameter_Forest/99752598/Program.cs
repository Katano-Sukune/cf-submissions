using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.DataStructure;

public class Program
{
    private int N, M;
    private List<int>[] E;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        E = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<int>();
        }

        var uf = new UnionFind(N);
        for (int i = 0; i < M; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            uf.Connect(u, v);
            E[u].Add(v);
            E[v].Add(u);
        }

        var grp = uf.Groups();
        int len = grp.Length;


        // 各頂点から一番遠い頂点、距離
        (int v, int d)[] ar = new (int v, int d)[N];
        for (int i = 0; i < N; i++)
        {
            ar[i] = Go(i, -1, 0);
        }

        // 各木
        // 一番遠い頂点までの距離最小の頂点
        (int v, int d)[] ar2 = new (int v, int d)[len];
        for (int i = 0; i < len; i++)
        {
            int tv = -1;
            int td = int.MaxValue;
            foreach (int j in grp[i])
            {
                if (ar[j].d < td)
                {
                    td = ar[j].d;
                    tv = j;
                }
            }

            ar2[i] = (tv, td);
        }

        int d = 0;

        // 各木の直径
        foreach (List<int> ls in grp)
        {
            d = Math.Max(d, ar[ar[ls[0]].v].d);
        }

        Array.Sort(ar2, (l, r) => r.d.CompareTo(l.d));
        // 1位と2位以下つなげる
        // max(1位 + 2位+1,2位+3位+2)
        if (len >= 2)
        {
            d = Math.Max(d, ar2[0].d + ar2[1].d + 1);
        }

        if (len >= 3)
        {
            d = Math.Max(d, ar2[1].d + ar2[2].d + 2);
        }

        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        Console.WriteLine(d);
        for (int i = 1; i < len; i++)
        {
            Console.WriteLine($"{ar2[0].v + 1} {ar2[i].v + 1}");
        }
        Console.Out.Flush();
    }

    (int v, int d) Go(int cur, int par, int depth)
    {
        int tv = cur;
        int td = depth;
        foreach (int to in E[cur])
        {
            if (to == par) continue;
            (int tv2, int td2) = Go(to, cur, depth + 1);
            if (td < td2)
            {
                tv = tv2;
                td = td2;
            }
        }

        return (tv, td);
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
        public List<int>[] Groups()
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