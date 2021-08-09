using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, A, B;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.NextInt();
        B = sc.NextInt();

        /*
         * n頂点無向グラフG
         * 
         * Gの連結成分a個
         * 
         * Gの補グラフ連結成分b個
         */

        /*
         */

        // どっちか1
        if (A != 1 && B != 1)
        {
            Console.WriteLine("NO");
            return;
        }
        if (N == 2 && A == 1 && B == 1)
        {
            Console.WriteLine("NO");
            return;
        }
        if (N == 3 && A == 1 && B == 1)
        {
            Console.WriteLine("NO");
            return;
        }

        int[][] ans = new int[N][];
        for (int i = 0; i < N; i++)
        {
            ans[i] = new int[N];
        }

        if (A >= B)
        {
            for (int i = 0; i < N - A; i++)
            {
                ans[i][i + 1] = 1;
                ans[i + 1][i] = 1;
            }
        }
        else
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (i == j) continue;
                    ans[i][j] = 1;
                }
            }
            for (int i = 0; i < N - B; i++)
            {
                ans[i][i + 1] = 0;
                ans[i + 1][i] = 0;
            }
        }
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        Console.WriteLine("YES");
        for (int i = 0; i < N; i++)
        {
            Console.WriteLine(string.Join("", ans[i]));
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
