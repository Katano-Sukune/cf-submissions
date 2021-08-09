using System;
using System.Collections.Generic;
using System.Text;
using CompLib.Collections;
using CompLib.Util;

public class Program
{
    private int N, M;
    private int[] A;
    private int[] B;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        A = sc.IntArray();
        B = sc.IntArray();

        for (int i = 0; i < N; i++)
        {
            A[i]--;
            B[i]--;
        }

        // 長さNの順列A,B
        // M個のクエリ

        // 1 la ra lb rb
        // A[la:ra] B:[lb,rb]に共通で含まれる数の個数を出力

        // 2 x y
        // b[x], b[y] をスワップ

        /*
         * A_i, B_jにkがある
         * (i,j)を1にする
         *
         * la,ra,lb,rbの矩形に1がいくつあるか?
         */
        int[] indexA = new int[N];
        int[] indexB = new int[N];
        for (int i = 0; i < N; i++)
        {
            indexA[A[i]] = i;
            indexB[B[i]] = i;
        }

        var q = new List<(int type, int la, int ra, int lb, int rb)>();

        var ft = new FenwickTree2D(N);
        for (int i = 0; i < N; i++)
        {
            ft.AddT(indexB[i], indexA[i]);
        }

        int[] tmpB = new int[N];
        Array.Copy(B, tmpB, N);
        for (int i = 0; i < M; i++)
        {
            if (sc.NextInt() == 1)
            {
                int la = sc.NextInt() - 1;
                int ra = sc.NextInt();
                int lb = sc.NextInt() - 1;
                int rb = sc.NextInt();
                ft.AddT(lb, la);
                ft.AddT(lb, ra);
                ft.AddT(rb, la);
                ft.AddT(rb, ra);
                q.Add((1, la, ra, lb, rb));
            }
            else
            {
                int x = sc.NextInt() - 1;
                int y = sc.NextInt() - 1;
                Swap(ref tmpB[x], ref tmpB[y]);
                ft.AddT(x, indexA[tmpB[x]]);
                ft.AddT(y, indexA[tmpB[y]]);
                q.Add((2, x, y, -1, -1));
            }
        }

        ft.Build();
        for (int i = 0; i < N; i++)
        {
            ft.Add(indexB[i], indexA[i], 1);
        }

        var sb = new StringBuilder();
        for (int i = 0; i < M; i++)
        {
            if (q[i].type == 1)
            {
                int la = q[i].la;
                int ra = q[i].ra;
                int lb = q[i].lb;
                int rb = q[i].rb;
                sb.AppendLine(ft.Sum(la, ra, lb, rb).ToString());
            }
            else
            {
                int x = q[i].la;
                int y = q[i].ra;
                ft.Add(x, indexA[B[x]], -1);
                ft.Add(y, indexA[B[y]], -1);
                Swap(ref B[x], ref B[y]);
                ft.Add(x, indexA[B[x]], 1);
                ft.Add(y, indexA[B[y]], 1);
            }
        }


        Console.Write(sb);
    }

    void Swap(ref int x, ref int y)
    {
        x ^= y;
        y ^= x;
        x ^= y;
    }

    public static void Main(string[] args) => new Program().Solve();
}

class FenwickTree2D
{
    private List<int>[] L;

    private FenwickTree[] Bits;

    public FenwickTree2D(int n)
    {
        L = new List<int>[n + 1];
        for (int i = 0; i <= n; i++)
        {
            L[i] = new List<int>();
        }
    }

    public void AddT(int r, int c)
    {
        c++;
        for (; c < L.Length; c += c & -c)
        {
            L[c].Add(r);
        }
    }

    public void Build()
    {
        Bits = new FenwickTree[L.Length];
        for (int i = 0; i < L.Length; i++)
        {
            L[i].Sort();
            var tmp = new List<int>();
            for (int j = 0; j < L[i].Count; j++)
            {
                if (j == 0 || L[i][j - 1] != L[i][j]) tmp.Add(L[i][j]);
            }

            Bits[i] = new FenwickTree(tmp.Count);
            L[i] = tmp;
        }
    }

    public void Add(int r, int c, int num)
    {
        c++;
        for (; c < L.Length; c += c & -c)
        {
            Bits[c].Add(IndexOf(c, r), num);
        }
    }

    public int Sum(int lb, int rb, int a)
    {
        int res = 0;
        for (; a > 0; a -= a & -a)
        {
            res += Bits[a].Sum(IndexOf(a, lb), IndexOf(a, rb));
        }

        return res;
    }

    public int Sum(int la, int ra, int lb, int rb)
    {
        return Sum(lb, rb, ra) - Sum(lb, rb, la);
    }

    int IndexOf(int ls, int r)
    {
        int ok = L[ls].Count ;
        int ng = -1;
        while (ok - ng > 1)
        {
            int mid = (ok + ng) / 2;
            if (L[ls][mid] >= r) ok = mid;
            else ng = mid;
        }

        return ok;
    }
}

namespace CompLib.Collections
{
    using Num = Int32;

    public class FenwickTree
    {
        private readonly Num[] _array;
        public readonly int Count;

        public FenwickTree(int size)
        {
            _array = new Num[size + 1];
            Count = size;
        }

        /// <summary>
        /// A[i]にnを加算
        /// </summary>
        /// <param name="i"></param>
        /// <param name="n"></param>
        public void Add(int i, Num n)
        {
            i++;
            for (; i <= Count; i += i & -i)
            {
                _array[i] += n;
            }
        }

        /// <summary>
        /// [0,r)の和を求める
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public Num Sum(int r)
        {
            Num result = 0;
            for (; r > 0; r -= r & -r)
            {
                result += _array[r];
            }

            return result;
        }

        /// <summary>
        /// [0,i)の和がw以上になるi
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        public int LowerBound(int w)
        {
            if (w <= 0) return 0;
            int x = 0;
            int k = 1;
            while (k * 2 < Count) k *= 2;
            for (; k > 0; k /= 2)
            {
                if (x + k < Count && _array[x + k] < w)
                {
                    w -= _array[x + k];
                    x += k;
                }
            }

            return x + 1;
        }

        // [l,r)の和を求める
        public Num Sum(int l, int r) => Sum(r) - Sum(l);
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