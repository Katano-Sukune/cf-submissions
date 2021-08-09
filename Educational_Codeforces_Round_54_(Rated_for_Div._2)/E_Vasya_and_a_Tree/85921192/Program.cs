using System;
using System.Collections.Generic;
using System.Threading;
using CompLib.Collections.Generic;
using CompLib.Util;

public class Program
{
    private long[] Imos;
    private int N;
    private List<int>[] E;
    private List<(int d, long x)>[] Q;
    private long[] Ans;

    public void Solve()
    {
        var sc = new Scanner();

        N = sc.NextInt();
        E = new List<int>[N];
        Q = new List<(int d, long x)>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<int>();
            Q[i] = new List<(int d, long x)>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            E[u].Add(v);
            E[v].Add(u);
        }

        int m = sc.NextInt();
        for (int i = 0; i < m; i++)
        {
            int v = sc.NextInt() - 1;
            int d = sc.NextInt();
            long x = sc.NextLong();
            Q[v].Add((d, x));
        }

        Imos = new long[N + 1];
        Ans = new long[N];
        Go(0, -1, 0);

        Console.WriteLine(string.Join(" ", Ans));
    }

    void Go(int cur, int par, int depth)
    {
        foreach ((int d, long x) t in Q[cur])
        {
            Imos[depth] += t.x;
            Imos[Math.Min(N, depth + t.d + 1)] -= t.x;
        }

        Imos[depth + 1] += Imos[depth];
        Ans[cur] = Imos[depth];

        foreach (int to in E[cur])
        {
            if (to == par) continue;
            Go(to, cur, depth + 1);
        }

        Imos[depth + 1] -= Imos[depth];

        foreach ((int d, long x) t in Q[cur])
        {
            Imos[depth] -= t.x;
            Imos[Math.Min(N, depth + t.d + 1)] += t.x;
        }
    }

    public static void Main(string[] args) => new Thread(new ThreadStart(new Program().Solve), 1 << 27).Start();
}

namespace CompLib.Collections.Generic
{
    using System;

    public class RangeUpdateQuery
    {
        private readonly int N;
        private long[] _array;

        /// <summary>
        /// 区間更新、点取得
        /// </summary>
        /// <param name="size"></param>
        /// <param name="operation">更新用の演算</param>
        /// <param name="identity">(T, operation)の左単位元</param>
        public RangeUpdateQuery(int size)
        {
            N = 1;
            while (N < size) N *= 2;
            _array = new long[N * 2];
        }

        private void Eval(int k, int l, int r)
        {
            if (_array[k] != 0)
            {
                if (r - l > 1)
                {
                    _array[k * 2] = _array[k * 2] + _array[k];
                    _array[k * 2 + 1] = _array[k * 2 + 1] + _array[k];
                    _array[k] = 0;
                }
            }
        }

        private void Update(int left, int right, int k, int l, int r, long item)
        {
            Eval(k, l, r);
            if (r <= left || right <= l)
            {
                return;
            }

            if (left <= l && r <= right)
            {
                _array[k] = _array[k] + item;
                Eval(k, l, r);
                return;
            }

            Update(left, right, k * 2, l, (l + r) / 2, item);
            Update(left, right, k * 2 + 1, (l + r) / 2, r, item);
        }

        /// <summary> 
        /// [left,right)をoperation(A[i],item)に更新
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="item"></param>
        public void Update(int left, int right, long item)
        {
            Update(left, right, 1, 0, N, item);
        }


        /// <summary>
        /// A[i]を取得 O(log N)
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public long Get(int i)
        {
            int l = 0;
            int r = N;
            int k = 1;
            while (r - l > 1)
            {
                Eval(k, l, r);
                int m = (l + r) / 2;
                if (i < m)
                {
                    r = m;
                    k = k * 2;
                }
                else
                {
                    l = m;
                    k = k * 2 + 1;
                }
            }

            return _array[k];
        }

        public long this[int i]
        {
            get { return Get(i); }
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