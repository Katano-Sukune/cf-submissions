using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CompLib.Collections.Generic;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        /*
         * 円形にn人並ぶ
         * 
         * iの高さ a_i
         * 
         * balanced circle
         * 隣り合う人の高さの差が1以下
         * 
         * 最大のbalanced circle
         */


        int n = sc.NextInt();
        int[] a = sc.IntArray();
        int maxA = 200000;
        /*
         * 最大1つ
         * 最小1つ
         * 
         * 間2以上
         * 
         * 
         */

        int[] cnt = new int[maxA + 1];
        for (int i = 0; i < n; i++)
        {
            cnt[a[i]]++;
        }

        int[] sum = new int[maxA + 2];
        for (int i = 0; i <= maxA; i++)
        {
            sum[i + 1] += sum[i] + cnt[i];
        }

        int mn = -1;
        int mx = -1;
        int size = 0;
        for (int min = 0; min <= maxA;)
        {
            if (cnt[min] > 0)
            {
                for (int max = min + 1; max <= maxA; max++)
                {
                    if (cnt[max] < 2 || max == maxA)
                    {
                        if (cnt[max] == 0)
                        {
                            int tmp = sum[max] - sum[min];

                            if (size < tmp)
                            {
                                size = tmp;
                                mn = min;
                                mx = max;
                            }
                            min = max;
                        }
                        else
                        {
                            int tmp = sum[max + 1] - sum[min];
                            if (size < tmp)
                            {
                                size = tmp;
                                mn = min;
                                mx = max + 1;
                            }
                            min = max;
                        }
                        break;
                    }
                }

                if (min == maxA)
                {
                    if (size < cnt[min])
                    {
                        size = cnt[min];
                        mn = min;
                        mx = min + 1;
                    }
                    min++;
                }
            }
            else
            {
                min++;
            }
        }

        int[] ans = new int[size];
        int left = 0;
        int right = size - 1;
        for (int i = mn; i < mx; i++)
        {
            if (i == mn || i + 1 == mx)
            {
                for (int j = 0; j < cnt[i]; j++)
                {
                    ans[left++] = i;
                }
            }
            else
            {
                for (int j = 0; j < cnt[i] - 1; j++)
                {
                    ans[left++] = i;
                }
                ans[right--] = i;
            }
        }

        Console.WriteLine(size);
        Console.WriteLine(string.Join(" ", ans));
    }

    public static void Main(string[] args) => new Program().Solve();
}


namespace CompLib.Collections.Generic
{
    using System;

    public class SegmentTree<T>
    {
        // 制約に合った2の冪
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

        public T this[int i]
        {
            set { Update(i, value); }
            get { return _array[i + N]; }
        }
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
