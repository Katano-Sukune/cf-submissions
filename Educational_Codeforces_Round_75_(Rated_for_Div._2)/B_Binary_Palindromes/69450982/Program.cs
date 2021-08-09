using System;
using System.Collections.Generic;
using System.Linq;
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
            int n = sc.NextInt();
            var s = new string[n];
            for (int j = 0; j < n; j++)
            {
                s[j] = sc.Next();
            }

            sb.AppendLine(Q(n, s));
        }

        Console.Write(sb.ToString());
    }

    string Q(int n, string[] s)
    {
        /*
         * 0回以上 sの任意の2桁入れ替え
         * 回文にできる文字列の数
         * 
　       */

        int zero = 0;
        int one = 0;

        int odd = 0;
        foreach (string t in s)
        {
            if (t.Length % 2 == 1)
            {
                odd++;
            }

            foreach (char c in t)
            {
                if (c == '0')
                {
                    zero++;
                }
                else
                {
                    one++;
                }
            }
        }

        return C(n, one, zero, odd).ToString();
    }

    private int C(int n, int one, int zero, int odd)
    {
        if (one % 2 == 0)
        {
            if (zero % 2 == 0)
            {
                // odd 奇数 ない
                // 偶数 
                return n;
            }
            else
            {
                // odd 偶数ない
                // odd 奇数
                return n;
            }
        }
        else
        {
            if (zero % 2 == 0)
            {
                // odd 奇数 ない
                // 偶数 
                return n;
            }
            else
            {
                // odd 偶数
                if (odd == 0)
                {
                    return n - 1;
                }
                else
                {
                    return n;
                }
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