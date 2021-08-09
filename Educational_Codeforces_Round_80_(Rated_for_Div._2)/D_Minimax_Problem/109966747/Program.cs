using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, M;
    private int[][] A;

    private List<int>[] L;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        A = new int[N][];
        for (int i = 0; i < N; i++)
        {
            A[i] = sc.IntArray();
        }

        /*
         * i,jを選ぶ
         *
         * b_k = max(a_i_k, a_j_k)
         *
         * bの最小を最大になるi,j
         */

        L = new List<int>[1 << M];
        for (int i = 0; i < (1 << M); i++)
        {
            L[i] = new List<int>();
            for (int j = 0; j < (1 << M); j++)
            {
                if ((i | j) == (1 << M) - 1) L[i].Add(j);
            }
        }

        int ok = 0;
        int ng = (int) 1e9 + 1;
        while (ng - ok > 1)
        {
            int mid = (ok + ng) / 2;
            if (F(mid)) ok = mid;
            else ng = mid;
        }

        W(ok);
    }

    void W(int bK)
    {
        int[] idx = new int[1 << M];
        for (int j = 0; j < N; j++)
        {
            var ar = A[j];

            int tmp = 0;
            for (int i = 0; i < M; i++)
            {
                if (ar[i] >= bK) tmp |= (1 << i);
            }


            idx[tmp] = j + 1;

            foreach (int i in L[tmp])
            {
                if (idx[i] != 0)
                {
                    Console.WriteLine($"{idx[i]} {j + 1}");
                    return;
                }
            }
        }
    }

    bool F(int bK)
    {
        bool[] flag = new bool[1 << M];
        foreach (int[] ar in A)
        {
            int tmp = 0;
            for (int i = 0; i < M; i++)
            {
                if (ar[i] >= bK) tmp |= (1 << i);
            }


            flag[tmp] = true;

            foreach (int i in L[tmp])
            {
                if (flag[i]) return true;
            }

            if (tmp == (1 << M) - 1) return true;
        }

        return false;
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