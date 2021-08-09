using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    int[] A;
    public void Solve()
    {
        checked
        {
            var sc = new Scanner();
            N = sc.NextInt();
            A = sc.IntArray();

            /*
             * k-extension
             * 
             * k* |i-j| <= min(a_i, a_j)
             * 
             * 
             */

            long ok = 0;
            long ng = int.MaxValue;

            while (ng - ok > 1)
            {
                long mid = (ok + ng) / 2;

                if (F(mid)) ok = mid;
                else ng = mid;
            }

            Console.WriteLine(ok);
        }
    }

    bool F(long k)
    {
        var ls = new List<(long num, long index)>();
        var ls2 = new List<(long num, long index)>();
        for (int i = 0; i < N; i++)
        {
            // i以前の i以上
            int ng = -1;
            int ok = ls.Count;
            while (ok - ng > 1)
            {
                int mid = (ok + ng) / 2;
                if (ls[mid].num >= A[i]) ok = mid;
                else ng = mid;
            }

            if (ok < ls.Count && (i - ls[ok].index) * k > A[i]) return false;

            int j = N - i - 1;
            ng = -1;
            ok = ls2.Count;
            while (ok - ng > 1)
            {
                int mid = (ok + ng) / 2;
                if (ls2[mid].num >= A[j]) ok = mid;
                else ng = mid;
            }

            if (ok < ls2.Count && (ls2[ok].index - j) * k > A[j]) return false;

            if (ls.Count == 0 || ls[ls.Count - 1].num < A[i])
            {
                ls.Add((A[i], i));
            }

            if (ls2.Count == 0 || ls2[ls2.Count - 1].num < A[j])
            {
                ls2.Add((A[j], j));
            }
        }
        return true;
    }

    public static void Main(string[] args) => new Program().Solve();
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
