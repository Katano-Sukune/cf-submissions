using System;
using System.Threading;
using System.IO;
using CompLib.Collections.Generic;

public class Program
{

    public void Solve()
    {
        int t = int.Parse(Console.ReadLine());
        for (int i = 0; i < t; i++)
        {
            Q();
        }
    }

    void Q()
    {
        var ln = Console.ReadLine().Split(' ');
        int n = int.Parse(ln[0]);
        int k = int.Parse(ln[1]);

        // k-xor
        // a k-xor b
        // a,bをk進数にする
        // c_i = (a_i + b_i)%k

        // 整数x
        // 0 <= x < n

        // yを聞く
        // x = yなら 1を返す　終了

        // else 0を返す
        // x:= x k-xor z=yを満たすz

        // n回まででxを当てる

        // n = 1
        // 0

        // n = 2
        // x=0,1
        // 0を聞く
        // 1を聞く

        // n = 3
        // x=0,1,2
        //

        var st = new RangeUpdateQuery<int>(n, (l, r) => l ^ r, 0);
        for (int i = 0; i < n; i++)
        {
            st.Update(i, i + 1, i);
        }

        for (int i = 0; i < n; i++)
        {
            int y = st[i];
            if (Q(y) == 1)
            {
                return;
            }
            st.Update(i + 1, n, y);
        }
    }

    int Q(int y)
    {
        Console.WriteLine(y);
        return int.Parse(Console.ReadLine());
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

