using System;
using System.Linq;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using CompLib.Mathematics;
using System.Runtime.Serialization.Json;

public class Program
{
    int N;
    public void Solve()
    {

        // N要素配列A

        // ソートされると等差数列

        // ? i A_iを返す
        // > x xより大きい要素があるか?

        N = int.Parse(Console.ReadLine());

        if (N <= 60)
        {
            var a = new long[N];
            for (int i = 0; i < N; i++)
            {
                a[i] = Q(i + 1);
            }
            Array.Sort(a);
            Console.WriteLine($"! {a[0]} {a[1] - a[0]}");
            return;
        }


        var rnd = new Random();

        var ls = new List<long>();

        long tMax = 0;
        long tMin = long.MaxValue;
        for (int i = 0; i < 40; i++)
        {
            var idx = rnd.Next(N) + 1;
            long ai = Q(idx);
            tMax = Math.Max(tMax, ai);
            tMin = Math.Min(tMin, ai);
            ls.Add(ai);
        }

        long gcd = 0;
        foreach (var i in ls)
        {
            foreach (var j in ls)
            {
                gcd = MathEx.GCD(gcd, Math.Abs(i - j));
            }
        }

        long ok = Math.Min(N - (tMax - tMin) / gcd - 1, (1000000000 - tMax) / gcd);
        long ng = -1;
        while (ok - ng > 1)
        {
            long mid = (ok + ng) / 2;
            if (G(tMax + mid * gcd)) ng = mid;
            else ok = mid;
        }

        // 全部ok以下
        long max = tMax + ok * gcd;


        Console.WriteLine($"! {max - gcd * (N - 1)} {gcd}");
    }

    long Q(int i)
    {
        Console.WriteLine($"? {i}");
        return int.Parse(Console.ReadLine());
    }

    bool G(long x)
    {
        Console.WriteLine($"> {x}");
        return Console.ReadLine() == "1";
    }


    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

// https://bitbucket.org/camypaper/complib
namespace CompLib.Mathematics
{
    using System;
    using System.Collections.Generic;
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
        public static int GCD(int n, int m) { return (int)GCD((long)n, m); }


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
        public static long LCM(long n, long m) { return (n / GCD(n, m)) * m; }
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
                else for (int j = i * i; j <= max; j += i) isPrime[j] = false;
            if (primes != null) for (int i = 0; i <= max; i++) if (isPrime[i]) primes.Add(i);

            return isPrime;
        }
    }
    #endregion
}
