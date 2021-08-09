using System;
using System.Text;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        var sb = new StringBuilder();
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            string s = sc.Next();
            int l = s.Length;
            char[] ans = new char[l];
            bool f = true;
            for (int j = 0; j < l; j++)
            {
                char next = j + 1 < l ? s[j + 1] : '?';
                char prev = j - 1 >= 0 ? ans[j - 1] : '?';
                if (s[j] == '?')
                {
                    for (char k = 'a'; k <= 'c'; k++)
                    {
                        if (k != next && k != prev)
                        {
                            ans[j] = k;
                            break;
                        }
                    }
                }
                else
                {
                    if (s[j] != next && s[j] != prev)
                    {
                        ans[j] = s[j];
                    }
                    else
                    {
                        f = false;
                        break;
                    }
                }
            }

            if (f)
            {
                sb.AppendLine(new string(ans));
            }
            else
            {
                sb.AppendLine("-1");
            }
        }

        Console.Write(sb.ToString());
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