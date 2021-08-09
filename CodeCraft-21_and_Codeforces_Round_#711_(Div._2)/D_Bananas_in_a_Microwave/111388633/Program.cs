using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, M;
    private const int T = 100000;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();

        int[] ans = new int[M + 1];
        Array.Fill(ans, -1);
        ans[0] = 0;
        for (int i = 0; i < N; i++)
        {
            int t = sc.NextInt();
            long x = sc.NextLong();
            int y = sc.NextInt();
            int[] tmp = new int[M + 1];
            if (t == 1)
            {
                long xx = (x + T - 1) / T;
                for (int j = 0; j <= M; j++)
                {
                    if (ans[j] == -1) continue;
                    if (j + xx > M) continue;
                    if (ans[j + xx] != -1) continue;
                    if (tmp[j] >= y) continue;
                    tmp[j + xx] = tmp[j] + 1;
                    ans[j + xx] = i + 1;
                }
            }
            else if (t == 2)
            {
                for (int j = 0; j <= M; j++)
                {
                    if (ans[j] == -1) continue;
                    long next = (j * x + T - 1) / T;

                    // Console.WriteLine($"{j} {x} {next}");
                    if (next > M) continue;
                    if (ans[next] != -1) continue;
                    if (tmp[j] >= y) continue;
                    tmp[next] = tmp[j] + 1;
                    ans[next] = i + 1;
                }
            }
        }

        Console.WriteLine(string.Join(" ", ans.Skip(1)));
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