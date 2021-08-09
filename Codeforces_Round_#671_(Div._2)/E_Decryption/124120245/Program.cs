using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using CompLib.Mathematics;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
#if !DEBUG
        System.Console.SetOut(new System.IO.StreamWriter(System.Console.OpenStandardOutput()) { AutoFlush = false });
#endif
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        System.Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        var div = new List<int>();
        for (int i = 2; i * i <= n; i++)
        {
            if (n % i == 0)
            {
                div.Add(i);
                if (n / i != i) div.Add(n / i);
            }
        }
        div.Add(n);
        div.Sort();

        var prime = new List<int>();
        var cp = n;
        for (int i = 2; i * i <= cp; i++)
        {
            if (cp % i == 0)
            {
                prime.Add(i);
                while (cp % i == 0) cp /= i;
            }
        }
        if (cp != 1) prime.Add(cp);


        if (prime.Count == 1)
        {
            Console.WriteLine(string.Join(" ", div));
            Console.WriteLine("0");
            return;
        }
        else if (prime.Count == 2)
        {
            if (div.Count == 3)
            {
                // 2素数の合成数
                Console.WriteLine(string.Join(" ", div));
                Console.WriteLine("1");
                return;
            }
            else
            {
                // 素数p,q
                // p, その他 q, n
                var ans = new List<int>(div.Count);
                ans.Add(prime[0]);
                foreach (int i in div)
                {
                    if (i == prime[0] || i == prime[1] || i == n) continue;
                    ans.Add(i);
                }
                ans.Add(prime[1]);
                ans.Add(n);
                Console.WriteLine(string.Join(" ", ans));
                Console.WriteLine("0");
                return;
            }
        }
        else
        {
            // p,q...r,

            // p,{pの倍数},pq,q,{qの倍数},qr,r,{rの倍数},{rp}
            bool[] used = new bool[div.Count];
            var map = new Dictionary<int, int>(div.Count);
            for (int i = 0; i < div.Count; i++) map[div[i]] = i;

            for (int i = 0; i < prime.Count; i++)
            {
                var p = prime[i];
                var q = prime[(i + 1) % prime.Count];
                used[map[p]] = true;
                used[map[p * q]] = true;
            }

            var ans = new List<int>(div.Count);
            for (int i = 0; i < prime.Count; i++)
            {
                var p = prime[i];
                ans.Add(p);
                for (int j = 0; j < div.Count; j++)
                {
                    if (div[j] % p != 0) continue;
                    if (used[j]) continue;
                    ans.Add(div[j]);
                    used[j] = true;
                }
                var q = prime[(i + 1) % prime.Count];
                ans.Add(p * q);
            }
            Console.WriteLine(string.Join(" ", ans));
            Console.WriteLine("0");
        }



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
