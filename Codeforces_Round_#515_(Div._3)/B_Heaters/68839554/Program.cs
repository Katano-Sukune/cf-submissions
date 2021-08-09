using System;
using System.Text;

public class Program
{
    private int N, R;
    private int[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        R = sc.NextInt();
        A = sc.IntArray();

        /*
         * a[i] = 1ならその位置にヒーターがある
         * a[i] のヒーターをつけると [i-r+1, i+r-1]が温まる
         * 最小いくつつける必要がある?
         *
         * 
         */

        // [0,i)を温めるのに必要なヒーターの数
        int[] dp = new int[N + 1];
        for (int i = 0; i <= N; i++)
        {
            dp[i] = int.MaxValue;
        }

        dp[0] = 0;
        for (int i = 0; i < N; i++)
        {
            if (A[i] == 1)
            {
                if (dp[Math.Max(0, i - R + 1)] == int.MaxValue)
                {
                    Console.WriteLine("-1");
                    return;
                }

                for (int j = Math.Max(0, i - R + 2); j <= Math.Min(N, i + R); j++)
                {
                    dp[j] = Math.Min(dp[j], dp[Math.Max(0, i - R + 1)] + 1);
                }
            }
        }

        Console.WriteLine(dp[N] == int.MaxValue ? -1 : dp[N]);
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