using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, M;
    private int Q;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();

        var I = new HashSet<int>[N];
        for (int i = 0; i < N; i++)
        {
            I[i] = new HashSet<int>();
        }

        int[] O = new int[N];

        for (int i = 0; i < M; i++)
        {
            int a = sc.NextInt() - 1;
            int b = sc.NextInt() - 1;

            int min = Math.Min(a, b);
            int max = Math.Max(a, b);
            I[min].Add(max);
            O[max]++;
        }

        Q = sc.NextInt();


        long cnt = 0;
        for (int i = 0; i < N; i++)
        {
            cnt += (long) I[i].Count * O[i];
        }

        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        Console.WriteLine(cnt);
        for (int i = 0; i < Q; i++)
        {
            int v = sc.NextInt() - 1;
            cnt -= (long) I[v].Count * O[v];
            foreach (var f in I[v].ToArray())
            {
                cnt += O[f] - I[f].Count - 1;
                // (a + 1) (b - 1) - ab
                // b - a - 1
                O[v]++;
                I[f].Add(v);
                O[f]--;
                I[v].Remove(f);
            }

            Console.WriteLine(cnt);
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