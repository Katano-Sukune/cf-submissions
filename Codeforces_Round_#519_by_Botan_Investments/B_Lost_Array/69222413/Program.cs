using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    private int N;
    private int[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();

        /*
         * 長さKのX
         * 長さN+1のA
         *
         * A[0] = 0
         *
         * A[i] = X[(i-1)%K] + A[i-1]
         * 
         */

        int[] X = new int[N];

        for (int i = 0; i < N; i++)
        {
            if (i == 0)
            {
                X[i] = A[i];
            }
            else
            {
                X[i] = A[i] - A[i - 1];
            }
        }

        var list = new List<int>();
        for (int i = 1; i <= N; i++)
        {
            // [i~N)が[0~N)でループしてるか
            bool f = true;
            for (int j = 0; j < N; j++)
            {
                int index = j % i;
                if (X[j] != X[index])
                {
                    f = false;
                    break;
                }
            }

            if (f)
            {
                list.Add(i);
            }
        }

        Console.WriteLine(list.Count);
        Console.WriteLine(string.Join(" ", list));
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