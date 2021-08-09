using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    int N;
    int[][] A;
    void Q(Scanner sc)
    {
        N = sc.NextInt();
        A = new int[2][];
        for (int i = 0; i < 2; i++)
        {
            A[i] = sc.IntArray();
        }


        List<int>[] ls = new List<int>[N + 1];
        for (int i = 1; i <= N; i++)
        {
            ls[i] = new List<int>();
        }
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < N; j++)
            {
                ls[A[i][j]].Add(j);
            }
        }
        for (int i = 1; i <= N; i++)
        {
            if (ls[i].Count != 2)
            {
                Console.WriteLine("-1");
                return;
            }
        }

        var ans = new List<int>();
        bool[] f = new bool[N];
        for (int i = 0; i < N; i++)
        {
            if (f[i]) continue;
            if (A[0][i] == A[1][i]) continue;
            // 上 A[0][i]
            // 下   1
            f[i] = true;
            int cur = i;
            bool rev = false;
            var w = new List<int>();
            var b = new List<int>();
            do
            {
                // 下にあるやつ
                int u = rev ? A[0][cur] : A[1][cur];

                // uがある位置
                int to = ls[u][0] == cur ? ls[u][1] : ls[u][0];

                // Console.WriteLine($"{cur} {u} {to}");
                f[to] = true;

                if (A[1][to] == u)
                {
                    rev = true;
                    b.Add(to + 1);
                }
                else
                {
                    rev = false;
                    w.Add(to + 1);
                }
                cur = to;
            } while (cur != i);
            if (b.Count < w.Count)
            {
                ans.AddRange(b);
            }
            else
            {
                ans.AddRange(w);
            }
        }
        Console.WriteLine(ans.Count);
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
