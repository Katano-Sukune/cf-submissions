using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Collections;

public class Program
{
    private int N;
    private int[] X, V;
    private (int x, int v)[] P;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        X = sc.IntArray();
        V = sc.IntArray();
        P = new (int x, int v)[N];
        for (int i = 0; i < N; i++)
        {
            P[i] = (X[i], V[i]);
        }

        Array.Sort(P, (l, r) => l.x.CompareTo(r.x));

        // 近づくやつ 0
        // 離れるやつ 最初の距離

        // iから見て、離れる
        // v[i] >= v[j]

        var tmpV = new int[N];
        Array.Copy(V, tmpV, N);
        Array.Sort(tmpV);

        var map = new Dictionary<int, int>();
        int k = 0;
        for (int i = 0; i < N; i++)
        {
            if (i == 0 || tmpV[i - 1] != tmpV[i])
            {
                map[tmpV[i]] = k++;
            }
        }

        var ftSum = new FenwickTree(k);
        var ftCnt = new FenwickTree(k);

        long ans = 0;
        foreach ((int x, int v) in P)
        {
            int mapV = map[v];
            ans -= ftSum.Sum(mapV + 1);
            ans += ftCnt.Sum(mapV + 1) * x;

            ftSum.Add(mapV, x);
            ftCnt.Add(mapV, 1);
        }

        Console.WriteLine(ans);
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

namespace CompLib.Collections
{
    using Num = Int64;

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
        public int LowerBound(Num w)
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