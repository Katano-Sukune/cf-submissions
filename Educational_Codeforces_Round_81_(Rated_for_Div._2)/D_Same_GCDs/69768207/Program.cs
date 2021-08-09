using Complib.Generic;
using CompLib.Util;
using System;
using System.Collections.Generic;
using System.Text;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        var sb = new StringBuilder();
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Q(sc.NextLong(), sc.NextLong()));
        }
        Console.Write(sb.ToString());
    }

    private string Q(long a, long m)
    {
        long gcdAM = Algorithm.GCD(a, m);

        return (C(m + a - 1, gcdAM, m) - C(a - 1, gcdAM, m)).ToString();


    }

    // [1,n] xでgcd(x,m) = tとなるやつ
    long C(long n, long t, long m)
    {
        long tt = m / t;
        // 素因数分解
        var primes = new List<long>();
        for (long i = 2; i * i <= tt; i++)
        {
            if (tt % i == 0)
            {
                primes.Add(i);
                while (tt % i == 0)
                {
                    tt /= i;
                }
            }
        }

        if (tt != 1) primes.Add(tt);


        long res = 0;
        int l = 1 << primes.Count;
        for (int i = 0; i < l; i++)
        {
            int cnt = 0;
            long tmp = t;
            for (int j = 0; j < primes.Count; j++)
            {
                if ((i & (1 << j)) > 0)
                {
                    tmp *= primes[j];
                    cnt++;
                }
            }

            if (cnt % 2 == 0)
            {
                res += n / tmp;
            }
            else
            {
                res -= n / tmp;
            }
        }
        return res;
    }

    public static void Main() => new Program().Solve();
}


namespace Complib.Generic
{
    using System.Collections.Generic;
    class HashMap<K, V> : Dictionary<K, V>
    {
        public V this[K key]
        {
            set { base[key] = value; }
            get
            {
                V o;
                return TryGetValue(key, out o) ? o : base[key] = default(V);
            }
        }
    }
}


class Algorithm
{
    #region NextPermutation
    public static bool NextPermutation<T>(T[] array, int start, int length)
    {
        return NextPermutation(array, start, length, Comparer<T>.Default);
    }

    public static bool NextPermutation<T>(T[] array, int start, int length, IComparer<T> comparer)
    {
        return NextPermutation(array, start, length, comparer.Compare);
    }

    public static bool NextPermutation<T>(T[] array, int start, int length, Comparison<T> compare)
    {
        int end = start + length - 1;

        if (end <= start) return false;

        int last = end;
        while (true)
        {
            int pos = last--;
            if (compare(array[last], array[pos]) < 0)
            {
                int i;
                for (i = end + 1; compare(array[last], array[--i]) >= 0;) ;
                T tmp = array[last];
                array[last] = array[i];
                array[i] = tmp;
                Array.Reverse(array, pos, end - pos + 1);
                return true;
            }

            if (last == start)
            {
                Array.Reverse(array, start, end - start);
                return false;
            }
        }

        throw new Exception("NextPermutation: Fatal error");
    }

    public static bool NextPermutation<T>(T[] array, IComparer<T> comparer)
    {
        return NextPermutation(array, 0, array.Length, comparer);
    }

    public static bool NextPermutation<T>(T[] array, Comparison<T> compare)
    {
        return NextPermutation(array, 0, array.Length, compare);
    }

    public static bool NextPermutation<T>(T[] array)
    {
        return NextPermutation(array, 0, array.Length);
    }

    #endregion
    #region BitCount
    int BitCount(int n)
    {
        n = (n & 0x55555555) + (n >> 1 & 0x55555555);
        n = (n & 0x33333333) + (n >> 2 & 0x33333333);
        n = (n & 0x0f0f0f0f) + (n >> 4 & 0x0f0f0f0f);
        n = (n & 0x00ff00ff) + (n >> 8 & 0x00ff00ff);
        return (n & 0x0000ffff) + (n >> 16 & 0x0000ffff);
    }
    #endregion
    #region LowerBound
    public static int LowerBound<T>(T[] array, T target, Comparison<T> comparison)
    {
        int ok = array.Length;
        int ng = -1;
        while (ok - ng > 1)
        {
            int med = (ok + ng) / 2;
            if (comparison(array[med], target) < 0) ng = med;
            else ok = med;
        }

        return ok;
    }
    public static int LowerBound<T>(T[] array, T target, IComparer<T> comparer) =>
        LowerBound(array, target, comparer.Compare);
    public static int LowerBound<T>(T[] array, T target) => LowerBound(array, target, Comparer<T>.Default);
    #endregion
    #region UpperBound
    public static int UpperBound<T>(T[] array, T target, Comparison<T> comparison)
    {
        int ok = array.Length;
        int ng = -1;
        while (ok - ng > 1)
        {
            int med = (ok + ng) / 2;
            if (comparison(array[med], target) <= 0) ng = med;
            else ok = med;
        }

        return ok;
    }

    public static int UpperBound<T>(T[] array, T target, IComparer<T> comparer) =>
        UpperBound(array, target, comparer.Compare);

    public static int UpperBound<T>(T[] array, T target) => UpperBound(array, target, Comparer<T>.Default);
    #endregion
    #region GCD
    static public long GCD(long a, long b)
    {
        while (a % b != 0)
        {
            long t = a % b;
            a = b;
            b = t;
        }

        return b;
    }
    #endregion
    #region LCM
    static public long LCM(long a, long b)
    {
        return a * b / GCD(a, b);
    }

    #endregion
}
namespace CompLib.Util
{
    using System;
    using System.Linq;
    class Scanner
    {
        private int _index;
        private string[] _line;
        const char separator = ' ';

        public Scanner()
        {
            _index = 0;
            _line = new string[0];
        }

        public string Next()
        {
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(separator);
                _index = 0;
            }
            return _line[_index++];
        }
        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(separator);
            _index = _line.Length;
            return _line;
        }
        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
    }
}