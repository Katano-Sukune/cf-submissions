using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        string[][] s = new string[4][];
        for (int i = 0; i < 4; i++)
        {
            s[i] = new string[n];
            for (int j = 0; j < n; j++)
            {
                s[i][j] = sc.Next();
            }
        }

        // iの左上が0,1での変更個数

        int[][] cnt = new int[4][];
        for (int i = 0; i < 4; i++)
        {
            cnt[i] = new int[2];
            var t = C(s[i]);
            cnt[i][0] = t.z;
            cnt[i][1] = t.o;
        }

        var dp = new int[5, 3, 3];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    dp[i, j, k] = int.MaxValue;
                }
            }
        }
        dp[0, 0, 0] = 0;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (dp[i, j, k] == int.MaxValue) continue;
                    if (j + 1 <= 2)
                    {
                        dp[i + 1, j + 1, k] = Math.Min(dp[i + 1, j + 1, k], dp[i, j, k] + cnt[i][0]);
                    }
                    if (k + 1 <= 2)
                    {
                        dp[i + 1, j, k + 1] = Math.Min(dp[i + 1, j, k + 1], dp[i, j, k] + cnt[i][1]);
                    }
                }
            }
        }

        Console.WriteLine(dp[4, 2, 2]);
    }

    (int z, int o) C(string[] s)
    {
        int z = 0;
        int o = 0;
        for (int i = 0; i < s.Length; i++)
        {
            for (int j = 0; j < s[0].Length; j++)
            {
                if (s[i][j] - '0' != (i + j) % 2)
                {
                    z++;
                }
                else
                {
                    o++;
                }
            }
        }

        return (z, o);
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
