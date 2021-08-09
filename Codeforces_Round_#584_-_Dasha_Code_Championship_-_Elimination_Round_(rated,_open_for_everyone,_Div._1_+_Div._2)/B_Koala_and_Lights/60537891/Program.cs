using System;
using System.Linq;

public class Program
{
    private int N;
    private bool[] On;

    private int[] A, B;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        On = sc.Next().Select(c => c == '1').ToArray();

        A = new int[N];
        B = new int[N];

        for (int i = 0; i < N; i++)
        {
            A[i] = sc.NextInt();
            B[i] = sc.NextInt();
        }

        int ans = -1;
        for (int i = 0; i < 1000; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (i - B[j] >= 0 && (i - B[j]) % A[j] == 0)
                {
                    On[j] = !On[j];
                }
            }

            ans = Math.Max(ans, On.Count(b => b));
        }
        
        Console.WriteLine(ans);
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