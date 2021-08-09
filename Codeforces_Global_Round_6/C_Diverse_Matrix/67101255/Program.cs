using System;
using System.Text;

public class Program
{
    private int R, C;

    public void Solve()
    {
        var sc = new Scanner();
        R = sc.NextInt();
        C = sc.NextInt();

        if (R == 1 && C == 1)
        {
            Console.WriteLine("0");
            return;
        }

        var ans = new long[R][];
        if (C == 1)
        {
            for (int i = 0; i < R; i++)
            {
                ans[i] = new long[C];
                ans[i][0] = i + 2;
            }
        }
        else
        {
            // 0行目 2....C+1 GCD =1
            // 1    2*(C+2)... (C+1)(C+2) GCD = (C+2)
            // 2    
            for (int i = 0; i < R; i++)
            {
                ans[i] = new long[C];
                if (i == 0)
                {
                    for (int j = 0; j < C; j++)
                    {
                        ans[i][j] = j + 2;
                    }
                }
                else
                {
                    for (int j = 0; j < C; j++)
                    {
                        ans[i][j] = (j + 2) * (C + i + 1);
                    }
                }
            }
        }

        var sb = new StringBuilder();
        for (int i = 0; i < R; i++)
        {
            sb.AppendLine(string.Join(" ", ans[i]));
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