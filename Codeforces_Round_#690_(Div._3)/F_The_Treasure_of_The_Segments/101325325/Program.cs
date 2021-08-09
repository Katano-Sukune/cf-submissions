using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using CompLib.Collections;

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
        int[] l = new int[n];
        int[] r = new int[n];
        for (int i = 0; i < n; i++)
        {
            l[i] = sc.NextInt();
            r[i] = sc.NextInt();
        }

        /*
         * 区間i [l_i, r_i]
         * 
         * k個のセグメントの集合
         * 
         */

        var ls = new List<int>(2 * n);
        for (int i = 0; i < n; i++)
        {
            ls.Add(l[i]);
            ls.Add(r[i]);
        }
        ls.Sort();
        var uq = new List<int>(2 * n);
        for (int i = 0; i < ls.Count; i++)
        {
            if (i == 0 || ls[i - 1] != ls[i]) uq.Add(ls[i]);
        }

        var map = new Dictionary<int, int>();
        for (int i = 0; i < uq.Count; i++)
        {
            map[uq[i]] = i;
        }


        var ft1 = new FenwickTree(uq.Count);
        var l2 = new List<int>[uq.Count];
        for (int i = 0; i < uq.Count; i++)
        {
            l2[i] = new List<int>();
        }
        for (int i = 0; i < n; i++)
        {
            int li = map[l[i]];
            l2[li].Add(map[r[i]]);
            ft1.Add(li, 1);
        }


        int max = 0;
        for (int i = 0; i < uq.Count; i++)
        {
            foreach (var j in l2[i])
            {
                int cnt = ft1.Sum(i, j + 1);
                max = Math.Max(max, cnt);
            }
            foreach (var j in l2[i])
            {
                ft1.Add(j, 1);
            }
        }

        Console.WriteLine(n - max);
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
