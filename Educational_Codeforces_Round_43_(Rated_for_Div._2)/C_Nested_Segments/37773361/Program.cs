using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Threading;

namespace Contest
{
    class Scanner
    {
        private string[] line = new string[0];
        private int index = 0;
        public string Next()
        {
            if (line.Length <= index)
            {
                line = Console.ReadLine().Split(' ');
                index = 0;
            }
            var res = line[index];
            index++;
            return res;
        }
        public int NextInt()
        {
            return int.Parse(Next());
        }
        public long NextLong()
        {
            return long.Parse(Next());
        }
        public string[] Array()
        {
            line = Console.ReadLine().Split(' ');
            index = line.Length;
            return line;
        }
        public int[] IntArray()
        {
            var array = Array();
            var result = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                result[i] = int.Parse(array[i]);
            }

            return result;
        }
        public long[] LongArray()
        {
            var array = Array();
            var result = new long[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                result[i] = long.Parse(array[i]);
            }

            return result;
        }
    }

    class SegTree<T>
    {
        private readonly T[] Array;
        private readonly Comparison<T> Compare;
        private readonly int N;
        private readonly T Max;
        public SegTree(int size, T max) : this(size, Comparer<T>.Default, max) { }
        public SegTree(int size, IComparer<T> comparer, T max) : this(size, comparer.Compare, max) { }
        public SegTree(int size, Comparison<T> comparison, T max)
        {
            Max = max;
            N = 1;
            while (N < size)
            {
                N *= 2;
                //Console.WriteLine(N);
            }
            Compare = comparison;
            Array = new T[2 * N];
            for (int i = 0; i < 2 * N; i++)
            {
                Array[i] = max;
            }
        }

        private T Min(T x, T y)
        {
            if (Compare(x, y) < 0)
            {
                return x;
            }
            else
            {
                return y;
            }
        }

        public void Update(T item, int index)
        {
            index += N;
            Array[index] = item;
            while (index > 1)
            {
                index /= 2;
                Array[index] = Min(Array[index * 2], Array[index * 2 + 1]);
            }
        }

        private T Q(int left, int right, int k, int l, int r)
        {

            if (left <= l && r <= right)
            {
                return Array[k];
            }

            if (r <= left || right <= l)
            {
                return Max;
            }

            return Min(Q(left, right, k * 2, l, (l + r) / 2), Q(left, right, k * 2 + 1, (l + r) / 2, r));
        }

        public T Query(int left, int right)
        {
            return Q(left, right, 1, 0, N);
        }

    }

    public struct S
    {
        public int Index;
        public int L, R;

        public S(int l, int r, int index)
        {
            L = l;
            R = r;
            Index = index;
        }
    }

    class Program
    {
        private int N;
        private S[] S;
        private void Scan()
        {
            var sc = new Scanner();
            N = sc.NextInt();
            S = new S[N];
            for (int i = 0; i < N; i++)
            {
                int l = sc.NextInt();
                int r = sc.NextInt();
                S[i] = new S(l, r, i + 1);
            }

        }

        public void Solve()
        {
            Scan();
            Array.Sort(S, (a, b) =>
            {
                if (a.L == b.L)
                {
                    return b.R.CompareTo(a.R);
                }
                else
                {
                    return a.L.CompareTo(b.L);
                }
            });
            SegTree<S> segTree = new SegTree<S>(N, (a, b) => a.R.CompareTo(b.R), new S(0, int.MaxValue, 0));
            for (int i = 0; i < N; i++)
            {
                segTree.Update(S[i], i);
            }

            for (int i = 0; i < N - 1; i++)
            {
                var q = segTree.Query(i + 1, N);
                if (S[i].R >= q.R)
                {
                    Console.WriteLine($"{q.Index} {S[i].Index}");
                    return;
                }
            }
            Console.WriteLine("-1 -1");
        }


        static void Main(string[] args)
        {
            new Program().Solve();
        }
    }
}