using System;
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
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    private int N, M;
    private bool[][] T;
    private List<string> Ans;

    void F1(int i, int j)
    {
        if (T[i][j] && T[i][j + 1])
        {
            Ans.Add($"{i + 1} {j + 1} {i + 1} {j + 2} {i + 2} {j + 1}");
            T[i][j] ^= true;
            T[i][j + 1] ^= true;
            T[i + 1][j] ^= true;
        }
        else if (T[i][j])
        {
            Ans.Add($"{i + 1} {j + 1} {i + 2} {j + 1} {i + 2} {j + 2}");
            T[i][j] ^= true;
            T[i + 1][j] ^= true;
            T[i + 1][j + 1] ^= true;
        }
        else if (T[i][j + 1])
        {
            Ans.Add($"{i + 1} {j + 2} {i + 2} {j + 1} {i + 2} {j + 2}");
            T[i][j + 1] ^= true;
            T[i + 1][j] ^= true;
            T[i + 1][j + 1] ^= true;
        }
    }

    void F2(int i, int j)
    {
        List<(int, int)> ii = new List<(int, int)>();
        List<(int, int)> oo = new List<(int, int)>();
        for (int k = 0; k <= 1; k++)
        {
            for (int l = 0; l <= 1; l++)
            {
                if (T[i + k][j + l]) ii.Add((i + k, j + l));
                else oo.Add((i + k, j + l));
            }
        }

        if (ii.Count == 0) return;

        (int x, int y)[] a = new (int x, int y)[3];
        if (ii.Count == 4 || ii.Count == 3)
        {
            for (int k = 0; k < 3; k++)
            {
                a[k] = ii[k];
            }
        }
        else
        {
            a[0] = ii[0];
            a[1] = oo[0];
            a[2] = oo[1];
        }

        for (int k = 0; k < 3; k++)
        {
            T[a[k].x][a[k].y] ^= true;
        }

        string[] tmp = new string[3];
        for (int k = 0; k < 3; k++)
        {
            tmp[k] = $"{a[k].x + 1} {a[k].y + 1}";
        }

        Ans.Add(string.Join(" ", tmp));
        F2(i, j);
    }

    void Q(Scanner sc)
    {
        N = sc.NextInt();
        M = sc.NextInt();
        T = new bool[N][];
        for (int i = 0; i < N; i++)
        {
            T[i] = sc.Next().Select(ch => ch == '1').ToArray();
        }

        Ans = new List<string>();
        // 4 1 2 3 0
        for (int i = 0; i < N - 2; i++)
        {
            for (int j = 0; j + 1 < M; j += 2)
            {
                F1(i, j);
            }

            if (M % 2 == 1)
            {
                F1(i, M - 2);
            }
        }

        {
            int i = N - 2;
            for (int j = 0; j + 1 < M; j += 2)
            {
                F2(i, j);
            }

            if (M % 2 == 1)
            {
                F2(i, M - 2);
            }
        }

        // for (int i = 0; i < N; i++)
        // {
        //     for (int j = 0; j < M; j++)
        //     {
        //         Console.Write($"{(T[i][j] ? '1' : '0')}");
        //     }
        //
        //     Console.WriteLine();
        // }

        Console.WriteLine(Ans.Count);
        foreach (string s in Ans)
        {
            Console.WriteLine(s);
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