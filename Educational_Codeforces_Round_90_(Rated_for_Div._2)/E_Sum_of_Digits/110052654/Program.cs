using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int[,] Ans;

    private int[] F;

    private const int T = 10000000;
    private const int MaxN = 150;
    private const int MaxK = 9;

    public void Solve()
    {
        Ans = new int[151, 10];
        for (int n = 1; n <= 150; n++)
        {
            for (int k = 0; k <= 9; k++)
            {
                Ans[n, k] = int.MaxValue;
            }
        }

        F = new int[T];
        for (int i = 1; i < T; i++)
        {
            int tmp = i;
            while (tmp > 0)
            {
                F[i] += tmp % 10;
                tmp /= 10;
            }
        }

        for (int k = 2; k <= 9; k++)
        {
            int sum = 0;
            for (int i = 0; i < k; i++)
            {
                sum += F[i];
            }

            for (int x = 0; x + k < T; x++)
            {
                sum += F[x + k];
                if (sum <= 150) Ans[sum, k] = Math.Min(Ans[sum, k], x);
                sum -= F[x];
            }
        }


        var sc = new Scanner();
#if !DEBUG
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
#endif
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    private int N;
    private int K;

    void Q(Scanner sc)
    {
        N = sc.NextInt();
        K = sc.NextInt();

        if (K >= 2)
        {
            if (Ans[N, K] == int.MaxValue)
            {
                Console.WriteLine("-1");
                return;
            }

            Console.WriteLine(Ans[N, K]);
            return;
        }
        else if (K == 1)
        {
            // x, x+1

            if (N % 2 == 1)
            {
                int fx = (N - 1) / 2;
                // 繰り上がりしない
                if (fx <= 8)
                {
                    Console.WriteLine(fx);
                    return;
                }

                var ls = new List<char>();
                ls.Add('8');
                fx -= 8;
                while (fx >= 9)
                {
                    ls.Add('9');
                    fx -= 9;
                }

                if (fx > 0)
                {
                    ls.Add((char) ('0' + fx));
                }

                ls.Reverse();
                Console.WriteLine(new string(ls.ToArray()));
            }
            else
            {
                // 一番下
                // 繰り上がり
                // f(x) + f(x+1)
                // fx + fx-8 = n
                int fx = (N + 8) / 2;
                if (fx < 9)
                {
                    Console.WriteLine("-1");
                    return;
                }

                var ls = new List<char>();
                ls.Add('9');
                fx -= 9;

                if (fx <= 8)
                {
                    if (fx > 0) ls.Add((char) ('0' + fx));
                    ls.Reverse();
                    Console.WriteLine(new string(ls.ToArray()));
                    return;
                }

                ls.Add('8');
                fx -= 8;

                while (fx >= 9)
                {
                    ls.Add('9');
                    fx -= 9;
                }

                if (fx > 0)
                {
                    ls.Add((char) ('0' + fx));
                }

                ls.Reverse();
                Console.WriteLine(new string(ls.ToArray()));
                return;
            }
        }
        else
        {
            var ls = new List<char>();
            var fx = N;
            while (fx >= 9)
            {
                ls.Add('9');
                fx -= 9;
            }

            if (fx > 0)
            {
                ls.Add((char) ('0' + fx));
            }

            ls.Reverse();
            Console.WriteLine(new string(ls.ToArray()));
            return;
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