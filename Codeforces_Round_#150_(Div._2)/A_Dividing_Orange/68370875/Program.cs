using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int k = sc.NextInt();
        int[] a = sc.IntArray();

        // k人
        // n

        // k行
        // n個
        // a[i]を含む

        // 1 ~ N*K

        var ans = new List<int>[k];
        var hs = new HashSet<int>();
        for (int i = 1; i <= n * k; i++)
        {
            hs.Add(i);
        }

        for (int i = 0; i < k; i++)
        {
            ans[i] = new List<int>();
            hs.Remove(a[i]);
            ans[i].Add(a[i]);
        }

        var ar = hs.ToArray();
        for (int i = 0; i < k; i++)
        {
            for (int j = 0; j < n - 1; j++)
            {
                ans[i].Add(ar[i * (n - 1) + j]);
            }
        }

        var sb = new StringBuilder();
        for (int i = 0; i < k; i++)
        {
            sb.AppendLine(string.Join(" ", ans[i]));
        }

        Console.Write(sb.ToString());
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