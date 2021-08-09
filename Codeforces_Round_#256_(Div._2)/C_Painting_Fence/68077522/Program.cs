using System;
using System.Collections;

public class Program
{
    private int N;
    private int[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();

        // 縦にn行塗る n本消える
        // 横にn列塗る nのやつが消える
        // 縦に塗るのは最後

        Console.WriteLine(Cnt(0, N, 0));
    }

    // [l,r)塗る 下からhは塗られてる
    long Cnt(int l, int r, int h)
    {
 
        int min = int.MaxValue;
        for (int i = l; i < r; i++)
        {
            min = Math.Min(min, A[i]);
        }

        long res = min - h;
        int left = -1;
        bool f = false;
        for (int i = l; i < r; i++)
        {
            if (f)
            {
                if (A[i] <= min)
                {
                    res += Cnt(left, i, min);
                    f = false;
                }
            }
            else
            {
                if (A[i] > min)
                {
                    left = i;
                    f = true;
                }
            }
        }

        if (f)
        {
            res += Cnt(left, r, min);
        }
        // Console.WriteLine($"{l} {r} {h} {Math.Min(r - l, res)}");
        return Math.Min(r - l, res);
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