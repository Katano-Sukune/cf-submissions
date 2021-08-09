using System;
using System.Collections.Generic;
using System.Text;

public class Program
{
    private int N, Q;
    private int[] R, C;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        Q = sc.NextInt();
        R = new int[Q];
        C = new int[Q];
        for (int i = 0; i < Q; i++)
        {
            R[i] = sc.NextInt() - 1;
            C[i] = sc.NextInt() - 1;
        }

        bool[] state0 = new bool[N];
        bool[] state1 = new bool[N];
        var sb = new StringBuilder();
        int cnt = 0;
        for (int i = 0; i < Q; i++)
        {
            if (R[i] == 0)
            {
                if (state0[C[i]])
                {
                    if (C[i] - 1 >= 0 && state1[C[i] - 1])
                    {
                        cnt--;
                    }

                    if (state1[C[i]])
                    {
                        cnt--;
                    }

                    if (C[i] + 1 < N && state1[C[i] + 1])
                    {
                        cnt--;
                    }

                    state0[C[i]] = false;
                }
                else
                {
                    if (C[i] - 1 >= 0 && state1[C[i] - 1])
                    {
                        cnt++;
                    }

                    if (state1[C[i]])
                    {
                        cnt++;
                    }

                    if (C[i] + 1 < N && state1[C[i] + 1])
                    {
                        cnt++;
                    }

                    state0[C[i]] = true;
                }
            }
            else
            {
                if (state1[C[i]])
                {
                    if (C[i] - 1 >= 0 && state0[C[i] - 1])
                    {
                        cnt--;
                    }

                    if (state0[C[i]])
                    {
                        cnt--;
                    }

                    if (C[i] + 1 < N && state0[C[i] + 1])
                    {
                        cnt--;
                    }

                    state1[C[i]] = false;
                }
                else
                {
                    if (C[i] - 1 >= 0 && state0[C[i] - 1])
                    {
                        cnt++;
                    }

                    if (state0[C[i]])
                    {
                        cnt++;
                    }

                    if (C[i] + 1 < N && state0[C[i] + 1])
                    {
                        cnt++;
                    }

                    state1[C[i]] = true;
                }
            }

            sb.AppendLine(cnt == 0 ? "Yes" : "No");
        }

        Console.Write(sb.ToString());
    }

    public static void Main(string[] args)
    {
        new Program().Solve();
    }
}

struct S
{
    public int L, R;

    public S(int l, int r)
    {
        L = l;
        R = r;
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