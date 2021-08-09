using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using CompLib.Algorithm;

public class Program
{
    int N, M;
    int[][] A;
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

        var rr = new int[N][];
        for (int i = 0; i < N; i++)
        {
            var ls = new List<int>();
            var tmp = A[i].ToArray();
            Array.Sort(tmp);
            for (int j = 0; j < M; j++)
            {
                if (j == 0 || tmp[j - 1] != tmp[j]) ls.Add(tmp[j]);
            }
            rr[i] = ls.ToArray();
        }

        var cc = new int[M][];
        for (int j = 0; j < M; j++)
        {
            var ls = new List<int>();
            var tmp = new int[N];
            for (int i = 0; i < N; i++)
            {
                tmp[i] = A[i][j];
            }
            Array.Sort(tmp);
            for (int i = 0; i < N; i++)
            {
                if (i == 0 || tmp[i - 1] != tmp[i]) ls.Add(tmp[i]);
            }
            cc[j] = ls.ToArray();
        }

        for (int i = 0; i < N; i++)
        {
            var ln = new int[M];
            for (int j = 0; j < M; j++)
            {
                // i列 i,j
                int[] cnt = new int[4];
                cnt[0] = Algorithm.LowerBound(rr[i], A[i][j]);
                cnt[1] = rr[i].Length - 1 - cnt[0];

                cnt[2] = Algorithm.LowerBound(cc[j], A[i][j]);
                cnt[3] = cc[j].Length - 1 - cnt[2];
                ln[j] = Math.Max(cnt[3], cnt[1]) + 1 + Math.Max(cnt[2], cnt[0]);
            }

            Console.WriteLine(string.Join(" ", ln));
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
