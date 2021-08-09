using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Collections;
using CompLib.Util;

public class Program
{
    int N, M;

    List<(int to, long w)>[] E;

    public void Solve()
    {
        var sc = new Scanner();

        /*
         * N個都市
         * 無向辺 (u,v) コストw
         * 
         * 都市iでのチケット a_i
         * 
         * iにいる人がjに行って a_j払ってiに戻る
         * 最小
         * 
         * 
         */

        /*
         * ゴール
         * 各都市
         * 
         */

        N = sc.NextInt();
        M = sc.NextInt();

        E = new List<(int to, long w)>[N + 1];
        for (int i = 0; i <= N; i++)
        {
            E[i] = new List<(int to, long w)>();
        }

        for (int i = 0; i < M; i++)
        {
            int v = sc.NextInt();
            int u = sc.NextInt();
            long w = sc.NextLong();
            E[u].Add((v, 2 * w));
            E[v].Add((u, 2 * w));
        }

        for (int i = 1; i <= N; i++)
        {
            E[0].Add((i, sc.NextLong()));
        }

        // 0から各都市へ
        long[] dist = new long[N + 1];
        for (int i = 0; i <= N; i++)
        {
            dist[i] = long.MaxValue;
        }


        var pq = new PriorityQueue<(int p, long cost)>((l, r) => l.cost.CompareTo(r.cost));
        dist[0] = 0;
        pq.Enqueue((0, 0));
        while (pq.Count > 0)
        {
            var d = pq.Dequeue();
            if (dist[d.p] < d.cost) continue;
            foreach (var t in E[d.p])
            {
                long nc = dist[d.p] + t.w;
                if (dist[t.to] <= nc) continue;
                dist[t.to] = nc;
                pq.Enqueue((t.to, nc));
            }
        }

        Console.WriteLine(string.Join(" ", dist.Skip(1)));

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
