using System;
using System.Collections.Generic;

public class Program
{
    private int N;
    private int[] P, C;
    private int[] A;
    private List<int>[] Child;
    private int Root;

    private int[,] Comp;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        P = new int[N];
        C = new int[N];
        for (int i = 0; i < N; i++)
        {
            P[i] = sc.NextInt() - 1;
            if (P[i] == -1)
            {
                Root = i;
            }

            C[i] = sc.NextInt();
        }

        A = new int[N];
        Comp = new int[N, N];
        Child = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            Child[i] = new List<int>();
        }

        for (int i = 0; i < N; i++)
        {
            if (i == Root) continue;
            Child[P[i]].Add(i);
        }
        // 根付き木
        /*
         * a[i] 数字
         * c[i] iを頂点とした部分木 a[j] < a[i] のノードの数
         * p[i] iの親
         */

        // aを復元

        /*
         * 葉 なに入れても0
         * 
         */
        int[] ans = new int[N];
        var ls = new List<int>();
        if (Search(Root, ls))
        {
            for (int i = 0; i < N; i++)
            {
                ans[ls[i]] = i + 1;
            }

            Console.WriteLine("YES");
            Console.WriteLine(string.Join(" ", ans));
        }
        else
        {
            Console.WriteLine("NO");
        }
    }

    private bool Search(int n, List<int> ls)
    {
        foreach (int i in Child[n])
        {
            var l = new List<int>();
            //Console.WriteLine($"{i}:{string.Join(" ",l)}");
            if (!Search(i, l))
            {
                return false;
            }

            foreach (int i1 in l)
            {
                ls.Add(i1);
            }
        }

        //Console.WriteLine(n);
        //Console.WriteLine(string.Join(" ", ls));
        if (ls.Count < C[n])
        {
            return false;
        }

        ls.Insert(C[n], n);
        return true;
    }

    public static void Main(string[] args)
    {
        new Program().Solve();
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