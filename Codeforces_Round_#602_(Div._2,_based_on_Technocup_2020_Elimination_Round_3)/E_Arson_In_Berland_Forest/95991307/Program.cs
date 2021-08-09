using System;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, M;
    private string[] S;

    private int[,] Sum;

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

        Sum = new int[N + 1, M + 1];
        for (int i = 0; i < N; i++)
        {
            var ln = new int[M + 1];
            for (int j = 0; j < M; j++)
            {
                ln[j + 1] = ln[j] + (S[i][j] == 'X' ? 1 : 0);
                Sum[i + 1, j + 1] = Sum[i, j + 1] + ln[j + 1];
            }
        }

        int ok = 0;
        int ng = N;
        while (ng - ok > 1)
        {
            int mid = (ok + ng) / 2;
            if (F(mid)) ok = mid;
            else ng = mid;
        }

        Console.WriteLine(ok);
        char[][] ans = new char[N][];
        for (int i = 0; i < N; i++)
        {
            ans[i] = new char[M];
            Array.Fill(ans[i], '.');
        }

        int t = 2 * ok + 1;
        for (int i = 0; i + t <= N; i++)
        {
            for (int j = 0; j + t <= M; j++)
            {
                int cnt = Sum[i + t, j + t] - Sum[i + t, j] - Sum[i, j + t] + Sum[i, j];
                if (cnt != t * t) continue;
                ans[i + ok][ j + ok] = 'X';
            }
        }

        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        for (int i = 0; i < N; i++)
        {
            Console.WriteLine(new string(ans[i]));
        }
        Console.Out.Flush();
    }

    bool F(int f)
    {
        int t = 2 * f + 1;
        int[,] imos = new int[N + 1, M + 1];
        for (int i = 0; i + t <= N; i++)
        {
            for (int j = 0; j + t <= M; j++)
            {
                int cnt = Sum[i + t, j + t] - Sum[i + t, j] - Sum[i, j + t] + Sum[i, j];
                if (cnt != t * t) continue;
                imos[i, j]++;
                imos[i + t, j]--;
                imos[i, j + t]--;
                imos[i + t, j + t]++;
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
                if ((S[i][j] == 'X') != (imos[i, j] > 0))
                {
                    return false;
                }
            }
        }

        return true;
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