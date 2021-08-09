using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    private int N;
    private long K;
    private const long INF = (long) 1e18;

    void Q(Scanner sc)
    {
        N = sc.NextInt();
        K = sc.NextLong();

        var ls = new List<long>();
        ls.Add(0);
        while (ls[^1] < INF)
        {
            ls.Add(1 + 4 * ls[^1]);
        }

        long min = 0;
        BigInteger max = 0;

        for (int i = N - 1; i >= 0 && min < K; i--)
        {
            // 前通り道だった個数
            long p = (1 << (N - i)) - 1;
            min += p;

            // 必要無い正方形
            long q = p * 2 - 1;
            // 大きさi
            long tmp = i >= ls.Count ? INF : ls[i];
            max += p;
            max += (BigInteger) q * tmp;
            if (min <= K && K <= max)
            {
                Console.WriteLine($"YES {i}");
                return;
            }


            // Console.WriteLine($"{i} {p} {min}");
        }

        Console.WriteLine("NO");
    }

    long Cnt(int i)
    {
        if (i == 0) return 0;
        long p = Cnt(i - 1);
        if (p >= INF) return INF;
        return 1 + 4 * p;
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