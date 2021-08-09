using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Program
{
    private int N;
    private int[] F;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        F = sc.IntArray();

        var index = new List<int>();
        var val = new List<int>();
        var flag = new bool[N + 1];

        for (int i = 0; i < N; i++)
        {
            flag[F[i]] = true;
            if (F[i] == 0)
            {
                index.Add(i);
            }
        }

        for (int i = 1; i <= N; i++)
        {
            if (!flag[i])
            {
                val.Add(i);
            }
        }

        // indexとvalが一致
        var o = new List<int>();

        for (int i = 0; i < index.Count; i++)
        {
            if (index[i] + 1 == val[i])
            {
                o.Add(i);
            }
        }

        var ans = new int[N];
        for (int i = 0; i < N; i++)
        {
            if (F[i] != 0)
            {
                ans[i] = F[i];
            }
        }

        if (o.Count == 1)
        {
            int t = val[o[0]];
            val[o[0]] = val[(o[0] + 1) % val.Count];
            val[(o[0] + 1) % val.Count] = t;


            for (int i = 0; i < index.Count; i++)
            {
                ans[index[i]] = val[i];
            }
        }
        else
        {
            var p = new List<int>();
            for (int i = 0; i < index.Count; i++)
            {
                if (index[i] + 1 == val[i])
                {
                    p.Add(i);
                }
                else
                {
                    ans[index[i]] = val[i];
                }
            }

            if (p.Count % 2 == 0)
            {
                for (int i = 0; i < p.Count; i += 2)
                {
                    ans[index[p[i]]] = val[p[i + 1]];
                    ans[index[p[i + 1]]] = val[p[i]];
                }
            }
            else
            {
                for (int i = 0; i < (p.Count - 3); i += 2)
                {
                    ans[index[p[i]]] = val[p[i + 1]];
                    ans[index[p[i + 1]]] = val[p[i]];
                }

                ans[index[p[p.Count - 3]]] = val[p[p.Count - 2]];
                ans[index[p[p.Count - 2]]] = val[p[p.Count - 1]];
                ans[index[p[p.Count - 1]]] = val[p[p.Count - 3]];
            }
        }

        Console.WriteLine(string.Join(" ", ans));
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