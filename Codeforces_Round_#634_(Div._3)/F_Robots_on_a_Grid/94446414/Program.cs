using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        // Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        // Console.Out.Flush();
    }

    private string Str = "URDL";
    private int[] DY = new int[] {-1, 0, 1, 0};
    private int[] DX = new int[] {0, 1, 0, -1};

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        int m = sc.NextInt();

        int[][] c = new int[n][];
        for (int i = 0; i < n; i++)
        {
            c[i] = sc.Next().Select(ch => ch - '0').ToArray();
        }

        char[][] s = new char[n][];
        for (int i = 0; i < n; i++)
        {
            s[i] = sc.NextCharArray();
        }

        // c 色 0黒 1白
        // s 移動する方向

        // いくつかのマスにロボット置く
        // ぶつからない位置に置く

        // ロボットの個数最大のうち、黒マスの個数最大

        // 連結成分
        // 奇数、偶数位置に置く、

        bool[,] flag = new bool[n, m];
        int[,] mod = new int[n, m];

        int[,] t = new int[n, m];
        int robot = 0;
        int black = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (flag[i, j]) continue;
                // 進む
                int p = 1;
                int y = i;
                int x = j;

                while (t[y, x] == 0)
                {
                    t[y, x] = p;
                    int idx = Str.IndexOf(s[y][x]);
                    y += DY[idx];
                    x += DX[idx];
                    p++;
                }

                int loop = p - t[y, x];

                bool[] b = new bool[loop];

                var q = new Queue<(int y, int x)>();
                q.Enqueue((y, x));
                b[0] |= c[y][x] == 0;
                mod[y, x] = 0;

                while (q.Count > 0)
                {
                    (int yy, int xx) = q.Dequeue();
                    for (int k = 0; k < 4; k++)
                    {
                        int ny = yy + DY[k];
                        int nx = xx + DX[k];
                        if (ny < 0 || nx < 0) continue;
                        if (ny >= n || nx >= m) continue;
                        if (flag[ny, nx]) continue;
                        if (Math.Abs(k - Str.IndexOf(s[ny][nx])) != 2) continue;
                        flag[ny, nx] = true;
                        mod[ny, nx] = (mod[yy, xx] + 1) % loop;
                        b[mod[ny, nx]] |= c[ny][nx] == 0;
                        q.Enqueue((ny,nx));
                    }
                }

                robot += loop;
                black += b.Count(f => f);
            }

        }

        Console.WriteLine($"{robot} {black}");
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