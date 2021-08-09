using System;
using System.Threading;
using System.IO;
using CompLib.Collections.Generic;
using System.Diagnostics;

public class Program
{

    public void Solve()
    {
#if DEBUG
        int t = 1;
#else
        int t = int.Parse(Console.ReadLine());
#endif
        for (int i = 0; i < t; i++)
        {
            Q();
        }
    }

#if DEBUG
    int x = 3;
#endif
    int N, K;


    void Q()
    {

#if DEBUG
        N = 5;
        K = 3;
#else
var ln = Console.ReadLine().Split(' ');
        N = int.Parse(ln[0]);
        K = int.Parse(ln[1]);
#endif

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

        // 0回目 
        // print(0)

        // 1~n-1 x := 0 - i

        // 1 print(1)

        // x:= 1 - (0 - i)

        // 2 - (1 - (0 - i))
        // = 2 - 1 + 0 - i

        int[] ar = new int[N];
        for (int i = 0; i < N; i++)
        {
            ar[i] = i;
        }

        int tmp = 0;
        for (int i = 0; i < N; i++)
        {
            if (i % 2 == 0)
            {
                ar[i] = Add(tmp, ar[i]);
            }
            else
            {
                ar[i] = Minus(tmp, ar[i]);
            }

            if (Q(ar[i]) == 1)
            {
                return;
            }

            tmp = Minus(ar[i], tmp);
        }
    }

    int Add(int a, int b)
    {
        int p = 1;
        int c = 0;
        while (a > 0 || b > 0)
        {
            int aa = a % K;
            int bb = b % K;
            c += ((aa + bb) % K) * p;
            a /= K;
            b /= K;
            p *= K;
        }

        return c;
    }

    int Minus(int a, int b)
    {
        int p = 1;
        int c = 0;
        while (a > 0 || b > 0)
        {
            int aa = a % K;
            int bb = b % K;
            c += ((((aa - bb) % K) + K) % K) * p;
            a /= K;
            b /= K;
            p *= K;
        }

        return c;
    }

    int Q(int y)
    {
#if DEBUG
        if (x == y)
        {
            Console.WriteLine($"[DEBUG] {y} 1");
            return 1;
        }

        int z = Minus(y, x);
        Debug.Assert(Add(x, z) == y);
        x = z;
        Console.WriteLine($"[DEBUG] y = {y} {0} x={x}");
        return 0;
#else
        Console.WriteLine(y);
        return int.Parse(Console.ReadLine());
#endif
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

