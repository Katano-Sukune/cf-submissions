using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, X, Y;
    private List<int>[] E;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        X = sc.NextInt() - 1;
        Y = sc.NextInt() - 1;
        E = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<int>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            E[u].Add(v);
            E[v].Add(u);
        }

        // Console.WriteLine( (long) Cnt(X, -1, Y, false) * Cnt(Y, -1, X, false));
        long c = (long) N * (N - 1) - (long) Cnt(X, -1, Y, false) * Cnt(Y, -1, X, false);
        Console.WriteLine(c);
    }

    int Cnt(int cur, int par, int t, bool f)
    {
        f |= cur == t;
        int result = f ? 1 : 0;
        foreach (var to in E[cur])
        {
            if (to == par) continue;
            result += Cnt(to, cur, t, f);
        }

        return result;
    }


    // public static void Main(string[] args) => new Program().Solve();
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