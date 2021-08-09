using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Mathematics;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < n; i++)
        {
            int a = sc.NextInt();
            int b = sc.NextInt();
            int q = sc.NextInt();
            var l = new long[q];
            var r = new long[q];
            for (int j = 0; j < q; j++)
            {
                l[j] = sc.NextLong();
                r[j] = sc.NextLong();
            }
            sb.AppendLine(Q(a, b, q, l, r));
        }
        Console.Write(sb);
    }

    string Q(int a, int b, int q, long[] l, long[] r)
    {
        if (a > b)
        {
            int t = a;
            a = b;
            b = t;
        }


        long lcm = MathEx.LCM(a, b);


        long[] ll = new long[q];
        long[] rr = new long[q];
        for (int i = 0; i < q; i++)
        {
            ll[i] = l[i] - 1;
            rr[i] = r[i];
        }

        for (int i = 0; i < a; i++)
        {
            for (int t = i; t < b; t += a)
            {
                // aで割った余り i
                // bで割った余り t
                int tmp = t;
                while (tmp % a != i && tmp % b != t)
                {
                    tmp += b;
                }
                for (int j = 0; j < q; j++)
                {
                    // llから引く分
                    if (l[j] - 1 >= tmp)
                    {
                        ll[j] -= (l[j] - 1 - tmp) / lcm + 1;

                    }
                    if (r[j] >= tmp)
                    {
                        rr[j] -= (r[j] - tmp) / lcm + 1;
                    }
                }

            }
        }



        var ans = new long[q];
        for (int i = 0; i < q; i++)
        {
            ans[i] = rr[i] - ll[i];
        }
        return string.Join(" ", ans);
    }



    public static void Main(string[] args) => new Program().Solve();
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

namespace CompLib.Util
{
    using System;
    using System.Linq;

    class Scanner
    {
        private string[] _line;
        private int _index;
        private const char Separator = ' ';

        public Scanner()
        {
            _line = new string[0];
            _index = 0;
        }

        public string Next()
        {
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
                _index = 0;
            }

            return _line[_index++];
        }

        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public decimal NextDecimal() => decimal.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
