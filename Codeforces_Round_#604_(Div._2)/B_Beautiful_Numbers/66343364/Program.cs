using System;
using System.Collections.Generic;
using System.Text;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        var sb = new StringBuilder();
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Query(sc.NextInt(), sc.IntArray()));
        }

        Console.Write(sb.ToString());
    }


    private string Query(int m, int[] p)
    {
        for (int i = 0; i < m; i++)
        {
            p[i]--;
        }

        var ans = new char[m];
        var rmq = new SegTree<int>(m, int.MaxValue);
        var min = new SegTree<int>(m, int.MaxValue);
        var max = new SegTree<int>(m, (a, b) => -a.CompareTo(b), int.MinValue);
        for (int i = 0; i < m; i++)
        {
            min[p[i]] = i;
            max[p[i]] = i;
            rmq[i] = p[i];
        }

        for (int i = 0; i < m; i++)
        {
            int mn = min.Query(0, i + 1);
            int mx = max.Query(0, i + 1);
            bool a = mx - mn + 1 == i + 1;
            bool b = rmq.Query(0, mn) > i;
            bool c = rmq.Query(mx + 1, m) > i;
            ans[i] = a && b && c ? '1' : '0';
        }

        return new string(ans);
    }


    public static void Main(string[] args)
    {
        new Program().Solve();
    }
}

class SegTree<T>
{
    private readonly T[] Array;
    private readonly Comparison<T> Compare;
    private readonly int N;
    private readonly T Max;

    public SegTree(int size, T max) : this(size, Comparer<T>.Default, max)
    {
    }

    public SegTree(int size, IComparer<T> comparer, T max) : this(size, comparer.Compare, max)
    {
    }

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

class Scanner
{
    public Scanner()
    {
        _pos = 0;
        _line = new string[0];
    }

    const char Separator = ' ';
    private int _pos;
    private string[] _line;

    #region スペース区切りで取得

    public string Next()
    {
        if (_pos >= _line.Length)
        {
            _line = Console.ReadLine().Split(Separator);
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

    #region 型変換

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

    #region 配列取得

    public string[] Array()
    {
        if (_pos >= _line.Length)
            _line = Console.ReadLine().Split(Separator);

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