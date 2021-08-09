using System;
using System.Collections.Generic;
using System.Text;

public class Program
{
    private int N;
    private int[] L;
    private int[][] S;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        L = new int[N];
        S = new int[N][];
        for (int i = 0; i < N; i++)
        {
            L[i] = sc.NextInt();
            S[i] = new int[L[i]];
            for (int j = 0; j < L[i]; j++)
            {
                S[i][j] = sc.NextInt();
            }
        }

        List<int> min = new List<int>();
        List<int> max = new List<int>();

        long ans = 0;
        long cnt = 0;
        for (int i = 0; i < N; i++)
        {
            bool f = false;
            for (int j = 0; j < L[i] - 1; j++)
            {
                if (S[i][j] < S[i][j + 1])
                {
                    f = true;
                    break;
                }
            }

            // Console.WriteLine($"{i} {f}");
            // S[i] にもうあるならそれを使ったやつは全部
            if (f)
            {
                ans += N * 2 - 1;
                cnt++;
                continue;
            }

// Console.WriteLine(i);
            int mn = int.MaxValue;
            int mx = int.MinValue;
            for (int j = 0; j < L[i]; j++)
            {
                mn = Math.Min(mn, S[i][j]);
                mx = Math.Max(mx, S[i][j]);
            }

            max.Add(mx);
            min.Add(mn);
        }

        min.Sort();
        max.Sort();
        int index = 0;
        foreach (int i in min)
        {
            for (; index < max.Count; index++)
            {
                if (i < max[index])
                {
                    break;
                }
            }

            // Console.WriteLine($"{i} {index}");
            ans += max.Count - index;
        }

        // fがtrueのやつ a,b a+b b+aを2回ずつかぞえてる
        ans -= (cnt - 1) * cnt;

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