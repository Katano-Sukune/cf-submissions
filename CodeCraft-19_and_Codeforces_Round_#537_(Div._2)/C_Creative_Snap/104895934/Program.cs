using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using CompLib.Algorithm;

public class Program
{
    int N, K;
    long A, B;
    int[] Ar;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        A = sc.NextLong();
        B = sc.NextLong();
        Ar = sc.IntArray();
        for (int i = 0; i < K; i++)
        {
            Ar[i]--;
        }
        Array.Sort(Ar);
        Console.WriteLine(F(0, 1 << N));
    }

    long F(int l, int r)
    {
        checked
        {
            int na = Algorithm.LowerBound(Ar, r) - Algorithm.LowerBound(Ar, l);
            if (na == 0) return A;
            else if (r - l == 1) return B * na;
            else return Math.Min(B * na * (r - l), F(l, (l + r) / 2) + F((l + r) / 2, r));
        }
    }


    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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
