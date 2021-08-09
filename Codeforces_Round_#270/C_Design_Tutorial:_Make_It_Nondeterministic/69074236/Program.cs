using System;

public class Program
{
    private int N;
    private string[] F, L;
    private int[] P;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        F = new string[N];
        L = new string[N];
        for (int i = 0; i < N; i++)
        {
            F[i] = sc.Next();
            L[i] = sc.Next();
        }

        P = new int[N];
        for (int i = 0; i < N; i++)
        {
            P[i] = sc.NextInt() - 1;
        }

        bool f = true;
        bool l = true;

        for (int i = 1; i < N; i++)
        {
            bool ff = false;
            bool ll = false;
            if (f)
            {
                if (F[P[i - 1]].CompareTo(F[P[i]]) <= 0)
                {
                    ff = true;
                }

                if (F[P[i - 1]].CompareTo(L[P[i]]) <= 0)
                {
                    ll = true;
                }
            }

            if (l)
            {
                if (L[P[i - 1]].CompareTo(F[P[i]]) <= 0)
                {
                    ff = true;
                }

                if (L[P[i - 1]].CompareTo(L[P[i]]) <= 0)
                {
                    ll = true;
                }
            }

            f = ff;
            l = ll;
        }

        Console.WriteLine(f || l ? "YES" : "NO");
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