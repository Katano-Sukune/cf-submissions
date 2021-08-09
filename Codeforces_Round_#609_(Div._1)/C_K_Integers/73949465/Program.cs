using System;
using CompLib.Collections;
using CompLib.Collections.Generic;
using CompLib.Util;

public class Program
{
    private int N;
    private int[] P;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        P = sc.IntArray();

        int[] index = new int[N + 1];
        for (int i = 0; i < N; i++)
        {
            index[P[i]] = i;
        }

        long[] ans = new long[N];
        var t = new long[N + 1];
        t[1] = 0;
        ans[0] = 0;
        var set = new Set<long>();
        var st = new SegmentTree<long>((l, r) => l + r, 0);
        set.Add(index[1]);
        st[index[1]] = index[1];
        for (long i = 2; i <= N; i++)
        {
            set.Add(index[i]);
            st[index[i]] = index[i];
            // 中央値

            long ind = set.Count / 2;
            long mid = set[(int) ind];

            // 前に集まるやつ
            var f = st.Query(0, (int) mid);
            var b = st.Query((int) mid, N);

            // [1,i]を集めるために必要な手数
            var a = 0L;
            a += st[(int) mid] * ind - f - (ind + 1) * ind / 2;
            a += b - st[(int) mid] * (i - ind) - (i - ind) * (i - ind - 1) / 2;
            // Console.WriteLine($"{i} {ind} {f} {b} {a}");
            // 転倒数

            t[i] = t[i - 1] + set.Count - set.UpperBound(index[i]);


            ans[i - 1] = a + t[i];
        }

        Console.WriteLine(string.Join(" ", ans));
    }

    public static void Main(string[] args) => new Program().Solve();
}

namespace CompLib.Collections.Generic
{
    using System;

    public class SegmentTree<T>
    {
        // 制約に合った2の冪
        private const int N = 1 << 21;
        private T[] _array;

        private T _identity;
        private Func<T, T, T> _operation;

        public SegmentTree(Func<T, T, T> operation, T identity)
        {
            _identity = identity;
            _operation = operation;
            _array = new T[N * 2];
            for (int i = 1; i < N * 2; i++)
            {
                _array[i] = _identity;
            }
        }

        /// <summary>
        /// A[i]をnに更新 O(log N)
        /// </summary>
        /// <param name="i"></param>
        /// <param name="n"></param>
        public void Update(int i, T n)
        {
            i += N;
            _array[i] = n;
            while (i > 1)
            {
                i /= 2;
                _array[i] = _operation(_array[i * 2], _array[i * 2 + 1]);
            }
        }

        private T Query(int left, int right, int k, int l, int r)
        {
            if (r <= left || right <= l)
            {
                return _identity;
            }

            if (left <= l && r <= right)
            {
                return _array[k];
            }

            return _operation(Query(left, right, k * 2, l, (l + r) / 2),
                Query(left, right, k * 2 + 1, (l + r) / 2, r));
        }

        /// <summary>
        /// A[left] op A[left+1] ... op A[right-1]を求める
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public T Query(int left, int right)
        {
            return Query(left, right, 1, 0, N);
        }

        public T this[int i]
        {
            set { Update(i, value); }
            get { return _array[i + N]; }
        }
    }
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

        public Set(Comparison<T> comaprison) : this(Comparer<T>.Create(comaprison))
        {
        }

        public Set() : this(Comparer<T>.Default)
        {
        }

        public bool Add(T v)
        {
            return insert(ref root, v);
        }

        public bool Remove(T v)
        {
            return remove(ref root, v);
        }

        public T this[int index]
        {
            get { return find(root, index); }
        }

        public int Count
        {
            get { return root.Count; }
        }

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
            if (t.Count == 0)
            {
                t = new Node(key);
                t.lst = t.rst = nil;
                t.Update();
                return true;
            }

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
                if (k == 0)
                {
                    t = t.rst;
                    return true;
                }

                if (t.rst.Count == 0)
                {
                    t = t.lst;
                    return true;
                }


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
                if (cnt == 0)
                {
                    t = t.rst;
                    return;
                }

                if (t.rst.Count == 0)
                {
                    t = t.lst;
                    return;
                }

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
                if (t.rst.lst.Height - t.rst.rst.Height > 0)
                {
                    rotR(ref t.rst);
                }

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
            for (;;)
            {
                if (k == t.lst.Count) return t.Key;
                else if (k < t.lst.Count) t = t.lst;
                else
                {
                    k -= t.lst.Count + 1;
                    t = t.rst;
                }
            }
        }

        public int LowerBound(T v)
        {
            var k = 0;
            var t = root;
            for (;;)
            {
                if (t.Count == 0) return k;
                if (comparer.Compare(v, t.Key) <= 0) t = t.lst;
                else
                {
                    k += t.lst.Count + 1;
                    t = t.rst;
                }
            }
        }

        public int UpperBound(T v)
        {
            var k = 0;
            var t = root;
            for (;;)
            {
                if (t.Count == 0) return k;
                if (comparer.Compare(t.Key, v) <= 0)
                {
                    k += t.lst.Count + 1;
                    t = t.rst;
                }
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
                Height = (sbyte) (1 + Math.Max(lst.Height, rst.Height));
            }

            public override string ToString()
            {
                return string.Format("Count = {0}, Key = {1}", Count, Key);
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