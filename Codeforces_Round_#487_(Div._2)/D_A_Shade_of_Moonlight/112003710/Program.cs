using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, L, WMax;
    private int[] X, V;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        L = sc.NextInt();
        WMax = sc.NextInt();

        X = new int[N];
        V = new int[N];
        for (int i = 0; i < N; i++)
        {
            X[i] = sc.NextInt();
            V[i] = sc.NextInt();
        }

        List<int> plus = new List<int>();
        List<int> minus = new List<int>();

        for (int i = 0; i < N; i++)
        {
            if (V[i] > 0) plus.Add(X[i]);
            else minus.Add(X[i]);
        }

        long ans = 0;

        minus.Sort();

        foreach (int x in plus)
        {
            int ng = -1;
            int ok = minus.Count;
            while (ok - ng > 1)
            {
                int mid = (ok + ng) / 2;

                // 2つ雲の距離
                // dist/2 で
                int dist = minus[mid] + L - x;
                if (dist <= 0)
                {
                    ng = mid;
                    continue;
                }

                int dist2 = -x;

                double w = dist2 / ((double) dist / 2) - 1;
                if (Math.Abs(w) < WMax) ok = mid;
                else ng = mid;
            }

            // Console.WriteLine($"{x} {ok}");
            ans += minus.Count - ok;
        }

        Console.WriteLine(ans);
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