using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    private int N;
    private int[] A, B;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = new int[N];
        B = new int[N];
        for (int i = 0; i < N; i++)
        {
            A[i] = sc.NextInt();
            B[i] = sc.NextInt();
        }

        for (int i = 1;; i++)
        {
            
            {
                int b = 1 << i;
                // iで割った余りで分割
                List<int> a = new List<int>();
                int m = (A[0] + B[0]) % b;
                if (m < 0) m += b;
                a.Add(1);
                for (int j = 1; j < N; j++)
                {
                    int mm = (A[j] + B[j]) % b;
                    if (mm < 0) mm += b;
                    if (m == mm)
                    {
                        a.Add(j + 1);
                    }
                }

                if (a.Count != N)
                {
                    Console.WriteLine(a.Count);
                    Console.WriteLine(string.Join(" ", a));
                    // Console.WriteLine($"b {i}");
                    return;
                }
            }
            {
                int b = 1 << i;
                List<int> a = new List<int>();
                int m = (A[0]) % b;
                if (m < 0) m += b;
                a.Add(1);
                for (int j = 1; j < N; j++)
                {
                    int mm = (A[j]) % b;
                    if (mm < 0) mm += b;
                    if (m == mm)
                    {
                        a.Add(j + 1);
                    }
                }

                if (a.Count != N)
                {
                    Console.WriteLine(a.Count);
                    Console.WriteLine(string.Join(" ", a));
                    // Console.WriteLine($"a {i}");
                    return;
                }
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