using System;
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
            int n = sc.NextInt();
            int[] a = sc.IntArray();

            sb.AppendLine(Query(n, a));
        }

        Console.Write(sb.ToString());
    }

    private string Query(int n, int[] a)
    {
        for (int index = 0; index < n - 1;)
        {
            int min = int.MaxValue;
            int minIndex = int.MaxValue;
            for (int i = index; i < n; i++)
            {
                if (a[i] < min)
                {
                    min = a[i];
                    minIndex = i;
                }
            }

            for (int i = minIndex; i > index; i--)
            {
                int t = a[i];
                a[i] = a[i - 1];
                a[i - 1] = t;
            }

            if (minIndex == index)
            {
                index = minIndex + 1;
            }
            else
            {
                index = minIndex;
            }
        }

        return string.Join(" ", a);
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