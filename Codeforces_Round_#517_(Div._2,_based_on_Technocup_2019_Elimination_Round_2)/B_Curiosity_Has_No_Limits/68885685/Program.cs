using System;
using System.Text;

public class Program
{
    private int N;
    private int[] A, B;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();
        B = sc.IntArray();

        var t = new int[4][];
        for (int i = 0; i < 4; i++)
        {
            t[i] = new int[N];
            for (int j = 0; j < N; j++)
            {
                t[i][j] = -1;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            t[i][0] = i;
        }

        for (int i = 1; i < N; i++)
        {
            // index
            for (int j = 0; j < 4; j++)
            {
                // 最初
                int b = t[j][i - 1];
                if(b == -1) continue;
                for (int c = 0; c < 4; c++)
                {
                    // 現在
                    if ((b | c) == A[i - 1] && (b & c) == B[i - 1])
                    {
                        t[j][i] = c;
                        break;
                    }
                }
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (t[i][N - 1] != -1)
            {
                Console.WriteLine("YES");
                Console.WriteLine(string.Join(" ",t[i]));
                return;
            }
        }
        Console.WriteLine("NO");
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