using System;
using CompLib.Collections;
using CompLib.Util;

public class Program
{
    private int N, M, K;
    private int[] X, Y;
    private Set<int>[] SetBR, SetBC;

    public void Solve()
    {
        var sc = new Scanner();

        // 迷路

        // 直進 or 右に方向転換

        // 1,1から 障害物の無いマスすべてを1回ずつ通れるか?
        N = sc.NextInt();
        M = sc.NextInt();
        K = sc.NextInt();

        // i行目にブロック
        SetBR = new Set<int>[N];
        for (int i = 0; i < N; i++)
        {
            SetBR[i] = new Set<int>();
            SetBR[i].Add(-1);
            SetBR[i].Add(M);
        }

        // i列
        SetBC = new Set<int>[M];
        for (int i = 0; i < M; i++)
        {
            SetBC[i] = new Set<int>();
            SetBC[i].Add(-1);
            SetBC[i].Add(N);
        }

        for (int i = 0; i < K; i++)
        {
            int x = sc.NextInt() - 1;
            int y = sc.NextInt() - 1;

            SetBR[x].Add(y);
            SetBC[y].Add(x);
        }

        Console.WriteLine(Check() ? "Yes" : "No");
    }

    bool Check()
    {
        // 使えない行
        var setR = new Set<int>();
        setR.Add(-1);
        setR.Add(N);
        // 使えない列
        var setC = new Set<int>();
        setC.Add(-1);
        setC.Add(M);
        int curX = 0;
        int curY = 0;
        long cnt = 1;

        int dir = 0;

        bool f = true;
        while (f)
        {
            switch (dir)
            {
                case 0:
                {
                    // 右

                    // この行で右にあるブロック
                    int b = Math.Min(setC[setC.UpperBound(curY)], SetBR[curX][SetBR[curX].UpperBound(curY)]);

                    // 移動できる距離
                    int d = b - curY - 1;
                    if (d == 0 && !(curX == 0 && curY == 0))
                    {
                        f = false;
                        break;
                    }

                    curY += d;
                    cnt += d;
                    setR.Add(curX);
                }
                    break;
                case 1:
                {
                    // 下
                    // この列で下にあるブロック
                    int b = Math.Min(setR[setR.UpperBound(curX)], SetBC[curY][SetBC[curY].UpperBound(curX)]);
                    int d = b - curX - 1;
                    if (d == 0)
                    {
                        f = false;
                        break;
                    }

                    curX += d;
                    cnt += d;
                    setC.Add(curY);
                }
                    break;
                case 2:
                {
                    // 左

                    int b = Math.Max(setC[setC.LowerBound(curY) - 1], SetBR[curX][SetBR[curX].LowerBound(curY) - 1]);
                    int d = curY - b - 1;
                    if (d == 0)
                    {
                        f = false;
                        break;
                    }

                    curY -= d;
                    cnt += d;
                    setR.Add(curX);
                }
                    break;
                case 3:
                {
                    // 上

                    int b = Math.Max(setR[setR.LowerBound(curX) - 1], SetBC[curY][SetBC[curY].LowerBound(curX) - 1]);
                    int d = curX - b - 1;
                    if (d == 0)
                    {
                        f = false;
                        break;
                    }

                    curX -= d;
                    cnt += d;
                    setC.Add(curY);
                }
                    break;
            }

            dir++;
            dir %= 4;
        }

        return cnt == (long) N * M - K;
    }

    public static void Main(string[] args) => new Program().Solve();
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
                string s = Console.ReadLine();
                while (s.Length == 0)
                {
                    s = Console.ReadLine();
                }

                _line = s.Split(Separator);
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