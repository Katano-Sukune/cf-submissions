using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{
    int N, M;
    int[] A, B;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();

        A = new int[M];
        B = new int[M];
        for (int i = 0; i < M; i++)
        {
            A[i] = sc.NextInt() - 1;
            B[i] = sc.NextInt() - 1;
        }

        var ls = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            ls[i] = new List<int>();
        }

        for (int i = 0; i < M; i++)
        {
            int ta = B[i] - A[i];
            if (ta < 0) ta += N;
            ls[A[i]].Add(ta);

            int tb = A[i] - B[i];
            if (tb < 0) tb += N;
            ls[B[i]].Add(tb);
        }

        for (int i = 0; i < N; i++)
        {
            ls[i].Sort();
        }

        var div = new List<int>();
        for (int i = 1; i * i <= N; i++)
        {
            if (N % i == 0)
            {
                div.Add(i);
                if (i != N / i) div.Add(N / i);
            }
        }

        foreach (var d in div)
        {
            if (d == N) continue;
            bool f = true;
            for (int i = 0; i < N && f; i++)
            {
                if (ls[i].Count != ls[(i + d) % N].Count)
                {
                    f = false;
                    break;
                }

                for (int j = 0; j < ls[i].Count && f; j++)
                {
                    f &= ls[i][j] == ls[(i + d) % N][j];
                }
            }
            if (f)
            {
                Console.WriteLine("Yes");
                return;
            }
        }
        Console.WriteLine("No");
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
