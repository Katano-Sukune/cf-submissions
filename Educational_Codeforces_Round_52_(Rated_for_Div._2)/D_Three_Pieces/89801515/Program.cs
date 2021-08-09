using System;
using System.Collections.Generic;
using CompLib.Util;

public class Program
{
    private int N;
    private int[,] A;

    private int[] X, Y;

    private const int K = 3;

    private int[] kDx = new int[] {-2, -2, -1, -1, 1, 1, 2, 2};
    private int[] kDy = new int[] {-1, 1, -2, 2, -2, 2, -1, 1};

    private int[] rDx = new int[] {-1, 0, 0, 1};
    private int[] rDy = new int[] {0, 1, -1, 0};

    private int[] bDx = new int[] {1, 1, -1, -1};
    private int[] bDy = new int[] {-1, 1, -1, 1};

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = new int[N, N];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                A[i, j] = sc.NextInt() - 1;
            }
        }

        X = new int[N * N];
        Y = new int[N * N];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                X[A[i, j]] = i;
                Y[A[i, j]] = j;
            }
        }

        var step = new int[N, N, N * N + 1, K];
        var r = new int[N, N, N * N + 1, K];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                for (int k = 0; k <= N * N; k++)
                {
                    for (int l = 0; l < K; l++)
                    {
                        step[i, j, k, l] = int.MaxValue;
                    }
                }
            }
        }

        var q = new Queue<(int x, int y, int t, int k)>();
        for (int i = 0; i < K; i++)
        {
            step[X[0], Y[0], 1, i] = 0;
            q.Enqueue((X[0], Y[0], 1, i));
        }

        while (q.Count > 0)
        {
            var d = q.Dequeue();
            int curS = step[d.x, d.y, d.t, d.k];
            int curR = r[d.x, d.y, d.t, d.k];
            // 交換
            for (int i = 0; i < K; i++)
            {
                if (d.k == i) continue;
                ref int nextS = ref step[d.x, d.y, d.t, i];
                ref int nextR = ref r[d.x, d.y, d.t, i];
                if (curS + 1 < nextS || (curS + 1 == nextS && curR + 1 < nextR))
                {
                    nextS = curS + 1;
                    nextR = curR + 1;
                    q.Enqueue((d.x, d.y, d.t, i));
                }
            }

            // 移動
            if (d.k == 0)
            {
                // ナイト
                for (int i = 0; i < kDx.Length; i++)
                {
                    int nx = d.x + kDx[i];
                    int ny = d.y + kDy[i];
                    if (nx < 0 || nx >= N) continue;
                    if (ny < 0 || ny >= N) continue;
                    int nt = A[nx, ny] == d.t ? d.t + 1 : d.t;

                    ref int nextS = ref step[nx, ny, nt, 0];
                    ref int nextR = ref r[nx, ny, nt, 0];
                    if (curS + 1 < nextS || (curS + 1 == nextS && curR < nextR))
                    {
                        nextS = curS + 1;
                        nextR = curR;
                        q.Enqueue((nx, ny, nt, 0));
                    }
                }
            }
            else if (d.k == 1)
            {
                // ビショップ ななめ
                for (int i = 0; i < bDx.Length; i++)
                {
                    for (int j = 1;; j++)
                    {
                        int nx = d.x + bDx[i] * j;
                        int ny = d.y + bDy[i] * j;
                        if (nx < 0 || nx >= N) break;
                        if (ny < 0 || ny >= N) break;

                        int nt = A[nx, ny] == d.t ? d.t + 1 : d.t;

                        ref int nextS = ref step[nx, ny, nt, 1];
                        ref int nextR = ref r[nx, ny, nt, 1];
                        if (curS + 1 < nextS || (curS + 1 == nextS && curR < nextR))
                        {
                            nextS = curS + 1;
                            nextR = curR;
                            q.Enqueue((nx, ny, nt, 1));
                        }
                    }
                }
            }
            else if (d.k == 2)
            {
                // ルーク　たてよこ
                for (int i = 0; i < rDx.Length; i++)
                {
                    for (int j = 1;; j++)
                    {
                        int nx = d.x + rDx[i] * j;
                        int ny = d.y + rDy[i] * j;
                        if (nx < 0 || nx >= N) break;
                        if (ny < 0 || ny >= N) break;

                        int nt = A[nx, ny] == d.t ? d.t + 1 : d.t;

                        ref int nextS = ref step[nx, ny, nt, 2];
                        ref int nextR = ref r[nx, ny, nt, 2];
                        if (curS + 1 < nextS || (curS + 1 == nextS && curR < nextR))
                        {
                            nextS = curS + 1;
                            nextR = curR;
                            q.Enqueue((nx, ny, nt, 2));
                        }
                    }
                }
            }
        }

        // ゴール
        int ansS = int.MaxValue;
        int ansR = int.MaxValue;
        int lx = X[N * N - 1];
        int ly = Y[N * N - 1];
        for (int i = 0; i < 3; i++)
        {
            int ss = step[lx, ly, N * N, i];
            int rr = r[lx, ly, N * N, i];

            if (ss < ansS || (ss == ansS && rr < ansR))
            {
                ansS = ss;
                ansR = rr;
            }
        }

        Console.WriteLine($"{ansS} {ansR}");
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