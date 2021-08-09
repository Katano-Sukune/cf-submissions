using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using CompLib.Collections;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    private int N;
    private long S;
    private List<(int to, long w, int c)>[] E;
    private PriorityQueue<(long w, int cnt)> Pq1, Pq2;
    private long S1, S2;

    void Q(Scanner sc)
    {
        S1 = 0;
        S2 = 0;
        N = sc.NextInt();
        S = sc.NextLong();
        E = new List<(int to, long w, int c)>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<(int to, long w, int c)>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            long w = sc.NextLong();
            int c = sc.NextInt();
            E[u].Add((v, w, c));
            E[v].Add((u, w, c));
        }

        // 木
        // 1から各頂点のパスのコスト総和をS以下にする
        // 操作 辺選んでwを/2切り捨て

        // 重さw 下にcnt個頂点


        Pq1 = new PriorityQueue<(long w, int cnt)>((l, r) =>
        {
            var ll = ((l.w + 1) / 2) * l.cnt;
            var rr = ((r.w + 1) / 2) * r.cnt;
            return rr.CompareTo(ll);
        });

        Pq2 = new PriorityQueue<(long w, int cnt)>((l, r) =>
        {
            var ll = ((l.w + 1) / 2) * l.cnt;
            var rr = ((r.w + 1) / 2) * r.cnt;
            return rr.CompareTo(ll);
        });

        Go(0, -1);

        List<long> l1 = new List<long>();
        l1.Add(S1);
        while (S1 > 0)
        {
            var d = Pq1.Dequeue();
            long m = ((d.w + 1) / 2) * d.cnt;
            S1 -= m;
            l1.Add(S1);
            Pq1.Enqueue((d.w / 2, d.cnt));
        }

        List<long> l2 = new List<long>();
        l2.Add(S2);
        while (S2 > 0)
        {
            var d = Pq2.Dequeue();
            long m = ((d.w + 1) / 2) * d.cnt;
            S2 -= m;
            l2.Add(S2);
            Pq2.Enqueue((d.w / 2, d.cnt));
        }

        long ans = long.MaxValue;
        for (int i = l1.Count - 1; i >= 0; i--)
        {
            if (l1[i] > S) break;
            // コスト1の辺にi回操作
            int ok = l2.Count - 1;
            int ng = -1;
            while (ok - ng > 1)
            {
                int mid = (ok + ng) / 2;
                if (l1[i] + l2[mid] <= S) ok = mid;
                else ng = mid;
            }

            ans = Math.Min(ans, i + ok * 2);
        }

        Console.WriteLine(ans);
    }

    int Go(int cur, int par)
    {
        int s = E[cur].Count == 1 ? 1 : 0;
        foreach (var t in E[cur])
        {
            if (t.to == par) continue;
            int g = Go(t.to, cur);
            if (t.c == 1)
            {
                Pq1.Enqueue((t.w, g));
                S1 += t.w * g;
            }
            else if (t.c == 2)
            {
                Pq2.Enqueue((t.w, g));
                S2 += t.w * g;
            }

            s += g;
        }

        return s;
    }

    public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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