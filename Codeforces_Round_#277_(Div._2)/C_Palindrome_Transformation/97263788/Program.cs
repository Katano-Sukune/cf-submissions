using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, P;
    private string S;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        P = sc.NextInt() - 1;
        S = sc.Next();

        List<(int l, int r)> ls = new List<(int l, int r)>();
        for (int i = 0; i < N / 2; i++)
        {
            if (S[i] != S[N - i - 1]) ls.Add((i, N - i - 1));
        }

        if (ls.Count == 0)
        {
            Console.WriteLine("0");
            return;
        }

        int a = C(ls[0].l, ls[^1].l, P);
        int b = C(ls[0].l, ls[^1].r, P);
        int c = C(ls[0].r, ls[^1].l, P);
        int d = C(ls[0].r, ls[^1].r, P);
        int m = Math.Min(Math.Min(a, b), Math.Min(c, d));
        int ans = m;
        foreach ((int l, int r)  in ls)
        {
            int t = Math.Abs(S[l] - S[r]);
            ans += Math.Min(t, 26 - t);
        }
        Console.WriteLine(ans);
    }

    int C(int l, int r, int p)
    {
        return Math.Min(Math.Abs(l - p) + Math.Abs(r - l), Math.Abs(r - p) + Math.Abs(l - r));
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