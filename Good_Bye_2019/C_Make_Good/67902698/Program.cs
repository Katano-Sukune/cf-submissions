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
            sb.AppendLine(Q(sc.NextInt(), sc.LongArray()));
        }

        Console.Write(sb);
    }

    string Q(int N, long[] A)
    {
        long xor = 0;
        long sum = 0;
        foreach (long l in A)
        {
            sum += l;
            xor ^= l;
        }

        // 3つまで数字足して sum = 2*xor

        if (sum == 2 * xor)
        {
            return "0\n";
        }

        // sum = long.max -1
        // xor = (long.max -1)/2

        long sumDif = (1L << 59) - 4 - sum;

        // sum 偶数 dif 偶数
        // 奇数 奇数
        // sumDif sumのbit反転 

        long xorDif = (((1L << 59) - 4) / 2) ^ xor;

        // sum 偶数 dif 偶数
        // 奇数 奇数

        // xorDif xorのbit反転

        // 足してsumDif xor xorDifの3つ以下

        // a = xorDif b = c = (sumDif - xorDif)/2

        return $"3\n{xorDif} {(sumDif - xorDif) / 2} {(sumDif - xorDif) / 2}";
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