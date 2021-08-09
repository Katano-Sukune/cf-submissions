using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using CompLib.Algorithm;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
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
        int[] a = sc.IntArray();
        Array.Sort(a);

        int ans = a[n - 1];
        for (int j = n - 2; j >= 0; j--)
        {
            if (a[n - 1] % a[j] == 0) continue;
            ans = Math.Max(ans, a[n - 1] + a[j]);
            break;
        }

        bool f = true;
        for (int j = n - 2; f && j >= 1; j--)
        {
            if (a[n - 1] + a[j] + a[j - 1] <= ans) break;
            if (a[n - 1] % a[j] == 0) continue;
            for (int k = j - 1; f && k >= 0; k--)
            {
                if (a[n - 1] + a[j] + a[k] <= ans) break;
                if (a[n - 1] % a[k] == 0) continue;
                if (a[j] % a[k] == 0) continue;
                ans = Math.Max(ans, a[n - 1] + a[j] + a[k]);
                f = false;
            }
            break;
        }

        if (a[n - 1] % 30 == 0)
        {
            bool f2 = false;
            bool f3 = false;
            bool f5 = false;
            foreach (var j in a)
            {
                if (j == a[n - 1] / 2) f2 = true;
                else if (j == a[n - 1] / 3) f3 = true;
                else if (j == a[n - 1] / 5) f5 = true;
            }
            if (f2 && f3 && f5) ans = Math.Max(ans, a[n - 1] / 30 * 31);
        }

        Console.WriteLine(ans);
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
