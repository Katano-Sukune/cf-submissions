using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{
    int N;
    int M;
    int[] X;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        int[] pos = new int[N];
        for (int i = 0; i < N; i++)
        {
            pos[i] = i;
        }
        M = sc.NextInt();
        X = new int[M];
        for (int i = 0; i < M; i++)
        {
            X[i] = sc.NextInt() - 1;
        }

        var idx = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            idx[i] = new List<int>();
        }
        for (int i = 0; i < M; i++)
        {
            idx[X[i]].Add(i);
        }

        long[] ans = new long[N];
        for (int i = 0; i < M - 1; i++)
        {
            ans[0] += Math.Abs(pos[X[i + 1]] - pos[X[i]]);
        }

        for (int i = 1; i < N; i++)
        {
            ans[i] = ans[i - 1];
            // i,i-1の寄与
            foreach (int j in idx[i])
            {
                if (j - 1 >= 0) ans[i] -= Math.Abs(pos[i] - pos[X[j - 1]]);
                if (j + 1 < M) ans[i] -= Math.Abs(pos[i] - pos[X[j + 1]]);
            }
            foreach (int j in idx[i - 1])
            {
                if (j - 1 >= 0 && X[j - 1] != i) ans[i] -= Math.Abs(pos[i - 1] - pos[X[j - 1]]);
                if (j + 1 < M && X[j + 1] != i) ans[i] -= Math.Abs(pos[i - 1] - pos[X[j + 1]]);
            }

            (pos[i], pos[i - 1]) = (pos[i - 1], pos[i]);

            foreach (int j in idx[i])
            {
                if (j - 1 >= 0) ans[i] += Math.Abs(pos[i] - pos[X[j - 1]]);
                if (j + 1 < M) ans[i] += Math.Abs(pos[i] - pos[X[j + 1]]);
            }
            foreach (int j in idx[i - 1])
            {
                if (j - 1 >= 0 && X[j - 1] != i) ans[i] += Math.Abs(pos[i - 1] - pos[X[j - 1]]);
                if (j + 1 < M && X[j + 1] != i) ans[i] += Math.Abs(pos[i - 1] - pos[X[j + 1]]);
            }

        }

        Console.WriteLine(string.Join(" ", ans));
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
