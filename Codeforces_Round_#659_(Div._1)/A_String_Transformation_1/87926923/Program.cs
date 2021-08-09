using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompLib.Collections.Generic;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
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
        string a = sc.Next();
        string b = sc.Next();

        bool[,] f = new bool[20, 20];
        for (int i = 0; i < n; i++)
        {
            if (a[i] > b[i])
            {
                Console.WriteLine("-1");
                return;
            }

            if (a[i] < b[i]) f[a[i] - 'a', b[i] - 'a'] = true;
        }

        int ans = 0;
        for (int i = 0; i < 20; i++)
        {
            int min = i;
            do
            {
                min++;
            } while (min < 20 && !f[i, min]);

            if (min >= 20)
            {
                continue;
            }

            ans++;
            for (int j = i + 1; j < 20; j++)
            {
                f[min, j] |= f[i, j];
            }
        }

        Console.WriteLine(ans);
    }

    public static void Main(string[] args) => new Program().Solve();
}

namespace CompLib.Collections.Generic
{
    using System;

    public class SegmentTree<T>
    {
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