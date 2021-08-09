using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{
    int N;
    int[] A;

    int[] Sorted;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();

        Sorted = new int[N];
        for (int i = 0; i < N; i++)
        {
            Sorted[i] = i;
        }

        Array.Sort(Sorted, (l, r) => A[r].CompareTo(A[l]));

        int ok = 0;
        int ng = int.MaxValue;
        while (ng - ok > 1)
        {
            int mid = (ok + ng) / 2;
            if (F(mid)) ok = mid;
            else ng = mid;
        }

        Console.WriteLine(ok);
    }

    bool F(int k)
    {
        int min = int.MaxValue;
        int max = int.MinValue;
        for (int t = 0; t < N; t++)
        {
            int i = Sorted[t];
            min = Math.Min(min, i);
            max = Math.Max(max, i);

            if ((long)k * Math.Abs(i - min) > A[i]) return false;
            if ((long)k * Math.Abs(i - max) > A[i]) return false;
        }
        return true;
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
