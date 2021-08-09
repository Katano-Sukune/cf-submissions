using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    int N, Q, M;
    int[] A;
    int[] T, L, R;
    int[] B;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        Q = sc.NextInt();
        M = sc.NextInt();
        A = sc.IntArray();
        T = new int[Q];
        L = new int[Q];
        R = new int[Q];
        for (int i = 0; i < Q; i++)
        {
            T[i] = sc.NextInt();
            L[i] = sc.NextInt() - 1;
            R[i] = sc.NextInt();
        }

        B = sc.Array().Select(s => int.Parse(s) - 1).ToArray();

        // Q個クエリ

        // 1 l r
        // [l,r] 区間内の a_x+1をa_x a_lをa_rにする

        // 2 l,r
        // 区間を反転する

        int[] ans = new int[M];
        for (int t = 0; t < M; t++)
        {
            int idx = B[t];
            for (int i = Q - 1; i >= 0; i--)
            {
                if (L[i] > idx || idx >= R[i]) continue;
                if (T[i] == 1)
                {
                    idx = idx == L[i] ? R[i] - 1 : idx - 1;
                }
                else
                {
                    int d = idx - L[i];
                    idx = R[i] - 1 - d;
                }

            }
            ans[t] = A[idx];
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
