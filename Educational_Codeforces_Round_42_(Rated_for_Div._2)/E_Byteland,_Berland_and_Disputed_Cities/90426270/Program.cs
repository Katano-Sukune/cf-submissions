using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CompLib.Graph;
using CompLib.Util;

public class Program
{
    int N;

    long[] X;
    char[] C;
    public void Solve()
    {
        checked
        {

            var sc = new Scanner();
            N = sc.NextInt();
            X = new long[N];
            C = new char[N];
            for (int i = 0; i < N; i++)
            {
                (X[i], C[i]) = (sc.NextLong(), sc.NextChar());
            }

            /*
             * x軸上の都市がある
             * 
             * byteland　-B
             * barland -R
             * 
             * あらそってる -P
             * 
             * 都市をケーブルでつなぐ
             * 
             * BとP の各都市は連結
             * R,P連結
             * 
             * つなぐコスト　距離
             * 
             * コスト最小
             * 
             * 
             * 
             */

            var lsB = new List<long>();
            var lsR = new List<long>();
            var lsP = new List<long>();
            long mx = long.MinValue;
            long mn = long.MaxValue;
            for (int i = 0; i < N; i++)
            {
                switch (C[i])
                {
                    case 'R':
                        lsR.Add(X[i]);
                        break;
                    case 'B':
                        lsB.Add(X[i]);
                        break;
                    case 'P':
                        lsP.Add(X[i]);
                        break;
                }

                mx = Math.Max(mx, X[i]);
                mn = Math.Min(mn, X[i]);
            }
            lsB.Sort();
            lsR.Sort();
            lsP.Sort();


            long ans = 0;

            if (lsP.Count == 0)
            {
                if (lsR.Count > 0)
                {
                    ans += lsR[lsR.Count - 1] - lsR[0];
                }
                if (lsB.Count > 0)
                {
                    ans += lsB[lsB.Count - 1] - lsB[0];
                }
                Console.WriteLine(ans);
                return;
            }

            for (int i = 0; i < lsP.Count - 1; i++)
            {
                // R,Bそれぞれで連結
                long s = 2 * (lsP[i + 1] - lsP[i]);
                // i-{i+1}をつなぐ
                // B,R両方が近い方と連結
                long t = lsP[i + 1] - lsP[i];

                {
                    int ok = lsR.Count;
                    int ng = -1;
                    while (ok - ng > 1)
                    {
                        int mid = (ok + ng) / 2;
                        if (lsR[mid] > lsP[i]) ok = mid;
                        else ng = mid;
                    }

                    if (ok < lsR.Count && lsR[ok] < lsP[i + 1])
                    {
                        // lsP[i+1]未満まで
                        // 最大の区間
                        long max = lsR[ok] - lsP[i];
                        ok++;
                        for (; ok < lsR.Count && lsR[ok] < lsP[i + 1]; ok++)
                        {
                            max = Math.Max(max, lsR[ok] - lsR[ok - 1]);
                        }
                        max = Math.Max(max, lsP[i + 1] - lsR[ok - 1]);

                        t += lsP[i + 1] - lsP[i] - max;
                    }
                }

                {
                    int ok = lsB.Count;
                    int ng = -1;
                    while (ok - ng > 1)
                    {
                        int mid = (ok + ng) / 2;
                        if (lsB[mid] > lsP[i]) ok = mid;
                        else ng = mid;
                    }

                    if (ok < lsB.Count && lsB[ok] < lsP[i + 1])
                    {
                        // lsP[i+1]未満まで
                        // 最大の区間
                        long max = lsB[ok] - lsP[i];
                        ok++;
                        for (; ok < lsB.Count && lsB[ok] < lsP[i + 1]; ok++)
                        {
                            max = Math.Max(max, lsB[ok] - lsB[ok - 1]);
                        }
                        max = Math.Max(max, lsP[i + 1] - lsB[ok - 1]);

                        t += lsP[i + 1] - lsP[i] - max;
                    }
                }

                ans += Math.Min(s, t);
            }

            if (lsB.Count > 0)
            {
                if (lsB[0] < lsP[0])
                {
                    ans += lsP[0] - lsB[0];
                }

                if (lsP[lsP.Count - 1] < lsB[lsB.Count - 1])
                {
                    ans += lsB[lsB.Count - 1] - lsP[lsP.Count - 1];
                }
            }
            if (lsR.Count > 0)
            {

                if (lsR[0] < lsP[0])
                {
                    ans += lsP[0] - lsR[0];
                }

                if (lsP[lsP.Count - 1] < lsR[lsR.Count - 1])
                {
                    ans += lsR[lsR.Count - 1] - lsP[lsP.Count - 1];
                }
            }
            Console.WriteLine(ans);
        }
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
