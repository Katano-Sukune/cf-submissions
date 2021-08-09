using System;
using System.Collections.Generic;

public class Program
{
    private int N, M;
    private int R, C;
    private int X, Y;
    private char[][] S;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        R = sc.NextInt() - 1;
        C = sc.NextInt() - 1;
        X = sc.NextInt();
        Y = sc.NextInt();

        S = new char[N][];
        for (int i = 0; i < N; i++)
        {
            S[i] = sc.Next().ToCharArray();
        }

        /* スタート i 現在地j
         * 右移動-左移動 = j-i 
         * j-iは定数
         *
         * (右|左)移動を最小
         * 片方大きくするともう片方も大きくなる
         */
        int[][] rCnt = new int[N][];
        for (int i = 0; i < N; i++)
        {
            rCnt[i] = new int[M];
            for (int j = 0; j < M; j++)
            {
                rCnt[i][j] = int.MaxValue;
            }
        }

        // 右移動回数
        rCnt[R][C] = 0;
        // 現在地
        var yq = new LinkedList<int>();
        var xq = new LinkedList<int>();
        yq.AddLast(R);
        xq.AddLast(C);

        while (xq.Count > 0)
        {
            int xx = xq.First.Value;
            xq.RemoveFirst();
            int yy = yq.First.Value;
            yq.RemoveFirst();
            if (yy - 1 >= 0 && S[yy - 1][xx] == '.')
            {
                if (rCnt[yy][xx] < rCnt[yy - 1][xx])
                {
                    rCnt[yy - 1][xx] = rCnt[yy][xx];
                    yq.AddFirst(yy - 1);
                    xq.AddFirst(xx);
                }
            }

            if (yy + 1 < N && S[yy + 1][xx] == '.')
            {
                if (rCnt[yy][xx] < rCnt[yy + 1][xx])
                {
                    rCnt[yy + 1][xx] = rCnt[yy][xx];

                    yq.AddFirst(yy + 1);
                    xq.AddFirst(xx);
                }
            }

            if (xx - 1 >= 0 && S[yy][xx - 1] == '.')
            {
                if (rCnt[yy][xx] < rCnt[yy][xx - 1])
                {
                    int r = rCnt[yy][xx];
                    int l = r - (xx - 1) + C;
                    if (r <= Y && l <= X)
                    {
                        rCnt[yy][xx - 1] = rCnt[yy][xx];
                        yq.AddFirst(yy);
                        xq.AddFirst(xx - 1);
                    }
                }
            }

            if (xx + 1 < M && S[yy][xx + 1] == '.')
            {
                if (rCnt[yy][xx] + 1 < rCnt[yy][xx + 1])
                {
                    int r = rCnt[yy][xx];
                    int l = r - (xx + 1) + C;
                    if (r <= Y && l <= X)
                    {
                        rCnt[yy][xx + 1] = rCnt[yy][xx] + 1;

                        yq.AddLast(yy);
                        xq.AddLast(xx + 1);
                    }
                }
            }
        }

        int ans = 0;

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                if (S[i][j] == '.')
                {
                    int r = rCnt[i][j];
                    // j-c = r-l
                    int l = r - j + C;

                    if (r <= Y && l <= X)
                    {
                        ans++;
                    }
                }
            }
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