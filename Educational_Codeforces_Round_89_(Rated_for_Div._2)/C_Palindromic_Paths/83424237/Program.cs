using System;
using System.IO;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        var sc = new Scanner();
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
        int m = sc.NextInt();

        int[][] a = new int[n][];
        for (int i = 0; i < n; i++)
        {
            a[i] = sc.IntArray();
        }

        // (1,1) -> (n,m)のパスが回文になるように

        int[,] cnt = new int[n + m - 1, 2];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                int s = i + j;
                int t = (n - 1 - i) + (m - 1 - j);
                cnt[Math.Min(s, t), a[i][j]]++;
            }
        }

        int ans = 0;
        for (int i = 0; i < n + m - 1; i++)
        {
            if ((n + m - 1) % 2 == 1 && i == (n + m - 1) / 2) continue;

            ans += Math.Min(cnt[i, 0], cnt[i, 1]);
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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
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
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}