using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.Collections.Generic;
using CompLib.DataStructure;
using CompLib.Collections;
using System.Text;

public class Program
{
    int N, M1, M2;

    // i行目、j列目
    // 要素がある列、行
    Set<int>[] Row, Col;

    // 要素がある行
    // 要素数、代表値
    Set<(int cnt, int leader)> Rows;

    // i行 j列目に要素valueがある
    Dictionary<int, int>[] Map;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M1 = sc.NextInt();
        M2 = sc.NextInt();

        var uf1 = new UnionFind(N);
        for (int i = 0; i < M1; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            uf1.Connect(u, v);
        }


        var uf2 = new UnionFind(N);
        for (int i = 0; i < M2; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            uf2.Connect(u, v);
        }

        if (M1 < M2) (uf1, uf2) = (uf2, uf1);

        Map = new Dictionary<int, int>[N];

        Row = new Set<int>[N];
        Col = new Set<int>[N];
        for (int i = 0; i < N; i++)
        {
            Map[i] = new Dictionary<int, int>();
            Row[i] = new Set<int>();
            Col[i] = new Set<int>();
        }
        Rows = new Set<(int cnt, int leader)>((l, r) => r.cnt != l.cnt ? r.cnt.CompareTo(l.cnt) : l.leader.CompareTo(r.leader));
        for (int i = 0; i < N; i++)
        {
            int g1 = uf1.Find(i);
            int g2 = uf2.Find(i);
            Row[g1].Add(g2);
            Col[g2].Add(g1);
            Map[g1][g2] = i;
        }
        for (int i = 0; i < N; i++)
        {
            int g1 = uf1.Find(i);
            if (g1 == i)
            {
                Rows.Add((Row[i].Count, i));
            }
        }

        int k = 0;
        StringBuilder sb = new StringBuilder();

        while (Rows.Count > 1)
        {
            int x = Rows[0].leader;
            Rows.RemoveAt(0);
            int y = Rows[0].leader;
            Rows.RemoveAt(0);

            if (Row[x].Count < Row[y].Count) (x, y) = (y, x);

            int a = Row[x][0];
            int b = Row[y][0];
            if (a == b)
            {
                a = Row[x][1];
            }
            k++;
            sb.AppendLine($"{Map[x][a] + 1} {Map[y][b] + 1}");
            if (Col[a].Count < Col[b].Count) (a, b) = (b, a);

            // 行x,y
            // 列a,bをマージ

            MergeRow(x, y);
            MergeCol(a, b);
            Rows.Add((Row[x].Count, x));
        }

        Console.WriteLine(k);
        Console.Write(sb);
    }

    void MergeCol(int a, int b)
    {
        foreach (int r in Col[b].Items)
        {
            Map[r][a] = Map[r][b];
            Col[a].Add(r);
            Row[r].Remove(b);
            Row[r].Add(a);
        }
    }

    void MergeRow(int x, int y)
    {
        foreach (int c in Row[y].Items)
        {
            Map[x][c] = Map[y][c];
            Row[x].Add(c);
            Col[c].Remove(y);
            Col[c].Add(x);
        }
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

namespace CompLib.DataStructure
{
    using System.Collections.Generic;
    using System.Linq;
    class UnionFind
    {
        private readonly int _n;
        private readonly int[] _parent, _size;

        /// <summary>
        /// n頂点の無向グラフに 1.辺を追加, 2.2頂点が同じ連結成分に属するか判定 ができるデータ構造
        /// </summary>
        /// <param name="n">頂点の個数</param>
        public UnionFind(int n)
        {
            _n = n;
            _parent = new int[_n];
            _size = new int[_n];
            for (int i = 0; i < _n; i++)
            {
                _parent[i] = i;
                _size[i] = 1;
            }
        }

        /// <summary>
        /// iがいる連結成分の代表値
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int Find(int i) => _parent[i] == i ? i : Find(_parent[i]);

        /// <summary>
        /// x,yが同じ連結成分にいるか?
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Same(int x, int y) => Find(x) == Find(y);

        /// <summary>
        /// (x, y)に辺を追加する
        /// </summary>
        /// <remarks>
        /// ACLでは連結された代表値を返しますが、ここでは連結できたか?を返します
        /// </remarks>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>x,yが違う連結成分だったならtrueを返す</returns>
        public bool Connect(int x, int y)
        {
            x = Find(x);
            y = Find(y);
            if (x == y) return false;

            // データ構造をマージする一般的なテク
            if (_size[x] > _size[y])
            {
                _parent[y] = x;
                _size[x] += _size[y];
            }
            else
            {
                _parent[x] = y;
                _size[y] += _size[x];
            }

            return true;
        }

        /// <summary>
        /// iが含まれる成分のサイズ
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int GetSize(int i) => _size[Find(i)];

        /// <summary>
        /// 連結成分のリスト
        /// </summary>
        /// <returns></returns>
        public List<int>[] Groups()
        {
            var leaderBuf = new int[_n];
            var groupSize = new int[_n];
            for (int i = 0; i < _n; i++)
            {
                leaderBuf[i] = Find(i);
                groupSize[leaderBuf[i]]++;
            }

            var result = new List<int>[_n];
            for (int i = 0; i < _n; i++)
            {
                result[i] = new List<int>(groupSize[i]);
            }

            for (int i = 0; i < _n; i++)
            {
                result[leaderBuf[i]].Add(i);
            }

            return result.Where(ls => ls.Count > 0).ToArray();
        }
    }
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