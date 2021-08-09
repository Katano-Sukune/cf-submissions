using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Collections;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < t; i++)
        {
            int n = sc.NextInt();
            int m = sc.NextInt();
            int a = sc.NextInt() - 1;
            int b = sc.NextInt() - 1;
            int c = sc.NextInt() - 1;
            int[] p = sc.IntArray();
            List<int>[] edge = new List<int>[n];
            for (int j = 0; j < n; j++)
            {
                edge[j] = new List<int>();
            }

            for (int j = 0; j < m; j++)
            {
                int u = sc.NextInt() - 1;
                int v = sc.NextInt() - 1;
                edge[u].Add(v);
                edge[v].Add(u);
            }
            sb.AppendLine(Q(n, m, a, b, c, p, edge));
        }
        Console.Write(sb);
    }

    string Q(int n, int m, int a, int b, int c, int[] p, List<int>[] e)
    {
        // a->b->cのパス
        // 辺のコストはp

        // b->t*2 + t->a + t->bが最小

        // aからの距離
        int[] aDist;
        {
            aDist = new int[n];
            for (int i = 0; i < n; i++)
            {
                aDist[i] = int.MaxValue;
            }
            var pq = new PriorityQueue<int>((l, r) => aDist[l].CompareTo(aDist[r]));
            aDist[a] = 0;
            pq.Enqueue(a);
            while (pq.Count > 0)
            {
                var d = pq.Dequeue();
                foreach (int to in e[d])
                {
                    if (aDist[to] <= aDist[d] + 1) continue;
                    aDist[to] = aDist[d] + 1;
                    pq.Enqueue(to);
                }
            }
        }
        int[] bDist;
        {
            bDist = new int[n];
            for (int i = 0; i < n; i++)
            {
                bDist[i] = int.MaxValue;
            }
            var pq = new PriorityQueue<int>((l, r) => bDist[l].CompareTo(bDist[r]));
            bDist[b] = 0;
            pq.Enqueue(b);
            while (pq.Count > 0)
            {
                var d = pq.Dequeue();
                foreach (int to in e[d])
                {
                    if (bDist[to] <= bDist[d] + 1) continue;
                    bDist[to] = bDist[d] + 1;
                    pq.Enqueue(to);
                }
            }
        }
        int[] cDist;
        {
            cDist = new int[n];
            for (int i = 0; i < n; i++)
            {
                cDist[i] = int.MaxValue;
            }
            var pq = new PriorityQueue<int>((l, r) => cDist[l].CompareTo(cDist[r]));
            cDist[c] = 0;
            pq.Enqueue(c);
            while (pq.Count > 0)
            {
                var d = pq.Dequeue();
                foreach (int to in e[d])
                {
                    if (cDist[to] <= cDist[d] + 1) continue;
                    cDist[to] = cDist[d] + 1;
                    pq.Enqueue(to);
                }
            }
        }

        Array.Sort(p);

        long[] sum = new long[m + 1];
        for (int i = 0; i < m; i++)
        {
            sum[i + 1] = sum[i] + p[i];
        }

        long ans = long.MaxValue;

        for (int t = 0; t < n; t++)
        {
            if (aDist[t] + bDist[t] + cDist[t] <= m)
            {
                ans = Math.Min(ans, sum[aDist[t] + bDist[t] + cDist[t]] + sum[bDist[t]]);
            }
        }

        return ans.ToString();
    }

    public static void Main(string[] args) => new Program().Solve();
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
