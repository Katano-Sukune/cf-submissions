using System;
using System.Collections.Generic;
using System.Text;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Query(sc.NextInt(), sc.IntArray()));
        }

        Console.Write(sb.ToString());
    }

    private string Query(int n, int[] a)
    {
        // aの真ん中に降りる
        // 左側 右側に連続したジャムを食べる
        // 残っている瓶は2種類とも同じ数になってる
        // 食べたジャムの数 最小

        int diff = 0;

        foreach (int i in a)
        {
            if (i == 1)
            {
                diff++;
            }
            else
            {
                diff--;
            }
        }

        var map = new Dictionary<int, int>();

        int cnt = 0;
        map[0] = 0;
        int o;
        for (int i = 1; i <= n; i++)
        {
            if (a[n - i] == 1)
            {
                cnt++;
            }
            else
            {
                cnt--;
            }


            if (!map.TryGetValue(cnt, out o))
            {
                map[cnt] = i;
            }
        }

        int ans = int.MaxValue;

        cnt = 0;
        if (map.TryGetValue(diff - cnt, out o))
        {
            ans = Math.Min(ans, o);
        }

        for (int i = 1; i <= n; i++)
        {
            if (a[n + i - 1] == 1)
            {
                cnt++;
            }
            else
            {
                cnt--;
            }

            if (map.TryGetValue(diff - cnt, out o))
            {
                ans = Math.Min(ans, o + i);
            }
        }

        return ans.ToString();
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