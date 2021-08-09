using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Algorithm;

public class Program
{
    private int N, M, K, Q;
    private int[] R, C;
    private int[] B;

    public void Solve()
    {
        checked
        {
            var sc = new Scanner();
            N = sc.NextInt();
            M = sc.NextInt();
            K = sc.NextInt();
            Q = sc.NextInt();

            R = new int[K];
            C = new int[K];
            for (int i = 0; i < K; i++)
            {
                R[i] = sc.NextInt() - 1;
                C[i] = sc.NextInt() - 1;
            }

            B = new int[Q];
            for (int i = 0; i < Q; i++)
            {
                B[i] = sc.NextInt() - 1;
            }

            Array.Sort(B);

            // H行目で終了
            int h = -1;

            // i行目にある 左端、右端
            int[] left = new int[N];
            int[] right = new int[N];
            for (int i = 0; i < N; i++)
            {
                left[i] = int.MaxValue;
                right[i] = int.MinValue;
            }

            for (int i = 0; i < K; i++)
            {
                h = Math.Max(h, R[i]);
                left[R[i]] = Math.Min(left[R[i]], C[i]);
                right[R[i]] = Math.Max(right[R[i]], C[i]);
            }

            // col列目にいる、横移動回数
            var cur = new List<(int col, long cost)>();
            cur.Add((0, 0));
            for (int i = 0; i < h; i++)
            {
                // この列に無い
                var next = new List<(int col, long cost)>();
                if (left[i] == int.MaxValue)
                {
                    if (i == 0)
                    {
                        next.Add((B[0], B[0]));
                        if (B[0] == 0 && Q >= 2)
                        {
                            next.Add((B[1], B[1]));
                        }

                        cur = next;
                    }

                    continue;
                }


                // 右端 -> 左端 -> 左のb
                int lr = Algorithm.LowerBound(B, left[i]);
                int ll = lr - 1;

                if (0 <= ll && ll < Q)
                {
                    long min = long.MaxValue;
                    foreach ((int col, long cost) in cur)
                    {
                        long c = cost + Math.Abs(col - right[i]) + (right[i] - left[i]) + (left[i] - B[ll]);
                        min = Math.Min(min, c);
                    }

                    next.Add((B[ll], min));
                }

                if (0 <= lr && lr < Q)
                {
                    long min = long.MaxValue;
                    foreach ((int col, long cost) in cur)
                    {
                        long c = cost + Math.Abs(col - right[i]) + (right[i] - left[i]) + (B[lr] - left[i]);
                        min = Math.Min(min, c);
                    }

                    next.Add((B[lr], min));

                    if (B[lr] == left[i])
                    {
                        int lrr = lr + 1;
                        if (0 <= lrr && lrr < Q)
                        {
                            long min2 = long.MaxValue;
                            foreach ((int col, long cost) in cur)
                            {
                                long c = cost + Math.Abs(col - right[i]) + (right[i] - left[i]) + (B[lrr] - left[i]);
                                min2 = Math.Min(min2, c);
                            }

                            next.Add((B[lrr], min2));
                        }
                    }
                }


                int rr = Algorithm.LowerBound(B, right[i]);
                int rl = rr - 1;

                if (0 <= rl && rl < Q)
                {
                    long min = long.MaxValue;
                    foreach ((int col, long cost) in cur)
                    {
                        long c = cost + Math.Abs(col - left[i]) + (right[i] - left[i]) + (right[i] - B[rl]);
                        min = Math.Min(min, c);
                    }

                    next.Add((B[rl], min));
                }

                if (0 <= rr && rr < Q)
                {
                    long min = long.MaxValue;
                    foreach ((int col, long cost) in cur)
                    {
                        long c = cost + Math.Abs(col - left[i]) + (right[i] - left[i]) + (B[rr] - right[i]);
                        min = Math.Min(min, c);
                    }

                    next.Add((B[rr], min));

                    if (B[rr] == right[i])
                    {
                        int rrr = rr + 1;
                        if (0 <= rrr && rrr < Q)
                        {
                            long min2 = long.MaxValue;
                            foreach ((int col, long cost) in cur)
                            {
                                long c = cost + Math.Abs(col - left[i]) + (right[i] - left[i]) + (B[rrr] - right[i]);
                                min2 = Math.Min(min2, c);
                            }

                            next.Add((B[rrr], min2));
                        }
                    }
                }


                cur = next;
            }

            long t = long.MaxValue;
            foreach ((int col, long cost) in cur)
            {
                long l = cost + Math.Abs(col - left[h]) + (right[h] - left[h]);
                long r = cost + Math.Abs(col - right[h]) + (right[h] - left[h]);
                t = Math.Min(t, Math.Min(l, r));
            }

            Console.WriteLine(t + h);
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