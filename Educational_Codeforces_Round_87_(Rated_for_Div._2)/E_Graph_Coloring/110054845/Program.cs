using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private int M;
    private int[] NX;
    private List<int>[] E;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        NX = sc.IntArray();
        E = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<int>();
        }

        for (int i = 0; i < M; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            E[u].Add(v);
            E[v].Add(u);
        }

        // i番目に見つけた連結成分
        // 白、黒
        var lsV = new List<List<int>[]>();

        var parity = new int [N];
        Array.Fill(parity, -1);

        for (int v = 0; v < N; v++)
        {
            if (parity[v] != -1) continue;
            var l = new List<int>[2];
            for (int i = 0; i < 2; i++)
            {
                l[i] = new List<int>();
            }

            var q = new Queue<int>();
            q.Enqueue(v);
            parity[v] = 0;

            while (q.Count > 0)
            {
                int cur = q.Dequeue();
                l[parity[cur]].Add(cur);

                foreach (var to in E[cur])
                {
                    if (parity[to] == -1)
                    {
                        parity[to] = 1 - parity[cur];
                        q.Enqueue(to);
                    }
                    else
                    {
                        if (parity[to] == parity[cur])
                        {
                            Console.WriteLine("NO");
                            return;
                        }
                    }
                }
            }

            lsV.Add(l);
        }

        var dp = new bool[lsV.Count + 1, NX[1] + 1];
        dp[0, 0] = true;
        for (int i = 0; i < lsV.Count; i++)
        {
            for (int j = 0; j <= NX[1]; j++)
            {
                if (!dp[i, j]) continue;
                for (int k = 0; k < 2; k++)
                {
                    if (j + lsV[i][k].Count <= NX[1]) dp[i + 1, j + lsV[i][k].Count] = true;
                }
            }
        }

        if (!dp[lsV.Count, NX[1]])
        {
            Console.WriteLine("NO");
            return;
        }

        int[] ans = new int[N];
        int p = NX[1];

        var cpNX = NX.ToArray();
        for (int i = lsV.Count - 1; i >= 0; i--)
        {
            if (p - lsV[i][0].Count >= 0 && dp[i, p - lsV[i][0].Count])
            {
                foreach (int v in lsV[i][0])
                {
                    ans[v] = 2;
                }

                foreach (int v in lsV[i][1])
                {
                    if (cpNX[0] > 0)
                    {
                        ans[v] = 1;
                        cpNX[0]--;
                    }
                    else
                    {
                        ans[v] = 3;
                    }
                }

                p -= lsV[i][0].Count;
            }
            else
            {
                foreach (int v in lsV[i][1])
                {
                    ans[v] = 2;
                }

                foreach (int v in lsV[i][0])
                {
                    if (cpNX[0] > 0)
                    {
                        ans[v] = 1;
                        cpNX[0]--;
                    }
                    else
                    {
                        ans[v] = 3;
                    }
                }

                p -= lsV[i][1].Count;
            }
        }

        Console.WriteLine("YES");
        Console.WriteLine(string.Join("", ans));
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