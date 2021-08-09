using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Program
{
    private int N;
    private int[] NextX = new int[] {2, 2, 1, 1, -1, -1, -2, -2};
    private int[] NextY = new int[] {1, -1, 2, -2, 2, -2, 1, -1};

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();

        char[][] ans = new char[N][];
        for (int i = 0; i < N; i++)
        {
            ans[i] = new char[N];
        }

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (ans[i][j] == default(char))
                {
                    var xq = new Queue<int>();
                    var yq = new Queue<int>();
                    ans[i][j] = 'W';
                    xq.Enqueue(j);
                    yq.Enqueue(i);
                    while (xq.Count > 0)
                    {
                        int x = xq.Dequeue();
                        int y = yq.Dequeue();

                        for (int k = 0; k < 8; k++)
                        {
                            int nx = x + NextX[k];
                            int ny = y + NextY[k];
                            if (nx < 0 || N <= nx)
                            {
                                continue;
                            }

                            if (ny < 0 || N <= ny)
                            {
                                continue;
                            }

                            if (ans[ny][nx] != default(char))
                            {
                                continue;
                            }

                            ans[ny][nx] = ans[y][x] == 'W' ? 'B' : 'W';
                            xq.Enqueue(nx);
                            yq.Enqueue(ny);
                        }
                    }
                }
            }
        }

        string a = string.Join("\n", ans.Select(s => new string(s)).ToArray());
        
        Console.WriteLine(a);

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