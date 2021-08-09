using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Text;

public class Program
{
    int N, M, K, S;
    int[] A;
    int[] B;
    int[] T, C;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        K = sc.NextInt();
        S = sc.NextInt();
        A = sc.IntArray();
        B = sc.IntArray();

        T = new int[M];
        C = new int[M];
        for (int i = 0; i < M; i++)
        {
            T[i] = sc.NextInt();
            C[i] = sc.NextInt();
        }

        /*
         * K買いたい
         * 所持金S
         * 
         * i日目
         * 1ドル A_i
         * 1ポンド B_i
         * 
         * M個
         */

        // ドル、ポンドが最安値更新するとき
        var ls = new List<(int dollar, int d1, int pound, int d2)>();
        ls.Add((A[0], 1, B[0], 1));
        for (int i = 1; i < N; i++)
        {
            if (A[i] < ls[^1].dollar)
            {
                if (B[i] < ls[^1].pound)
                {
                    ls.Add((A[i], i + 1, B[i], i + 1));
                }
                else
                {
                    ls.Add((A[i], i + 1, ls[^1].pound, ls[^1].d2));
                }
            }
            else
            {
                if (B[i] < ls[^1].pound)
                {
                    ls.Add((ls[^1].dollar, ls[^1].d1, B[i], i + 1));
                }
            }
        }

        int ng = -1;
        int ok = ls.Count;
        while (ok - ng > 1)
        {
            int mid = (ok + ng) / 2;
            if (F(ls[mid].dollar, ls[mid].pound)) ok = mid;
            else ng = mid;
        }

        if (ok >= ls.Count)
        {
            Console.WriteLine("-1");
            return;
        }

        var sorted = new int[M];
        for (int i = 0; i < M; i++)
        {
            sorted[i] = i;
        }

        Array.Sort(sorted, (l, r) =>
        {
            long costL = (long)C[l] * (T[l] == 1 ? ls[ok].dollar : ls[ok].pound);
            long costR = (long)C[r] * (T[r] == 1 ? ls[ok].dollar : ls[ok].pound);
            return costL.CompareTo(costR);
        });
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        Console.WriteLine(Math.Max(ls[ok].d1, ls[ok].d2));
        for (int i = 0; i < K; i++)
        {
            Console.WriteLine($"{sorted[i] + 1} {(T[sorted[i]] == 1 ? ls[ok].d1 : ls[ok].d2)}");
        }
        Console.Out.Flush();

    }

    bool F(int dollar, int pound)
    {
        long[] cost = new long[M];
        for (int i = 0; i < M; i++)
        {
            cost[i] = (long)C[i] * (T[i] == 1 ? dollar : pound);
        }

        Array.Sort(cost);
        long sum = 0;
        for (int i = 0; i < K && sum <= S; i++)
        {
            sum += cost[i];
        }

        return sum <= S;
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
