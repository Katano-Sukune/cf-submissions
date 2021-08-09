using System;
using System.Collections.Generic;

public class Program
{
    private int N;
    private int[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();

        // Aを L,Rに分ける　max(Lの部分列,Rの,Lの右端部分列Rの左端部分列)

        // iが右端の最長部分列
        var left = new int[N];
        int p = A[0];
        left[0] = 1;
        for (int i = 1; i < N; i++)
        {
            if (p < A[i])
            {
                left[i] = left[i - 1] + 1;
            }
            else
            {
                left[i] = 1;
            }

            p = A[i];
        }

        // iが左端の最長部分列
        var right = new int[N];
        p = A[N - 1];
        right[N - 1] = 1;
        for (int i = N - 2; i >= 0; i--)
        {
            if (A[i] < p)
            {
                right[i] = right[i + 1] + 1;
            }
            else
            {
                right[i] = 1;
            }

            p = A[i];
        }

        // [0,i]の最長
        var longL = new int[N];
        longL[0] = 1;
        int cnt = 1;
        p = A[0];
        for (int i = 1; i < N; i++)
        {
            if (p < A[i])
            {
                cnt++;
            }
            else
            {
                cnt = 1;
            }

            longL[i] = Math.Max(longL[i - 1], cnt);
            p = A[i];
        }

        var longR = new int[N];
        longR[N - 1] = 1;
        cnt = 1;
        p = A[N - 1];
        for (int i = N - 2; i >= 0; i--)
        {
            if (A[i] < p)
            {
                cnt++;
            }
            else
            {
                cnt = 1;
            }

            longR[i] = Math.Max(longR[i + 1], cnt);
            p = A[i];
        }

        long ans = longL[N - 1];
        for (int i = 0; i < N; i++)
        {
            bool aa = i > 0;
            bool bb = i < N - 1;
            long a = aa ? longL[i - 1] : 0;
            ans = Math.Max(ans, a);
            long b = bb ? longR[i + 1] : 0;
            ans = Math.Max(ans, b);
            if (aa && bb)
            {
                if (A[i - 1] >= A[i + 1])
                {
                    continue;
                }
            }

            long c = (0 < i ? left[i - 1] : 0) + (i < N - 1 ? right[i + 1] : 0);
            //Console.WriteLine($"{i} {a} {b} {c}");
            ans = Math.Max(ans, c);
        }

        Console.WriteLine(ans);
    }

    public static void Main(string[] args)
    {
        new Program().Solve();
    }
}

struct S
{
    public int L, R;

    public S(int l, int r)
    {
        L = l;
        R = r;
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