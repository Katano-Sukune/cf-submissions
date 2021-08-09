using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Net.Http.Headers;

public class Program
{
    int N, Q;
    int[] A;
    int[] T;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        Q = sc.NextInt();
        A = sc.IntArray();
        T = sc.IntArray();

        var s = new List<int>[51];
        for (int i = 0; i <= 50; i++)
        {
            s[i] = new List<int>();
        }
        for (int i = N - 1; i >= 0; i--)
        {
            s[A[i]].Add(i);
        }


        int[] ans = new int[Q];

        for (int i = 0; i < Q; i++)
        {
            int idx = s[T[i]][^1];
            s[T[i]].RemoveAt(s[T[i]].Count - 1);

            for (int j = 1; j <= 50; j++)
            {
                int ok = s[j].Count;
                int ng = -1;

                while (ok - ng > 1)
                {
                    int mid = (ok + ng) / 2;
                    if (s[j][mid] < idx) ok = mid;
                    else ng = mid;
                }
                ans[i] += s[j].Count - ok;
            }
            ans[i]++;
            s[T[i]].Add(-i - 1);
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
