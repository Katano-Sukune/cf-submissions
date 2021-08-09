using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Collections;
using CompLib.Mathematics;
using CompLib.Util;

public class Program
{
    private int N;
    private int[] K;
    private Vec[][] P;
    private int Q;
    private List<(int r, int i)>[] Query;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = new int[N];
        P = new Vec[N][];
        for (int i = 0; i < N; i++)
        {
            K[i] = sc.NextInt();
            P[i] = new Vec[K[i]];
            for (int j = 0; j < K[i]; j++)
            {
                P[i][j] = new Vec(sc.NextInt(), sc.NextInt());
            }
        }

        Q = sc.NextInt();
        Query = new List<(int r, int i)>[N];
        for (int i = 0; i < N; i++)
        {
            Query[i] = new List<(int r, int i)>();
        }

        for (int i = 0; i < Q; i++)
        {
            int l = sc.NextInt() - 1;
            int r = sc.NextInt();
            Query[l].Add((r, i));
        }

        // 多角形iのベクトル 種類
        int[][] dir;
        int e;
        {
            dir = new int[N][];
            var edges = new List<(Vec v, int i, int j)>();
            for (int i = 0; i < N; i++)
            {
                dir[i] = new int[K[i]];
                for (int j = 0; j < K[i]; j++)
                {
                    int dx = P[i][(j + 1) % K[i]].X - P[i][j].X;
                    int dy = P[i][(j + 1) % K[i]].Y - P[i][j].Y;
                    int gcd = MathEx.GCD(Math.Abs(dx), Math.Abs(dy));
                    var v = new Vec(dx / gcd, dy / gcd);
                    edges.Add((v, i, j));
                }
            }

            edges.Sort((l, r) => l.v.X != r.v.X ? l.v.X.CompareTo(r.v.X) : l.v.Y.CompareTo(r.v.Y));
            e = 0;
            for (int i = 0; i < edges.Count; i++)
            {
                if (i > 0 && (edges[i - 1].v.X != edges[i].v.X || edges[i - 1].v.Y != edges[i].v.Y)) e++;
                dir[edges[i].i][edges[i].j] = e;
            }

            e++;
        }


        // ミンコフスキー和 ベクトルの和集合

        // i,jを持つ次のやつ 
        int[][] next;
        int[] last;
        {
            next = new int[N][];
            last = new int[e];
            for (int i = 0; i < e; i++)
            {
                last[i] = N;
            }

            for (int i = N - 1; i >= 0; i--)
            {
                next[i] = new int[K[i]];
                for (int j = 0; j < K[i]; j++)
                {
                    next[i][j] = last[dir[i][j]];
                    last[dir[i][j]] = i;
                }
            }
        }

        var ft = new FenwickTree(N + 1);
        for (int i = 0; i < e; i++)
        {
            ft.Add(last[i], 1);
        }

        int[] ans = new int[Q];
        for (int left = 0; left < N; left++)
        {
            foreach (var pair in Query[left])
            {
                ans[pair.i] = ft.Sum(left, pair.r);
            }

            // ft.Add(left, -K[left]);
            for (int j = 0; j < K[left]; j++)
            {
                ft.Add(next[left][j], 1);
            }
        }

        Console.WriteLine(string.Join("\n", ans));
    }

    public static void Main(string[] args) => new Program().Solve();
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
        public int LowerBound(int w)
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

namespace CompLib.Collections.Generic
{
    using System;

    public class SegmentTree<T>
    {
        private readonly int N;
        private T[] _array;

        private T _identity;
        private Func<T, T, T> _operation;

        public SegmentTree(int size, Func<T, T, T> operation, T identity)
        {
            N = 1;
            while (N < size) N *= 2;
            _identity = identity;
            _operation = operation;
            _array = new T[N * 2];
            for (int i = 1; i < N * 2; i++)
            {
                _array[i] = _identity;
            }
        }

        /// <summary>
        /// A[i]をnに更新 O(log N)
        /// </summary>
        /// <param name="i"></param>
        /// <param name="n"></param>
        public void Update(int i, T n)
        {
            i += N;
            _array[i] = n;
            while (i > 1)
            {
                i /= 2;
                _array[i] = _operation(_array[i * 2], _array[i * 2 + 1]);
            }
        }

        private T Query(int left, int right, int k, int l, int r)
        {
            if (r <= left || right <= l)
            {
                return _identity;
            }

            if (left <= l && r <= right)
            {
                return _array[k];
            }

            return _operation(Query(left, right, k * 2, l, (l + r) / 2),
                Query(left, right, k * 2 + 1, (l + r) / 2, r));
        }

        /// <summary>
        /// A[left] op A[left+1] ... op A[right-1]を求める
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public T Query(int left, int right)
        {
            return Query(left, right, 1, 0, N);
        }

        public T Root()
        {
            return _array[1];
        }

        public T this[int i]
        {
            set { Update(i, value); }
            get { return _array[i + N]; }
        }
    }
}

struct Vec
{
    public int X, Y;

    public Vec(int x, int y)
    {
        X = x;
        Y = y;
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