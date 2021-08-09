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
            sb.Append(Query(sc.NextInt(), sc.NextInt(), sc.IntArray()).ToString());
        }

        Console.Write(sb.ToString());
    }

    string Query(int n, int m, int[] a)
    {
        var sb = new StringBuilder();
        long cost = 0;
        if (n > m || n <= 2)
            return "-1\n";
        var p = new Pair[n];
        for (int i = 0; i < n; i++)
        {
            p[i] = new Pair(a[i], i);
        }

        Array.Sort(p, (aa, bb) => aa.A.CompareTo(bb.A));

        for (int i = 0; i < n; i++)
        {
            cost += p[i].A;
            cost += p[(i + 1) % n].A;
            sb.AppendLine($"{p[i].Index + 1} {p[(i + 1) % n].Index + 1}");
        }

        for (int i = 0; i < m - n; i++)
        {
            cost += p[0].A;
            cost += p[1].A;
            sb.AppendLine($"{p[0].Index + 1} {p[1].Index + 1}");
        }

        return $"{cost}\n{sb.ToString()}";
    }

    public static void Main(string[] args)
    {
        new Program().Solve();
    }
}

struct Pair
{
    public int A, Index;

    public Pair(int a, int i)
    {
        A = a;
        Index = i;
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