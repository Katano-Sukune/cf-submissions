using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Contest
{

    class PriorityQueue<T>
    {
        private readonly List<T> heap;
        private readonly Comparison<T> compare;
        private int size;
        public PriorityQueue() : this(Comparer<T>.Default) { }
        public PriorityQueue(IComparer<T> comparer) : this(16, comparer.Compare) { }
        public PriorityQueue(Comparison<T> comparison) : this(16, comparison) { }
        public PriorityQueue(int capacity, Comparison<T> comparison)
        {
            this.heap = new List<T>(capacity);
            this.compare = comparison;
        }
        public void Enqueue(T item)
        {
            this.heap.Add(item);
            var i = size++;
            while (i > 0)
            {
                var p = (i - 1) >> 1;
                if (compare(this.heap[p], item) <= 0)
                    break;
                this.heap[i] = heap[p];
                i = p;
            }
            this.heap[i] = item;

        }
        public T Dequeue()
        {
            var ret = this.heap[0];
            var x = this.heap[--size];
            var i = 0;
            while ((i << 1) + 1 < size)
            {
                var a = (i << 1) + 1;
                var b = (i << 1) + 2;
                if (b < size && compare(heap[b], heap[a]) < 0) a = b;
                if (compare(heap[a], x) >= 0)
                    break;
                heap[i] = heap[a];
                i = a;
            }
            heap[i] = x;
            heap.RemoveAt(size);
            return ret;
        }
        public T Peek() { return heap[0]; }
        public int Count { get { return size; } }
        public bool Any() { return size > 0; }
    }

    struct S
    {
        public int Num, Count;

        public S(int n, int c)
        {
            Num = n;
            Count = c;
        }
    }
    class T
    {
        private int N;
        private int M;
        private List<int>[] E;

        public T(int n, int m, List<int>[] e)
        {
            N = n;
            M = m;
            E = e;
        }

        public List<int> Calc()
        {
            bool[] b = new bool[N];
            int[] cnt = new int[N];
            cnt[0] = 1;
            var q = new Queue<int>();
            q.Enqueue(0);
            while (q.Count > 0)
            {
                int s = q.Dequeue();
                if (b[s]) continue;

                b[s] = true;
                foreach (var i in E[s])
                {
                    if (cnt[i] == 0)
                    {
                        cnt[i] = cnt[s] + 1;
                        q.Enqueue(i);
                    }
                }
            }

            int cc = 0;
            foreach (var i in cnt)
            {
                if (i % 2 == 0) cc++;
            }
            var ans = new List<int>();
            if (cc <= N / 2)
            {
                for (int i = 0; i < N; i++)
                {
                    if (cnt[i] % 2 == 0) ans.Add(i + 1);
                }
            }
            else
            {
                for (int i = 0; i < N; i++)
                {
                    if (cnt[i] % 2 == 1) ans.Add(i + 1);
                }
            }

            return ans;
        }
    }
    class Program
    {
        private Scanner sc;
        public void Solve()
        {
            sc = new Scanner();
            int Q = sc.NextInt();
            var sb = new StringBuilder();
            for (int i = 0; i < Q; i++)
            {
                int n = sc.NextInt();
                int m = sc.NextInt();
                List<int>[] e = new List<int>[n];
                for (int j = 0; j < n; j++)
                {
                    e[j] = new List<int>();
                }

                for (int j = 0; j < m; j++)
                {
                    int a = sc.NextInt() - 1;
                    int b = sc.NextInt() - 1;
                    e[a].Add(b);
                    e[b].Add(a);
                }
                var ans = new T(n, m, e).Calc();
                sb.AppendLine(ans.Count.ToString());
                sb.AppendLine(string.Join(" ", ans));
            }
            Console.Write(sb.ToString());
        }



        static void Main() => new Program().Solve();
    }
}

class Scanner
{
    public Scanner()
    {
        _stream = new StreamReader(Console.OpenStandardInput());
        _pos = 0;
        _line = new string[0];
        _separator = ' ';
    }
    private char _separator;
    private StreamReader _stream;
    private int _pos;
    private string[] _line;
    #region get a element
    public string Next()
    {
        if (_pos >= _line.Length)
        {
            _line = _stream.ReadLine().Split(_separator);
            _pos = 0;
        }
        return _line[_pos++];
    }
    public int NextInt()
    {
        return int.Parse(Next());
    }
    public long NextLong()
    {
        return long.Parse(Next());
    }
    public double NextDouble()
    {
        return double.Parse(Next());
    }
    #endregion
    #region convert array
    private int[] ToIntArray(string[] array)
    {
        var result = new int[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = int.Parse(array[i]);
        }

        return result;
    }
    private long[] ToLongArray(string[] array)
    {
        var result = new long[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = long.Parse(array[i]);
        }

        return result;
    }
    private double[] ToDoubleArray(string[] array)
    {
        var result = new double[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = double.Parse(array[i]);
        }

        return result;
    }
    #endregion
    #region get array
    public string[] Array()
    {
        if (_pos >= _line.Length)
            _line = _stream.ReadLine().Split(_separator);
        _pos = _line.Length;
        return _line;
    }
    public int[] IntArray()
    {
        return ToIntArray(Array());
    }
    public long[] LongArray()
    {
        return ToLongArray(Array());
    }
    public double[] DoubleArray()
    {
        return ToDoubleArray(Array());
    }
    #endregion
}