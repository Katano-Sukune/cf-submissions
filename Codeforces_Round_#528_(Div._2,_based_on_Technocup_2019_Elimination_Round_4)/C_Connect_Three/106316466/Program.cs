using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private const int L = 1001;
    private (int x, int y)[] P;
    private int[] dx = new int[] {0, 1, 0, -1};
    private int[] dy = new int[] {-1, 0, 1, 0};

    public void Solve()
    {
        var sc = new Scanner();
        P = new (int x, int y)[3];
        for (int i = 0; i < 3; i++)
        {
            P[i] = (sc.NextInt(), sc.NextInt());
        }

        /*
         * 0,1の間の長方形
         *
         * 2から一番近いところ
         */
        int[,] t = new int[L, L];
        for (int i = 0; i < L; i++)
        {
            for (int j = 0; j < L; j++)
            {
                t[i, j] = int.MaxValue;
            }
        }

        var q = new Queue<(int x, int y)>();
        q.Enqueue(P[0]);
        t[P[0].x, P[0].y] = 0;
        while (q.Count > 0)
        {
            (int x, int y) = q.Dequeue();
            for (int i = 0; i < 4; i++)
            {
                int nx = x + dx[i];
                int ny = y + dy[i];
                if (nx < 0 || ny < 0 || nx >= L || ny >= L) continue;
                if (t[nx, ny] != int.MaxValue) continue;
                t[nx, ny] = t[x, y] + 1;
                q.Enqueue((nx, ny));
            }
        }

        int min = int.MaxValue;
        int tx = -1;
        int ty = -1;
        for (int xx = Math.Min(P[1].x, P[2].x); xx <= Math.Max(P[1].x, P[2].x); xx++)
        {
            for (int yy = Math.Min(P[1].y, P[2].y); yy <= Math.Max(P[1].y, P[2].y); yy++)
            {
                if (t[xx, yy] < min)
                {
                    tx = xx;
                    ty = yy;
                    min = t[xx, yy];
                }
            }
        }

        var res = new HashSet<(int x, int y)>();
        for (int i = 0; i < 3; i++)
        {
            var f = F((tx, ty), P[i]);
            foreach ((int x, int y) tuple in f)
            {
                res.Add(tuple);
            }
        }

        Console.WriteLine(res.Count);
        foreach ((int x, int y) in res)
        {
            Console.WriteLine($"{x} {y}");
        }
    }

    HashSet<(int x, int y)> F((int x, int y) p1, (int x, int y) p2)
    {
        var res = new HashSet<(int x, int y)>();
        if (p1.x < p2.x)
        {
            for (int i = p1.x; i <= p2.x; i++)
            {
                res.Add((i, p1.y));
            }
        }
        else
        {
            for (int i = p2.x; i <= p1.x; i++)
            {
                res.Add((i, p1.y));
            }
        }

        if (p1.y < p2.y)
        {
            for (int i = p1.y; i <= p2.y; i++)
            {
                res.Add((p2.x, i));
            }
        }
        else
        {
            for (int i = p2.y; i <= p1.y; i++)
            {
                res.Add((p2.x, i));
            }
        }

        return res;
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