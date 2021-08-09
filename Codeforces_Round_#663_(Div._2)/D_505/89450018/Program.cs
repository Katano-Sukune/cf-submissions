using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    private int N;
    private int M;
    private int H, W;
    private string[] S;
    private int[][] T;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        S = new string[N];
        for (int i = 0; i < N; i++)
        {
            S[i] = sc.Next();
        }

        if (N >= 4 && M >= 4)
        {
            Console.WriteLine("-1");
            return;
        }

        
        if (N <= 3)
        {
            H = N;
            W = M;
            T = new int[H][];
            for (int i = 0; i < H; i++)
            {
                T[i] = new int[W];
                for (int j = 0; j < W; j++)
                {
                    T[i][j] = S[i][j] - '0';
                }
            }
        }
        else
        {
            H = M;
            W = N;
            T = new int[H][];
            for (int i = 0; i < H; i++)
            {
                T[i] = new int[W];
                for (int j = 0; j < W; j++)
                {
                    T[i][j] = S[j][i] - '0';
                }
            }
        }

        if (H == 1)
        {
            Console.WriteLine("0");
            return;
        }
        long ans = 0;
        long[] dp;
        if (H == 2)
        {
            dp = new long[2];
            for (int i = 0; i < W; i++)
            {
                int s = (T[0][i] + T[1][i]) % 2;
                for (int j = 0; j < 2; j++)
                {
                    if ((i + j) % 2 != s) dp[j]++;
                }
            }
        }
        else
        {
            dp = new long[4];
            for (int i = 0; i < W; i++)
            {
                int s = (T[0][i] + T[1][i]) % 2;
                int t = (T[1][i] + T[2][i]) % 2;
                for (int j = 0; j < 4; j++)
                {
                    bool f1 = ((j % 2) + i) % 2 == s;
                    bool f2 = ((j / 2) + i) % 2 == t;
                    if (!f1 || !f2) dp[j]++;
                }
            }
        }

        Console.WriteLine(dp.Min());
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