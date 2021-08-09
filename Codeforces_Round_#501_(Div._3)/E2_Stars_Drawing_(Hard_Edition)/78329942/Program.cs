using System;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{
    int N, M;
    string[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        A = new string[N];
        for (int i = 0; i < N; i++)
        {
            A[i] = sc.Next();
        }

        // 各*について 上下左右に連結してるやつ最小数える O(NM)

        // 数えたやつ 実際に埋める O(NM)

        int[,] u = new int[N, M];
        int[,] d = new int[N, M];
        int[,] l = new int[N, M];
        int[,] r = new int[N, M];

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                if (A[i][j] == '*')
                {
                    u[i, j] = (i == 0) ? 1 : u[i - 1, j] + 1;
                    l[i, j] = (j == 0) ? 1 : l[i, j-1] + 1;
                }
                int ii = N - i - 1;
                int jj = M - j - 1;
                if (A[ii][jj] == '*')
                {
                    d[ii, jj] = (i == 0) ? 1 : d[ii + 1, jj] + 1;
                    r[ii, jj] = (j == 0) ? 1 : r[ii, jj + 1] + 1;
                }
            }
        }

        int[,] imos = new int[N + 1, M + 1];
        int k = 0;
        var sb = new StringBuilder();
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                if (A[i][j] != '*') continue;
                int s = Math.Min(u[i, j], Math.Min(r[i, j], Math.Min(d[i, j], l[i, j]))) - 1;
                if (s == 0) continue;
                k++;
                sb.AppendLine($"{i + 1} {j + 1} {s}");
                imos[i - s, j]++;
                imos[i - s, j + 1]--;
                imos[i + s + 1, j]--;
                imos[i + s + 1, j + 1]++;

                imos[i, j - s]++;
                imos[i + 1, j - s]--;
                imos[i, j + s + 1]--;
                imos[i + 1, j + s + 1]++;
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
            for (int j = 0; j <= M; j++)
            {
                imos[i + 1, j] += imos[i, j];
            }
        }

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                if ((imos[i, j] > 0) ^ (A[i][j] == '*'))
                {
                    Console.WriteLine("-1");
                    return;
                }
            }
        }
        Console.WriteLine(k);
        Console.Write(sb);
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
