using CompLib.Graph;
using CompLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    int N, M;
    int[] J, K;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();

        J = new int[M];
        K = new int[M];
        for (int i = 0; i < M; i++)
        {
            J[i] = sc.NextInt() - 1;
            K[i] = sc.NextInt() - 1;
        }

        /*
         * ∀x,∃ y, x < y
         * すべての実数xに対して x < yを満たすyが存在する
         * 
         * M項 論理式 (x_{j_i} < x_{k_i}) and ....
         * 
         */

        /*
         * k -> jに有向辺張る
         * 
         * すべて入る辺なら A
         * 
         * 残りE
         * 
         * 閉路があるなら -1
         */

        List<int>[] e = new List<int>[N];
        List<int>[] rev = new List<int>[N];
        int[] cnt = new int[N];
        for (int i = 0; i < N; i++)
        {
            e[i] = new List<int>();
            rev[i] = new List<int>();
        }

        for (int i = 0; i < M; i++)
        {
            e[K[i]].Add(J[i]);
            rev[J[i]].Add(K[i]);
            cnt[J[i]]++;
        }

        // 閉路検出
        {
            var q = new Queue<int>();
            int t = 0;
            for (int i = 0; i < N; i++)
            {
                if (cnt[i] == 0) q.Enqueue(i);
            }
            while (q.Count > 0)
            {
                var d = q.Dequeue();
                foreach (var to in e[d])
                {
                    cnt[to]--;
                    if (cnt[to] <= 0)
                    {
                        q.Enqueue(to);
                    }
                }
            }

            for (int i = 0; i < N; i++)
            {
                if (cnt[i] > 0)
                {
                    Console.WriteLine("-1");
                    return;
                }
            }
        }

        // 閉路が無い

        // j > kの辺がある 全部e

        // 連結成分 最小がA Aへ行く頂点E Aから行く頂点もE
        int cntA = 0;
        char[] ans = new char[N];
        bool[] f = new bool[N];
        bool[] f2 = new bool[N];
        var q2 = new Queue<int>();
        for (int i = 0; i < N; i++)
        {
            if (!f[i] && !f2[i])
            {
                ans[i] = 'A';
                cntA++;
            }
            else
            {
                ans[i] = 'E';
            }

            f[i] = true;
            f2[i] = true;
            q2.Enqueue(i);
            while (q2.Count > 0)
            {
                var d = q2.Dequeue();
                foreach (var to in e[d])
                {
                    if (f[to]) continue;
                    q2.Enqueue(to);
                    f[to] = true;
                }
            }

            q2.Enqueue(i);
            while (q2.Count > 0)
            {
                var d = q2.Dequeue();
                foreach (var to in rev[d])
                {
                    if (f2[to]) continue;
                    q2.Enqueue(to);
                    f2[to] = true;
                }
            }
        }

        Console.WriteLine(cntA);
        Console.WriteLine(new string(ans));
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
