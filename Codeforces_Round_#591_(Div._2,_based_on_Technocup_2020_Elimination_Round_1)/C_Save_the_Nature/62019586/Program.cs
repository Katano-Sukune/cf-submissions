using System;
using System.Collections.Generic;
using System.Text;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int q = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < q; i++)
        {
            int n = sc.NextInt();
            int[] p = sc.IntArray();
            int x = sc.NextInt();
            int a = sc.NextInt();
            int y = sc.NextInt();
            int b = sc.NextInt();
            long k = sc.NextLong();
            if (x < y)
            {
                int t = y;
                y = x;
                x = t;
                int tt = b;
                b = a;
                a = tt;
            }

            sb.AppendLine(Query(n, p, x, a, y, b, k).ToString());
        }

        Console.Write(sb.ToString());
    }

    private long Query(long n, int[] p, int x, long a, int y, long b, long k)
    {
        Array.Sort(p, (aa, bb) => -aa.CompareTo(bb));
        long[] sum = new long[n + 1];
        for (int i = 0; i < n; i++)
        {
            sum[i + 1] = sum[i] + p[i];
        }

        long lcm = MathEx.LCM(a, b);

        long ng = 0;
        long ok = n + 1;
        while (ok - ng > 1)
        {
            long med = (ok + ng) / 2;
            long ryoho = med / lcm;
            long aDake = med / a - ryoho;
            long bDake = med / b - ryoho;
            long s = 0;
            s += sum[ryoho] / 100 * (x + y);
            s += (sum[aDake + ryoho] - sum[ryoho]) / 100 * x;
            s += (sum[aDake + bDake + ryoho] - sum[ryoho + aDake]) / 100 * y;
            if (s >= k)
            {
                ok = med;
            }
            else
            {
                ng = med;
            }
        }

        if (ok == n + 1)
        {
            return -1;
        }

        return ok;
    }

    public static void Main(string[] args)
    {
        new Program().Solve();
    }
}

#region GCD LCM

/// <summary>
/// 様々な数学的関数の静的メソッドを提供します．
/// </summary>
public static partial class MathEx
{
    /// <summary>
    /// 2 つの整数の最大公約数を求めます．
    /// </summary>
    /// <param name="n">最初の値</param>
    /// <param name="m">2 番目の値</param>
    /// <returns>2 つの整数の最大公約数</returns>
    /// <remarks>ユークリッドの互除法に基づき最悪計算量 O(log N) で実行されます．</remarks>
    public static int GCD(int n, int m)
    {
        return (int) GCD((long) n, m);
    }


    /// <summary>
    /// 2 つの整数の最大公約数を求めます．
    /// </summary>
    /// <param name="n">最初の値</param>
    /// <param name="m">2 番目の値</param>
    /// <returns>2 つの整数の最大公約数</returns>
    /// <remarks>ユークリッドの互除法に基づき最悪計算量 O(log N) で実行されます．</remarks>
    public static long GCD(long n, long m)
    {
        n = Math.Abs(n);
        m = Math.Abs(m);
        while (n != 0)
        {
            m %= n;
            if (m == 0) return n;
            n %= m;
        }

        return m;
    }


    /// <summary>
    /// 2 つの整数の最小公倍数を求めます．
    /// </summary>
    /// <param name="n">最初の値</param>
    /// <param name="m">2 番目の値</param>
    /// <returns>2 つの整数の最小公倍数</returns>
    /// <remarks>最悪計算量 O(log N) で実行されます．</remarks>
    public static long LCM(long n, long m)
    {
        return (n / GCD(n, m)) * m;
    }
}

#endregion

#region PrimeSieve

public static partial class MathEx
{
    /// <summary>
    /// ある値までに素数表を構築します．
    /// </summary>
    /// <param name="max">最大の値</param>
    /// <param name="primes">素数のみを入れた数列が返される</param>
    /// <returns>0 から max までの素数表</returns>
    /// <remarks>エラトステネスの篩に基づき，最悪計算量 O(N loglog N) で実行されます．</remarks>
    public static bool[] Sieve(int max, List<int> primes = null)
    {
        var isPrime = new bool[max + 1];
        for (int i = 2; i < isPrime.Length; i++) isPrime[i] = true;
        for (int i = 2; i * i <= max; i++)
            if (!isPrime[i]) continue;
            else
                for (int j = i * i; j <= max; j += i)
                    isPrime[j] = false;
        if (primes != null)
            for (int i = 0; i <= max; i++)
                if (isPrime[i])
                    primes.Add(i);

        return isPrime;
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