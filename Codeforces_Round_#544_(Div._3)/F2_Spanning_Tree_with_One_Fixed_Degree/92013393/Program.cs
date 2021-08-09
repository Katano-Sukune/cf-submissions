using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Graph;
using CompLib.Util;

public class Program
{
    int N, M, D;
    List<int>[] E;

    int[] U, V;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        D = sc.NextInt();
        U = new int[M];
        V = new int[M];
        E = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<int>();
        }

        for (int i = 0; i < M; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            U[i] = u;
            V[i] = v;
            E[u].Add(v);
            E[v].Add(u);
        }

        /*
         * 1の次数がDな全域木
         * 
         * 1-p使わずに繋げられる
         * 
         * 1と繋がっている辺同士が1と繋げずにつながる
         * 
         * 次数最小値 上のグループの個数
         * 
         * 最大 1の次数
         */

        if (D > E[0].Count)
        {
            Console.WriteLine("NO");
            return;
        }

        var uf1 = new UnionFind(N);
        for (int i = 0; i < M; i++)
        {
            if (U[i] == 0 || V[i] == 0) continue;
            uf1.Connect(U[i], V[i]);
        }

        int[] g = new int[E[0].Count];

        for (int i = 0; i < E[0].Count; i++)
        {
            g[i] = uf1.Find(E[0][i]);
        }

        int[] h = new int[E[0].Count];

        Array.Copy(g, h, E[0].Count);

        Array.Sort(h);

        int k = 0;
        for (int i = 0; i < E[0].Count; i++)
        {
            if (i == 0 || h[i - 1] != h[i]) k++;
        }

        if (D < k)
        {
            Console.WriteLine("NO");
            return;
        }

        var hs2 = new HashSet<int>();

        int diff = D - k;
        var uf2 = new UnionFind(N);
        var sb = new StringBuilder();
        for (int i = 0; i < E[0].Count; i++)
        {
            if (hs2.Add(g[i]))
            {
                uf2.Connect(0, E[0][i]);
                sb.AppendLine($"1 {E[0][i] + 1}");
            }
            else if (diff > 0)
            {
                uf2.Connect(0, E[0][i]);
                sb.AppendLine($"1 {E[0][i] + 1}");
                diff--;
            }
        }

        for (int i = 0; i < M; i++)
        {
            if (U[i] == 0 || V[i] == 0) continue;
            if (uf2.Connect(U[i], V[i]))
            {
                sb.AppendLine($"{U[i] + 1} {V[i] + 1}");
            }
        }
        Console.WriteLine("YES");
        Console.Write(sb);
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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
