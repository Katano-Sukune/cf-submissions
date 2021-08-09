using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Collections.Generic;

public class Program
{
    private int T;
    private int[] A;

    public void Solve()
    {
        var sc = new Scanner();
        T = sc.NextInt();
        A = sc.IntArray();
        int max = A.Max();
        long s = 0;
        int[] cnt = new int[T];
        foreach (int i in A)
        {
            cnt[i]++;
            s += i;
        }

        for (int n = 1; n * n <= T; n++)
        {
            if (T % n != 0) continue;
            int m = T / n;

            // (0,0)
            int[] tmp = new int[T];
            long sum = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    sum += i + j;
                    tmp[i + j]++;
                }
            }

            var imos = new int[T + 1];
            for (int i = 0; i < T; i++)
            {
                imos[i] += tmp[i];
                imos[i + 1] -= tmp[i];
            }

            for (int i = 0; i < (n + 1) / 2; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (j == 0)
                    {
                        if (i != 0)
                        {
                            // 消えるところ
                            {
                                int mn = n - i;
                                int mx = mn + m - 1;
                                imos[mn]--;
                                imos[mx + 1]++;
                                sum -= (long) (mn + mx) * (mx - mn + 1) / 2;
                            }
                            // 増える
                            {
                                int mn = i;
                                int mx = mn + m - 1;
                                imos[mn]++;
                                imos[mx + 1]--;
                                sum += (long) (mn + mx) * (mx - mn + 1) / 2;
                            }
                        }
                    }
                    else
                    {
                        // 消える
                        {
                            int mn = m - j;
                            int mx1 = mn + (n - i - 1);
                            int mx2 = mn + i;
                            imos[mn]--;
                            imos[mx1 + 1]++;
                            sum -= (long) (mn + mx1) * (mx1 - mn + 1) / 2;
                            imos[mn+1]--;
                            imos[mx2 + 1]++;
                            sum -= (long) (mn + 1 + mx2) * (mx2 - mn) / 2;
                        }
                        {
                            int mn = j;
                            int mx1 = mn + (n - i - 1);
                            int mx2 = mn + i;
                            imos[mn]++;
                            imos[mx1 + 1]--;
                            sum += (long) (mn + mx1) * (mx1 - mn + 1) / 2;
                            imos[mn+1]++;
                            imos[mx2 + 1]--;
                            sum += (long) (mn + 1 + mx2) * (mx2 - mn) / 2;
                        }
                    }

                    if (sum == s)
                    {
                        bool flag = true;
                        int k = 0;
                        for (; k <= max; k++)
                        {
                            flag &= imos[k] == cnt[k];
                            imos[k + 1] += imos[k];
                        }

                        for (int l = k - 1; l >= 0; l--)
                        {
                            imos[l + 1] -= imos[l];
                        }

                        if (!flag) continue;
                        Console.WriteLine($"{n} {m}\n{i + 1} {j + 1}");
                        return;
                    }
                }
            }
        }

        Console.WriteLine("-1");
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

namespace CompLib.Collections.Generic
{
    using System;

    public class RangeUpdateQuery<T>
    {
        private readonly int N;
        private T[] _array;
        private readonly bool[] flag;

        private readonly T _identity;
        private readonly Func<T, T, T> _operation;

        /// <summary>
        /// 区間更新、点取得
        /// </summary>
        /// <param name="size"></param>
        /// <param name="operation">更新用の演算</param>
        /// <param name="identity">(T, operation)の左単位元</param>
        public RangeUpdateQuery(int size, Func<T, T, T> operation, T identity)
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

            flag = new bool[N * 2];
        }

        private void Eval(int k, int l, int r)
        {
            if (flag[k])
            {
                if (r - l > 1)
                {
                    _array[k * 2] = _operation(_array[k * 2], _array[k]);
                    flag[k * 2] = true;
                    _array[k * 2 + 1] = _operation(_array[k * 2 + 1], _array[k]);
                    flag[k * 2 + 1] = true;
                    _array[k] = _identity;
                }

                flag[k] = false;
            }
        }

        private void Update(int left, int right, int k, int l, int r, T item)
        {
            Eval(k, l, r);
            if (r <= left || right <= l)
            {
                return;
            }

            if (left <= l && r <= right)
            {
                _array[k] = _operation(_array[k], item);
                flag[k] = true;
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
        public void Update(int left, int right, T item)
        {
            Update(left, right, 1, 0, N, item);
        }


        /// <summary>
        /// A[i]を取得 O(log N)
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public T Get(int i)
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

        public T this[int i]
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