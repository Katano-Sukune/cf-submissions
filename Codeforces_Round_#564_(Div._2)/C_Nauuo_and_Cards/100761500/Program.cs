using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private int[] A, B;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();
        B = sc.IntArray();

        int[] idx = new int[N + 1];
        Array.Fill(idx, -1);
        for (int i = 0; i < N; i++)
        {
            idx[B[i]] = i;
        }

        bool[] fa = new bool[N + 1];
        foreach (int i in A)
        {
            fa[i] = true;
        }

        int ans = int.MaxValue;

        // 最初から1,2,3,4...並べる
        {
            bool f = true;
            bool[] tmp = new bool[N + 1];
            Array.Copy(fa, tmp, N + 1);
            for (int i = 0; i < N; i++)
            {
                if (!tmp[i + 1])
                {
                    f = false;
                    break;
                }

                tmp[B[i]] = true;
            }

            if (f) ans = Math.Min(ans, N);
        }

        {
            // Bの後ろに 1,2,3...
            // につなげる
            bool f = idx[1] != -1;
            for (int i = idx[1] + 1; f && i < N; i++)
            {
                f &= B[i - 1] + 1 == B[i];
            }

            if (f)
            {
                bool[] tmp = new bool[N + 1];
                Array.Copy(fa, tmp, N + 1);
                for (int i = 0; i < N - B[N - 1]; i++)
                {
                    if (!tmp[B[N - 1] + i + 1])
                    {
                        f = false;
                        break;
                    }

                    tmp[B[i]] = true;
                }

                if (f) ans = Math.Min(ans, N - B[N - 1]);
            }

            int max = int.MinValue;
            for (int i = N - 1; i >= 0; i--)
            {
                if (max <= 0)
                {
                    ans = Math.Min(ans, N + i + 1);
                }

                max += 1;
                if(B[i] != 0) max = Math.Max(max, 2-B[i]);
            }

            Console.WriteLine(ans);
        }
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