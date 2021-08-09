using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Graph;

public class Program
{
    int N, Q;
    int[] T, L, R, V;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        Q = sc.NextInt();
        T = new int[Q];
        L = new int[Q];
        R = new int[Q];
        V = new int[Q];
        for (int i = 0; i < Q; i++)
        {
            (T[i], L[i], R[i], V[i]) = (sc.NextInt(), sc.NextInt() - 1, sc.NextInt(), sc.NextInt());
        }

        // 要素n 1~n

        // q個条件
        // 1 l r v
        // [l,r]はv以上

        // 2 
        // [l,r]はv以下

        // iの出現数 cnt(i)
        // Σ cnt(i)^2の最小



        bool[,] f = new bool[N, N];
        for (int i = 0; i < Q; i++)
        {
            for (int j = L[i]; j < R[i]; j++)
            {
                if (T[i] == 1)
                {
                    for (int k = 0; k < V[i] - 1; k++)
                    {
                        f[j, k] = true;
                    }
                }
                else
                {
                    for (int k = V[i]; k < N; k++)
                    {
                        f[j, k] = true;
                    }
                }
            }
        }

        for (int i = 0; i < N; i++)
        {
            bool flag = true;
            for (int j = 0; flag && j < N; j++)
            {
                flag &= f[i, j];
            }
            if (flag)
            {
                Console.WriteLine("-1");
                return;
            }
        }

        // 0~N-1 A_i
        // N~2N-1 各数
        // 2N スタート
        // 2N+1 ゴール
        int start = 2 * N;
        int goal = 2 * N + 1;
        var mcf = new MinCostFlow(1 + 1 + N + N);
        for (int i = 0; i < N; i++)
        {
            mcf.AddEdge(start, i, 1, 0);
            for (int j = 0; j < N; j++)
            {
                if (f[i, j]) continue;
                mcf.AddEdge(i, j + N, 1, 0);
            }
            for (int j = 1; j <= N; j++)
            {
                mcf.AddEdge(i + N, goal, 1, j * 2 - 1);
            }
        }

        Console.WriteLine(mcf.Flow(start, goal).Cost);
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}


namespace CompLib.Graph
{
    using CompLib.Collections;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Cap = System.Int32;
    using Cost = System.Int32;
    class MinCostFlow
    {
        private readonly int N;
        private readonly List<PairII> Pos;
        private readonly List<InternalEdge>[] G;

        public MinCostFlow(int n)
        {
            N = n;
            Pos = new List<PairII>();
            G = new List<InternalEdge>[N];
            for (int i = 0; i < N; i++)
            {
                G[i] = new List<InternalEdge>();
            }
        }

        /// <summary>
        /// fromからtoへ最大容量cap, コストcostの辺を追加する 何番目に追加された辺かを返す
        /// </summary>
        /// <remarks>O(1)</remarks>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="cap"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        public int AddEdge(int from, int to, Cap cap, Cost cost)
        {
            Debug.Assert(0 <= from && from < N);
            Debug.Assert(0 <= to && to < N);
            int m = Pos.Count;
            Pos.Add(new PairII(from, G[from].Count));
            int fromID = G[from].Count;
            int toID = G[to].Count;
            if (from == to) toID++;
            G[from].Add(new InternalEdge(to, toID, cap, cost));
            G[to].Add(new InternalEdge(from, fromID, 0, -cost));
            return m;
        }

        /// <summary>
        /// i番目に追加された辺の状態を返します
        /// </summary>
        /// <remarks>O(1)</remarks>
        /// <param name="i"></param>
        /// <returns></returns>
        public Edge GetEdge(int i)
        {
            int m = Pos.Count;
            Debug.Assert(0 <= i && i < m);
            var e = G[Pos[i].From][Pos[i].Index];
            var re = G[e.To][e.Rev];
            return new Edge(Pos[i].From, e.To, e.Cap + re.Cap, re.Cap, e.Cost);
        }

        /// <summary>
        /// 現在の辺の状態を返します
        /// </summary>
        /// <remarks>O(m)</remarks>
        /// <returns></returns>
        public Edge[] Edges()
        {
            int m = Pos.Count;
            Edge[] result = new Edge[m];
            for (int i = 0; i < m; i++)
            {
                result[i] = GetEdge(i);
            }
            return result;
        }

        /// <summary>
        /// sからtへ最大限流すときの(tへの最大流量、最小コスト)
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public PairCapCost Flow(int s, int t)
        {
            return Flow(s, t, Cap.MaxValue);
        }

        /// <summary>
        /// sからtへflowLimit流すときの(tへの最大流量、最小コスト)
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <param name="flowLimit"></param>
        /// <returns></returns>
        public PairCapCost Flow(int s, int t, Cap flowLimit)
        {
            var ls = Slope(s, t, flowLimit);
            return ls[ls.Count - 1];
        }

        /// <summary>
        /// 返り値に流量、コストの関係の折れ線が入る
        /// <para>流量xのときのコストg(x)として、(x,g(x))のリストが返る</para>
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public List<PairCapCost> Slope(int s, int t)
        {
            return Slope(s, t, Cap.MaxValue);
        }

        /// <summary>
        /// 返り値に流量、コストの関係の折れ線が入る
        /// <para>流量xのときのコストg(x)として、(x,g(x))のリストが返る</para>
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <param name="flowLimit"></param>
        /// <returns></returns>
        public List<PairCapCost> Slope(int s, int t, Cap flowLimit)
        {
            Debug.Assert(0 <= s && s < N);
            Debug.Assert(0 <= t && t < N);
            Debug.Assert(s != t);

            Cost[] dual = new Cost[N];
            Cost[] dist = new Cost[N];
            int[] pv = new int[N];
            int[] pe = new int[N];
            bool[] vis = new bool[N];

            bool dualRef()
            {
                for (int i = 0; i < N; i++)
                {
                    dist[i] = Cost.MaxValue;
                    pv[i] = -1;
                    pe[i] = -1;
                    vis[i] = false;
                }

                // C++のstd::priority_queueは降順
                // これは昇順
                PriorityQueue<Q> que = new PriorityQueue<Q>((l, r) => l.Key.CompareTo(r.Key));

                dist[s] = 0;
                que.Enqueue(new Q(0, s));
                while (que.Any())
                {
                    int v = que.Dequeue().To;
                    if (vis[v]) continue;
                    vis[v] = true;
                    if (v == t) break;
                    for (int i = 0; i < G[v].Count; i++)
                    {
                        var e = G[v][i];
                        if (vis[e.To] || e.Cap == 0) continue;
                        Cost cost2 = e.Cost - dual[e.To] + dual[v];
                        if (dist[e.To] - dist[v] > cost2)
                        {
                            dist[e.To] = dist[v] + cost2;
                            pv[e.To] = v;
                            pe[e.To] = i;
                            que.Enqueue(new Q(dist[e.To], e.To));
                        }
                    }
                }

                if (!vis[t])
                {
                    return false;
                }

                for (int v = 0; v < N; v++)
                {
                    if (!vis[v]) continue;
                    dual[v] -= dist[t] - dist[v];
                }

                return true;
            }

            Cap flow = 0;
            Cost cost = 0;
            Cost prevCostPerFlow = -1;
            List<PairCapCost> result = new List<PairCapCost>();
            result.Add(new PairCapCost(flow, cost));
            while (flow < flowLimit)
            {
                if (!dualRef()) break;
                Cap c = flowLimit - flow;
                for (int v = t; v != s; v = pv[v])
                {
                    c = Math.Min(c, G[pv[v]][pe[v]].Cap);
                }

                for (int v = t; v != s; v = pv[v])
                {
                    var e = G[pv[v]][pe[v]];
                    e.Cap -= c;
                    var rev = G[v][e.Rev];
                    rev.Cap += c;
                    G[pv[v]][pe[v]] = e;
                    G[v][e.Rev] = rev;
                }
                Cost d = -dual[s];
                flow += c;
                cost += c * d;
                if (prevCostPerFlow == d)
                {
                    result.RemoveAt(result.Count - 1);
                }
                result.Add(new PairCapCost(flow, cost));
                prevCostPerFlow = d;
            }
            return result;
        }

        private struct PairII
        {
            public int From, Index;
            public PairII(int from, int index)
            {
                From = from;
                Index = index;
            }
        }

        private struct InternalEdge
        {
            public int To, Rev;
            public Cap Cap;
            public Cost Cost;
            public InternalEdge(int to, int rev, Cap cap, Cost cost)
            {
                To = to;
                Rev = rev;
                Cap = cap;
                Cost = cost;
            }
        }

        private struct Q
        {
            public Cost Key;
            public int To;
            public Q(Cost key, int to)
            {
                Key = key;
                To = to;
            }
        }
    }

    public struct Edge
    {
        public readonly int From, To;
        public readonly Cap Cap, Flow;
        public readonly Cost Cost;
        public Edge(int from, int to, Cap cap, Cap flow, Cost cost)
        {
            From = from;
            To = to;
            Cap = cap;
            Flow = flow;
            Cost = cost;
        }
    }

    public struct PairCapCost
    {
        public Cap Cap;
        public Cost Cost;
        public PairCapCost(Cap cap, Cost cost)
        {
            Cap = cap;
            Cost = cost;
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
