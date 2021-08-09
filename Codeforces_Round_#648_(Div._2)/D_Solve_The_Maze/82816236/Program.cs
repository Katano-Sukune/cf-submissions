using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{
    int[] Dx = new int[] { 0, 1, 0, -1 };
    int[] Dy = new int[] { 1, 0, -1, 0 };
    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < t; i++)
        {
            int n = sc.NextInt();
            int m = sc.NextInt();
            var s = new char[n][];
            for (int j = 0; j < n; j++)
            {
                s[j] = sc.NextCharArray();
            }

            sb.AppendLine(Q(n, m, s));
        }



        Console.Write(sb);
    }

    string Q(int n, int m, char[][] s)
    {
        // G いい人
        // B わるいひと
        // . 空 # 壁
        // 空マスに壁を置いて　いい人は全員 n,mに移動できてわるいひとはできないようにできるか?

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (s[i][j] == 'B')
                {
                    for (int k = 0; k < 4; k++)
                    {
                        int y = i + Dy[k];
                        int x = j + Dx[k];
                        if (0 <= y && y < n && 0 <= x && x < m && (s[y][x] != 'B') && s[y][x] != 'G')
                        {
                            s[y][x] = '#';
                        }
                    }
                }
            }
        }

        bool[,] f = new bool[n, m];
        if (s[n - 1][m - 1] != '#')
        {
            var xq = new Queue<int>();
            var yq = new Queue<int>();
            yq.Enqueue(n - 1);
            xq.Enqueue(m - 1);
            f[n - 1, m - 1] = true;

            while (xq.Count > 0)
            {
                int xx = xq.Dequeue();
                int yy = yq.Dequeue();
                for (int i = 0; i < 4; i++)
                {
                    int nx = xx + Dx[i];
                    int ny = yy + Dy[i];
                    if (nx < 0 || nx >= m) continue;
                    if (ny < 0 || ny >= n) continue;
                    if (s[ny][nx] == '#') continue;
                    if (f[ny, nx]) continue;
                    f[ny, nx] = true;
                    yq.Enqueue(ny);
                    xq.Enqueue(nx);
                }
            }
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (s[i][j] == 'G' && !f[i, j]) return "NO";
                if (s[i][j] == 'B' && f[i, j]) return "NO";
            }
        }

        return "YES";
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
