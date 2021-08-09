using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Collections.Generic;
using CompLib.Util;

public class Program
{
    int N, M, K;
    int[] S;
    int[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        K = sc.NextInt();
        S = sc.IntArray();
        A = sc.IntArray();

        /*
         * ランプ設置
         * 0~N-1に
         * 
         * lのランプxに置くと[x,x+l]が照らされる
         * 
         * lのランプのコストA_l
         * Sには置けない
         * 
         * 1種のランプで全部照らす
         * コスト最小
         */
        // trueなら障害物がある
        bool[] flag = new bool[N + 1];
        foreach (var i in S)
        {
            flag[i] = true;
        }

        if (flag[0])
        {
            Console.WriteLine("-1");
            return;
        }

        // 無い位置のリスト
        List<int> ls = new List<int>();
        for (int i = 0; i <= N; i++)
        {
            if (!flag[i]) ls.Add(i);
        }

        // これ未満はゴールできない
        int min = 0;
        for (int i = 1; i < ls.Count; i++)
        {
            min = Math.Max(min, ls[i] - ls[i - 1]);
        }

        long ans = long.MaxValue;
        for (int l = min; l <= K; l++)
        {
            int pos = 0;
            int index = 0;
            long t = 0;
            while (t < ans && pos < N)
            {
                int ok = index + 1;
                int ng = ls.Count;
                while (ng - ok > 1)
                {
                    int mid = (ok + ng) / 2;
                    if (ls[mid] <= pos + l) ok = mid;
                    else ng = mid;
                }

                pos = ls[ok];
                index = ok;
                t += A[l - 1];
            }

            ans = Math.Min(ans, t);
        }

        if(ans == long.MaxValue)
        {
            Console.WriteLine("-1");
            return;
        }
        Console.WriteLine(ans);
    }

    public static void Main(string[] args) => new Program().Solve();
}


namespace CompLib.Collections.Generic
{
    using System;

    public class SegmentTree
    {
        // 制約に合った2の冪
        private const int N = 1 << 20;
        private int[] _array;

        private const int _identity = int.MinValue;


        public SegmentTree(int[] array)
        {

            _array = new int[N * 2];
            for (int i = 0; i < array.Length; i++)
            {
                _array[i + N] = array[i];
            }
            for (int i = array.Length; i < N; i++)
            {
                _array[i + N] = _identity;
            }
            for (int i = N - 1; i >= 1; i--)
            {
                _array[i] = Math.Max(_array[i * 2], _array[i * 2 + 1]);
            }
        }



        private int Query(int left, int right, int k, int l, int r)
        {
            if (r <= left || right <= l)
            {
                return _identity;
            }

            if (left <= l && r <= right)
            {
                return _array[k];
            }

            return Math.Max(Query(left, right, k * 2, l, (l + r) / 2),
                Query(left, right, k * 2 + 1, (l + r) / 2, r));
        }

        /// <summary>
        /// A[left] op A[left+1] ... op A[right-1]を求める
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public int Query(int left, int right)
        {
            return Query(left, right, 1, 0, N);
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
