using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Collections;
using CompLib.Graph;

public class Program
{
    private int N, M;
    private AdjacencyList E, Rev;
    private const long Mod = 998244353;
    private const int Threshold = 25;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        E = new AdjacencyList(N);
        Rev = new AdjacencyList(N);

        for (int i = 0; i < M; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            E.AddDirectedEdge(u, v);
            Rev.AddDirectedEdge(v, u);
        }

        E.Build();
        Rev.Build();

        // 少ない回数でゴールに着ける
        {
            // iに反転j回で着く
            long[,] dist = new long[N, Threshold + 1];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j <= Threshold; j++)
                {
                    dist[i, j] = long.MaxValue;
                }
            }

            var pq = new PriorityQueue<(int i, int j, long cost)>((l, r) => l.cost.CompareTo(r.cost));
            dist[0, 0] = 0;
            pq.Enqueue((0, 0, 0));
            while (pq.Count > 0)
            {
                (int i, int j, long cost) = pq.Dequeue();
                if (dist[i, j] < cost) continue;
                if (j % 2 == 0)
                {
                    foreach (var to in E[i])
                    {
                        if (cost + 1 < dist[to, j])
                        {
                            dist[to, j] = cost + 1;
                            pq.Enqueue((to, j, cost + 1));
                        }
                    }
                }
                else
                {
                    foreach (var to in Rev[i])
                    {
                        if (cost + 1 < dist[to, j])
                        {
                            dist[to, j] = cost + 1;
                            pq.Enqueue((to, j, cost + 1));
                        }
                    }
                }

                if (j + 1 <= Threshold && cost + (1L << j) < dist[i, j + 1])
                {
                    dist[i, j + 1] = cost + (1L << j);
                    pq.Enqueue((i, j + 1, cost + (1L << j)));
                }
            }

            long ans = long.MaxValue;
            for (int i = 0; i <= Threshold; i++)
            {
                ans = Math.Min(ans, dist[N - 1, i]);
            }

            if (ans != long.MaxValue)
            {
                Console.WriteLine(ans % Mod);
                return;
            }
        }

        // 閾値以下ではゴールに着けない
        // 反転回数優先で減らす
        {
            (int rev, int move)[,] dist = new (int rev, int move)[N, 2];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    dist[i, j] = (int.MaxValue, int.MaxValue);
                }
            }

            var pq = new PriorityQueue<(int i, bool f, int rev, int move)>((l, r) =>
                Compare((l.rev, l.move), (r.rev, r.move)));

            pq.Enqueue((0, false, 0, 0));
            dist[0, 0] = (0, 0);
            while (pq.Count > 0)
            {
                (int i, bool f, int rev, int move) = pq.Dequeue();
                if (Compare(dist[i, f ? 1 : 0], (rev, move)) < 0) continue;
                var c1 = (rev, move + 1);
                var c2 = (rev + 1, move);
                if (f)
                {
                    foreach (var to in Rev[i])
                    {
                        if (Compare(c1, dist[to, 1]) < 0)
                        {
                            dist[to, 1] = c1;
                            pq.Enqueue((to, true, rev, move + 1));
                        }
                    }

                    if (Compare(c2, dist[i, 0]) < 0)
                    {
                        dist[i, 0] = c2;
                        pq.Enqueue((i, false, rev + 1, move));
                    }
                }
                else
                {
                    foreach (var to in E[i])
                    {
                        if (Compare(c1, dist[to, 0]) < 0)
                        {
                            dist[to, 0] = c1;
                            pq.Enqueue((to, false, rev, move + 1));
                        }
                    }

                    if (Compare(c2, dist[i, 1]) < 0)
                    {
                        dist[i, 1] = c2;
                        pq.Enqueue((i, true, rev + 1, move));
                    }
                }
            }

            var ans = Compare(dist[N - 1, 0], dist[N - 1, 1]) <= 0 ? dist[N - 1, 0] : dist[N - 1, 1];

            long rem = 1;
            long a = 2;
            long b = ans.rev;
            while (b > 0)
            {
                if (b % 2 == 1)
                {
                    rem *= a;
                    rem %= Mod;
                }

                a *= a;
                a %= Mod;
                b /= 2;
            }

            rem--;
            rem += ans.move;
            rem %= Mod;
            if (rem < 0) rem += Mod;
            Console.WriteLine(rem);
        }
    }

    int Compare((int rev, int move) l, (int rev, int move) r)
    {
        if (l.rev == r.rev) return l.move.CompareTo(r.move);
        return l.rev.CompareTo(r.rev);
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

namespace CompLib.Graph
{
    using System;
    using System.Collections.Generic;

    class AdjacencyList
    {
        private readonly int _n;
        private readonly List<(int f, int t)> _edges;

        private int[] _start;
        private int[] _eList;

        public AdjacencyList(int n)
        {
            _n = n;
            _edges = new List<(int f, int t)>();
        }

        public void AddDirectedEdge(int from, int to)
        {
            _edges.Add((from, to));
        }

        public void AddUndirectedEdge(int f, int t)
        {
            AddDirectedEdge(f, t);
            AddDirectedEdge(t, f);
        }

        public void Build()
        {
            _start = new int[_n + 1];
            foreach (var e in _edges)
            {
                _start[e.f + 1]++;
            }

            for (int i = 1; i <= _n; i++)
            {
                _start[i] += _start[i - 1];
            }

            int[] counter = new int[_n + 1];
            _eList = new int[_edges.Count];

            foreach (var e in _edges)
            {
                _eList[_start[e.f] + counter[e.f]++] = e.t;
            }
        }

        public ReadOnlySpan<int> this[int f]
        {
            get { return _eList.AsSpan(_start[f], _start[f + 1] - _start[f]); }
        }
    }
}

// https://bitbucket.org/camypaper/complib
namespace CompLib.Collections
{
    using System;
    using System.Collections.Generic;

    #region PriorityQueue

    /// <summary>
    /// 指定した型のインスタンスを最も価値が低い順に取り出すことが可能な可変サイズのコレクションを表します．
    /// </summary>
    /// <typeparam name="T">優先度付きキュー内の要素の型を指定します．</typeparam>
    /// <remarks>内部的にはバイナリヒープによって実装されています．</remarks>
    public class PriorityQueue<T>
    {
        readonly List<T> heap = new List<T>();
        readonly Comparison<T> cmp;

        /// <summary>
        /// デフォルトの比較子を使用してインスタンスを初期化します．
        /// </summary>
        /// <remarks>この操作は O(1) で実行されます．</remarks>
        public PriorityQueue()
        {
            cmp = Comparer<T>.Default.Compare;
        }

        /// <summary>
        /// デリゲートで表されるような比較関数を使用してインスタンスを初期化します．
        /// </summary>
        /// <param name="comparison"></param>
        /// <remarks>この操作は O(1) で実行されます．</remarks>
        public PriorityQueue(Comparison<T> comparison)
        {
            cmp = comparison;
        }

        /// <summary>
        /// 指定された比較子を使用してインスタンスを初期化します．
        /// </summary>
        /// <param name="comparer"></param>
        /// <remarks>この操作は O(1) で実行されます．</remarks>
        public PriorityQueue(IComparer<T> comparer)
        {
            cmp = comparer.Compare;
        }

        /// <summary>
        /// 優先度付きキューに要素を追加します．
        /// </summary>
        /// <param name="item">優先度付きキューに追加される要素</param>
        /// <remarks>最悪計算量 O(log N) で実行されます．</remarks>
        public void Enqueue(T item)
        {
            var pos = heap.Count;
            heap.Add(item);
            while (pos > 0)
            {
                var par = (pos - 1) / 2;
                if (cmp(heap[par], item) <= 0)
                    break;
                heap[pos] = heap[par];
                pos = par;
            }

            heap[pos] = item;
        }

        /// <summary>
        /// 優先度付きキューから最も価値が低い要素を削除し，返します．
        /// </summary>
        /// <returns>優先度付きキューから削除された要素．</returns>
        /// <remarks>最悪計算量 O(log N) で実行されます．</remarks>
        public T Dequeue()
        {
            var ret = heap[0];
            var pos = 0;
            var x = heap[heap.Count - 1];

            while (pos * 2 + 1 < heap.Count - 1)
            {
                var lch = pos * 2 + 1;
                var rch = pos * 2 + 2;
                if (rch < heap.Count - 1 && cmp(heap[rch], heap[lch]) < 0) lch = rch;
                if (cmp(heap[lch], x) >= 0)
                    break;
                heap[pos] = heap[lch];
                pos = lch;
            }

            heap[pos] = x;
            heap.RemoveAt(heap.Count - 1);
            return ret;
        }

        /// <summary>
        ///  優先度付きキューに含まれる最も価値が低い要素を削除せずに返します．
        /// </summary>
        /// <returns>優先度付きキューに含まれる最も価値が低い要素．</returns>
        /// <remarks>この操作は O(1) で実行されます．</remarks>
        public T Peek()
        {
            return heap[0];
        }

        /// <summary>
        /// 優先度付きキュー内の要素の数を取得します．
        /// </summary>
        /// <returns>優先度付キュー内にある要素の数</returns>
        /// <remarks>最悪計算量 O(1) で実行されます．</remarks>
        public int Count
        {
            get { return heap.Count; }
        }

        /// <summary>
        /// 優先度付きキュー内に要素が存在するかどうかを O(1) で判定します．
        /// </summary>
        /// <returns>優先度付キュー内にある要素が存在するならば true，そうでなければ　false．</returns>
        /// <remarks>この操作は O(1) で実行されます．</remarks>
        public bool Any()
        {
            return heap.Count > 0;
        }

        /// <summary>
        /// 優先度付きキューに含まれる要素を昇順に並べて返します．
        /// </summary>
        /// <remarks>この操作は計算量 O(N log N)で実行されます．</remarks>
        public T[] Items
        {
            get
            {
                var ret = heap.ToArray();
                Array.Sort(ret, cmp);
                return ret;
            }
        }
    }

    #endregion
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