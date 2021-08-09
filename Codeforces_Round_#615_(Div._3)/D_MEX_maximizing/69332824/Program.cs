using System;
using System.Collections.Generic;
using System.Text;

public class Program
{
    private int Q, X;
    private int[] Y;

    public void Solve()
    {
        var sc = new Scanner();
        Q = sc.NextInt();
        X = sc.NextInt();
        Y = new int[Q];
        for (int i = 0; i < Q; i++)
        {
            Y[i] = sc.NextInt();
        }

        /*
         * MEX 配列に無い最小の非負整数
         *
         * Y[i] を追加する
         * Xを足すか引くかする MEXを最大化
         */

        var sb = new StringBuilder();
        int[] cnt = new int[X];
        int c = 0;
        for (int i = 0; i < Q; i++)
        {
            cnt[Y[i] % X]++;
            while (cnt[c % X] > 0)
            {
                cnt[c % X]--;
                c++;
            }

            sb.AppendLine(c.ToString());
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