using System;
using System.Collections.Generic;
using System.Text;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int T = sc.NextInt();

        var sb = new StringBuilder();
        for (int i = 0; i < T; i++)
        {
            sb.AppendLine(Query(sc.NextInt(), sc.IntArray()).ToString());
        }

        Console.WriteLine(sb.ToString());
    }

    private int Query(int n, int[] a)
    {
        var index = new int[n + 1];
        for (int i = 0; i <= n; i++)
        {
            index[i] = -1;
        }

        int ans = int.MaxValue;
        for (int i = 0; i < n; i++)
        {
            if (index[a[i]] != -1)
            {
                ans = Math.Min(ans, i - index[a[i]] + 1);
            }

            index[a[i]] = i;
        }

        return ans == int.MaxValue ? -1 : ans;
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