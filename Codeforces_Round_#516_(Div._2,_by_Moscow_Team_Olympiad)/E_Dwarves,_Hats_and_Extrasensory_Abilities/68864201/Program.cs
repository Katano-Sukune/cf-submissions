using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

public class Program
{
    public void Solve()
    {
        int N = int.Parse(Console.ReadLine());

        long white = 2;
        long black = (long) 2e9;

        for (int i = 0; i < N; i++)
        {
            long med = (white + black) / 2;

            if (med % 2 == 0)
            {
                Console.WriteLine($"{med / 2} {med / 2 }");
            }
            else
            {
                Console.WriteLine($"{med / 2 + 1} {med / 2}");
            }

            //var ans = "black";
            //var ans = "white";
            var ans = Console.ReadLine();
            if (ans == "black")
            {
                black = med;
            }
            else if (ans == "white")
            {
                white = med;
            }
        }

        if (white % 2 == 0)
        {
            Console.WriteLine($"{white / 2} {white / 2+1} {white / 2 + 1} {white / 2 - 1}");
        }
        else
        {
            Console.WriteLine($"{white / 2} {white / 2 + 1} {white / 2 + 2} {white / 2 }");
        }
    }

    public static void Main(string[] args)
    {
        new Program().Solve();
    }
}

struct S
{
    public int Y, X;

    public S(int y, int x)
    {
        Y = y;
        X = x;
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