using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    string S;

    public void Solve()
    {
        var sc = new Scanner();

#if DEBUG
        N = 1000000;
        S = new string('0', N);
#else
        N = sc.NextInt();
        S = sc.Next();
#endif

        var dp = new int[(N + 1) * 2];
        for (int i = N - 1; i >= 0; i--)
        {
            if (S[i] != '0')
            {
                dp[C(i, 1)] = dp[C(i + 1, 1)] + 1;
            }
            if (S[i] != '1')
            {
                dp[C(i, 0)] = dp[C(i + 1, 0)] + 1;
            }
        }

        var ls = new S[N + 1][];
        ls[N] = new S[0];

        for (int i = N - 1; i >= 0; i--)
        {
            int t = Math.Max(dp[C(i, 0)], dp[C(i, 1)]);


            // i+1のt超過
            int ok = ls[i + 1].Length;
            int ng = -1;
            while (ok - ng > 1)
            {
                int mid = (ok + ng) / 2;
                if (t < ls[i + 1][mid].len) ok = mid;
                else ng = mid;
            }
            ls[i] = new S[ls[i + 1].Length - ok + 1];

            for (int j = ok; j < ls[i + 1].Length; j++) ls[i][j - ok + 1] = ls[i + 1][j];
            ls[i][0] = new S(i, t);
        }

        var ans = new int[N];
        for (int x = 1; x <= N; x++)
        {
            int cur = 0;
            int cnt = 0;
            while (cur < N)
            {
                // curのx以上
                int ng = -1;
                int ok = ls[cur].Length;
                while (ok - ng > 1)
                {
                    int mid = (ok + ng) / 2;
                    if (x <= ls[cur][mid].len) ok = mid;
                    else ng = mid;
                }

                if (ok < ls[cur].Length)
                {
                    cnt++;
                    cur = ls[cur][ok].index + x;
                }
                else
                {
                    break;
                }
            }

            ans[x - 1] = cnt;
        }

        Console.WriteLine(string.Join(" ", ans));
    }

    int C(int i, int j)
    {
        return i * 2 + j;
    }

    public static void Main(string[] args) => new Program().Solve();
}

struct S
{
    public int index, len;
    public S(int i, int l)
    {
        index = i;
        len = l;
    }
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
