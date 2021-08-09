using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.IO;
using System.Diagnostics;

class Scanner
{
    private readonly char Separator = ' ';
    private int Index = 0;
    private string[] Line = new string[0];
    public string Next()
    {
        if (Index >= Line.Length)
        {
            Line = Console.ReadLine().Split(Separator);
            Index = 0;
        }
        var ret = Line[Index];
        Index++;
        return ret;
    }

    public int NextInt()
    {
        return int.Parse(Next());
    }

    private long NextLong()
    {
        return long.Parse(Next());
    }
}

class SegmentTree<T>
{
    private Comparison<T> Comparison;
    private int N;
    private T[] Tree;
    private T Min;

    public SegmentTree(int length, Comparison<T> comparition, T min)
    {
        for (N = 1; N <= length; N *= 2) ;
        Comparison = comparition;
        Tree = (new T[2 * N - 1]).Select(i => min).ToArray();
        Min = min;
    }
    /// <summary>
    /// index(0_indexed)をvに更新
    /// </summary>
    /// <param name="index"></param>
    /// <param name="v"></param>
    public void Update(int index, T v)
    {
        int i = index + N - 1;
        Tree[i] = v;
        while (i > 0)
        {
            i = (i - 1) / 2;
            Tree[i] = Max(Tree[i * 2 + 1], Tree[i * 2 + 2]);
        }
    }
    /// <summary>
    /// [left,right)の最大値
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public T Query(int left, int right)
    {
        return Query(left, right, 0, 0, N);
    }

    private T Query(int a, int b, int k, int l, int r)
    {
        if (r <= a || b <= l) return Min;
        if (a <= l && r <= b)
        {
            return Tree[k];
        }
        else
        {
            T vl = Query(a, b, k * 2 + 1, l, (l + r) / 2);
            T vr = Query(a, b, k * 2 + 2, (l + r) / 2, r);
            return Max(vl, vr);
        }
    }

    private T Max(T a, T b)
    {
        return Comparison(a, b) >= 0 ? a : b;
    }
}


class Magatro
{
    private int N, K;
    private int[] A;
    private void Scan()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        A = new int[N];
        for (int i = 0; i < N; i++)
        {
            A[i] = sc.NextInt();
        }
    }

    public void Solve()
    {
        Scan();
        if (K == 1)
        {
            Console.WriteLine(A.Min());
        }
        else if(K>=3)
        {
            Console.WriteLine(A.Max());
        }
        else
        {
            var st = new SegmentTree<int>(N, (a, b) => -a.CompareTo(b), int.MaxValue);
            for(int i = 0; i < N; i++)
            {
                st.Update(i, A[i]);
            }
            int ans = int.MinValue;
            for(int i=1;i<N;i++)
            {
                int a = st.Query(0, i);
                int b = st.Query(i, N);
                ans = Math.Max(ans, Math.Max(a, b));
            }
            Console.WriteLine(ans);
        }
    }

    static public void Main()
    {
        new Magatro().Solve();
    }
}
