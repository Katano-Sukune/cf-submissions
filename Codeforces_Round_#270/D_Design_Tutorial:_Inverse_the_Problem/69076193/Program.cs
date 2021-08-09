using System;
using System.Collections.Generic;

public class Program
{
    private int N;
    private int[][] D;
    private List<int>[] Edges;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        D = new int[N][];
        for (int i = 0; i < N; i++)
        {
            D[i] = sc.IntArray();
        }

        /*
         * D[i][j] i~jの距離
         *
         * 木が存在するか
         */

        /* 適当頂点 i
         * [i][j] + [j][k] = [i][k]なら[i][k]は i => j => kの順で通る[i][j]に辺が無い
         * 
         */

        for (int i = 0; i < N; i++)
        {
            if (D[i][i] != 0)
            {
                Console.WriteLine("NO");
                return;
            }
        }


        for (int i = 0; i < N; i++)
        {
            for (int j = i + 1; j < N; j++)
            {
                if (D[i][j] != D[j][i] || D[i][j] == 0)
                {
                    Console.WriteLine("NO");
                    return;
                }
            }
        }

        List<E> edges = new List<E>();

        for (int i = 0; i < N; i++)
        {
            for (int j = i + 1; j < N; j++)
            {
                edges.Add(new E(i, j, D[i][j]));
            }
        }

        edges.Sort((a, b) => a.C.CompareTo(b.C));
        var uf = new UnionFind(N);

        int cnt = 0;

        var use = new List<E>();
        foreach (E edge in edges)
        {
            if (!uf.Same(edge.U, edge.V))
            {
                uf.Union(edge.U, edge.V);
                cnt++;
                use.Add(edge);
            }

            if (cnt >= N - 1)
            {
                break;
            }
        }

        Edges = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            Edges[i] = new List<int>();
        }

        foreach (E e in use)
        {
            Edges[e.U].Add(e.V);
            Edges[e.V].Add(e.U);
        }

        for (int i = 0; i < N; i++)
        {
            if (!Search(i, -1, i, 0))
            {
                Console.WriteLine("NO");
                return;
            }
        }
        Console.WriteLine("YES");
    }

    private bool Search(int i, int parent, int root, int dist)
    {
        if (D[i][root] != dist)
        {
            return false;
        }

        foreach (var j in Edges[i])
        {
            if (j == parent) continue;
            if (!Search(j, i, root, dist + D[i][j]))
            {
                return false;
            }
        }

        return true;
    }


    public static void Main(string[] args)
    {
        new Program().Solve();
    }
}

struct E
{
    public int U, V, C;

    public E(int u, int v, int c)
    {
        U = u;
        V = v;
        C = c;
    }
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