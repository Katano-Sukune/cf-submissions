using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.DataStructure;

public class Program
{
    private int N;

    private int[] Col1, Col2, Val;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        Col1 = new int[N];
        Col2 = new int[N];
        Val = new int[N];
        for (int i = 0; i < N; i++)
        {
            Col1[i] = sc.NextInt() - 1;
            Val[i] = sc.NextInt();
            Col2[i] = sc.NextInt() - 1;
            if (Col1[i] < Col2[i])
            {
                (Col1[i], Col2[i]) = (Col2[i], Col1[i]);
            }
        }

        /*
         * 連結、先頭、末尾が奇数
         * 全部偶数
         */

        var dp = new long[N + 1, 1 << 4, 1 << 4, 1 << 6];
        for (int i = 0; i <= N; i++)
        {
            for (int j = 0; j < (1 << 4); j++)
            {
                for (int k = 0; k < (1 << 4); k++)
                {
                    for (int l = 0; l < (1 << 6); l++)
                    {
                        dp[i, j, k, l] = long.MinValue;
                    }
                }
            }
        }

        int[][] e2Num = new int[4][];
        (int u, int v)[] num2E = new (int u, int v)[6];
        int p = 0;
        for (int i = 0; i < 4; i++)
        {
            e2Num[i] = new int[i];
            for (int j = 0; j < i; j++)
            {
                e2Num[i][j] = p;
                num2E[p] = (i, j);
                p++;
            }
        }

        dp[0, 0, 0, 0] = 0;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < (1 << 4); j++)
            {
                for (int k = 0; k < (1 << 4); k++)
                {
                    for (int l = 0; l < (1 << p); l++)
                    {
                        if (dp[i, j, k, l] == long.MinValue) continue;
                        dp[i + 1, j, k, l] = Math.Max(dp[i + 1, j, k, l], dp[i, j, k, l]);
                        int b = (1 << Col1[i]) ^ (1 << Col2[i]);
                        int b2 = (1 << Col1[i]) | (1 << Col2[i]);
                        int c;
                        if (Col1[i] == Col2[i])
                        {
                            c = 0;
                        }
                        else
                        {
                            c = 1 << e2Num[Col1[i]][Col2[i]];
                        }

                        dp[i + 1, j ^ b, k | b2, l | c] =
                            Math.Max(dp[i + 1, j ^ b, k | b2, l | c], dp[i, j, k, l] + Val[i]);
                    }
                }
            }
        }

        long ans = long.MinValue;

        for (int j = 0; j < (1 << 4); j++)
        {
            for (int k = 0; k < (1 << 4); k++)
            {
                for (int l = 0; l < (1 << p); l++)
                {
                    if (dp[N, j, k, l] == long.MinValue) continue;
                    int bCnt = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        if ((j & (1 << i)) > 0) bCnt++;
                    }

                    if (bCnt != 0 && bCnt != 2) continue;
                    var uf = new UnionFind(4);
                    for (int i = 0; i < p; i++)
                    {
                        if ((l & (1 << i)) == 0) continue;
                        uf.Connect(num2E[i].u, num2E[i].v);
                    }

                    bool flag = true;
                    int t = -1;
                    for (int i = 0; i < 4 && flag; i++)
                    {
                        if ((k & (1 << i)) == 0) continue;
                        if (t == -1) t = i;
                        else flag &= uf.Same(t, i);
                    }

                    if (flag)
                    {
                        ans = Math.Max(ans, dp[N, j, k,l]);
                        // Console.WriteLine("-----");
                        // Console.WriteLine(Convert.ToString(j, 2));
                        // Console.WriteLine(Convert.ToString(k, 2));
                        // Console.WriteLine(Convert.ToString(l,2));
                        // Console.WriteLine(dp[N, j, k,l]);
                    }
                }
            }
        }

        Console.WriteLine(ans);
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