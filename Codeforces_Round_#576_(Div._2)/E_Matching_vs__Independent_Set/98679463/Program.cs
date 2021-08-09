using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    private int N, M;
    private int[] U, V;

    void Q(Scanner sc)
    {
        N = sc.NextInt();
        M = sc.NextInt();
        U = new int[M];
        V = new int[M];
        for (int i = 0; i < M; i++)
        {
            U[i] = sc.NextInt() - 1;
            V[i] = sc.NextInt() - 1;
        }

        bool[] used = new bool[3 * N];
        var matching = new List<int>();

        for (int i = 0; i < M; i++)
        {
            if (used[U[i]] || used[V[i]]) continue;
            matching.Add(i);
            used[U[i]] = used[V[i]] = true;
        }

        if (matching.Count >= N)
        {
            Console.WriteLine("Matching");
            int[] ans = new int[N];
            for (int i = 0; i < N; i++)
            {
                ans[i] = matching[i] + 1;
            }

            Console.WriteLine(string.Join(" ", ans));
            return;
        }

        int ptr = 0;
        int[] indSet = new int[N];
        for (int i = 0; i < 3 * N && ptr < N; i++)
        {
            if (!used[i])
            {
                indSet[ptr++] = i + 1;
            }
        }

        Console.WriteLine("IndSet");
        Console.WriteLine(string.Join(" ", indSet));
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