using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Graph;
using CompLib.Util;

struct edge { public int to, from, weight; }
struct query { public int q, index; }
public class Program
{
    int N;
    int M;
    edge[] edges;
    query[] query;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        edges = new edge[N - 1];
        for (int i = 0; i < N - 1; i++)
        {
            edges[i] = new edge() { from = sc.NextInt() - 1, to = sc.NextInt() - 1, weight = sc.NextInt() };
        }
        query = new query[M];
        for (int i = 0; i < M; i++)
        {
            query[i] = new query() { q = sc.NextInt(), index = i };
        }

        var uf = new UnionFind(N);

        Array.Sort(query, (l, r) => l.q.CompareTo(r.q));
        Array.Sort(edges, (l, r) => l.weight.CompareTo(r.weight));
        long[] ans = new long[M];
        long tmp = 0;
        int index = 0;
        for (int i = 0; i < M; i++)
        {
            // weightがquery[i].q以下
            for (; index < N - 1 && edges[index].weight <= query[i].q; index++)
            {
                int from = edges[index].from;
                int to = edges[index].to;
                long ff = uf.GetSize(from);
                long tt = uf.GetSize(to);
                if (uf.Unite(from, to))
                {
                    tmp += (long)(ff + tt - 1) * (ff + tt) / 2;
                    tmp -= (ff - 1) * ff / 2;
                    tmp -= (tt - 1) * tt / 2;
                }
            }
            ans[query[i].index] = tmp;
        }

        Console.WriteLine(string.Join(" ", ans));
    }

    public static void Main(string[] args) => new Program().Solve();
}


namespace CompLib.Graph
{
    class UnionFind
    {
        private readonly int[] _parent, _size;

        public UnionFind(int size)
        {
            _parent = new int[size];
            _size = new int[size];
            for (int i = 0; i < size; i++)
            {
                _parent[i] = i;
                _size[i] = 1;
            }
        }

        /// <summary>
        /// iが含まれる木の根を調べる
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int Find(int i) => _parent[i] == i ? i : Find(_parent[i]);

        /// <summary>
        /// x,yが同じグループに含まれるか?
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Same(int x, int y) => Find(x) == Find(y);

        /// <summary>
        /// xとyを同じグループにする 元々同じグループならfalseを返す
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Unite(int x, int y)
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
        /// iが含まれるグループのサイズ
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int GetSize(int i) => _size[Find(i)];
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
