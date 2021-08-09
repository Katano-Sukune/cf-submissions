using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.DataStructure;

public class Program
{
    private int N, M;
    private int[] X, Y;
    private bool[] Z;

    private int A, B;

    private List<(int to, bool z)>[] E2;
    private bool[] H;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        X = new int[M];
        Y = new int[M];
        Z = new bool[M];
        for (int i = 0; i < M; i++)
        {
            X[i] = sc.NextInt() - 1;
            Y[i] = sc.NextInt() - 1;
            Z[i] = sc.Next() == "1";
        }

        var b = new Bridge(N);
        for (int i = 0; i < M; i++)
        {
            b.AddEdge(X[i], Y[i]);
        }

        var ar = b.Execute();
        bool[] isBridge = new bool[M];
        foreach (int i in ar)
        {
            isBridge[i] = true;
        }


        A = sc.NextInt() - 1;
        B = sc.NextInt() - 1;

        // 橋以外まとめる
        // 木
        // A->Bに1があるYES

        var uf = new UnionFind(N);
        for (int i = 0; i < M; i++)
        {
            if (isBridge[i]) continue;
            uf.Connect(X[i], Y[i]);
        }

        E2 = new List<(int to, bool z)>[N];
        H = new bool[N];
        for (int i = 0; i < M; i++)
        {
            if (isBridge[i] || !Z[i]) continue;
            H[uf.Find(X[i])] = true;
        }

        for (int i = 0; i < N; i++)
        {
            E2[i] = new List<(int to, bool z)>();
        }

        foreach (var i in ar)
        {
            int la = uf.Find(X[i]);
            int lb = uf.Find(Y[i]);
            E2[la].Add((lb, Z[i]));
            E2[lb].Add((la, Z[i]));
        }

        Console.WriteLine(Go(uf.Find(A), -1, uf.Find(B), false) ? "YES" : "NO");
    }

    bool Go(int cur, int par, int t, bool f)
    {
        f |= H[cur];
        if (cur == t) return f;
        foreach ((int to, bool z) in E2[cur])
        {
            if(to == par) continue;
            if (Go(to, cur, t, f | z)) return true;
        }

        return false;
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


class Bridge
{
    private readonly int N;
    private List<(int to, int num)>[] E;
    private int Ptr;

    private int P;
    private int[] Pre, Low;
    private List<int> Result;

    public Bridge(int n)
    {
        N = n;
        E = new List<(int to, int num)>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<(int to, int num)>();
        }

        Ptr = 0;
    }

    public int AddEdge(int f, int t)
    {
        E[f].Add((t, Ptr));
        E[t].Add((f, Ptr));
        return Ptr++;
    }


    public int[] Execute()
    {
        // 行きがけ順
        Pre = new int[N];
        Array.Fill(Pre, -1);
        // iから行けるpreの最小値
        Low = new int[N];

        Result = new List<int>();
        P = 0;
        Go(0, -1);

        return Result.ToArray();
    }

    private int Go(int cur, int prev)
    {
        Pre[cur] = P++;
        Low[cur] = Pre[cur];
        foreach ((int to, int num) in E[cur])
        {
            if (Pre[to] == -1)
            {
                Low[cur] = Math.Min(Low[cur], Go(to, cur));
                if (Low[to] == Pre[to]) Result.Add(num);
            }
            else
            {
                if (to == prev) continue;
                Low[cur] = Math.Min(Low[cur], Low[to]);
            }
        }

        return Low[cur];
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