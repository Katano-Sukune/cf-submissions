using System;
using System.Collections.Generic;
using System.Text;

public class Program
{
    private int Q;
    private char[] Type;
    private int[] Id;

    public void Solve()
    {
        var sc = new Scanner();
        Q = sc.NextInt();
        Type = new char[Q];
        Id = new int[Q];
        for (int i = 0; i < Q; i++)
        {
            Type[i] = sc.Next()[0];
            Id[i] = sc.NextInt();
        }

        /*
         * L id 左端に本Id[i]置く
         * R 右
         * ? Id 本Idが右or左端に出るためにpopする本の数
         */

        // 本の数
        var sb = new StringBuilder();
        int cnt = 1;

        int minNum = 0;
        int maxNum = 0;
        // 本iに付けた番号j
        var map = new Dictionary<int, int>();
        map[Id[0]] = 0;
        for (int i = 1; i < Q; i++)
        {
            if (Type[i] == '?')
            {
                // 左何番目か
                int index = map[Id[i]] - minNum;
                int ans = Math.Min(index, cnt - index - 1);
                sb.AppendLine(ans.ToString());
            }
            else if(Type[i] == 'L')
            {
                map[Id[i]] = ++maxNum;
                cnt++;
            }
            else if (Type[i] == 'R')
            {
                map[Id[i]] = --minNum;
                cnt++;
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