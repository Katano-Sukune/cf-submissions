using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{
    int N, K;
    int[] X;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        X = sc.IntArray();

        var ls = new List<int>[N + 1];
        for (int i = 1; i <= N; i++)
        {
            ls[i] = new List<int>();
        }

        for (int i = 0; i < K; i++)
        {
            ls[X[i]].Add(i);
        }


        long ans = 0;

        for (int i = 1; i <= N; i++)
        {
            if (ls[i].Count == 0)
            {
                ans++;
                if (i - 1 >= 1) ans++;
                if (i + 1 <= N) ans++;
            }
            else
            {
                if (i - 1 >= 1)
                {
                    int ng = -1;
                    int ok = ls[i - 1].Count;
                    while (ok - ng > 1)
                    {
                        int mid = (ok + ng) / 2;
                        if (ls[i - 1][mid] > ls[i][0]) ok = mid;
                        else ng = mid;
                    }

                    if (ok == ls[i - 1].Count) ans++;
                }
                if (i + 1 <= N)
                {
                    int ng = -1;
                    int ok = ls[i + 1].Count;
                    while (ok - ng > 1)
                    {
                        int mid = (ok + ng) / 2;
                        if (ls[i + 1][mid] > ls[i][0]) ok = mid;
                        else ng = mid;
                    }

                    if (ok == ls[i + 1].Count) ans++;
                }
            }
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
