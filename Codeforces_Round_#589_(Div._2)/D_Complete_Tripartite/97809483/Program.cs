using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, M;
    private List<int>[] E;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        E = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<int>();
        }

        for (int i = 0; i < M; i++)
        {
            int a = sc.NextInt() - 1;
            int b = sc.NextInt() - 1;
            E[a].Add(b);
            E[b].Add(a);
        }

        for (int i = 0; i < N; i++)
        {
            E[i].Sort();
        }

        int[] sorted = new int[N];
        for (int i = 0; i < N; i++)
        {
            sorted[i] = i;
        }

        Array.Sort(sorted, (l, r) => E[l].Count.CompareTo(E[r].Count));

        int[] ans = new int[N];
        int[] count = new int[4];
        List<int>[] t = new List<int>[4];
        t[1] = E[sorted[0]];
        ans[sorted[0]] = 1;
        count[1]++;

        for (int q = 1; q < N; q++)
        {
            int i = sorted[q];
            bool f = false;
            int j = 1;
            for (; j <= 3 && t[j] != null; j++)
            {
                if (t[j].Count != E[i].Count) continue;
                bool flag = true;
                for (int k = 0; k < t[j].Count && flag; k++)
                {
                    flag &= E[i][k] == t[j][k];
                }

                if (flag)
                {
                    f = true;
                    ans[i] = j;
                    count[j]++;
                    break;
                }
            }

            if (!f)
            {
                if (j <= 3)
                {
                    t[j] = E[i];
                    ans[i] = j;
                    count[j]++;
                }
                else
                {
                    Console.WriteLine("-1");
                    return;
                }
            }
        }

        long edge = N * (N - 1) / 2;
        for (int i = 1; i <= 3; i++)
        {
            if (count[i] == 0)
            {
                Console.WriteLine("-1");
                return;
            }
            edge -= count[i] * (count[i] - 1) / 2;
        }

        if (edge != M)
        {
            Console.WriteLine("-1");
            return;
        }

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