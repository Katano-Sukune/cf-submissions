using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CompLib.Graph;
using CompLib.Util;

public class Program
{
    int N, M;
    List<(int to, int num)>[] E;

    int[] X, Y;

    // iの行きがけ順の値
    // iから行ける頂点のPreの最小
    int[] Pre, Low;

    bool[] IsBridge;

    List<int>[] E2;

    int P;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        E = new List<(int to, int num)>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<(int to, int num)>();
        }
        X = new int[M];
        Y = new int[M];
        for (int i = 0; i < M; i++)
        {
            X[i] = sc.NextInt() - 1;
            Y[i] = sc.NextInt() - 1;
            E[X[i]].Add((Y[i], i));
            E[Y[i]].Add((X[i], i));
        }


        /*
         * (x_i,y_i)がブリッジな頂点だけ
         * 
         * 木の直径
         */
        IsBridge = new bool[M];
        Pre = new int[N];
        Low = new int[N];
        for (int i = 0; i < N; i++)
        {
            Pre[i] = int.MaxValue;
            Low[i] = int.MaxValue;
        }
        P = 0;

        Go(0, -1);

        var uf = new UnionFind(N);
        for (int i = 0; i < M; i++)
        {
            if (!IsBridge[i]) uf.Connect(X[i], Y[i]);
        }
        E2 = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            E2[i] = new List<int>();
        }

        for (int i = 0; i < M; i++)
        {
            if (!IsBridge[i]) continue;
            int rX = uf.Find(X[i]);
            int rY = uf.Find(Y[i]);
            E2[rX].Add(rY);
            E2[rY].Add(rX);
        }

        var s = Go2(uf.Find(0), -1);
        var t = Go2(s.v, -1);
        Console.WriteLine(t.max);
    }

    (int max, int v) Go2(int cur, int par)
    {
        (int max, int v) res = (0, cur);
        foreach (var to in E2[cur])
        {
            if (to == par) continue;
            var t = Go2(to, cur);
            if(res.max < t.max + 1)
            {
                res = (t.max + 1, t.v);
            }
        }
        return res;
    }

    void Go(int cur, int prev)
    {
        Pre[cur] = P++;
        Low[cur] = Pre[cur];
        foreach (var t in E[cur])
        {
            if (t.to == prev) continue;
            if (Pre[t.to] == int.MaxValue)
            {
                Go(t.to, cur);

                Low[cur] = Math.Min(Low[cur], Low[t.to]);
                if (Low[t.to] == Pre[t.to])
                {
                    IsBridge[t.num] = true;
                }
            }
            else
            {
                Low[cur] = Math.Min(Low[cur], Low[t.to]);
            }
        }

    }

    public static void Main(string[] args) => new Thread(new Program().Solve,1<<27).Start();
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
