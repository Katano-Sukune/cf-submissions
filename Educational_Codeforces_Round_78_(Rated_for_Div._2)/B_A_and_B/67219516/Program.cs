using System;
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
            sb.AppendLine(Query(sc.NextInt(), sc.NextInt()));
        }

        Console.Write(sb.ToString());
    }

    private string Query(long a, long b)
    {
        // i回目の操作ではiをa,bのどっちかに足す
        // 最低何回でa = bにできるか
        if (a > b)
        {
            return Query(b, a);
        }

        if (a == b)
        {
            return "0";
        }

        // a <= b
        // 1+2+3...で b-aを超える最低の探す
        // aに足す

        long diff = b - a;

        long ng = 0;
        long ok = (int) 1e9;

        while (ok - ng > 1)
        {
            long med = (ng + ok) / 2;

            long sum = (med + 1) * med / 2;
            if (sum >= diff)
            {
                ok = med;
            }
            else
            {
                ng = med;
            }
        }

        long s = (ok + 1) * ok / 2;
        for (long l = ok;; l++)
        {
            long d = (s + a) - b;
            if (d % 2 == 0)
            {
                return l.ToString();
            }

            s += l + 1;
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