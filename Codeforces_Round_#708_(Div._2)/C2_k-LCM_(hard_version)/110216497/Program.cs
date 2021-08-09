using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Mathematics;

public class Program
{
    private List<int> Prime;

    public void Solve()
    {
        // Prime = new List<int>();
        // MathEx.Sieve(100000, Prime);

        var sc = new Scanner();
#if !DEBUG
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
#endif
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        int k = sc.NextInt();

        // 和n
        // lcm <= n/2


        var ans = new List<int>(k);

        if (k >= 5)
        {
            // k-2が3以上
            if (2 * (n - (k - 1)) <= n)
            {
                for (int i = 0; i < k - 1; i++)
                {
                    ans.Add(1);
                }

                ans.Add(n - (k - 1));
            }
            else
            {
                int tmp = n - (k - 2);
                int d = tmp % 4;
                for (int i = 0; i < k - 2 - d; i++)
                {
                    ans.Add(1);
                }

                for (int i = 0; i < d; i++)
                {
                    ans.Add(2);
                }

                ans.Add((tmp - d) / 2);
                ans.Add((tmp - d) / 2);
            }
        }
        else if (k == 3)
        {
            if (n % 2 == 0)
            {
                if ((n - 2) % 4 == 0)
                {
                    ans.Add(2);
                    ans.Add((n - 2) / 2);
                    ans.Add((n - 2) / 2);
                }
                else
                {
                    // 16 3
                    ans.Add(n / 4);
                    ans.Add(n / 4);
                    ans.Add(n / 2);
                }
            }
            else
            {
                ans.Add(1);
                ans.Add((n - 1) / 2);
                ans.Add((n - 1) / 2);
            }
        }
        else if (k == 4)
        {
            if (2 * (n - (k - 1)) <= n)
            {
                for (int i = 0; i < k - 1; i++)
                {
                    ans.Add(1);
                }

                ans.Add(n - (k - 1));
            }
            else
            {
                int tmp = n - (k - 2);
                int d = tmp % 4;
                if (d <= 2)
                {
                    for (int i = 0; i < 2 - d; i++)
                    {
                        ans.Add(1);
                    }

                    for (int i = 0; i < d; i++)
                    {
                        ans.Add(2);
                    }

                    ans.Add((tmp - d) / 2);
                    ans.Add((tmp - d) / 2);
                }
                else
                {
                    // n-2は4で割ったら3余る
                    // nは4で割ったら1余る
                    // 9 4
                    ans.Add(1);
                    ans.Add((n - 1) / 4);
                    ans.Add((n - 1) / 4);
                    ans.Add((n - 1) / 2);
                }
            }
        }

        Console.WriteLine(string.Join(" ", ans));
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