using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.DataStructure;

public class Program
{
    private int N;
    private int[] A;
    private List<int>[] E;


    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();
        E = new List<int>[N];

        for (int i = 0; i < N; i++)
        {
            E[i] = new List<int>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            E[u].Add(v);
            E[v].Add(u);
        }

        var sorted = new int[N];
        for (int i = 0; i < N; i++)
        {
            sorted[i] = i;
        }

        Array.Sort(sorted, (l, r) => A[l].CompareTo(A[r]));

        long sumMax;
        {
            sumMax = 0;
            var uf = new UnionFind(N);
            bool[] flag = new bool[N];
            foreach (int i in sorted)
            {
                var ls = new List<int>(E[i].Count);

                foreach (int to in E[i])
                {
                    if (flag[to])
                    {
                        ls.Add(uf.GetSize(to));
                        uf.Connect(i, to);
                    }
                }

                long sum = 0;
                foreach (int size in ls)
                {
                    sumMax += sum * size * A[i];
                    sum += size;
                }

                flag[i] = true;
                sumMax += sum * A[i];
            }
        }

        Array.Reverse(sorted);
        long sumMin;
        {
            sumMin = 0;
            var uf = new UnionFind(N);
            bool[] flag = new bool[N];
            foreach (int i in sorted)
            {
                var ls = new List<int>(E[i].Count);

                foreach (int to in E[i])
                {
                    if (flag[to])
                    {
                        ls.Add(uf.GetSize(to));
                        uf.Connect(i, to);
                    }
                }

                long sum = 0;
                foreach (int size in ls)
                {
                    sumMin += sum * size * A[i];
                    sum += size;
                }

                sumMin += sum * A[i];
                flag[i] = true;
            }
        }

        Console.WriteLine(sumMax - sumMin);

        // (1,2) 2
        // (1,3) 3
        // (1,4) 2
        // (2,3) 3
        // (2,4) 2
        // (3,4) 3
        // sum = 15
        // Console.WriteLine(sumMax);

        // (1,2) = 2
        // (1,3) = 2
        // (1,4) = 1
        // (2,3) = 2
        // (2,4) = 1
        // (3,4) = 1
        // sum = 9
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