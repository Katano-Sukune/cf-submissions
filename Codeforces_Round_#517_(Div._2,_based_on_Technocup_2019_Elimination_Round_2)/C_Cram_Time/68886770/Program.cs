using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Program
{
    private long A, B;

    public void Solve()
    {
        var sc = new Scanner();
        A = sc.NextLong();
        B = sc.NextLong();

        // 今日a時間　明日b時間
        /*
         * k時間でkを読める
         *
         * a+bを最大化
         */

        long k;
        for (k = 0;; k++)
        {
            long a = (k + 1) * (k + 2) / 2;
            if (a > A + B)
            {
                break;
            }
        }

        bool[] f = new bool[k + 1];
        var p = new List<long>();
        var q = new List<long>();

        long sum = 0;
        for (long i = k; i >= 1; i--)
        {
            if (sum == A)
            {
                break;
            }

            if (sum + i > A)
            {
                p.Add(A - sum);
                f[A - sum] = true;
                sum += A - sum;
                break;
            }
            else
            {
                p.Add(i);
                f[i] = true;
                sum += i;
            }
        }

        for (int i = 1; i <= k; i++)
        {
            if (!f[i])
            {
                q.Add(i);
            }
        }

        p.Sort();
        q.Sort();
        Console.WriteLine(p.Count);
        if (p.Count > 0)
            Console.WriteLine(string.Join(" ", p));
        Console.WriteLine(q.Count);
        if (q.Count > 0)
            Console.WriteLine(string.Join(" ", q));
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