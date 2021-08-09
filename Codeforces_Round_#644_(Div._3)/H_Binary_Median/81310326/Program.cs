using CompLib.Algorithm;
using CompLib.Util;
using System;
using System.Linq;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < t; i++)
        {
            int n = sc.NextInt();
            int m = sc.NextInt();
            long[] a = new long[n];
            for (int j = 0; j < n; j++)
            {
                string s = sc.Next();
                long tmp = 0;
                for (int k = 0; k < m; k++)
                {
                    tmp *= 2;
                    tmp += s[k] - '0';
                }
                a[j] = tmp;
            }
            Console.WriteLine(Q(n, m, a));
        }
        Console.Out.Flush();
    }
    string Q(int n, int m, long[] a)
    {
        Array.Sort(a);

        long target = ((1L << m) - n - 1) / 2;
        long ng = (1L << m);
        long ok = 0;
        // Console.WriteLine("-----");
        while (ng - ok > 1)
        {
            long mid = (ok + ng) / 2;
            // mid未満がいくつあるか

            // aのmid未満の数
            int aa = Algorithm.LowerBound(a, mid);
            //Console.WriteLine($"{mid} {aa}");
            if (mid - aa > target) ng = mid;
            else ok = mid;
        }

        return string.Format(Convert.ToString(ok, 2).PadLeft(m, '0'));
    }

    public static void Main(string[] args) => new Program().Solve();
}


namespace CompLib.Algorithm
{
    using System;
    using System.Collections.Generic;

    static class Algorithm
    {
        /// <summary>
        /// array[i] > targetを満たす最小のiを求める
        /// </summary>
        /// <param name="array"></param>
        /// <param name="target"></param>
        /// <param name="comparison"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int UpperBound<T>(T[] array, T target, Comparison<T> comparison)
        {
            int ok = array.Length;
            int ng = -1;
            while (ok - ng > 1)
            {
                int med = (ok + ng) / 2;
                if (comparison(array[med], target) > 0)
                {
                    ok = med;
                }
                else
                {
                    ng = med;
                }
            }

            return ok;
        }

        /// <summary>
        /// array[i] > targetを満たす最小のiを求める
        /// </summary>
        /// <param name="array"></param>
        /// <param name="target"></param>
        /// <param name="comparer"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int UpperBound<T>(T[] array, T target, Comparer<T> comparer) =>
            UpperBound(array, target, comparer.Compare);

        /// <summary>
        /// array[i] > targetを満たす最小のiを求める
        /// </summary>
        /// <param name="array"></param>
        /// <param name="target"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int UpperBound<T>(T[] array, T target) => UpperBound(array, target, Comparer<T>.Default);

        /// <summary>
        /// array[i] >= targetを満たす最小のiを求める
        /// </summary>
        /// <param name="array"></param>
        /// <param name="target"></param>
        /// <param name="comparison"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int LowerBound<T>(T[] array, T target, Comparison<T> comparison)
        {
            int ng = -1;
            int ok = array.Length;
            while (ok - ng > 1)
            {
                int med = (ok + ng) / 2;
                if (comparison(array[med], target) >= 0)
                {
                    ok = med;
                }
                else
                {
                    ng = med;
                }
            }

            return ok;
        }

        /// <summary>
        /// array[i] >= targetを満たす最小のiを求める
        /// </summary>
        /// <param name="array"></param>
        /// <param name="target"></param>
        /// <param name="comparer"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int LowerBound<T>(T[] array, T target, Comparer<T> comparer) =>
            LowerBound(array, target, comparer.Compare);

        /// <summary>
        /// array[i] >= targetを満たす最小のiを求める
        /// </summary>
        /// <param name="array"></param>
        /// <param name="target"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int LowerBound<T>(T[] array, T target) => LowerBound(array, target, Comparer<T>.Default);
    }
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
