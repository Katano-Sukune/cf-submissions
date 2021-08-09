using CompLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    int N, M, K;
    int[] A;

    List<int>[] Edges;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        K = sc.NextInt();
        A = new int[K];
        for (int i = 0; i < K; i++)
        {
            A[i] = sc.NextInt() - 1;
        }

        Edges = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            Edges[i] = new List<int>();
        }

        for (int i = 0; i < M; i++)
        {
            int x = sc.NextInt() - 1;
            int y = sc.NextInt() - 1;
            Edges[x].Add(y);
            Edges[y].Add(x);
        }

        // n個のfield m個の無向道路
        /*
         * A special fields
         * 1つ Aの2つ結ぶ道路を作る
         * 初期状態で既に移動できる
         * 
         * 1->nの最短経路を最大化
         */


        var dist1 = new int[N];
        {
            var flag = new bool[N];
            var q = new Queue<int>();
            for (int i = 0; i < N; i++)
            {
                dist1[i] = int.MaxValue;
            }
            q.Enqueue(0);
            dist1[0] = 0;

            while (q.Count > 0)
            {
                var p = q.Dequeue();
                if (flag[p]) continue;
                flag[p] = true;
                foreach (var i in Edges[p])
                {
                    if (flag[i]) continue;
                    if (dist1[p] + 1 < dist1[i])
                    {
                        dist1[i] = dist1[p] + 1;
                        q.Enqueue(i);
                    }
                }
            }
        }



        var distN = new int[N];
        {
            var flag = new bool[N];
            var q = new Queue<int>();
            for (int i = 0; i < N; i++)
            {
                distN[i] = int.MaxValue;
            }
            q.Enqueue(N - 1);
            distN[N - 1] = 0;

            while (q.Count > 0)
            {
                var p = q.Dequeue();
                if (flag[p]) continue;
                flag[p] = true;
                foreach (var i in Edges[p])
                {
                    if (flag[i]) continue;
                    if (distN[p] + 1 < distN[i])
                    {
                        distN[i] = distN[p] + 1;
                        q.Enqueue(i);
                    }
                }
            }
        }

        // a[i] a[j]を作る

        // min(1 -> a[i] -> a[j] -> n, 1 -> a[j] -> a[i] -> n)
        var diff = new int[N];
        for (int i = 0; i < N; i++)
        {
            diff[i] = dist1[i] - distN[i];
        }

        var st = new SegTree<int>(2000000, (a, b) => -a.CompareTo(b), int.MinValue);


        long ans = int.MinValue;
        for (int i = 0; i < K; i++)
        {
            // a[i] を選ぶ
            // a[j] の候補

            // min(1 -> a[i] -> a[j] -> n, 1 -> a[j] -> a[i] -> n) が最大

            // 1 ->a[i] 固定
            // 1[i] + n[j] <= n[i] + 1[j]
            // 1[i] - n[i] <= 1[j] - n[j]

            ans = Math.Max(ans, dist1[A[i]] + 1 + st.Query(dist1[A[i]] - distN[A[i]] + 1000000, 2000000));
            st[dist1[A[i]] - distN[A[i]] + 1000000] = Math.Max(st[dist1[A[i]] - distN[A[i]] + 1000000], distN[A[i]]);
        }

        st = new SegTree<int>(2000000, (a, b) => -a.CompareTo(b), int.MinValue);

        for (int i = K - 1; i >= 0; i--)
        {
            ans = Math.Max(ans, dist1[A[i]] + 1 + st.Query(dist1[A[i]] - distN[A[i]] + 1000000, 2000000));
            st[dist1[A[i]] - distN[A[i]] + 1000000] = Math.Max(st[dist1[A[i]] - distN[A[i]] + 1000000], distN[A[i]]);
        }

        Console.WriteLine(Math.Min(dist1[N - 1], ans));
    }

    public static void Main(string[] args) => new Program().Solve();
}



class UnionFind
{
    private int[] Par, Rank;
    public UnionFind(int max)
    {
        Par = new int[max];
        Rank = new int[max];
        for (int i = 0; i < max; i++)
        {
            Par[i] = i;
        }
    }

    private int Find(int n)
    {
        if (Par[n] == n)
        {
            return n;
        }
        else
        {
            return Find(Par[n]);
        }
    }

    public bool Same(int a, int b)
    {
        return Find(a) == Find(b);
    }

    public void Union(int a, int b)
    {
        a = Find(a);
        b = Find(b);
        if (a == b) return;
        if (Rank[a] < Rank[b])
        {
            Par[a] = b;
        }
        else
        {
            Par[b] = a;
            if (Rank[a] == Rank[b]) Rank[a]++;
        }
    }
}


class SegTree<T>
{
    private readonly T[] Array;
    private readonly Comparison<T> Compare;
    private readonly int N;
    private readonly T Max;
    public SegTree(int size, T max) : this(size, Comparer<T>.Default, max) { }
    public SegTree(int size, IComparer<T> comparer, T max) : this(size, comparer.Compare, max) { }
    public SegTree(int size, Comparison<T> comparison, T max)
    {
        Max = max;
        N = 1;
        while (N < size)
        {
            N *= 2;
        }
        Compare = comparison;
        Array = new T[2 * N];
        for (int i = 1; i < 2 * N; i++)
        {
            Array[i] = max;
        }
    }

    new public T this[int i]
    {
        get { return Array[i + N]; }
        set { Update(value, i); }
    }


    private T Min(T x, T y)
    {
        return Compare(x, y) < 0 ? x : y;
    }

    public void Update(T item, int index)
    {
        index += N;
        Array[index] = item;
        while (index > 1)
        {
            index /= 2;
            Array[index] = Min(Array[index * 2], Array[index * 2 + 1]);
        }
    }

    private T Q(int left, int right, int k, int l, int r)
    {
        if (left <= l && r <= right)
        {
            return Array[k];
        }
        if (r <= left || right <= l)
        {
            return Max;
        }
        return Min(Q(left, right, k * 2, l, (l + r) / 2), Q(left, right, k * 2 + 1, (l + r) / 2, r));
    }

    public T Query(int left, int right)
    {
        return Q(left, right, 1, 0, N);
    }

}

namespace CompLib.Util
{
    using System;
    using System.Linq;
    class Scanner
    {
        private int _index;
        private string[] _line;
        const char separator = ' ';

        public Scanner()
        {
            _index = 0;
            _line = new string[0];
        }

        public string Next()
        {
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(separator);
                _index = 0;
            }
            return _line[_index++];
        }
        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(separator);
            _index = _line.Length;
            return _line;
        }
        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
    }
}