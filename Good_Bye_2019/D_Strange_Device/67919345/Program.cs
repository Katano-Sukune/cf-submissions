using System;
using System.Collections.Generic;

public class Program
{
    private int N, K;

    public void Solve()
    {
        var line = Console.ReadLine().Split(' ');
        N = int.Parse(line[0]);
        K = int.Parse(line[1]);
        if (K == 1)
        {
            Console.WriteLine($"! 1");
            return;
        }


        // 1 ~ K聞く -a

        var f = new int[K];
        for (int i = 0; i < K; i++)
        {
            f[i] = i + 1;
        }

        Console.WriteLine($"? {string.Join(" ", f)}");

        line = Console.ReadLine().Split(' ');

        int a = int.Parse(line[0]);
        int b = int.Parse(line[1]);

        // 1 ~ K からa以外引く, K+1

        // 増える 引いたやつ a未満 K+1 a強
        // 減る 引いたやつ a強 K+1 a未満


        // aより大きい奴
        int p = 0;
        int e = 0;

        // aよりちいさいやつ
        int m = 0;

        for (int i = 1; i <= K; i++)
        {
            // i 引くやつ
            if (i == a) continue;
            var list = new List<int>();
            for (int j = 1; j <= K; j++)
            {
                if (i == j) continue;
                list.Add(j);
            }

            list.Add(K + 1);

            Console.WriteLine($"? {string.Join(" ", list)}");
            line = Console.ReadLine().Split(' ');

            int c = int.Parse(line[0]);
            int d = int.Parse(line[1]);

            if (b > d) p++;
            if (b == d) e++;
            if (b < d) m++;
        }

        if (p > 0)
        {
            Console.WriteLine($"! {e + 1}");
        }
        else if (m > 0)
        {
            Console.WriteLine($"! {m + 1}");
        }
        else
        {
            // 全部変わらない 1~K a以外 ,K+1 a未満 or a強
            var list = new List<int>();
            for (int i = 1; i <= K; i++)
            {
                if (i == a) continue;
                list.Add(i);
            }
            list.Add(K+1);
            
            Console.WriteLine($"? {string.Join(" ", list)}");
            line = Console.ReadLine().Split(' ');

            int c = int.Parse(line[0]);
            int d = int.Parse(line[1]);

            
            if (b > d)
            {
                Console.WriteLine($"! {K}");    
            }
            else
            {
                Console.WriteLine($"! 1");
            }
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