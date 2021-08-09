using System;
using System.Collections.Generic;
using System.Text;
using CompLib.Graph;
using CompLib.Util;

public class Program
{
    private int N;
    private int[] X, Y;

    private int[] C, K;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        X = new int[N];
        Y = new int[N];
        for (int i = 0; i < N; i++)
        {
            X[i] = sc.NextInt();
            Y[i] = sc.NextInt();
        }

        C = sc.IntArray();
        K = sc.IntArray();

        // 都市iに発電所を建てるには c_i円かかる

        // i,jをつなぐには 1Kmあたり K_i + K_j円かかる
        List<(int s, int t, long d)> edges = new List<(int s, int t, long d)>();
        for (int i = 0; i < N; i++)
        {
            edges.Add((i, N, C[i]));
            for (int j = i + 1; j < N; j++)
            {
                edges.Add((i, j, (long) (K[i] + K[j]) * (Math.Abs(X[i] - X[j]) + Math.Abs(Y[i] - Y[j]))));
            }
        }

        edges.Sort((l, r) => l.d.CompareTo(r.d));


        long cost = 0;
        var ls = new List<int>();
        int e = 0;
        var sb2 = new StringBuilder();

        var uf = new UnionFind(N + 1);
        foreach (var edge in edges)
        {
            if (uf.Unite(edge.s, edge.t))
            {
                cost += edge.d;
                if (edge.t == N)
                {
                    ls.Add(edge.s + 1);
                }
                else
                {
                    e++;
                    sb2.AppendLine($"{edge.s + 1} {edge.t + 1}");
                }
            }
        }

        Console.WriteLine(cost);
        Console.WriteLine(ls.Count);
        Console.WriteLine(string.Join(" ", ls));
        Console.WriteLine(e);
        Console.Write(sb2);
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