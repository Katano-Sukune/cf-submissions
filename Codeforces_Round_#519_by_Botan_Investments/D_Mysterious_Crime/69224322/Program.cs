using System;
using System.Collections.Generic;

public class Program
{
    private int N, M;
    private int[][] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        A = new int[M][];
        for (int i = 0; i < M; i++)
        {
            A[i] = sc.IntArray();
        }

        int[][] Index = new int[M][];
        for (int i = 0; i < M; i++)
        {
            Index[i] = new int[N + 1];
            for (int j = 0; j < N; j++)
            {
                Index[i][A[i][j]] = j;
            }
        }

        // A[0][i]に揃える
        long cnt = 0;
        for (int i = 0; i < N;)
        {
            // A[0][i] がA[i]のどこにあるか
            int c = 0;
            for (int j = 0; i + j < N; j++)
            {
                bool flag = true;
                for (int k = 0; k < M; k++)
                {
                    int index = Index[k][A[0][i]] + j;
                    if (index >= N || A[k][index] != A[0][i + j])
                    {
                        flag = false;
                        break;
                    }
                }

                if (!flag)
                {
                    break;
                }

                c++;
            }

            // Console.WriteLine($"{i} {c}");
            cnt += (long) (c + 1) * c / 2;
            i += c;
        }

        Console.WriteLine(cnt);
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