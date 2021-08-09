using System;
using System.Linq;
using CompLib.Util;
using Microsoft.VisualBasic;

public class Program
{
    private int N, M;
    private string[] S;

    private int[,] SumX;

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


        SumX = new int[N + 1, M + 1];
        for (int i = 0; i < N; i++)
        {
            int[] line = new int[M + 1];
            for (int j = 0; j < M; j++)
            {
                line[j + 1] = line[j];
                if (S[i][j] == 'X') line[j + 1]++;
                SumX[i + 1, j + 1] = SumX[i, j + 1] + line[j + 1];
            }
        }

        // 森
        // Xのところが燃えた

        // 火をつけると 1分後に周囲8マス燃える

        // 燃えた結果がSになるように
        // 最初火をつけたところ、 何分燃えたか?

        int ok = 0;
        int ng = 1000000;

        while (ng - ok > 1)
        {
            int mid = (ok + ng) / 2;
            if (Check(mid))
            {
                ok = mid;
            }
            else
            {
                ng = mid;
            }
        }

        Console.WriteLine(ok);

        var ans = new char[N][];
        for (int i = 0; i < N; i++)
        {
            ans[i] = new char[M];
            for (int j = 0; j < M; j++)
            {
                ans[i][j] = '.';
            }
        }

        int tt = ok * 2 + 1;
        for (int i = 0; i <= N - tt; i++)
        {
            for (int j = 0; j <= M - tt; j++)
            {
                int cnt = SumX[i + tt, j + tt] - SumX[i + tt, j] - SumX[i, j + tt] + SumX[i, j];
                if (cnt == tt * tt)
                {
                    ans[i + ok][j + ok] = 'X';
                }
            }
        }

        Console.WriteLine(string.Join("\n",ans.Select(ar => new string(ar))));
    }


    bool Check(int t)
    {
        int[,] imos = new int[N + 1, M + 1];
        int tt = t * 2 + 1;
        if (tt > N || tt > M) return false;
        for (int i = 0; i <= N - tt; i++)
        {
            for (int j = 0; j <= M - tt; j++)
            {
                // 範囲内 xいくつあるか?
                int cnt = SumX[i + tt, j + tt] - SumX[i + tt, j] - SumX[i, j + tt] + SumX[i, j];
                if (cnt == tt * tt)
                {
                    imos[i, j]++;
                    imos[i + tt, j]--;
                    imos[i, j + tt]--;
                    imos[i + tt, j + tt]++;
                }
            }
        }

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j <= M; j++)
            {
                imos[i + 1, j] += imos[i, j];
            }
        }

        for (int i = 0; i <= N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                imos[i, j + 1] += imos[i, j];
            }
        }

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                if ((S[i][j] == 'X') ^ (imos[i, j] > 0))
                {
                    return false;
                }
            }
        }


        return true;
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