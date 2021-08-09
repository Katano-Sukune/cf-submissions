using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    private int N;
    private int[] X;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        X = sc.IntArray();

        /*
         * 全X[i] を　+1 -1 +-0 する
         * X[i] の種類 最小　最大
         */

        var hs = new HashSet<int>();
        foreach (int i in X)
        {
            hs.Add(i);
        }

        var list = hs.ToList();
        list.Sort();
        // 0 -1, 1 0, 1 +1
        var minDp = new int[list.Count, 3];
        minDp[0, 0] = 1;
        minDp[0, 1] = 1;
        minDp[0, 2] = 1;
        for (int i = 1; i < list.Count; i++)
        {
            {
                int a = 1 + minDp[i - 1, 0];
                int b = (list[i - 1] == list[i] - 1 ? 0 : 1) + minDp[i - 1, 1];
                int c = (list[i - 1] + 1 == list[i] - 1 ? 0 : 1) + minDp[i - 1, 2];
                minDp[i, 0] = Math.Min(a, Math.Min(b, c));
            }
            {
                int a = 1 + minDp[i - 1, 0];
                int b = 1 + minDp[i - 1, 1];
                int c = (list[i - 1] + 1 == list[i] ? 0 : 1) + minDp[i - 1, 2];
                minDp[i, 1] = Math.Min(a, Math.Min(b, c));
            }
            {
                int a = 1 + minDp[i - 1, 0];
                int b = 1 + minDp[i - 1, 1];
                int c = 1 + minDp[i - 1, 2];
                minDp[i, 2] = Math.Min(a, Math.Min(b, c));
            }
        }

        int min = Math.Min(minDp[list.Count - 1, 0], Math.Min(minDp[list.Count - 1, 1], minDp[list.Count - 1, 2]));

        bool[] flag = new bool[N + 2];
        int max = 0;
        var tmp = X.ToArray();
        Array.Sort(tmp);
        foreach (int i in tmp)
        {
            if (!flag[i - 1])
            {
                max++;
                flag[i - 1] = true;
            }
            else if (!flag[i])
            {
                max++;
                flag[i] = true;
            }
            else if (!flag[i + 1])
            {
                max++;
                flag[i + 1] = true;
            }
            // Console.WriteLine($"{i} {max}");
        }

        Console.WriteLine($"{min} {max}");
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