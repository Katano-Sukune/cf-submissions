using CompLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Q(sc.NextInt(),sc.IntArray()));
        }

        Console.Write(sb.ToString());
    }

    private string Q(int n, int[] b)
    {
        var a = new int[n * 2];

        var hs = new Set<int>();
        for (int i = 1; i <= n * 2; i++)
        {
            hs.Add(i);
        }

        for (int i = 0; i < n; i++)
        {
            hs.Remove(b[i]);
            
        }

        for(int i = 0; i < n; i++)
        {
            a[i * 2] = b[i];
            var ptr = hs.UpperBound(b[i]);
            if(ptr < hs.Count)
            {
                a[i * 2 + 1] = hs[ptr];
                hs.RemoveAt(ptr);
            }
            else
            {
                return "-1";
            }
        }

        return string.Join(" ", a);
    }

    public static void Main(string[] args) => new Program().Solve();
}

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