using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int A, B, C;
    private int M;
    private List<int> USB, PS2;

    public void Solve()
    {
        var sc = new Scanner();
        A = sc.NextInt();
        B = sc.NextInt();
        C = sc.NextInt();
        M = sc.NextInt();
        USB = new List<int>();
        PS2 = new List<int>();
        for (int i = 0; i < M; i++)
        {
            int val = sc.NextInt();
            if (sc.Next() == "USB") USB.Add(val);
            else PS2.Add(val);
        }

        USB.Sort();
        PS2.Sort();

        (int cnt, long cost)[] u = new (int cnt, long cost)[USB.Count + 1];
        u[0] = (0, 0);
        for (int i = 0; i < USB.Count; i++)
        {
            u[i + 1] = (u[i].cnt + 1, u[i].cost + USB[i]);
        }

        (int cnt, long cost)[] p = new (int cnt, long cost)[PS2.Count + 1];
        p[0] = (0, 0);
        for (int i = 0; i < PS2.Count; i++)
        {
            p[i + 1] = (p[i].cnt + 1, p[i].cost + PS2[i]);
        }

        int cnt = 0;
        long cost = 0;

        for (int i = 0; i <= C; i++)
        {
            int uu = A + i;
            int pp = B + C - i;

            (int cntU, long costU) = u[Math.Min(USB.Count, uu)];
            (int cntP, long costP) = p[Math.Min(PS2.Count, pp)];
            int cntT = cntU + cntP;
            long costT = costU + costP;
            // Console.WriteLine($"{i} {cntT} {costT}");
            if (cnt < cntT || (cnt == cntT && costT < cost))
            {
                cnt = cntT;
                cost = costT;
            }
        }

        Console.WriteLine($"{cnt} {cost}");
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