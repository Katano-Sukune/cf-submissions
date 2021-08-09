using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, M;
    int[] B;
    int[] G;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        B = sc.IntArray();
        G = sc.IntArray();

        /*
         * N人男児
         * M人女児
         * 
         * それぞれ男児がそれぞれ女児にプレゼント
         * 
         * 男児iが女児に送った最小数 B_i
         * 
         * 女児j もらった最大数 G_j
         * 
         * 男子が送った最小合計
         */

        /*
         * 
         */

        Array.Sort(B, (l, r) => r.CompareTo(l));
        Array.Sort(G);
        int maxB = B[0];

        int minG = G[0];

        if (minG < maxB)
        {
            Console.WriteLine("-1");
            return;
        }

        // B 1位
        long ans = 0;
        for (int i = 0; i < N; i++)
        {
            ans += (long)B[i] * M;
        }
        for (int j = 1; j < M; j++)
        {
            ans += G[j] - B[0];
        }
        if (minG != maxB)
        {
            ans += G[0] - B[1];
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
