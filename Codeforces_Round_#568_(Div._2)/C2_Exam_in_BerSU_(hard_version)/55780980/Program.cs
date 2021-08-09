using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Contest
{
    struct S
    {
        public int Num;
        public int Index;

        public S(int num, int i)
        {
            Num = num;
            Index = i;
        }
    }

    class SegTree
    {
        private readonly long[] Array;
        private readonly int N;

        public SegTree(int size)
        {
            N = 1;
            while (N < size)
            {
                N *= 2;
            }
            Array = new long[N * 2];
        }

        public void Update(long item, int index)
        {
            index += N;
            Array[index] = item;
            while (index > 1)
            {
                index /= 2;
                Array[index] = Array[index * 2] + Array[index * 2 + 1];
            }
        }

        private long Q(int left, int right, int k, int l, int r)
        {
            if (left <= l && r <= right)
            {
                return Array[k];
            }
            if (r <= left || right <= l)
            {
                return 0;
            }
            return Q(left, right, k * 2, l, (l + r) / 2) + Q(left, right, k * 2 + 1, (l + r) / 2, r);
        }

        public long Query(int left, int right)
        {
            return Q(left, right, 1, 0, N);
        }

        public long this[int i]
        {
            get { return Array[i + N]; }
            set { Update(value, i); }
        }
    }
    class Program
    {
        private Scanner sc;
        private int N;
        private int M;
        private int[] T;
        public void Solve()
        {
            sc = new Scanner();
            N = sc.NextInt();
            M = sc.NextInt();
            T = sc.IntArray();
            var TT = T.Select((i, index) => new S(i, index)).ToArray();
            var sortT = TT.ToArray();
            Array.Sort(sortT, (a, b) => a.Num.CompareTo(b.Num));
            var map = new Dictionary<S, int>();
            for (int i = 0; i < N; i++)
            {
                map[sortT[i]] = i;
            }
            var st1 = new SegTree(N);
            var st2 = new SegTree(N);
            var ans = new long[N];
            //Console.WriteLine(string.Join(" ", ar.Select(s => s.Index).ToArray()));
            for (int i = 0; i < N; i++)
            {
                //[ok, ng)の範囲なら合計がM-T[i]以下

                int ok = 0;
                int ng = N + 2;
                while (ng - ok > 1)
                {
                    int med = (ok + ng) / 2;
                    long ss = st1.Query(0, med);
                    if (ss <= M - T[i])
                    {
                        ok = med;
                    }
                    else
                    {
                        ng = med;
                    }
                }
                // int cnt = ok + 1;
                //for (int j = 0; j < N; j++)
                //{
                //    Console.Write(st1[j]);
                //    Console.Write(" ");
                //}
                //Console.WriteLine();
                ans[i] = i - st2.Query(0, ok);
                st1[map[TT[i]]] = T[i];
                st2[map[TT[i]]] = 1;
            }
            Console.WriteLine(string.Join(" ", ans));
        }

        static void Main() => new Program().Solve();
    }
}

class Scanner
{
    public Scanner()
    {
        _stream = new StreamReader(Console.OpenStandardInput());
        _pos = 0;
        _line = new string[0];
        _separator = ' ';
    }
    private readonly char _separator;
    private readonly StreamReader _stream;
    private int _pos;
    private string[] _line;
    #region get a element
    public string Next()
    {
        if (_pos >= _line.Length)
        {
            _line = _stream.ReadLine().Split(_separator);
            _pos = 0;
        }
        return _line[_pos++];
    }
    public int NextInt()
    {
        return int.Parse(Next());
    }
    public long NextLong()
    {
        return long.Parse(Next());
    }
    public double NextDouble()
    {
        return double.Parse(Next());
    }
    #endregion
    #region convert array
    private int[] ToIntArray(string[] array)
    {
        var result = new int[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = int.Parse(array[i]);
        }

        return result;
    }
    private long[] ToLongArray(string[] array)
    {
        var result = new long[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = long.Parse(array[i]);
        }

        return result;
    }
    private double[] ToDoubleArray(string[] array)
    {
        var result = new double[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = double.Parse(array[i]);
        }

        return result;
    }
    #endregion
    #region get array
    public string[] Array()
    {
        if (_pos >= _line.Length)
            _line = _stream.ReadLine().Split(_separator);
        _pos = _line.Length;
        return _line;
    }
    public int[] IntArray()
    {
        return ToIntArray(Array());
    }
    public long[] LongArray()
    {
        return ToLongArray(Array());
    }
    public double[] DoubleArray()
    {
        return ToDoubleArray(Array());
    }
    #endregion
}