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
            sb.AppendLine(Q(sc.Next()));
        }

        Console.Write(sb.ToString());
    }

    string Q(string s)
    {
        // 偶奇で分ける
        // 一致してるやつは追い抜けない

        var oddQ = new Queue<char>();
        var evenQ = new Queue<char>();
        int n = s.Length;
        var ans = new char[n];
        foreach (char c in s)
        {
            if (c % 2 == 0)
            {
                evenQ.Enqueue(c);
            }
            else
            {
                oddQ.Enqueue(c);
            }
        }

        for (int i = 0; i < n; i++)
        {
            if (oddQ.Count == 0)
            {
                ans[i] = evenQ.Dequeue();
            }
            else if (evenQ.Count == 0)
            {
                ans[i] = oddQ.Dequeue();
            }
            else
            {
                char e = evenQ.Peek();
                char o = oddQ.Peek();
                ans[i] = e < o ? evenQ.Dequeue() : oddQ.Dequeue();
            }
        }

        return new string(ans);
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