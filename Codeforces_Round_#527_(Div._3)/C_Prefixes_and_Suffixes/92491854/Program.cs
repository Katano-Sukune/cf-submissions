using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    int M;
    string[] S;
    int[] Sorted;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = 2 * N - 2;
        S = new string[M];
        for (int i = 0; i < M; i++)
        {
            S[i] = sc.Next();
        }

        Sorted = new int[M];
        for (int i = 0; i < M; i++)
        {
            Sorted[i] = i;
        }

        Array.Sort(Sorted, (l, r) => S[l].Length.CompareTo(S[r].Length));

        if (F(Sorted[M - 1], Sorted[M - 2])) return;
        if (F(Sorted[M - 2], Sorted[M - 1])) return;
    }

    // 最長 prefix, 最長 suffix
    bool F(int lp, int ls)
    {
        char[] ans = new char[M];
        ans[lp] = 'P';
        ans[ls] = 'S';
        char[] s = new char[N];
        Array.Copy(S[lp].ToArray(), s, N - 1);
        s[N - 1] = S[ls][N - 2];

        for (int len = 1; len <= N - 1; len++)
        {
            int i = Sorted[len * 2 - 2];
            int j = Sorted[len * 2 - 1];

            // iがprefix jがsuffix
            {
                bool f1 = true;
                for (int idx = 0; f1 && idx < len; idx++)
                {
                    f1 &= S[i][idx] == s[idx];
                    f1 &= S[j][len - idx - 1] == s[N - idx - 1];
                }
                if (f1)
                {
                    ans[i] = 'P';
                    ans[j] = 'S';
                    continue;
                }
            }

            {
                bool f2 = true;
                for (int idx = 0; f2 && idx < len; idx++)
                {
                    f2 &= S[j][idx] == s[idx];
                    f2 &= S[i][len - idx - 1] == s[N - idx - 1];
                }
                if (f2)
                {
                    ans[j] = 'P';
                    ans[i] = 'S';
                    continue;
                }
            }

            return false;
        }
        Console.WriteLine(new string(ans));
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
