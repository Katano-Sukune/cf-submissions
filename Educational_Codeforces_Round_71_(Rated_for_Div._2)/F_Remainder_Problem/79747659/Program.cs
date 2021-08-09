using CompLib.Util;
using System;


public class Program
{
    const int N = 500000;
    const int K = 710;
    public void Solve()
    {
        var sc = new Scanner();
        // 0 fill配列がある
        // 1 x y a_x += y
        // 2 x y xで割った余りがyな要素の和
        int q = sc.NextInt();
        // N = 5*10^5
        // 普通に実装 O(N/x)
        // xが大きい時はナイーブな実装しても高速

        // x小さい時 基準 x <= K

        // クエリの種類 O(K^2)種類
        // なので K = sqrt(N)

        // 全体で O(q*K)

        var tmp = new long[K + 1][];
        for (int i = 1; i <= K; i++)
        {
            tmp[i] = new long[i];
        }
        var array = new long[N + 1];

        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < q; i++)
        {
            int t = sc.NextInt();
            int x = sc.NextInt();
            int y = sc.NextInt();
            if (t == 1)
            {
                array[x] += y;
                for (int j = 1; j <= K; j++)
                {
                    tmp[j][x % j] += y;
                }
            }
            else // t == 2
            {
                if (x <= K)
                {
                    Console.WriteLine(tmp[x][y]);
                }
                else
                {
                    long res = 0;
                    for (int j = y; j <= N; j += x)
                    {
                        res += array[j];
                    }
                    Console.WriteLine(res);
                }
            }
        }

        Console.Out.Flush();
    }

    public static void Main(string[] args) => new Program().Solve();
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

        // [l,r)の和を求める
        public Num Sum(int l, int r) => Sum(r) - Sum(l);
    }
}

public struct Pair
{
    // mod, 余り
    public int S, T;
    public Pair(int s, int t)
    {
        S = s;
        T = t;
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
