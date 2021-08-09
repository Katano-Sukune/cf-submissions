using System;
using System.Collections.Generic;

public class Program
{
    private int N;

    public void Solve()
    {
        N = int.Parse(Console.ReadLine());

        // 全部聞く 1+2+3+4  (n+1)*n /2
        // 最初以外全部聞く 1+2+3 (n-1)*n/2
        
        // (n^2+n)+(n^2-n)/2
        // i文字の消えたやつ 最後 i番目
        if (N == 1)
        {
            Console.WriteLine("? 1 1");
            Console.WriteLine($"! {Console.ReadLine()}");
            return;
        }
        var set = new HashMap<string, int>();
        Console.WriteLine($"? {1} {N}");

        for (int i = 1; i <= N; i++)
        {
            for (int j = 0; j < i; j++)
            {
                var c = Console.ReadLine().ToCharArray();
                Array.Sort(c);
                set[new string(c)]++;
            }
        }

        Console.WriteLine($"? 2 {N}");
        for (int i = 1; i <= N - 1; i++)
        {
            for (int j = 0; j < i; j++)
            {
                var c = Console.ReadLine().ToCharArray();
                Array.Sort(c);
                set[new string(c)]--;
            }
        }


        var ls = new List<string>();
        foreach (KeyValuePair<string, int> pair in set)
        {
            if (pair.Value != 0)
            {
                ls.Add(pair.Key);
            }
        }

        var ans = new char[N];
        ls.Sort((a, b) => a.Length.CompareTo(b.Length));
        int[] cnt = new int[256];
        for (int i = 0; i < N; i++)
        {
            int[] cc = new int[256];
            foreach (var c in ls[i])
            {
                cc[c]++;
            }

            for (int j = 0; j < 256; j++)
            {
                cc[j] -= cnt[j];
            }

            char aa = '0';
            for (int j = 0; j < 256; j++)
            {
                if (cc[j] != 0)
                {
                    aa = (char) j;
                    break;
                }
            }

            cnt[aa]++;
            ans[i] = aa;
        }

        //Console.WriteLine("-----");
        //Console.WriteLine(string.Join("\n", ls));
        Console.WriteLine($"! {new string(ans)}");
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