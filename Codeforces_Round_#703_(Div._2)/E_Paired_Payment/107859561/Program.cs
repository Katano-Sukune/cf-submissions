using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Collections;

public class Program
{
    private int N, M;
    private List<E>[] Edge;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        Edge = new List<E>[N];
        for (int i = 0; i < N; i++)
        {
            Edge[i] = new List<E>();
        }

        for (int i = 0; i < M; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            int w = sc.NextInt();
            Edge[u].Add(new E(v, w));
            Edge[v].Add(new E(u, w));
        }

        Array2D dist = new Array2D(N, 51);
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j <= 50; j++)
            {
                dist[i, j] = long.MaxValue;
            }
        }

        dist[0, 0] = 0;
        var pq = new PriorityQueue<S>((l, r) => l.Cost.CompareTo(r.Cost));
        pq.Enqueue(new S(0, 0, 0));

        bool[] flag = new bool[N];
        int cnt = 0;
        while (pq.Count > 0 && cnt < N)
        {
            var d = pq.Dequeue();
            int v = d.V;
            int wAB = d.WAB;
            long cost = d.Cost;

            if (dist[v, wAB] < cost) continue;
            if (wAB == 0)
            {
                foreach (E e in Edge[v])
                {
                    if (dist[e.To, e.W] <= dist[v, wAB]) continue;
                    dist[e.To, e.W] = dist[v, wAB];
                    pq.Enqueue(new S(e.To, e.W, dist[v, wAB]));
                }
            }
            else
            {
                foreach (E e in Edge[v])
                {
                    if (dist[e.To, 0] <= dist[v, wAB] + (wAB + e.W) * (wAB + e.W)) continue;
                    dist[e.To, 0] = dist[v, wAB] + (wAB + e.W) * (wAB + e.W);
                    pq.Enqueue(new S(e.To, 0, dist[v, wAB] + (wAB + e.W) * (wAB + e.W)));
                }
            }
        }

        long[] ans = new long[N];
        for (int i = 0; i < N; i++)
        {
            if (dist[i, 0] == long.MaxValue) ans[i] = -1;
            else ans[i] = dist[i, 0];
        }

        Console.WriteLine(string.Join(" ", ans));
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

struct E
{
    public int To;
    public int W;

    public E(int to, int w)
    {
        To = to;
        W = w;
    }
}

struct S
{
    public int V, WAB;
    public long Cost;

    public S(int v, int wAB, long cost)
    {
        V = v;
        WAB = wAB;
        Cost = cost;
    }
}

class Array2D
{
    public int N, M;
    private long[] T;

    public Array2D(int n, int m)
    {
        N = n;
        M = m;
        T = new long[N * M];
    }

    public long this[int i, int j]
    {
        get { return T[i * M + j]; }
        set { T[i * M + j] = value; }
    }
}

// https://bitbucket.org/camypaper/complib
namespace CompLib.Collections
{
    using System;
    using System.Collections.Generic;

    #region PriorityQueue

    /// <summary>
    /// ??????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????
    /// </summary>
    /// <typeparam name="T">???????????????????????????????????????????????????????????????</typeparam>
    /// <remarks>???????????????????????????????????????????????????????????????????????????</remarks>
    public class PriorityQueue<T>
    {
        readonly List<T> heap = new List<T>();
        readonly Comparison<T> cmp;

        /// <summary>
        /// ????????????????????????????????????????????????????????????????????????????????????
        /// </summary>
        /// <remarks>??????????????? O(1) ????????????????????????</remarks>
        public PriorityQueue()
        {
            cmp = Comparer<T>.Default.Compare;
        }

        /// <summary>
        /// ????????????????????????????????????????????????????????????????????????????????????????????????????????????
        /// </summary>
        /// <param name="comparison"></param>
        /// <remarks>??????????????? O(1) ????????????????????????</remarks>
        public PriorityQueue(Comparison<T> comparison)
        {
            cmp = comparison;
        }

        /// <summary>
        /// ?????????????????????????????????????????????????????????????????????????????????
        /// </summary>
        /// <param name="comparer"></param>
        /// <remarks>??????????????? O(1) ????????????????????????</remarks>
        public PriorityQueue(IComparer<T> comparer)
        {
            cmp = comparer.Compare;
        }

        /// <summary>
        /// ??????????????????????????????????????????????????????
        /// </summary>
        /// <param name="item">????????????????????????????????????????????????</param>
        /// <remarks>??????????????? O(log N) ????????????????????????</remarks>
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
        /// ???????????????????????????????????????????????????????????????????????????????????????
        /// </summary>
        /// <returns>??????????????????????????????????????????????????????</returns>
        /// <remarks>??????????????? O(log N) ????????????????????????</remarks>
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
        ///  ???????????????????????????????????????????????????????????????????????????????????????????????????
        /// </summary>
        /// <returns>?????????????????????????????????????????????????????????????????????</returns>
        /// <remarks>??????????????? O(1) ????????????????????????</remarks>
        public T Peek()
        {
            return heap[0];
        }

        /// <summary>
        /// ???????????????????????????????????????????????????????????????
        /// </summary>
        /// <returns>?????????????????????????????????????????????</returns>
        /// <remarks>??????????????? O(1) ????????????????????????</remarks>
        public int Count
        {
            get { return heap.Count; }
        }

        /// <summary>
        /// ?????????????????????????????????????????????????????????????????? O(1) ?????????????????????
        /// </summary>
        /// <returns>??????????????????????????????????????????????????????????????? true???????????????????????????false???</returns>
        /// <remarks>??????????????? O(1) ????????????????????????</remarks>
        public bool Any()
        {
            return heap.Count > 0;
        }

        /// <summary>
        /// ?????????????????????????????????????????????????????????????????????????????????
        /// </summary>
        /// <remarks>???????????????????????? O(N log N)????????????????????????</remarks>
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