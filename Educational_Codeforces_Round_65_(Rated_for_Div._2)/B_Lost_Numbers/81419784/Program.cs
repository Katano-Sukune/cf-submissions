using System;
using System.Linq;
using CompLib.Algorithm;
using CompLib.Mathematics;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        // 4,8,15,16,23,42の順列がある
        // 4回まで i,jを聞く
        // a[i] * a[j] が返る

        Console.WriteLine("? 1 2");
        int q = sc.NextInt();
        Console.WriteLine("? 2 3");
        int w = sc.NextInt();
        Console.WriteLine("? 4 5");
        int e = sc.NextInt();
        Console.WriteLine("? 5 6");
        int r = sc.NextInt();

        int[] a = new int[] { 4, 8, 15, 16, 23, 42 };

        do
        {
            if (a[0] * a[1] == q && a[1] * a[2] == w && a[3] * a[4] == e && a[4] * a[5] == r)
            {
                Console.WriteLine($"! {string.Join(" ", a)}");
            }
        }
        while (Algorithm.NextPermutation(a));
    }

    public static void Main(string[] args) => new Program().Solve();
}


namespace CompLib.Algorithm
{
    using System;
    using System.Collections.Generic;
    public static partial class Algorithm
    {
        private static void Swap<T>(ref T a, ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }

        private static void Reverse<T>(T[] array, int begin)
        {
            // [begin, array.Length)を反転
            if (array.Length - begin >= 2)
            {
                for (int i = begin, j = array.Length - 1; i < j; i++, j--)
                {
                    Swap(ref array[i], ref array[j]);
                }
            }
        }

        /// <summary>
        /// arrayを辞書順で次の順列にする 存在しないときはfalseを返す
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool NextPermutation<T>(T[] array, Comparison<T> comparison)
        {
            for (int i = array.Length - 2; i >= 0; i--)
            {
                if (comparison(array[i], array[i + 1]) < 0)
                {
                    int j = array.Length - 1;
                    for (; j > i; j--)
                    {
                        if (comparison(array[i], array[j]) < 0)
                        {
                            break;
                        }
                    }

                    Swap(ref array[i], ref array[j]);
                    Reverse(array, i + 1);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// arrayを辞書順で次の順列にする 存在しないときはfalseを返す
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool NextPermutation<T>(T[] array, Comparer<T> comparer) =>
            NextPermutation(array, comparer.Compare);

        /// <summary>
        /// arrayを辞書順で次の順列にする 存在しないときはfalseを返す
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool NextPermutation<T>(T[] array) => NextPermutation(array, Comparer<T>.Default);
    }
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
