using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{
    int N, M, P;
    int[] S;
    string[] T;


    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        P = sc.NextInt();
        S = sc.IntArray();
        T = new string[N];
        for (int i = 0; i < N; i++)
        {
            T[i] = sc.Next();
        }

        int[,] table = new int[N, M];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                table[i, j] = -1;
            }
        }

        var q = new Queue<(int r, int c, int k)>[P];
        for (int i = 0; i < P; i++)
        {
            q[i] = new Queue<(int r, int c, int k)>();
        }

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                if ('1' <= T[i][j] && T[i][j] <= '9')
                {
                    int num = T[i][j] - '1';
                    q[num].Enqueue((i, j, 0));
                    table[i, j] = num;
                }
            }
        }

        int[] dx = new[] { 1, 0, -1, 0 };
        int[] dy = new[] { 0, 1, 0, -1 };

        while (true)
        {
            // 新しく更新があったか?
            bool flag = false;
            for (int i = 0; i < P; i++)
            {
                var tmpQ = new Queue<(int r, int c, int k)>();
                while (q[i].Count > 0)
                {
                    var d = q[i].Dequeue();
                    tmpQ.Enqueue((d.r, d.c, S[i]));
                }

                while (tmpQ.Count > 0 && tmpQ.Peek().k > 0)
                {
                    (int r, int c, int k) = tmpQ.Dequeue();
                    for (int j = 0; j < 4; j++)
                    {
                        int nr = r + dx[j];
                        int nc = c + dy[j];
                        if (nr < 0 || nc < 0 || nr >= N || nc >= M) continue;
                        if (table[nr, nc] != -1 || T[nr][nc] == '#') continue;
                        flag = true;
                        table[nr, nc] = i;
                        tmpQ.Enqueue((nr, nc, k - 1));
                    }
                }
                q[i] = tmpQ;
            }
            if (!flag) break;
        }

        int[] ans = new int[P];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                if (table[i, j] == -1) continue;
                ans[table[i, j]]++;
            }
        }

        Console.WriteLine(string.Join(" ", ans));
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
