using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Mathematics;

public class Program
{
    private int N, M;
    private int[][] A;
    private const int Max = (int) 1e6;

    private bool[] F;
    private int[] Mul;

    private int[] T =
    {
        0, 13439, 106064, 189279, 106064, 330095, 388944, 106064, 106064, 189279, 560720, 486464, 388944, 263744,
        106064, 670095, 106064
    };

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        A = new int[N][];
        for (int i = 0; i < N; i++)
        {
            A[i] = sc.IntArray();
        }

        int lcm = 720720;
#if !DEBUG
System.Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
#endif
        for (int i = 0; i < N; i++)
        {
            var ln = new int[M];
            for (int j = 0; j < M; j++)
            {
                if ((i + j) % 2 == 0) ln[j] = lcm;
                else ln[j] = T[A[i][j]];
            }

            Console.WriteLine(string.Join(" ", ln));
        }

        Console.Out.Flush();

        // F = new bool[Max];
        // var ls = new List<int>();
        // for (int k = 1; k * k * k * k <= Max; k++)
        // {
        //     F[k * k * k * k] = true;
        //     ls.Add(k * k * k * k);
        // }
        //
        // int lcm = 720720;
        //
        // var min = new int[17];
        // for (int i = 1; i <= 16; i++)
        // {
        //     foreach (int j in ls)
        //     {
        //         if (lcm + j <= Max && (lcm + j) % i == 0) min[i] = lcm + j;
        //         if (lcm - j >= 1 && (lcm - j) % i == 0) min[i] = lcm - j;
        //     }
        // }
        //
        // Console.WriteLine(string.Join(",", min));
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
            if (_index >= _line.Length)
            {
                string s;
                do
                {
                    s = Console.ReadLine();
                } while (s.Length == 0);

                _line = s.Split(Separator);
                _index = 0;
            }

            return _line[_index++];
        }

        public string ReadLine()
        {
            _index = _line.Length;
            return Console.ReadLine();
        }

        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public decimal NextDecimal() => decimal.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            string s = Console.ReadLine();
            _line = s.Length == 0 ? new string[0] : s.Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}