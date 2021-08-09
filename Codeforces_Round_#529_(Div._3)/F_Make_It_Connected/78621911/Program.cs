using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Graph;
using CompLib.Util;

public class Program
{
    int N, M;
    long[] A;
    int[] X, Y;
    long[] W;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        A = sc.LongArray();
        X = new int[M];
        Y = new int[M];
        W = new long[M];
        for (int i = 0; i < M; i++)
        {
            X[i] = sc.NextInt() - 1;
            Y[i] = sc.NextInt() - 1;
            W[i] = sc.NextLong();
        }

        // x,yに辺を張る
        // a_x + a_yコイン
        // x_i ,y_iに辺を張る W_iコイン

        // 使うはずの辺 minへ

        var ls = new List<P>();
        for (int i = 0; i < M; i++)
        {
            ls.Add(new P(X[i], Y[i], Math.Min(W[i], A[X[i]] + A[Y[i]])));
        }

        long min = long.MaxValue;
        int index = -1;
        for (int i = 0; i < N; i++)
        {
            if (A[i] < min)
            {
                min = A[i];
                index = i;
            }
        }

        for (int i = 0; i < N; i++)
        {
            if (i == index) continue;
            ls.Add(new P(index, i, A[i] + A[index]));
        }

        ls.Sort((l, r) => l.W.CompareTo(r.W));
        var uf = new UnionFind(N);
        long ans = 0;
        foreach (var p in ls)
        {
            if (uf.Unite(p.F, p.T)) ans += p.W;
        }
        Console.WriteLine(ans);
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

struct P
{
    public int F, T;
    public long W;
    public P(int f, int t, long w)
    {
        F = f;
        T = t;
        W = w;
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
