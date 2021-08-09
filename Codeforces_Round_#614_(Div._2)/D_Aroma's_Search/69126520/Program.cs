using System;
using System.Collections.Generic;

public class Program
{
    private long X0, Y0, AX, AY, BX, BY;
    private long XS, YS, T;

    private const long Max = 20000000000000000;

    public void Solve()
    {
        var sc = new Scanner();
        X0 = sc.NextLong();
        Y0 = sc.NextLong();
        AX = sc.NextLong();
        AY = sc.NextLong();
        BX = sc.NextLong();
        BY = sc.NextLong();
        XS = sc.NextLong();
        YS = sc.NextLong();
        T = sc.NextLong();

        /*
         * 0番目 (x0,y0)
         * 以降 (ax * x_i-1 + b_x,ay * y_i-1 + b_y)
         *
         * (xs,ys)から移動 T秒で取れる最大
         */

        /*
         * ポイントは60個程度
         * 
         */

        List<long> xx = new List<long>();
        List<long> yy = new List<long>();

        bool f = false;
        while (X0 <= Max && Y0 <= Max)
        {
            xx.Add(X0);
            yy.Add(Y0);

            X0 = X0 * AX + BX;
            Y0 = Y0 * AY + BY;
        }

        int ans = 0;
        for (int i = 0; i < xx.Count; i++)
        {
            for (int j = i; j < xx.Count; j++)
            {
                for (int k = i; k >= 0; k--)
                {
                    // i => j => k
                    long time = 0;
                    time += Math.Abs(XS - xx[i]) + Math.Abs(YS - yy[i]);
                    time += Math.Abs(xx[i] - xx[j]) + Math.Abs(yy[i] - yy[j]);
                    time += Math.Abs(xx[j] - xx[k]) + Math.Abs(yy[j] - yy[k]);
                    if (time <= T)
                    {
                        ans = Math.Max(ans, j - k + 1);
                    }

                    // i => k => j
                    time = 0;
                    time += Math.Abs(XS - xx[i]) + Math.Abs(YS - yy[i]);
                    time += Math.Abs(xx[i] - xx[k]) + Math.Abs(yy[i] - yy[k]);
                    time += Math.Abs(xx[k] - xx[j]) + Math.Abs(yy[k] - yy[j]);
                    if (time <= T)
                    {
                        ans = Math.Max(ans, j - k + 1);
                    }
                }
            }
        }

        Console.WriteLine(ans);
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