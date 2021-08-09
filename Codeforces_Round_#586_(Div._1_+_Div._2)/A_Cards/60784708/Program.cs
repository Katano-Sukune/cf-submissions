using System;
using System.Collections.Generic;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int N = sc.NextInt();
        string S = sc.Next();

        int[] cnt = new int[256];
        foreach (char c in S)
        {
            cnt[c]++;
        }

        for (int one = N / 3; one >= 0; one--)
        {
            int zero = (N - one * 3) / 4;
            bool o = one + zero == cnt['o'];
            bool n = one == cnt['n'];
            bool e = one + zero == cnt['e'];
            bool r = zero == cnt['r'];
            bool z = zero == cnt['z'];
            if (o && n && e && r && z)
            {
                List<string> ans = new List<string>();
                for (int i = 0; i < one; i++)
                {
                    ans.Add("1");
                }

                for (int i = 0; i < zero; i++)
                {
                    ans.Add("0");
                }
                Console.WriteLine(string.Join(" ",ans));
                return;
            }
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