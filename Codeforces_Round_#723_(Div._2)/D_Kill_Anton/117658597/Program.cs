using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Reflection.Emit;
using CompLib.Algorithm;
using CompLib.Collections;
using System.Text;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
#if !DEBUG
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
#endif
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }
    string S;
    void Q(Scanner sc)
    {
        S = sc.Next();
        int n = S.Length;
        List<int>[] idx = new List<int>[4];
        for (int i = 0; i < 4; i++)
        {
            idx[i] = new List<int>();
        }
        const string t = "ANOT";
        for (int i = 0; i < n; i++)
        {
            idx[t.IndexOf(S[i])].Add(i);
        }

        int[] ar = new int[] { 0, 1, 2, 3 };
        long max = long.MinValue;
        int[] ans = new int[4];

        do
        {
            int[] a = new int[n];
            int p = 0;
            for (int j = 0; j < 4; j++)
            {
                foreach (int k in idx[ar[j]])
                {
                    a[p++] = k;
                }
            }

            long r = 0;
            var ft = new FenwickTree(n);
            for (int i = 0; i < n; i++)
            {
                r += ft.Sum(a[i] + 1, n);
                ft.Add(a[i], 1);
            }

            if (max < r)
            {
                max = r;
                ans = ar.ToArray();
            }
        } while (Algorithm.NextPermutation(ar));

        var sb = new StringBuilder();
        for (int i = 0; i < 4; i++)
        {
            sb.Append(t[ans[i]], idx[ans[i]].Count);
        }
        Console.WriteLine(sb);
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

namespace CompLib.Collections
{
    using Num = System.Int32;

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

namespace CompLib.Algorithm
{
    using System;
    using System.Collections.Generic;
    public static partial class Algorithm
    {
        private static void Swap<T>(ref T a, ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }

        private static void Reverse<T>(T[] array, int begin)
        {
            // [begin, array.Length)を反転
            if (array.Length - begin >= 2)
            {
                for (int i = begin, j = array.Length - 1; i < j; i++, j--)
                {
                    Swap(ref array[i], ref array[j]);
                }
            }
        }

        /// <summary>
        /// arrayを辞書順で次の順列にする 存在しないときはfalseを返す
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool NextPermutation<T>(T[] array, Comparison<T> comparison)
        {
            for (int i = array.Length - 2; i >= 0; i--)
            {
                if (comparison(array[i], array[i + 1]) < 0)
                {
                    int j = array.Length - 1;
                    for (; j > i; j--)
                    {
                        if (comparison(array[i], array[j]) < 0)
                        {
                            break;
                        }
                    }

                    Swap(ref array[i], ref array[j]);
                    Reverse(array, i + 1);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// arrayを辞書順で次の順列にする 存在しないときはfalseを返す
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool NextPermutation<T>(T[] array, Comparer<T> comparer) =>
            NextPermutation(array, comparer.Compare);

        /// <summary>
        /// arrayを辞書順で次の順列にする 存在しないときはfalseを返す
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool NextPermutation<T>(T[] array) => NextPermutation(array, Comparer<T>.Default);
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