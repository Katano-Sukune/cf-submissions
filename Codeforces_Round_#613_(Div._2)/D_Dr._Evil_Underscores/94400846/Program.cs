using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private int[] A;
    private List<int> B;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();
        Array.Sort(A);

        B = new List<int>();
        for (int i = 0; i < N; i++)
        {
            if (i == 0 || A[i] != A[i - 1]) B.Add(A[i]);
        }

        Console.WriteLine(Go(B, 29));
    }

    int Go(List<int> ls, int d)
    {
        if (d < 0) return 0;
        if (ls.Count <= 1) return 0;

        var z = new List<int>();
        var o = new List<int>();

        int p = 1 << d;
        foreach (int i in ls)
        {
            if ((i & p) == 0)
            {
                z.Add(i);
            }
            else
            {
                o.Add(i);
            }
        }

        if (z.Count == 0)
        {
            return Go(o, d - 1);
        }

        if (o.Count == 0)
        {
            return Go(z, d - 1);
        }

        return p + Math.Min(Go(o, d - 1), Go(z, d - 1));
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