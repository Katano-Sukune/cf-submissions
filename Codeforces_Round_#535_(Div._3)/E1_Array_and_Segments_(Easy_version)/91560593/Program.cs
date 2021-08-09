using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, M;
    int[] A;
    int[] L, R;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        A = sc.IntArray();
        L = new int[M];
        R = new int[M];
        for (int i = 0; i < M; i++)
        {
            L[i] = sc.NextInt() - 1;
            R[i] = sc.NextInt();
        }

        /*
         * 配列A
         * 区間 L,R
         * 
         * 区間iを適用すると [l_i,r_i]から1引く
         * 
         * 最大-最小を最大化
         */

        long max = 0;
        List<int> ls = new List<int>();

        for (int mn = 0; mn < N; mn++)
        {
            for (int mx = 0; mx < N; mx++)
            {
                if (mn == mx) continue;

                int s = A[mn];
                int t = A[mx];
                var tmp = new List<int>();
                for (int i = 0; i < M; i++)
                {
                    if (L[i] <= mn && mn < R[i] && (mx < L[i] || R[i] <= mx))
                    {
                        s--;
                        tmp.Add(i + 1);
                    }
                }
                if (max < t - s)
                {
                    max = t - s;
                    ls = tmp;
                }
            }
        }
        Console.WriteLine(max);
        Console.WriteLine(ls.Count);
        Console.WriteLine(string.Join(" ",ls));
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
