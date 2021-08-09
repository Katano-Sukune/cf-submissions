using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private string[] S;

    public void Solve()
    {
        var sc = new Scanner();
        S = new string[2];
        for (int i = 0; i < 2; i++)
        {
            S[i] = $"XX{sc.Next()}X";
        }

        int n = S[0].Length;
        var dp = new int[n, 16];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                dp[i, j] = int.MinValue;
            }
        }

        dp[0, 15] = 0;
        for (int i = 0; i < n - 2; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                int cur = dp[i, j];
                if (cur == int.MinValue) continue;
                int next = 0;
                for (int k = 0; k < 2; k++)
                {
                    if (S[k][i + 2] == 'X') next |= 1 << k;
                }

                {
                    ref int to = ref dp[i + 1, ((j % 4) << 2) | next];
                    to = Math.Max(to, cur);
                }

                for (int k = 0; k < 4; k++)
                {
                    int block = (1 << 4) - 1 - (1 << k);
                    if ((j & block) != 0) continue;
                    ref int to = ref dp[i + 1, (((j | block) % 4) << 2) | next];
                    to = Math.Max(to, cur + 1);
                }
            }
        }

        int ans = 0;
        for (int i = 0; i < 16; i++)
        {
            ans = Math.Max(ans, dp[n - 2, i]);
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