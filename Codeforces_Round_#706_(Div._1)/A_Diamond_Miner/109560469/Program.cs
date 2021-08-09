using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
#if !DEBUG
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
#endif
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    private int N;
    int[] X, Y;

    void Q(Scanner sc)
    {
        N = sc.NextInt();
        X = new int[2 * N];
        Y = new int[2 * N];
        for (int i = 0; i < 2 * N; i++)
        {
            X[i] = sc.NextInt();
            Y[i] = sc.NextInt();
        }

        // N人　miner
        // N個　鉱山
        var miner = new List<int>(N);
        var mine = new List<int>(N);
        for (int i = 0; i < 2 * N; i++)
        {
            if (X[i] == 0)
            {
                miner.Add(Math.Abs(Y[i]));
            }
            else if (Y[i] == 0)
            {
                mine.Add(Math.Abs(X[i]));
            }
        }

        miner.Sort();
        mine.Sort();
        double ans = 0;
        for (int i = 0; i < N; i++)
        {
            // ans += Math.Sqrt((long) miner[i] * miner[i] + (long) mine[N - i - 1] * mine[N - i - 1]);
            ans += Math.Sqrt((long) miner[i] * miner[i] + (long) mine[i] * mine[i]);
        }

        Console.WriteLine($"{ans:F20}");
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