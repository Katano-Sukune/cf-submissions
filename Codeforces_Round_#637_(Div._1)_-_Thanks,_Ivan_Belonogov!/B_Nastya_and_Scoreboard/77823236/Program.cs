using System;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{
    int N, K;
    string[] S;

    readonly string[] SS =
    {
        "1110111", "0010010", "1011101", "1011011", "0111010", "1101011", "1101111", "1010010", "1111111", "1111011"
    };
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        S = new string[N];

        // s[i] -> jに行くために何本使うか
        int[,] to = new int[N, 10];
        for (int i = 0; i < N; i++)
        {
            S[i] = sc.Next();
            for (int j = 0; j <= 9; j++)
            {
                bool f = true;
                int cnt = 0;
                for (int k = 0; k < 7; k++)
                {
                    if (S[i][k] == '1' && SS[j][k] == '0')
                    {
                        f = false;
                        break;
                    }
                    if (S[i][k] == '0' && SS[j][k] == '1') cnt++;
                }
                to[i, j] = f ? cnt : -1;
            }
        }

        // 999999...8888

        // 9999999... 9にできない 8にする

        // 各桁 一番近い 

        // 99999999 最後まで行く 余る -1

        // indexが小さい方が上の桁

        // 下i桁,残り
        var dp = new bool[N + 1, K + 1];
        dp[0, K] = true;
        for (int i = 0; i < N; i++)
        {
            int ii = N - i - 1;
            for (int j = 0; j <= K; j++)
            {
                if (!dp[i, j]) continue;
                for (int k = 0; k <= 9; k++)
                {
                    if (to[ii, k] == -1) continue;
                    if (j - to[ii, k] < 0) continue;
                    dp[i + 1, j - to[ii, k]] = true;
                }
            }
        }

        if (!dp[N, 0])
        {
            Console.WriteLine("-1");
            return;
        }

        int p = 0;
        char[] ans = new char[N];
        for (int i = 0; i < N; i++)
        {
            for (int j = 9; j >= 0; j--)
            {
                if (to[i, j] == -1) continue;
                int next = p + to[i, j];
                if (next > K) continue;
                if (dp[N - 1 - i, next])
                {
                    ans[i] = (char)(j + '0');
                    p = next;
                    break;
                }
            }
        }

        Console.WriteLine(new string(ans));
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
