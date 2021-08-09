using System;
using System.Collections.Generic;
using System.Threading;
using CompLib.Util;

public class Program
{
    private int N;
    private long[] A;
    private int[] B;
    private List<int>[] E;

    private long Ans;
    private int Index;
    private int[] P;
    private bool[] F;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.LongArray();
        B = sc.IntArray();

        /*
         * 最初ans = 0
         * iを選ぶ
         * ans += a_i
         *
         * b_i != -1なら a_{B_i} += a_i
         *
         * iを 1~N1回ずつ 最大のans
         */

        // 有向グラフ
        // 閉路無い

        E = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<int>();
        }

        for (int i = 0; i < N; i++)
        {
            if (B[i] == -1) continue;
            E[B[i] - 1].Add(i);
        }

        Index = 0;
        P = new int[N];
        F = new bool[N];
        Ans = 0;

        for (int i = 0; i < N; i++)
        {
            if (B[i] == -1)
            {
                Go(i);
            }
        }

        for (int i = 0; i < N; i++)
        {
            if (B[i] == -1)
            {
                Go2(i);
            }
        }

        Console.WriteLine(Ans);
        Console.WriteLine(string.Join(" ", P));
    }

    void Go(int cur)
    {
        foreach (int to in E[cur])
        {
            Go(to);
        }

        if (A[cur] < 0)
        {
            F[cur] = true;
            return;
        }

        Ans += A[cur];
        P[Index++] = cur + 1;
        if (B[cur] != -1)
        {
            A[B[cur] - 1] += A[cur];
        }
    }

    void Go2(int cur)
    {
        if (F[cur])
        {
            Ans += A[cur];
            P[Index++] = cur + 1;
            if (B[cur] != -1)
            {
                A[B[cur] - 1] += A[cur];
            }
        }

        foreach (var to in E[cur])
        {
            Go2(to);
        }
    }

    public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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