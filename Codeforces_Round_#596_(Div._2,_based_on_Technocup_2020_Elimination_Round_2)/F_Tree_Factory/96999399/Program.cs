using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private int[] P;
    private List<int>[] C;

    private int[] D;

    // 通りがけ順
    private int[] A;
    private int Ptr;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        P = new int[N];
        for (int i = 1; i < N; i++)
        {
            P[i] = sc.NextInt();
        }

        C = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            C[i] = new List<int>();
        }

        for (int i = 1; i < N; i++)
        {
            C[P[i]].Add(i);
        }

        // 部分木iの深さ
        D = new int[N];
        Go2(0);
        for (int i = 0; i < N; i++)
        {
            C[i].Sort((l,r ) => D[l].CompareTo(D[r]));
        }
        A = new int[N];
        Ptr = 0;
        Go(0);
        /*
         * n頂点
         * 0根
         * 木
         *
         * iの親 p_i
         *
         * 竹 葉以外各頂点が1個の子を持つ木
         *
         * 自分も親も根じゃない頂点 vを選ぶ
         *
         * vの親を vの親の親にする
         *
         * 竹を与えられた木に加工する
         *
         * 
         */

        /*
         * 0の子
         * a,b,c...
         *
         * 0 aの部分木 bの部分木....
         *
         * b b b ... c c c ......
         * 
         * 
         */

        int[] par = new int[N];
        for (int i = 1; i < N; i++)
        {
            par[A[i]] = A[i - 1];
        }

        List<int> ans = new List<int>();
        for (int i = 1; i < N; i++)
        {
            while (par[A[i]] != P[A[i]])
            {
                ans.Add(A[i]);
                par[A[i]] = par[par[A[i]]];
            }
        }

        Console.WriteLine(string.Join(" ", A));
        Console.WriteLine(ans.Count);
        Console.WriteLine(string.Join(" ", ans));
    }

    void Go2(int cur)
    {
        D[cur] = 0;
        foreach (var to in C[cur])
        {
            Go2(to);
            D[cur] = Math.Max(D[cur], D[to] + 1);
        }
    }

    void Go(int cur)
    {
        A[Ptr++] = cur;
        foreach (var to in C[cur])
        {
            Go(to);
        }
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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