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
            int n = sc.NextInt();
            long s = sc.NextLong();
            long[] l = new long[n];
            long[] r = new long[n];
            for (int j = 0; j < n; j++)
            {
                l[j] = sc.NextLong();
                r[j] = sc.NextLong();
            }

            sb.AppendLine(Q(n, s, l, r));
        }

        Console.Write(sb.ToString());
    }

    string Q(int n, long s, long[] l, long[] r)
    {
        /*
         * n人
         * 合計s以下
         * iには [l,r]払う
         *
         * 中央値を最大化
         */

        // ok以上払うのがn/2+1人以上か
        long ok = 0;
        long ng = long.MaxValue;

        while (ng - ok > 1)
        {
            long med = (ok + ng) / 2;
            int cnt = n / 2 + 1;
            long tmp = s;
            var list = new List<int>();
            for (int i = 0; i < n; i++)
            {
                if (r[i] < med)
                {
                    tmp -= l[i];
                }
                else if (med <= l[i])
                {
                    cnt--;
                    tmp -= l[i];
                }
                else
                {
                    list.Add(i);
                }
            }

            if (cnt <= 0)
            {
                ok = med;
                continue;
            }

            if (list.Count < cnt)
            {
                ng = med;
                continue;
            }

            list.Sort((a, b) => -l[a].CompareTo(l[b]));
            for (int i = 0; i < cnt; i++)
            {
                tmp -= med;
                if (tmp < 0)
                {
                    break;
                }
            }

            for (int i = cnt; i < list.Count; i++)
            {
                tmp -= l[list[i]];
                if (tmp < 0)
                {
                    break;
                }
            }

            if (tmp >= 0)
            {
                ok = med;
            }
            else
            {
                ng = med;
            }
        }

        return ok.ToString();
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