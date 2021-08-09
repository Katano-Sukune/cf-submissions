using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;

public class Program
{
    int N, K;
    int[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        A = sc.IntArray();

        int l = -1;
        int r = -1;

        const int MaxA = (int)1e6;


        int[] cnt = new int[MaxA + 1];
        int d = 0;

        int j = 0;
        int i = 0;

        while (j <= N)
        {
            // k個超えるまでjを伸ばす
            while (j < N)
            {
                if (cnt[A[j]] == 0) d++;
                cnt[A[j]]++;
                if (d > K) break;
                j++;
            }

            //Console.WriteLine($"{i} {j} {d}");
            //for (int k = 0; k < 10; k++)
            //{
            //    Console.Write($"{cnt[k]} ");
            //}
            //Console.WriteLine();

            if (r - l < j - i)
            {
                (l, r) = (i, j);
            }

            for (; i < j && d > K; i++)
            {
                if (cnt[A[i]] == 1) d--;
                cnt[A[i]]--;
            }
            j++;
        }

        Console.WriteLine($"{l + 1} {r}");
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
