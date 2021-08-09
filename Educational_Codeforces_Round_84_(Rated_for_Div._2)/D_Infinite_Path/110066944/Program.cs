using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private const int MaxN = 200000;
    private List<int>[] Div;

    public void Solve()
    {
        Div = new List<int>[MaxN + 1];
        for (int i = 0; i <= MaxN; i++)
        {
            Div[i] = new List<int>();
        }

        for (int i = 1; i <= MaxN; i++)
        {
            for (int j = i; j <= MaxN; j += i)
            {
                Div[j].Add(i);
            }
        }


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
    private int[] P, C;

    void Q(Scanner sc)
    {
        N = sc.NextInt();
        P = sc.IntArray();
        for (int i = 0; i < N; i++)
        {
            P[i]--;
        }

        C = sc.IntArray();

        bool[] flag = new bool[N];

        int ans = int.MaxValue;
        for (int i = 0; i < N; i++)
        {
            if (flag[i]) continue;

            int v = i;
            var loop = new List<int>();
            do
            {
                flag[v] = true;
                loop.Add(C[v]);
                v = P[v];
            } while (!flag[v]);

            ans = Math.Min(ans, loop.Count);

            foreach (int k in Div[loop.Count])
            {
                if (k >= ans) break;
                var flag2 = new bool[k];
                for (int j = 0; j < loop.Count; j++)
                {
                    if (loop[j] != loop[(j + k) % loop.Count]) flag2[j % k] = true;
                }

                if (flag2.Any(b => !b)) ans = k;
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