using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private int[] A;
    private int M;
    private int[] B;

    public void Solve()
    {
        checked
        {

            var sc = new Scanner();
            N = sc.NextInt();
            A = sc.IntArray();
            M = sc.NextInt();
            B = sc.IntArray();

            long sA = 0;
            foreach (int i in A)
            {
                sA += i;
            }

            long sB = 0;
            foreach (int i in B)
            {
                sB += i;
            }

            long v = Math.Abs(sB - sA);
            (int x, int y)[] p = new (int x, int y)[0];

            long v0 = sB - sA;

            // A_i, B_j入れ替える
            // 2(A_i - B_j) 増える
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < M; j++)
                    {
                        long tmp = v0 + 2L * (A[i] - B[j]);
                        if (Math.Abs(tmp) < v)
                        {
                            v = Math.Abs(tmp);
                            p = new[] {(i, j)};
                        }
                    }
                }
            }

            {
                var ls = new List<(int j1, int j2, long n)>();
                for (int j1 = 0; j1 < M; j1++)
                {
                    for (int j2 = j1 + 1; j2 < M; j2++)
                    {
                        ls.Add((j1, j2, (long) -(B[j1] + B[j2])));
                    }
                }

                ls.Sort((l, r) => l.n.CompareTo(r.n));

                for (int i1 = 0; i1 < N; i1++)
                {
                    for (int i2 = i1 + 1; i2 < N; i2++)
                    {
                        long tmp1 = v0 + 2L * (A[i1] + A[i2]);

                        // tmp1 + ls[v] の絶対値を0に近づける
                        int ok = -1;
                        int ng = ls.Count;
                        while (ng - ok > 1)
                        {
                            int mid = (ok + ng) / 2;
                            if (tmp1 + 2 * ls[mid].n < 0) ok = mid;
                            else ng = mid;
                        }

                        if (0 <= ok && ok < ls.Count)
                        {
                            long tmp2 = tmp1 + 2L * ls[ok].n;
                            if (Math.Abs(tmp2) < v)
                            {
                                // + 4
                                // 
                                v = Math.Abs(tmp2);
                                p = new[] {(i1, ls[ok].j1), (i2, ls[ok].j2)};
                            }
                        }

                        if (0 <= ok + 1 && ok + 1 < ls.Count)
                        {
                            long tmp3 = tmp1 + 2L * ls[ok + 1].n;
                            if (Math.Abs(tmp3) < v)
                            {
                                v = Math.Abs(tmp3);
                                p = new[] {(i1, ls[ok + 1].j1), (i2, ls[ok + 1].j2)};
                            }
                        }
                    }
                }
            }

            Console.WriteLine(v);
            Console.WriteLine(p.Length);
            for (int i = 0; i < p.Length; i++)
            {
                Console.WriteLine($"{p[i].x + 1} {p[i].y + 1}");
            }
        }
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