using System;
using System.IO;
using CompLib.Util;

public class Program
{
    private int N;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();

        // 1~N^2 Lab
        // i < jなら jからiに水を移動できる

        // NLab Nグループ に分ける


        int[][] ans = new int[N][];
        for (int i = 0; i < N; i++)
        {
            ans[i] = new int[N];
        }

        for (int i = 0; i < N * N; i++)
        {
            int y = i / N;
            int x = i % N;
            if (y % 2 == 1) x = N - 1 - x;

            ans[x][y] = i + 1;
        }

        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});

        for (int i = 0; i < N; i++)
        {
            Console.WriteLine(string.Join(" ", ans[i]));
        }

        Console.Out.Flush();
    }

    public static void Main(string[] args) => new Program().Solve();
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
                string s = Console.ReadLine();
                while (s.Length == 0)
                {
                    s = Console.ReadLine();
                }

                _line = s.Split(Separator);
                _index = 0;
            }

            return _line[_index++];
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