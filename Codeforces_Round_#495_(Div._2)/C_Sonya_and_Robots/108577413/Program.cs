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
        int[] a = sc.IntArray();

        int[] first = new int[100001];
        int[] last = new int[100001];
        Array.Fill(first, -1);
        Array.Fill(last, int.MaxValue);
        for (int i = 0; i < n; i++)
        {
            last[a[i]] = i;
            first[a[n - i - 1]] = n - i - 1;
        }

        var lsLast = new List<int>();
        for (int i = 1; i <= 100000; i++)
        {
            if (last[i] == int.MaxValue) continue;
            lsLast.Add(last[i]);
        }

        lsLast.Sort();

        long ans = 0;
        for (int i = 1; i <= 100000; i++)
        {
            if (first[i] == -1) continue;
            int ok = lsLast.Count;
            int ng = -1;
            while (ok - ng > 1)
            {
                int mid = (ok + ng) / 2;
                if (lsLast[mid] > first[i]) ok = mid;
                else ng = mid;
            }

            ans += lsLast.Count - ok;
        }

        Console.WriteLine(ans);
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