using System;
using System.Collections;
using System.Collections.Generic;
using CompLib.Util;

public class Program
{
    private int N, M;
    private int[] A;

    public void Solve()
    {
        var sc = new Scanner();


        N = sc.NextInt();
        M = sc.NextInt();
        A = sc.IntArray();


        int[] rev = new int[1 << N];
        Array.Fill(rev, -1);
        for (int i = 0; i < M; i++)
        {
            int r = (1 << N) - 1 - A[i];
            rev[r] = i;
        }

        int ans = 0;
        BitArray f = new BitArray(M + (1 << N));
        int[] q = new int[M + (1 << N)];
        for (int i = 0; i < M; i++)
        {
            if (f[i]) continue;
            ans++;
            int begin = 0;
            int end = 0;
            q[end++] = i;
            f[i] = true;

            while (end - begin > 0)
            {
                int d = q[begin++];
                if (d < M)
                {
                    int to = A[d] + M;
                    if (f[to]) continue;
                    f[to] = true;
                    q[end++] = to;
                }
                else
                {
                    int cur = d - M;
                    if (rev[cur] != -1 && !f[rev[cur]])
                    {
                        f[rev[cur]] = true;
                        q[end++] = rev[cur];
                    }

                    for (int j = 0; j < N; j++)
                    {
                        if ((cur & (1 << j)) == 0)
                        {
                            int to = (cur | (1 << j)) + M;
                            if (f[to]) continue;
                            f[to] = true;
                            q[end++] = to;
                        }
                    }
                }
            }
        }

        Console.WriteLine(ans);
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