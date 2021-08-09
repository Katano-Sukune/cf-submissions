using System;
using System.Collections.Generic;

public class Program
{
    private long X, N;

    public void Solve()
    {
        var sc = new Scanner();
        X = sc.NextLong();
        N = sc.NextLong();

        var prime = Prime(X);

        ModInt ans = 1;

        foreach (long l in prime)
        {
            long cnt = 0;
            long tmp = N;
            while (tmp > 0)
            {
                cnt += tmp / l;
                tmp /= l;
            }
            ans *= ModInt.Pow(l,cnt);
        }
        
        Console.WriteLine(ans);
    }

    private long[] Prime(long x)
    {
        List<long> result = new List<long>();
        for (long i = 2; i * i <= x; i++)
        {
            if (x % i == 0)
            {
                result.Add(i);
                while (x % i == 0)
                {
                    x /= i;
                }
            }
        }

        if (x != 1)
            result.Add(x);
        return result.ToArray();
    }

    public static void Main(string[] args)
    {
        new Program().Solve();
    }
}

#region ModInt

/// <summary>
/// [0,<see cref="Mod"/>) までの値を取るような数
/// </summary>
public struct ModInt
{
    /// <summary>
    /// 剰余を取る値．
    /// </summary>
    public const long Mod = (int) 1e9 + 7;

    /// <summary>
    /// 実際の数値．
    /// </summary>
    public long num;

    /// <summary>
    /// 値が <paramref name="n"/> であるようなインスタンスを構築します．
    /// </summary>
    /// <param name="n">インスタンスが持つ値</param>
    /// <remarks>パフォーマンスの問題上，コンストラクタ内では剰余を取りません．そのため，<paramref name="n"/> ∈ [0,<see cref="Mod"/>) を満たすような <paramref name="n"/> を渡してください．このコンストラクタは O(1) で実行されます．</remarks>
    public ModInt(long n)
    {
        num = n;
    }

    /// <summary>
    /// このインスタンスの数値を文字列に変換します．
    /// </summary>
    /// <returns>[0,<see cref="Mod"/>) の範囲内の整数を 10 進表記したもの．</returns>
    public override string ToString()
    {
        return num.ToString();
    }

    public static ModInt operator +(ModInt l, ModInt r)
    {
        l.num += r.num;
        if (l.num >= Mod) l.num -= Mod;
        return l;
    }

    public static ModInt operator -(ModInt l, ModInt r)
    {
        l.num -= r.num;
        if (l.num < 0) l.num += Mod;
        return l;
    }

    public static ModInt operator *(ModInt l, ModInt r)
    {
        return new ModInt(l.num * r.num % Mod);
    }

    public static implicit operator ModInt(long n)
    {
        n %= Mod;
        if (n < 0) n += Mod;
        return new ModInt(n);
    }

    /// <summary>
    /// 与えられた 2 つの数値からべき剰余を計算します．
    /// </summary>
    /// <param name="v">べき乗の底</param>
    /// <param name="k">べき指数</param>
    /// <returns>繰り返し二乗法により O(N log N) で実行されます．</returns>
    public static ModInt Pow(ModInt v, long k)
    {
        return Pow(v.num, k);
    }

    /// <summary>
    /// 与えられた 2 つの数値からべき剰余を計算します．
    /// </summary>
    /// <param name="v">べき乗の底</param>
    /// <param name="k">べき指数</param>
    /// <returns>繰り返し二乗法により O(N log N) で実行されます．</returns>
    public static ModInt Pow(long v, long k)
    {
        long ret = 1;
        for (k %= Mod - 1; k > 0; k >>= 1, v = v * v % Mod)
            if ((k & 1) == 1)
                ret = ret * v % Mod;
        return new ModInt(ret);
    }

    /// <summary>
    /// 与えられた数の逆元を計算します．
    /// </summary>
    /// <param name="v">逆元を取る対象となる数</param>
    /// <returns>逆元となるような値</returns>
    /// <remarks>法が素数であることを仮定して，フェルマーの小定理に従って逆元を O(log N) で計算します．</remarks>
    public static ModInt Inverse(ModInt v)
    {
        return Pow(v, Mod - 2);
    }
}

#endregion

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