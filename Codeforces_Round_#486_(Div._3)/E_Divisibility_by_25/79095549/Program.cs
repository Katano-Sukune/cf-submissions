using System;
using System.Linq;
using CompLib.Collections;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        var n = sc.Next().Reverse().ToArray();
        if (n.Length == 1)
        {
            Console.WriteLine("-1");
            return;
        }

        // 先頭0以外
        // 下2桁 00 25 50 75

        int ans = int.MaxValue;

        // 先頭
        for (int i = 0; i < n.Length; i++)
        {
            if (n[i] == '0') continue;
            // 2桁目
            for (int j = 0; j < n.Length; j++)
            {
                if (n.Length > 2 && i == j) continue;
                for (int k = 0; k < n.Length; k++)
                {
                    if (i == k || j == k) continue;

                    bool zero = n[j] == '0' && n[k] == '0';
                    bool twentyfive = n[j] == '2' && n[k] == '5';
                    bool fifty = n[j] == '5' && n[k] == '0';
                    bool seventyfive = n[j] == '7' && n[k] == '5';
                    if (!zero && !twentyfive && !fifty && !seventyfive) continue;
                    var array = new int[n.Length];
                    for (int l = 0; l < n.Length; l++)
                    {
                        if (l == i) array[l] = 0;
                        else if (l == j) array[l] = 2;
                        else if (l == k) array[l] = 3;
                        else array[l] = 1;
                    }
                    int tmp = 0;
                    var bit = new FenwickTree(4);
                    for (int l = 0; l < n.Length; l++)
                    {
                        tmp += bit.Sum(array[l]);
                        bit.Add(array[l], 1);
                    }
                    ans = Math.Min(ans, tmp);
                }
            }
        }
        if (ans == int.MaxValue) Console.WriteLine("-1");
        else Console.WriteLine(ans);
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
