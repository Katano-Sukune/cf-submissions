using System;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int[] a = sc.IntArray();


        int even = n / 2;
        int odd = n - even;
        for (int i = 0; i < n; i++)
        {
            if (a[i] != 0)
            {
                if (a[i] % 2 == 0)
                {
                    even--;
                }
                else
                {
                    odd--;
                }
            }
        }

        // 前の割ったあまり 残り奇数 残り偶数 = comp
        var dp = new int[2, odd + 1, even + 1];
        for (int i = 0; i <= odd; i++)
        {
            for (int j = 0; j <= even; j++)
            {
                dp[0, i, j] = int.MaxValue / 2;
                dp[1, i, j] = int.MaxValue / 2;
            }
        }

        dp[0, odd, even] = 0;
        dp[1, odd, even] = 0;

        for (int i = 0; i < n; i++)
        {
            var next = new int[2, odd + 1, even + 1];
            for (int j = 0; j <= odd; j++)
            {
                for (int k = 0; k <= even; k++)
                {
                    next[0, j, k] = int.MaxValue / 2;
                    next[1, j, k] = int.MaxValue / 2;
                }
            }

            if (a[i] == 0)
            {
                for (int j = 0; j <= odd; j++)
                {
                    for (int k = 0; k <= even; k++)
                    {
                        int o = dp[1, j, k];
                        int e = dp[0, j, k];

                        // 奇数
                        if (j - 1 >= 0)
                        {
                            next[1, j - 1, k] = Math.Min(next[1, j - 1, k], Math.Min(o, e + 1));
                        }

                        if (k - 1 >= 0)
                        {
                            next[0, j, k - 1] = Math.Min(next[0, j, k - 1], Math.Min(o + 1, e));
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j <= odd; j++)
                {
                    for (int k = 0; k <= even; k++)
                    {
                        int o = dp[1, j, k];
                        int e = dp[0, j, k];
                        if (a[i] % 2 == 0)
                        {
                            next[0, j, k] = Math.Min(next[0, j, k], Math.Min(o + 1, e));
                        }
                        else
                        {
                            next[1, j, k] = Math.Min(next[1, j, k], Math.Min(o, e + 1));
                        }
                    }
                }
            }

            dp = next;
        }

        Console.WriteLine(Math.Min(dp[0, 0, 0], dp[1, 0, 0]));
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