using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;

public class Program
{
    int N;
    long[] X, Y;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        X = new long[N];
        Y = new long[N];
        for (int i = 0; i < N; i++)
        {
            X[i] = sc.NextInt();
            Y[i] = sc.NextInt();
        }

        int f = 1;
        long xx = int.MinValue;
        long yy = int.MinValue;
        for (int i = 0; i < N; i++)
        {
            if (X[i] > xx || (X[i] >= xx && Y[i] > yy))
            {
                xx = X[i];
                yy = Y[i];
                f = i;
            }
        }

        int[] ans = new int[N];
        ans[0] = f;
        bool[] flag = new bool[N];
        flag[f] = true;
        int cur = f;
        for (int i = 1; i < N; i++)
        {
            long td2 = long.MinValue;
            int to = -1;
            for (int j = 0; j < N; j++)
            {
                if (flag[j]) continue;
                long dist2 = (X[cur] - X[j]) * (X[cur] - X[j]) + (Y[cur] - Y[j]) * (Y[cur] - Y[j]);
                if (dist2 > td2)
                {
                    to = j;
                    td2 = dist2;
                }
            }
            ans[i] = to;
            flag[to] = true;
            cur = to;
            //// if (Calc(ans[i - 2], ans[i - 1], to))
            //{

            //}
            //else
            //{
            //    // Console.WriteLine($"{ans[i - 2]} {ans[i - 1]} {to}");
            //    Console.WriteLine("-1");
            //    return;
            //}
        }


        for (int i = 0; i < N; i++)
        {
            ans[i]++;
        }
        Console.WriteLine(string.Join(" ", ans));
    }

    bool Calc(int a, int b, int c)
    {
        long distAC2 = (X[a] - X[c]) * (X[a] - X[c]) + (Y[a] - Y[c]) * (Y[a] - Y[c]);
        long mx = X[a] + X[c];
        long my = Y[a] + Y[c];

        long distMB2 = (mx - 2 * X[b]) * (mx - 2 * X[b]) + (my - 2 * Y[b]) * (my - 2 * Y[b]);
        return distMB2 > distAC2;
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