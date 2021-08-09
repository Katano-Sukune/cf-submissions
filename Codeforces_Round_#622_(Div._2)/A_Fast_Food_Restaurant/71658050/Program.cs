using CompLib.Util;
using System;
using System.Linq;
using System.Text;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        var t = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Q(sc.NextInt(), sc.NextInt(), sc.NextInt()));
        }

        Console.Write(sb.ToString());
    }

    private string Q(int a, int b, int c)
    {
        var dp = new int[5, 5, 5];
        for (int i = 0; i <= 4; i++)
        {
            for (int j = 0; j <= 4; j++)
            {
                for (int k = 0; k <= 4; k++)
                {
                    dp[i, j, k] = -1;
                }
            }
        }
        dp[0, 0, 0] = 0;

        for (int i = 1; i <= 7; i++)
        {
            var aa = (i & 1) > 0 ? 1 : 0;
            var bb = (i & 2) > 0 ? 1 : 0;
            var cc = (i & 4) > 0 ? 1 : 0;
            for (int aaa = 4; aaa >= 0; aaa--)
            {
                for (int bbb = 4; bbb >= 0; bbb--)
                {
                    for (int ccc = 4; ccc >= 0; ccc--)
                    {
                        if (dp[aaa, bbb, ccc] == -1) continue;
                        if (aaa + aa <= 4 && bbb + bb <= 4 && ccc + cc <= 4)
                        {
                            dp[aaa + aa, bbb + bb, ccc + cc] = Math.Max(dp[aaa + aa, bbb + bb, ccc + cc], dp[aaa, bbb, ccc] + 1);
                        }
                    }
                }
            }
        }

        int ans = 0;
        for (int i = 0; i <= 4; i++)
        {
            for (int j = 0; j <= 4; j++)
            {
                for (int k = 0; k <= 4; k++)
                {
                    if(i <= a && j <= b && k <= c)
                    {
                        ans = Math.Max(ans, dp[i, j, k]);
                    }
                }
            }
        }
        return ans.ToString();
    }

    public static void Main(string[] args) => new Program().Solve();
}

namespace CompLib.Util
{
    using System;
    using System.Linq;
    class Scanner
    {
        private int _index;
        private string[] _line;
        const char separator = ' ';

        public Scanner()
        {
            _index = 0;
            _line = new string[0];
        }

        public string Next()
        {
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(separator);
                _index = 0;
            }
            return _line[_index++];
        }
        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(separator);
            _index = _line.Length;
            return _line;
        }
        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
    }
}