using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    private int N;
    private long[][] C;
    private List<int>[] E;

    public void Solve()
    {
        var sc = new Scanner();

        // 木
        // 頂点iを色cで塗る時のコスト c[c][i]
        // 3頂点を通るパスの3点が違う色にする

        // コスト最小
        N = sc.NextInt();
        E = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<int>();
        }

        C = new long[3][];
        for (int i = 0; i < 3; i++)
        {
            C[i] = sc.LongArray();
        }

        for (int i = 0; i < N - 1; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;

            E[u].Add(v);
            E[v].Add(u);
        }

        // 次数が3以上-1

        int l = -1;

        for (int i = 0; i < N; i++)
        {
            if (E[i].Count > 2)
            {
                Console.WriteLine("-1");
                return;
            }

            if (E[i].Count == 1) l = i;
        }

        // 通る順番
        bool[] flag = new bool[N];
        var ar = new int[N];
        ar[0] = l;
        flag[l] = true;

        for (int i = 1; i < N; i++)
        {
            foreach (int to in E[l])
            {
                if (!flag[to])
                {
                    flag[to] = true;
                    ar[i] = to;
                    l = to;
                    break;
                }
            }
        }

        var dp = new long[3, 3, N + 1];
        for (int i = 0; i <= N; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    dp[j, k, i] = long.MaxValue;
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                dp[i, j, 0] = 0;
            }
        }

        for (int i = 0; i < N; i++)
        {
            // 塗る色
            for (int j = 0; j < 3; j++)
            {
                // 前の色
                for (int k = 0; k < 3; k++)
                {
                    if (j == k) continue;
                    // 2個前
                    int kk = 3 - (j + k);
                    dp[j, k, i + 1] = Math.Min(dp[j, k, i + 1], dp[k, kk, i] + C[j][ar[i]]);
                }
            }
        }

        long cost = long.MaxValue;
        int p = -1, pp = -1;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (dp[i, j, N] < cost)
                {
                    cost = dp[i, j, N];
                    p = i;
                    pp = j;
                }
            }
        }

        Console.WriteLine(cost);
        var ans = new int[N];
        for (int i = N - 1; i >= 0; i--)
        {
            ans[ar[i]] = p + 1;
            int ppp = 3 - (p + pp);
            p = pp;
            pp = ppp;
        }

        Console.WriteLine(string.Join(" ", ans));
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
                string s = Console.ReadLine();
                while (s.Length == 0)
                {
                    s = Console.ReadLine();
                }

                _line = s.Split(Separator);
                _index = 0;
            }

            return _line[_index++];
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