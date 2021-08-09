using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int k = sc.NextInt();
        var a = new int[4][];
        for (int i = 0; i < 4; i++)
        {
            a[i] = sc.IntArray();
        }

        // 回転寿司


        int cnt = 0;
        var ls = new List<(int i, int r, int c)>(20000);
        bool f = 2 * n == k;
        while (cnt < k)
        {
            for (int r = 1; r <= 2; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    ref int cur = ref a[r][c];
                    if (cur == 0) continue;
                    int o = a[r == 1 ? 0 : 3][c];
                    if (o == cur)
                    {
                        ls.Add((cur, r == 1 ? 1 : 4, c + 1));
                        cnt++;
                        cur = 0;
                        f = false;
                    }
                    else
                    {
                        int nR, nC;
                        if (r == 1)
                        {
                            nR = c == n - 1 ? 2 : 1;
                            nC = c == n - 1 ? n - 1 : c + 1;
                        }
                        else
                        {
                            nR = c == 0 ? 1 : 2;
                            nC = c == 0 ? 0 : c - 1;
                        }

                        ref int next = ref a[nR][nC];
                        if (next != 0) continue;
                        ls.Add((cur, nR + 1, nC + 1));
                        next = cur;
                        cur = 0;
                    }
                }
            }

            if (f)
            {
                Console.WriteLine("-1");
                return;
            }
        }
#if !DEBUG
System.Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
#endif
        Console.WriteLine(ls.Count);
        foreach ((int i, int r, int c) in ls)
        {
            Console.WriteLine($"{i} {r} {c}");
        }

        Console.Out.Flush();
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