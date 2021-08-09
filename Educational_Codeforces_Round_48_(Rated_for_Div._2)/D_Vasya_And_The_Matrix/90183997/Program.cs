using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, M;
    int[] A, B;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        A = sc.IntArray();
        B = sc.IntArray();

        // N*M行列
        // i行のxor A[i]

        // j列のxor B[j]

        // Aのxor != Bのxor NO

        // bitごと

        int[][] ans = new int[N][];
        for (int i = 0; i < N; i++)
        {
            ans[i] = new int[M];
        }
        for (int i = 0; i < 30; i++)
        {
            int b = 1 << i;
            List<int> la = new List<int>();
            for (int j = 0; j < N; j++)
            {
                if ((A[j] & b) > 0) la.Add(j);
            }
            List<int> lb = new List<int>();
            for (int j = 0; j < M; j++)
            {
                if ((B[j] & b) > 0) lb.Add(j);
            }

            if (la.Count % 2 != lb.Count % 2)
            {
                Console.WriteLine("NO");
                return;
            }

            for (int j = 0; j < Math.Min(la.Count, lb.Count); j++)
            {
                ans[la[j]][lb[j]] |= b;
            }

            if (la.Count < lb.Count)
            {
                for (int j = la.Count; j < lb.Count; j++)
                {
                    ans[0][lb[j]] |= b;
                }
            }
            else
            {
                for (int j = lb.Count; j < la.Count; j++)
                {
                    ans[la[j]][0] |= b;
                }
            }
        }

        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        Console.WriteLine("YES");
        for (int i = 0; i < N; i++)
        {
            Console.WriteLine(string.Join(" ", ans[i]));
        }

        Console.Out.Flush();

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
