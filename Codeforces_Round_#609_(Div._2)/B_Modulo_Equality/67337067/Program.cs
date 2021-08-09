using System;
using System.Collections.Generic;
using System.Text;

public class Program
{
    private int N, M;
    private int[] A, B;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        A = sc.IntArray();
        B = sc.IntArray();

        var bMap = new HashMap<int, int>();
        foreach (int i in B)
        {
            bMap[i]++;
        }

        int ans = int.MaxValue;
        for (int i = 0; i < N; i++)
        {
            // A[0]をB[i]にする
            int x = B[i] - A[0];
            if (x < 0) x += M;
            
            var aMap = new HashMap<int,int>();
            foreach (int j in A)
            {
                aMap[(j + x) % M]++;
            }

            bool f = true;

            foreach (KeyValuePair<int,int> pair in aMap)
            {
                if (pair.Value != bMap[pair.Key])
                {
                    f = false;
                    break;
                }
            }

            if (f)
            {
                ans = Math.Min(ans, x);
            }
        }
        Console.Write(ans);
    }

    public static void Main(string[] args)
    {
        new Program().Solve();
    }
}

class HashMap<K, V> : Dictionary<K, V>
{
    new public V this[K i]
    {
        get
        {
            V v;
            return TryGetValue(i, out v) ? v : base[i] = default(V);
        }
        set { base[i] = value; }
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