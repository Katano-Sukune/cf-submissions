using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Collections;
using CompLib.Mathematics;

public class Program
{
    private int N;
    private int[] K;
    private (long dx, long dy)[][] E;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = new int[N];
        E = new (long dx, long dy)[N][];

        List<(long dx, long dy, int i, int j)> ls = new List<(long dx, long dy, int i, int j)>();
        {
            long[][] x = new long[N][];
            long[][] y = new long[N][];
            for (int i = 0; i < N; i++)
            {
                K[i] = sc.NextInt();
                x[i] = new long[K[i]];
                y[i] = new long[K[i]];
                E[i] = new (long dx, long dy)[K[i]];
                for (int j = 0; j < K[i]; j++)
                {
                    x[i][j] = sc.NextLong();
                    y[i][j] = sc.NextLong();
                }

                for (int j = 0; j < K[i]; j++)
                {
                    long dx = x[i][(j + 1) % K[i]] - x[i][j];
                    long dy = y[i][(j + 1) % K[i]] - y[i][j];

                    long gcd = MathEx.GCD(dx, dy);
                    E[i][j] = (dx / gcd, dy / gcd);
                    ls.Add((dx / gcd, dy / gcd, i, j));
                }
            }
        }

        ls.Sort((l, r) => l.dx != r.dx ? l.dx.CompareTo(r.dx) : l.dy.CompareTo(r.dy));

        int[][] num = new int[N][];
        for (int i = 0; i < N; i++)
        {
            num[i] = new int[K[i]];
        }

        int ptr = 0;
        for (int i = 0; i < ls.Count; i++)
        {
            if (i == 0 || (ls[i - 1].dx == ls[i].dx && ls[i - 1].dy == ls[i].dy))
            {
            }
            else
            {
                ptr++;
            }

            num[ls[i].i][ls[i].j] = ptr;
        }

        int cnt = ptr + 1;

        var query = new List<(int r, int idx)>[N];
        for (int i = 0; i < N; i++)
        {
            query[i] = new List<(int r, int idx)>();
        }

        int q = sc.NextInt();
        for (int i = 0; i < q; i++)
        {
            int l = sc.NextInt() - 1;
            int r = sc.NextInt();
            query[l].Add((r, i));
        }

        int[] last = new int[cnt];
        Array.Fill(last, N);
        int[][] next = new int[N][];
        for (int i = 0; i < N; i++)
        {
            next[i] = new int[K[i]];
        }


        for (int i = N - 1; i >= 0; i--)
        {
            for (int j = 0; j < K[i]; j++)
            {
                next[i][j] = last[num[i][j]];
                last[num[i][j]] = i;
            }
        }

        FenwickTree ft = new FenwickTree(N);
        for (int i = 0; i < cnt; i++)
        {
            ft.Add(last[i], 1);
        }

        int[] ans = new int[q];

        for (int l = 0; l < N; l++)
        {
            foreach ((int r, int idx) in query[l])
            {
                ans[idx] = ft.Sum(l, r);
            }

            for (int i = 0; i < K[l]; i++)
            {
                ft.Add(next[l][i], 1);
            }
        }

        Console.WriteLine(string.Join("\n", ans));
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

namespace CompLib.Collections
{
    using Num = Int32;

    public class FenwickTree
    {
        private readonly Num[] _array;
        public readonly int Count;

        public FenwickTree(int size)
        {
            _array = new Num[size + 1];
            Count = size;
        }

        /// <summary>
        /// A[i]にnを加算
        /// </summary>
        /// <param name="i"></param>
        /// <param name="n"></param>
        public void Add(int i, Num n)
        {
            i++;
            for (; i <= Count; i += i & -i)
            {
                _array[i] += n;
            }
        }

        /// <summary>
        /// [0,r)の和を求める
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public Num Sum(int r)
        {
            Num result = 0;
            for (; r > 0; r -= r & -r)
            {
                result += _array[r];
            }

            return result;
        }

        /// <summary>
        /// [0,i)の和がw以上になるi
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        public int LowerBound(Num w)
        {
            if (w <= 0) return 0;
            int x = 0;
            int k = 1;
            while (k * 2 < Count) k *= 2;
            for (; k > 0; k /= 2)
            {
                if (x + k < Count && _array[x + k] < w)
                {
                    w -= _array[x + k];
                    x += k;
                }
            }

            return x + 1;
        }

        // [l,r)の和を求める
        public Num Sum(int l, int r) => Sum(r) - Sum(l);
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