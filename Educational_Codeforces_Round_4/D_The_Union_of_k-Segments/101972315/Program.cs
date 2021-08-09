using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Globalization;

public class Program
{
    int N, K;
    int[] L, R;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        L = new int[N];
        R = new int[N];
        for (int i = 0; i < N; i++)
        {
            L[i] = sc.NextInt();
            R[i] = sc.NextInt();
        }

        var ls = new List<(int, int)>(2 * N);
        for (int i = 0; i < N; i++)
        {
            ls.Add((L[i], 0));
            ls.Add((R[i], 1));
        }

        ls.Sort((l, r) => l.Item1 == r.Item1 ? l.Item2.CompareTo(r.Item2) : l.Item1.CompareTo(r.Item1));

        var uq = new List<(int, int)>();
        for (int i = 0; i < 2 * N; i++)
        {
            if (i == 0 || ls[i - 1] != ls[i]) uq.Add(ls[i]);
        }

        var map = new Dictionary<(int, int), int>();
        for (int i = 0; i < uq.Count; i++)
        {
            map[uq[i]] = i;
        }

        var imos = new int[uq.Count];

        for (int i = 0; i < N; i++)
        {
            imos[map[(L[i], 0)]]++;
            imos[map[(R[i], 1)]]--;
        }

        for (int i = 0; i < uq.Count - 1; i++)
        {
            imos[i + 1] += imos[i];
        }

        List<(int a, int b)> ans = new List<(int a, int b)>();

        int l = -1;
        for (int i = 0; i < uq.Count; i++)
        {
            if (l == -1 && imos[i] >= K)
            {
                l = i;
            }
            else if (l != -1 && imos[i] < K)
            {
                ans.Add((uq[l].Item1, uq[i].Item1));
                l = -1;
            }
        }

        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });

        Console.WriteLine(ans.Count);

        foreach ((int a, int b) in ans)
        {
            Console.WriteLine($"{a} {b}");
        }

        Console.Out.Flush();
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
