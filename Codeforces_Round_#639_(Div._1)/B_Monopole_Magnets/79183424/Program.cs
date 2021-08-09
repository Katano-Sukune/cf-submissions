using System;
using System.Collections;
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

        // n*mグリッド
        // N極のみ、S極のみの磁石を適当に置く
        // 同じ行 or 列 のN,S磁石を活性化させる
        // N磁石がS磁石に1つ移動する

        // すべての行、列に少なくとも1つ S磁石がある
        // マスが黒だと 操作をするとN磁石が通る可能性がある
        // 白だと絶対にこない

        // N磁石の個数最小値

        bool aa = false;
        for (int i = 0; i < N; i++)
        {
            int cnt = S[i][0] == '#' ? 1 : 0;
            for (int j = 1; j < M; j++)
            {
                if (S[i][j - 1] == '.' && S[i][j] == '#') cnt++;
            }
            if (cnt > 1)
            {
                Console.WriteLine("-1");
                return;
            }
            if (cnt == 0) aa = true;
        }

        bool bb = false;
        for (int j = 0; j < M; j++)
        {
            int cnt = S[0][j] == '#' ? 1 : 0;
            for (int i = 1; i < N; i++)
            {
                if (S[i - 1][j] == '.' && S[i][j] == '#') cnt++;
            }
            if (cnt > 1)
            {
                Console.WriteLine("-1");
                return;
            }
            if (cnt == 0) bb = true;
        }

        // 0個のやつ
        // 行、列片方に0個のやつがある 不可能

        if (aa ^ bb)
        {
            Console.WriteLine("-1");
            return;
        }
        // 連結成分
        int ans = 0;
        int[] dx = new int[] { 1, 0, -1, 0 };
        int[] dy = new int[] { 0, 1, 0, -1 };
        {
            bool[,] flag = new bool[N, M];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (flag[i, j] || S[i][j] == '.') continue;
                    ans++;
                    var xq = new Queue<int>();
                    var yq = new Queue<int>();
                    flag[i, j] = true;
                    yq.Enqueue(i);
                    xq.Enqueue(j);
                    while (yq.Count > 0)
                    {
                        var yy = yq.Dequeue();
                        var xx = xq.Dequeue();
                        for (int k = 0; k < 4; k++)
                        {
                            int ny = yy + dy[k];
                            int nx = xx + dx[k];
                            if (ny < 0 || ny >= N) continue;
                            if (nx < 0 || nx >= M) continue;
                            if (S[ny][nx] == '.') continue;
                            if (flag[ny, nx]) continue;
                            flag[ny, nx] = true;
                            yq.Enqueue(ny);
                            xq.Enqueue(nx);
                        }
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
