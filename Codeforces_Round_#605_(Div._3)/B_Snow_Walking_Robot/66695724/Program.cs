using System;
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
            sb.Append(Query(sc.Next().ToCharArray()));
        }

        Console.Write(sb.ToString());
    }

    private string Query(char[] s)
    {
        int n = s.Length;
        int u = 0;
        int d = 0;
        int l = 0;
        int r = 0;
        foreach (char c in s)
        {
            switch (c)
            {
                case 'U':
                    u++;
                    break;
                case 'D':
                    d++;
                    break;
                case 'L':
                    l++;
                    break;
                case 'R':
                    r++;
                    break;
            }
        }

        int ud = Math.Min(u, d);
        int lr = Math.Min(l, r);
        StringBuilder res = new StringBuilder();
        if (ud > 0 && lr > 0)
        {
            res.AppendLine($"{(ud + lr) * 2}");
            res.AppendLine($"{new string('U', ud)}{new string('L', lr)}{new string('D', ud)}{new string('R', lr)}");
        }
        else if (ud == 0 && lr > 0)
        {
            res.AppendLine("2");
            res.AppendLine("LR");
        }
        else if (lr == 0 && ud > 0)
        {
            res.AppendLine("2");
            res.AppendLine("UD");
        }
        else
        {
            res.AppendLine("0");
            res.AppendLine();
        }

        return res.ToString();
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