using CompLib.Collections;
using CompLib.Util;
using System;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int k = sc.NextInt();
        int[] a = sc.IntArray();

        var set = new Set<(int num, int index)>((l, r) => l.index.CompareTo(r.index));
        var pq = new PriorityQueue<(int num, int index)>((l, r) => r.num.CompareTo(l.num));

        for (int i = 0; i < n; i++)
        {
            set.Add((a[i], i));
            pq.Enqueue((a[i], i));
        }
        int[] ans = new int[n];

        int p = 0;
        while (set.Count > 0)
        {
            var max = pq.Dequeue();
            var ptr = set.LowerBound(max);

            if (ptr >= set.Count || set[ptr].num != max.num) continue;

            int begin = Math.Max(0, ptr - k);
            int end = Math.Min(set.Count, ptr + k + 1);
            for (int i = 0; i < end - begin; i++)
            {
                var tmp = set[begin];
                ans[tmp.index] = p + 1;
                set.RemoveAt(begin);
            }

            p++;
            p %= 2;
        }

        Console.WriteLine(string.Join("",ans));
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

// https://bitbucket.org/camypaper/complib
namespace CompLib.Collections
{
    using System;
    using System.Collections.Generic;
    #region Set
    public class Set<T>
    {
        Node root;
        readonly IComparer<T> comparer;
        readonly Node nil;
        public bool IsMultiSet { get; set; }
        public Set(IComparer<T> comparer)
        {
            nil = new Node(default(T));
            root = nil;
            this.comparer = comparer;
        }
        public Set(Comparison<T> comaprison) : this(Comparer<T>.Create(comaprison)) { }
        public Set() : this(Comparer<T>.Default) { }
        public bool Add(T v)
        {
            return insert(ref root, v);
        }
        public bool Remove(T v)
        {
            return remove(ref root, v);
        }
        public T this[int index] { get { return find(root, index); } }
        public int Count { get { return root.Count; } }
        public void RemoveAt(int k)
        {
            if (k < 0 || k >= root.Count) throw new ArgumentOutOfRangeException();
            removeAt(ref root, k);
        }
        public T[] Items
        {
            get
            {
                var ret = new T[root.Count];
                var k = 0;
                walk(root, ret, ref k);
                return ret;
            }
        }
        void walk(Node t, T[] a, ref int k)
        {
            if (t.Count == 0) return;
            walk(t.lst, a, ref k);
            a[k++] = t.Key;
            walk(t.rst, a, ref k);
        }

        bool insert(ref Node t, T key)
        {
            if (t.Count == 0) { t = new Node(key); t.lst = t.rst = nil; t.Update(); return true; }
            var cmp = comparer.Compare(t.Key, key);
            bool res;
            if (cmp > 0)
                res = insert(ref t.lst, key);
            else if (cmp == 0)
            {
                if (IsMultiSet) res = insert(ref t.lst, key);
                else return false;
            }
            else res = insert(ref t.rst, key);
            balance(ref t);
            return res;
        }
        bool remove(ref Node t, T key)
        {
            if (t.Count == 0) return false;
            var cmp = comparer.Compare(key, t.Key);
            bool ret;
            if (cmp < 0) ret = remove(ref t.lst, key);
            else if (cmp > 0) ret = remove(ref t.rst, key);
            else
            {
                ret = true;
                var k = t.lst.Count;
                if (k == 0) { t = t.rst; return true; }
                if (t.rst.Count == 0) { t = t.lst; return true; }


                t.Key = find(t.lst, k - 1);
                removeAt(ref t.lst, k - 1);
            }
            balance(ref t);
            return ret;
        }
        void removeAt(ref Node t, int k)
        {
            var cnt = t.lst.Count;
            if (cnt < k) removeAt(ref t.rst, k - cnt - 1);
            else if (cnt > k) removeAt(ref t.lst, k);
            else
            {
                if (cnt == 0) { t = t.rst; return; }
                if (t.rst.Count == 0) { t = t.lst; return; }

                t.Key = find(t.lst, k - 1);
                removeAt(ref t.lst, k - 1);
            }
            balance(ref t);
        }
        void balance(ref Node t)
        {
            var balance = t.lst.Height - t.rst.Height;
            if (balance == -2)
            {
                if (t.rst.lst.Height - t.rst.rst.Height > 0) { rotR(ref t.rst); }
                rotL(ref t);
            }
            else if (balance == 2)
            {
                if (t.lst.lst.Height - t.lst.rst.Height < 0) rotL(ref t.lst);
                rotR(ref t);
            }
            else t.Update();
        }

        T find(Node t, int k)
        {
            if (k < 0 || k > root.Count) throw new ArgumentOutOfRangeException();
            for (; ; )
            {
                if (k == t.lst.Count) return t.Key;
                else if (k < t.lst.Count) t = t.lst;
                else { k -= t.lst.Count + 1; t = t.rst; }
            }
        }
        public int LowerBound(T v)
        {
            var k = 0;
            var t = root;
            for (; ; )
            {
                if (t.Count == 0) return k;
                if (comparer.Compare(v, t.Key) <= 0) t = t.lst;
                else { k += t.lst.Count + 1; t = t.rst; }
            }
        }
        public int UpperBound(T v)
        {
            var k = 0;
            var t = root;
            for (; ; )
            {
                if (t.Count == 0) return k;
                if (comparer.Compare(t.Key, v) <= 0) { k += t.lst.Count + 1; t = t.rst; }
                else t = t.lst;
            }
        }

        void rotR(ref Node t)
        {
            var l = t.lst;
            t.lst = l.rst;
            l.rst = t;
            t.Update();
            l.Update();
            t = l;
        }
        void rotL(ref Node t)
        {
            var r = t.rst;
            t.rst = r.lst;
            r.lst = t;
            t.Update();
            r.Update();
            t = r;
        }


        class Node
        {
            public Node(T key)
            {
                Key = key;
            }
            public int Count { get; private set; }
            public sbyte Height { get; private set; }
            public T Key { get; set; }
            public Node lst, rst;
            public void Update()
            {
                Count = 1 + lst.Count + rst.Count;
                Height = (sbyte)(1 + Math.Max(lst.Height, rst.Height));
            }
            public override string ToString()
            {
                return string.Format("Count = {0}, Key = {1}", Count, Key);
            }
        }
    }
    #endregion
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
