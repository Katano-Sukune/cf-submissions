using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, M;
    string[] S;
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

        /*
         * グリッド
         * 
         * 単極磁石
         * すべての行、列に1つずつS磁石を置く
         * 
         * 活性化させるN磁石、S磁石選ぶ
         * 
         * 同じ行 or 列にある
         * 
         * N磁石がS磁石の方向に動く
         * 
         * N磁石が通過可能な位置は#になる
         * 
         * 可能なら最小のN磁石個数
         */
        if (N == 1 || M == 1)
        {
            // 全部#か全部.
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (S[i][j] != S[0][0])
                    {
                        Console.WriteLine("-1");
                        return;
                    }
                }
            }
            Console.WriteLine(S[0][0] == '#' ? 1 : 0);
            return;
        }

        // 無い行、無い列 0個ずつ or 1個以上

        // 無い行が存在する
        bool rr = false;
        bool cc = false;

        for (int i = 0; !rr && i < N; i++)
        {
            // i行目には存在しない
            bool r2 = true;
            for (int j = 0; j < M && r2; j++)
            {
                r2 &= S[i][j] == '.';
            }
            rr |= r2;
        }

        for (int j = 0; !cc && j < M; j++)
        {
            bool c2 = true;
            for (int i = 0; i < N && c2; i++)
            {
                c2 &= S[i][j] == '.';
            }

            cc |= c2;
        }

        if(rr != cc)
        {
            Console.WriteLine("-1");
            return;
        }

        for (int i = 0; i < N; i++)
        {
            // i行は存在しない or 連続して#
            bool f = false;
            for (int j = 1; j < M; j++)
            {
                if (S[i][j - 1] == '#' && S[i][j] == '.')
                {
                    f = true;
                }

                if (S[i][j - 1] == '.' && S[i][j] == '#' && f)
                {
                    Console.WriteLine("-1");
                    return;
                }
            }
        }

        for (int j = 0; j < M; j++)
        {
            bool f = false;
            for (int i = 1; i < N; i++)
            {
                if (S[i - 1][j] == '#' && S[i][j] == '.')
                {
                    f = true;
                }

                if (S[i - 1][j] == '.' && S[i][j] == '#' && f)
                {
                    Console.WriteLine("-1");
                    return;
                }
            }
        }

        int[] dx = new int[] { 1, 0, -1, 0 };
        int[] dy = new int[] { 0, 1, 0, -1 };
        // 連結成分の個数
        int ans = 0;
        bool[,] flag = new bool[N, M];
        var xq = new Queue<int>();
        var yq = new Queue<int>();
        for (int r = 0; r < N; r++)
        {
            for (int c = 0; c < M; c++)
            {
                if (S[r][c] != '#') continue;
                if (flag[r, c]) continue;
                ans++;
                yq.Enqueue(r);
                xq.Enqueue(c);
                flag[r, c] = true;
                while (xq.Count > 0)
                {
                    var yy = yq.Dequeue();
                    var xx = xq.Dequeue();
                    for (int i = 0; i < 4; i++)
                    {
                        int ny = yy + dy[i];
                        int nx = xx + dx[i];
                        if (ny < 0 || nx < 0) continue;
                        if (ny >= N || nx >= M) continue;
                        if (S[ny][nx] != '#') continue;
                        if (flag[ny, nx]) continue;
                        flag[ny, nx] = true;
                        yq.Enqueue(ny);
                        xq.Enqueue(nx);
                    }
                }
            }
        }

        Console.WriteLine(ans);
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
