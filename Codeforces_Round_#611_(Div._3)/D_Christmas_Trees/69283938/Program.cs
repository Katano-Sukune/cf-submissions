using System;
using System.Collections.Generic;

public class Program
{
    private int N, M;
    private int[] X;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        X = sc.IntArray();

        /*
         * i番目はX[i]で成長
         * m人y[j]にいる
         * tree 一番近い
         */

        long res = 0;
        long[] y = new long[M];
        var q = new Queue<S>();
        var hs = new HashSet<int>();

        for (int i = 0; i < N; i++)
        {
            hs.Add(X[i]);
            q.Enqueue(new S(X[i] + 1, 1));
            q.Enqueue(new S(X[i] - 1, 1));
        }

        for (int i = 0; i < M;)
        {
            var d = q.Dequeue();
            if (hs.Add(d.Y))
            {
                y[i] = d.Y;
                if (!hs.Contains(d.Y + 1))
                {
                    q.Enqueue(new S(d.Y + 1, d.Dist + 1));
                }
                if (!hs.Contains(d.Y - 1))
                {
                    q.Enqueue(new S(d.Y - 1, d.Dist + 1));
                }

                res += d.Dist;
                i++;
            }
        }

        Console.WriteLine(res);
        Console.WriteLine(string.Join(" ", y));
    }

    public static void Main(string[] args)
    {
        new Program().Solve();
    }
}

struct S
{
    public int Y;
    public int Dist;

    public S(int y, int d)
    {
        Y = y;
        Dist = d;
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