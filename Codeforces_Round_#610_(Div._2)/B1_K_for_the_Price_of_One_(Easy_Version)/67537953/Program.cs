using System;
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
            sb.AppendLine(Query(sc.NextInt(), sc.NextInt(), sc.NextInt(), sc.IntArray()));
        }

        Console.Write(sb.ToString());
    }

    private string Query(int n, int p, int k, int[] a)
    {
        // n個商品がある
        // 所持金p
        // 1個買う a[i] はらう
        // a[i] 以下 k個買う a[i] 払う
        // 商品個数最大
        Array.Sort(a);

        long odd = 0;
        long even = 0;
        int ans = 0;
        for (int i = 0; i < n; i++)
        {
            if (i % 2 == 0)
            {
                even += a[i];
                if (even <= p)
                {
                    ans = Math.Max(ans, i + 1);
                }
            }
            else
            {
                odd += a[i];
                if (odd <= p)
                {
                    ans = Math.Max(ans, i + 1);
                }
            }
        }

        return ans.ToString();
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