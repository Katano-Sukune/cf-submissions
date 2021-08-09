using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, R;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        R = sc.NextInt();
        List<W> plus = new List<W>();

        List<W> minus = new List<W>();
        for (int i = 0; i < N; i++)
        {
            int a = sc.NextInt();
            int b = sc.NextInt();
            if (b >= 0) plus.Add(new W() { A = a, B = b });
            else minus.Add(new W() { A = a, B = b });
        }

        // 小
        plus.Sort((l, r) => l.A.CompareTo(r.A));
        foreach (var w in plus)
        {
            if (R < w.A)
            {
                Console.WriteLine("NO");
                return;
            }
            R += w.B;
        }

        // 問題の言い換え
        // 残り時間R
        // 残り時間がmax(0,A+B)までに終わらせなきゃいけない

        minus.Sort((l, r) => Math.Max(0, r.A + r.B).CompareTo(Math.Max(0, l.A + l.B)));

        foreach (var w in minus)
        {
            if (R < w.A)
            {
                Console.WriteLine("NO");
                return;
            }
            R += w.B;
            if (R < 0)
            {
                Console.WriteLine("NO");
                return;
            }
        }
        Console.WriteLine("YES");
    }

    public static void Main(string[] args) => new Program().Solve();
}


struct W
{
    public int A, B;
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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
                _index = 0;
            }

            return _line[_index++];
        }

        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public decimal NextDecimal() => decimal.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
