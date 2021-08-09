using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private long[] W;
    private int[] U, V;
    private long[] C;

    private List<(int to, long c)>[] E;

    // iの子孫からiにたどり着くまでに残せるガソリン v側から来た
    // 1位、2位
    private (long g, int v)[] D1;
    long[] D2;

    // iに残せるガソリン
    private long[] G;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        W = sc.LongArray();
        U = new int[N - 1];
        V = new int[N - 1];
        C = new long[N - 1];
        for (int i = 0; i < N - 1; i++)
        {
            U[i] = sc.NextInt() - 1;
            V[i] = sc.NextInt() - 1;
            C[i] = sc.NextLong();
        }

        E = new List<(int to, long c)>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<(int to, long c)>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            E[U[i]].Add((V[i], C[i]));
            E[V[i]].Add((U[i], C[i]));
        }

        D1 = new (long g, int v)[N];
        D2 = new long[N];
        Go(0, -1);
        G = new long[N];
        Go2(0, -1);
        Console.WriteLine(D1.Max(t => t.g));
    }

    void Go(int cur, int par)
    {
        D1[cur] = (W[cur], -1);
        D2[cur] = W[cur];
        foreach ((int to, long c) in E[cur])
        {
            if (to == par) continue;
            Go(to, cur);
            long g = D1[to].g + W[cur] - c;
            if (g > D1[cur].g)
            {
                D2[cur] = D1[cur].g;
                D1[cur] = (g, to);
            }
            else if (g > D2[cur])
            {
                D2[cur] = g;
            }
        }
    }

    void Go2(int cur, int par)
    {
        foreach ((int to, long c) in E[cur])
        {
            if (to == par) continue;
            long g;
            if (D1[cur].v == to)
            {
                g = D2[cur] + W[to] - c;
            }
            else
            {
                g = D1[cur].g + W[to] - c;
            }

            if (g > D1[to].g)
            {
                D2[to] = D1[to].g;
                D1[to] = (g, -1);
            }
            else if (g > D2[to])
            {
                D2[to] = g;
            }

            Go2(to, cur);
        }
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