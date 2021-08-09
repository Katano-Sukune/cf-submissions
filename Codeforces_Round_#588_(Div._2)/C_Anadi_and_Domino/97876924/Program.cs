using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using CompLib.Algorithm;
using CompLib.Graph;

public class Program
{
    private int N, M;
    private int[] A, B;

    private int[] Ar;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        A = new int[M];
        B = new int[M];
        for (int i = 0; i < M; i++)
        {
            A[i] = sc.NextInt() - 1;
            B[i] = sc.NextInt() - 1;
        }

        Ar = new int[N];

        Console.WriteLine(Go(0));
    }

    int Go(int i)
    {
        if (i >= N)
        {
            bool[,] f = new bool[6, 6];
            for (int j = 0; j < M; j++)
            {
                f[Ar[A[j]], Ar[B[j]]] = f[Ar[B[j]], Ar[A[j]]] = true;
            }

            int cnt = 0;
            for (int j = 0; j < 6; j++)
            {
                for (int k = j; k < 6; k++)
                {
                    if (f[j, k]) cnt++;
                }
            }

            return cnt;
        }

        int ans = int.MinValue;
        for (int j = 0; j < 6; j++)
        {
            Ar[i] = j;
            ans = Math.Max(ans, Go(i + 1));
        }

        return ans;
    }


    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

namespace CompLib.Graph
{
    using System;
    using System.Collections.Generic;

    class AdjacencyList
    {
        private readonly int _n;
        private readonly List<(int f, int t)> _edges;

        private int[] _start;
        private int[] _eList;

        public AdjacencyList(int n)
        {
            _n = n;
            _edges = new List<(int f, int t)>();
        }

        public void AddDirectedEdge(int from, int to)
        {
            _edges.Add((from, to));
        }

        public void AddUndirectedEdge(int f, int t)
        {
            AddDirectedEdge(f, t);
            AddDirectedEdge(t, f);
        }

        public void Build()
        {
            _start = new int[_n + 1];
            foreach (var e in _edges)
            {
                _start[e.f + 1]++;
            }

            for (int i = 1; i <= _n; i++)
            {
                _start[i] += _start[i - 1];
            }

            int[] counter = new int[_n + 1];
            _eList = new int[_edges.Count];

            foreach (var e in _edges)
            {
                _eList[_start[e.f] + counter[e.f]++] = e.t;
            }
        }

        public ReadOnlySpan<int> this[int f]
        {
            get { return _eList.AsSpan(_start[f], _start[f + 1] - _start[f]); }
        }
    }
}

namespace CompLib.Algorithm
{
    using System;
    using System.Collections.Generic;

    public static partial class Algorithm
    {
        private static void Swap<T>(ref T a, ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }

        private static void Reverse<T>(T[] array, int begin)
        {
            // [begin, array.Length)を反転
            if (array.Length - begin >= 2)
            {
                for (int i = begin, j = array.Length - 1; i < j; i++, j--)
                {
                    Swap(ref array[i], ref array[j]);
                }
            }
        }

        /// <summary>
        /// arrayを辞書順で次の順列にする 存在しないときはfalseを返す
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool NextPermutation<T>(T[] array, Comparison<T> comparison)
        {
            for (int i = array.Length - 2; i >= 0; i--)
            {
                if (comparison(array[i], array[i + 1]) < 0)
                {
                    int j = array.Length - 1;
                    for (; j > i; j--)
                    {
                        if (comparison(array[i], array[j]) < 0)
                        {
                            break;
                        }
                    }

                    Swap(ref array[i], ref array[j]);
                    Reverse(array, i + 1);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// arrayを辞書順で次の順列にする 存在しないときはfalseを返す
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool NextPermutation<T>(T[] array, Comparer<T> comparer) =>
            NextPermutation(array, comparer.Compare);

        /// <summary>
        /// arrayを辞書順で次の順列にする 存在しないときはfalseを返す
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool NextPermutation<T>(T[] array) => NextPermutation(array, Comparer<T>.Default);
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