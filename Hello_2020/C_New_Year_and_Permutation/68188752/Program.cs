using System;
using System.Collections.Generic;
using System.Text;
using CompLib.Collections.Generic;

public class Program
{
    private long N;
    private long M;

    private long[] Fact;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextLong();

        Fact = new long[1000001];
        Fact[0] = 1;
        for (int i = 1; i <= 1000000; i++)
        {
            Fact[i] = Fact[i - 1] * i;
            Fact[i] %= M;
        }
// mod M
// N長 数列
// happiness p [l,r] [l,m]のmax-min = r-l となる個数

        long ans = 0;
        // r-l = 1
        ans += N * Fact[N];
        ans %= M;
        // r-l >= 2
        if (N >= 2)
        {
            for (int i = 2; i <= N; i++)
            {
                // 長さiのペア
                // ペアの個数
                // min = 1 ~ N-(i-1)
                // max = min + (i-1) .. i ~ N

                // 使える数字 i-2個

                // ペアの個数

                long tmp = 1;
                long pat = N - (i - 1);

                tmp *= pat;
                
                // ペアの位置
                long pos = N - (i - 1);
                tmp *= pos;
                tmp %= M;

                // 並べかた
                tmp *= Fact[i];
                tmp %= M;

                // 残り
                tmp *= Fact[N - i];
                tmp %= M;

                ans += tmp;
                ans %= M;
            }
        }

        Console.WriteLine(ans);
    }

    private long Pow(long a, long b)
    {
        long res = 1;
        while (b > 0)
        {
            if (b % 2 == 1)
            {
                res *= a;
                res %= M;
            }

            a *= a;
            a %= M;
            b /= 2;
        }

        return res;
    }


    public static void Main(string[] args)
    {
        new Program().Solve();
    }
}

namespace CompLib.Collections.Generic
{
    using System;

    public class SegmentTree<T>
    {
        // 制約に合った2の冪
        private const int N = 1 << 21;
        private T[] _array;

        private T _identity;
        private Func<T, T, T> _operation;

        public SegmentTree(Func<T, T, T> operation, T identity)
        {
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

        public T this[int i]
        {
            set { Update(i, value); }
            get { return _array[i + N]; }
        }
    }
}

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