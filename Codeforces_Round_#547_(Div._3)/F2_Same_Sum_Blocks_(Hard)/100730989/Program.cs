using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private int[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();
        var map = new Dictionary<long, List<(int l, int r)>>();
        for (int l = 0; l < N; l++)
        {
            long sum = 0;
            for (int r = l + 1; r <= N; r++)
            {
                sum += A[r - 1];
                if (!map.TryGetValue(sum, out _)) map[sum] = new List<(int l, int r)>();
                map[sum].Add((l, r));
            }
        }

        var ls = new List<(int l, int r)>();
        foreach ((long key, List<(int l, int r)> value) in map)
        {
            var tmp = new List<(int l, int r)>();
            value.Sort((l, r) => l.r.CompareTo(r.r));
            int max = int.MinValue;
            foreach ((int l, int r) tuple in value)
            {
                if (max <= tuple.l)
                {
                    tmp.Add(tuple);
                    max = tuple.r;
                }
            }

            if (ls.Count < tmp.Count)
            {
                ls = tmp;
            }
        }

        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        Console.WriteLine(ls.Count);
        foreach ((int l, int r) in ls)
        {
            Console.WriteLine($"{l + 1} {r}");
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