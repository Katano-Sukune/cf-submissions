using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    char[] S;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        S = sc.NextCharArray();
        int[] map = new int[256];
        map['R'] = 0;
        map['G'] = 1;
        map['B'] = 2;
        int[,] dp = new int[N, 3];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                dp[i, j] = int.MaxValue;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            dp[0, i] = 1;
        }
        dp[0, map[S[0]]] = 0;

        for (int i = 1; i < N; i++)
        {
            for (int prev = 0; prev < 3; prev++)
            {
                for (int cur = 0; cur < 3; cur++)
                {
                    if (prev == cur) continue;
                    dp[i, cur] = Math.Min(dp[i, cur], dp[i - 1, prev] + (map[S[i]] == cur ? 0 : 1));
                }
            }
        }

        int p = -1;
        int min = int.MaxValue;
        for (int i = 0; i < 3; i++)
        {
            if (dp[N - 1, i] < min)
            {
                p = i;
                min = dp[N - 1, i];
            }
        }

        string rgb = "RGB";
        var t = new char[N];
        t[N - 1] = rgb[p];
        for (int i = N - 2; i >= 0; i--)
        {
            for (int cur = 0; cur < 3; cur++)
            {
                if (cur == p) continue;
                if(dp[i+1,p] == dp[i,cur] + (map[S[i+1]] == p? 0 : 1))
                {
                    p = cur;
                    break;
                }
            }
            t[i] = rgb[p];
        }

        Console.WriteLine(min);
        Console.WriteLine(new string(t));
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
