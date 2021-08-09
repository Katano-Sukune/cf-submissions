using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Program
{
    private int N;
    private char[] S;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        S = sc.Next().ToCharArray();

        // 全部黒
        bool[] b = S.Select(c => c == 'W').ToArray();
        List<int> black = new List<int>();
        for (int i = 0; i < N - 1; i++)
        {
            if (b[i])
            {
                b[i] = !b[i];
                b[i + 1] = !b[i + 1];
                black.Add(i + 1);
            }
        }

        List<int> white = new List<int>();
        bool[] w = S.Select(c => c == 'B').ToArray();
        for (int i = 0; i < N - 1; i++)
        {
            if (w[i])
            {
                w[i] = !w[i];
                w[i + 1] = !w[i + 1];
                white.Add(i + 1);
            }
        }

        if (b[N - 1] && w[N - 1])
        {
            Console.WriteLine("-1");
        }
        else
        {
            int bb = b[N - 1] ? int.MaxValue : black.Count;
            int ww = w[N - 1] ? int.MaxValue : white.Count;
            if (bb < ww)
            {
                Console.WriteLine(bb);
                Console.WriteLine(string.Join(" ", black));
            }
            else
            {
                Console.WriteLine(ww);
                Console.WriteLine(string.Join(" ", white));
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