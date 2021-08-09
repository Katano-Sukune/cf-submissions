using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{
    int N, K;
    List<(int to, int idx)>[] E;

    int[] U, V;

    int[] C;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        E = new List<(int to, int idx)>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<(int to, int idx)>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            E[v].Add((u, i));
            E[u].Add((v, i));
        }

        int ng = 0;
        int ok = N;
        while (ok - ng > 1)
        {
            int mid = (ok + ng) / 2;
            if (F(mid)) ok = mid;
            else ng = mid;
        }

        C = new int[N - 1];
        Go(0, -1, 1, ok);

        Console.WriteLine(ok);
        Console.WriteLine(string.Join(" ", C));
    }

    void Go(int cur, int par, int pCOl, int c)
    {
        if (E[cur].Count > c)
        {
            foreach ((int to, int idx) in E[cur])
            {
                if (to == par) continue;
                C[idx] = pCOl;
                Go(to, cur, pCOl, c);
            }
        }
        else
        {
            int p = pCOl == 1 && par != -1 ? 2 : 1;
            foreach ((int to, int idx) in E[cur])
            {
                if (to == par) continue;
                C[idx] = p;
                Go(to, cur, p, c);
                p++;
                if (p == pCOl) p++;
            }
        }
    }

    // c色で可能か?
    bool F(int c)
    {
        int cnt = 0;
        for (int i = 0; i < N; i++)
        {
            if (E[i].Count > c) cnt++;
        }
        return cnt <= K;
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
