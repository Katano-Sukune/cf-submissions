using System;
using System.Collections.Generic;

public class Program
{
    private int N;
    private int[] A;

    // iを頂点とする部分木の番号最大の頂点
    private int[] Max;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();

        /*
         * 後ろから見る
         *
         * 部分木を A[i]にくっつける 元の根にあとでくっつく -> くっつけた辺より後のが小さい
         * くっつけたらもうその頂点には接続しない
         *
         */

        // 以降登場しない つなげるコストが小さい順にくっつける

        var cnt = new int[N + 1];
        for (int i = 0; i < N - 1; i++)
        {
            cnt[A[i]]++;
        }

        Max = new int[N + 1];
        for (int i = 1; i <= N; i++)
        {
            Max[i] = i;
        }

        var pq = new PriorityQueue<int>((a, b) => Max[a].CompareTo(Max[b]));

        for (int i = 1; i <= N; i++)
        {
            if (cnt[i] == 0)
            {
                pq.Enqueue(i);
            }
        }

        int root = -1;
        Edge[] ans = new Edge[N - 1];

        for (int i = N - 1 - 1; i >= 0; i--)
        {
            if (pq.Count > 0)
            {
                int d = pq.Dequeue();
                // A[i] にdをくっつける
                ans[i] = new Edge(A[i], d);
                Max[A[i]] = Math.Max(Max[A[i]], d);
                cnt[A[i]]--;
                if (cnt[A[i]] == 0)
                {
                    pq.Enqueue(A[i]);
                }

                if (i == 0)
                {
                    root = A[i];
                }
            }
            else
            {
                Console.WriteLine("-1");
                return;
            }
        }

        Console.WriteLine(root);
        Console.WriteLine(string.Join("\n", ans));
    }

    public static void Main(string[] args)
    {
        new Program().Solve();
    }
}

struct Edge
{
    public int U, V;

    public Edge(int u, int v)
    {
        U = u;
        V = v;
    }

    public override string ToString()
    {
        return $"{U} {V}";
    }
}


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