using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int q = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < q; i++)
        {
            sb.AppendLine(Query(sc.NextLong(), sc.NextLong(), sc.Next().ToCharArray()));
        }

        Console.Write(sb.ToString());
    }

    private string Query(long n, long k, char[] s)
    {
        int tIndex = -1;
        for (tIndex = 0; tIndex < n; tIndex++)
            if (s[tIndex] == '1')
                break;

        if (tIndex == n)
        {
            return new string(s);
        }
        
        for (int i = tIndex + 1; i < n; i++)
        {
            if (s[i] == '0')
            {
                if (k <= i - tIndex)
                {
                    long ii = i - k;
                    s[ii] = '0';
                    s[i] = '1';
                    break;
                }
                else
                {
                    s[tIndex] = '0';
                    s[i] = '1';
                    k -= i - tIndex;
                    tIndex++;
              
                }
            }
        }
        return new string(s);
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