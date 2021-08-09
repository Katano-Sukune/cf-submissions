using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Program
{
    private int N, SX, SY;
    private int[] X, Y;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        SX = sc.NextInt();
        SY = sc.NextInt();
        X = new int[N];
        Y = new int[N];
        for (int i = 0; i < N; i++)
        {
            X[i] = sc.NextInt();
            Y[i] = sc.NextInt();
        }

        // 上
        int up = 0;
        int down = 0;
        int left = 0;
        int right = 0;

        for (int i = 0; i < N; i++)
        {
            if (X[i] < SX)
            {
                left++;
            }

            if (X[i] > SX)
            {
                right++;
            }

            if (Y[i] < SY)
            {
                up++;
            }

            if (Y[i] > SY)
            {
                down++;
            }
        }

        if (left > right && left > up && left > down)
        {
            Console.WriteLine(left);
            Console.WriteLine($"{SX - 1} {SY}");
        }
        else if (right > up && right > down)
        {
            Console.WriteLine(right);
            Console.WriteLine($"{SX + 1} {SY}");
        }
        else if (up > down)
        {
            Console.WriteLine(up);
            Console.WriteLine($"{SX} {SY - 1}");
        }
        else
        {
            Console.WriteLine(down);
            Console.WriteLine($"{SX} {SY + 1}");
        }
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