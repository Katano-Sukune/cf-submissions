using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    int[][] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();

        A = new int[N][];
        for (int i = 0; i < N; i++)
        {
            A[i] = sc.IntArray();
        }

        // 子供iの次、もう1つ次

        if (F(A[0][0], A[0][1]))
        {
            return;
        }

        if (F(A[0][1], A[0][0]))
        {
            return;
        }
    }

    bool F(int ans1, int ans2)
    {
        int[] ans = new int[N];
        ans[0] = 1;
        ans[1] = ans1;
        ans[2] = ans2;
        bool[] used = new bool[N + 1];
        used[1] = used[ans[1]] = used[ans[2]] = true;

        for (int i = 1; i < N - 2; i++)
        {
            int id = ans[i];
            int next = ans[i + 1];

            if (A[id - 1][0] == next)
            {
                int t = A[id - 1][1];
                if (used[t]) return false;
                ans[i + 2] = t;
                used[t] = true;
            }
            else if (A[id - 1][1] == next)
            {
                int t = A[id - 1][0];
                if (used[t]) return false;
                ans[i + 2] = t;
                used[t] = true;
            }
            else
            {
                return false;
            }
        }

        // N-2
        {
            int s1 = ans[N - 1];
            int t1 = ans[0];

            int s2 = A[ans[N - 2] - 1][0];
            int t2 = A[ans[N - 2] - 1][1];

            if (s1 > t1) { s1 ^= t1; t1 ^= s1; s1 ^= t1; }
            if (s2 > t2) { s2 ^= t2; t2 ^= s2; s2 ^= t2; }
            if (s1 != s2 || t1 != t2) return false;
        }
        {
            int s1 = ans[0];
            int t1 = ans[1];

            int s2 = A[ans[N - 1] - 1][0];
            int t2 = A[ans[N - 1] - 1][1];

            if (s1 > t1) { s1 ^= t1; t1 ^= s1; s1 ^= t1; }
            if (s2 > t2) { s2 ^= t2; t2 ^= s2; s2 ^= t2; }

            if (s1 != s2 || t1 != t2) return false;
        }

        Console.WriteLine(string.Join(" ", ans));
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
