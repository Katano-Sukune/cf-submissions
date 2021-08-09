using System;
using System.Collections.Generic;
using System.IO;
using CompLib.Util;

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

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        int[] ar = sc.IntArray();

        List<int>[] index = new List<int>[201];
        for (int i = 1; i <= 200; i++)
        {
            index[i] = new List<int>();
        }

        for (int i = 0; i < n; i++)
        {
            index[ar[i]].Add(i);
        }

        int[][] sum = new int[201][];
        for (int i = 1; i <= 200; i++)
        {
            sum[i] = new int[n + 1];
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 1; j <= 200; j++)
            {
                sum[j][i + 1] = sum[j][i];
            }

            sum[ar[i]][i + 1]++;
        }

        int ans = int.MinValue;
        // x = 0
        for (int b = 1; b <= 200; b++)
        {
            ans = Math.Max(ans, sum[b][n] - sum[b][0]);
        }

        for (int a = 1; a <= 200; a++)
        {
            for (int x = 1; x <= index[a].Count / 2; x++)
            {
                int max = int.MinValue;
                for (int b = 1; b <= 200; b++)
                {
                    max = Math.Max(max, sum[b][index[a][index[a].Count - x]] - sum[b][index[a][x - 1] + 1]);
                }

                ans = Math.Max(ans, x + max + x);
            }
        }

        Console.WriteLine(ans);
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