using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CompLib.Util;

public class Program
{
    private int N;
    private HashSet<(int to, int val)>[] E;
    private List<(int to, int val)>[] Edge;
    private long[,] Ans;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        E = new HashSet<(int to, int val)>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new HashSet<(int to, int val)>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            int val = sc.NextInt();
            E[u].Add((v, val));
            E[v].Add((u, val));
        }

        // 辺を減らす
        bool[] f = new bool[N];
        var ls = new List<int>();
        for (int i = 0; i < N; i++)
        {
            if (E[i].Count == 2)
            {
                f[i] = true;
                ls.Add(i);
            }
        }

        foreach (var v in ls)
        {
            var ar = E[v].ToArray();
            if (ar[0].val != ar[1].val)
            {
                Console.WriteLine("NO");
                return;
            }

            var val = ar[0].val;
            E[ar[0].to].Remove((v, val));
            E[ar[0].to].Add((ar[1].to, val));
            E[ar[1].to].Remove((v, val));
            E[ar[1].to].Add((ar[0].to, val));
        }

        int root = -1;
        Edge = new List<(int to, int val)>[N];
        for (int i = 0; i < N; i++)
        {
            if (!f[i])
            {
                if (E[i].Count == 1) root = i;
                Edge[i] = E[i].ToList();
            }
        }

        Ans = new long[N, N];
        Go(root, -1);

        var sb = new StringBuilder();
        int s = 0;
        for (int i = 0; i < N; i++)
        {
            for (int j = i + 1; j < N; j++)
            {
                if (Ans[i, j] != 0)
                {
                    s++;
                    sb.AppendLine($"{i + 1} {j + 1} {Ans[i, j]}");
                }
            }
        }

        Console.WriteLine("YES");
        Console.WriteLine(s);
        Console.Write(sb);
    }

    void Go(int cur, int par)
    {
        foreach (var e in Edge[cur])
        {
            if (e.to == par) continue;
            // cur側
            int[] leafCur;
            if (Edge[cur].Count == 1)
            {
                leafCur = new int[1];
                leafCur[0] = cur;
            }
            else if (Edge[cur].Count >= 3)
            {
                leafCur = new int[2];
                int c = 0;
                foreach (var f in Edge[cur])
                {
                    if (c >= 2) break;
                    if (f.to == e.to) continue;
                    leafCur[c++] = Leaf(f.to, cur);
                }
            }
            else
            {
                throw new Exception();
            }

            // to
            int[] leafTo;
            if (Edge[e.to].Count == 1)
            {
                leafTo = new int[1];
                leafTo[0] = e.to;
            }
            else if (Edge[e.to].Count >= 3)
            {
                leafTo = new int[2];
                int c = 0;
                foreach (var f in Edge[e.to])
                {
                    if (c >= 2) break;
                    if (f.to == cur) continue;
                    leafTo[c++] = Leaf(f.to, e.to);
                }
            }
            else
            {
                throw new Exception();
            }

            if (leafCur.Length == 1 && leafTo.Length == 1)
            {
                Ans[leafCur[0], leafTo[0]] += e.val;
                Ans[leafTo[0], leafCur[0]] += e.val;
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    Ans[leafCur[i % leafCur.Length], leafTo[i % leafTo.Length]] += e.val / 2;
                    Ans[leafTo[i % leafTo.Length], leafCur[i % leafCur.Length]] += e.val / 2;
                }

                if (leafCur.Length == 2)
                {
                    Ans[leafCur[0], leafCur[1]] -= e.val / 2;
                    Ans[leafCur[1], leafCur[0]] -= e.val / 2;
                }

                if (leafTo.Length == 2)
                {
                    Ans[leafTo[0], leafTo[1]] -= e.val / 2;
                    Ans[leafTo[1], leafTo[0]] -= e.val / 2;
                }
            }

            Go(e.to, cur);
        }
    }

    int Leaf(int cur, int par)
    {
        if (Edge[cur].Count == 1) return cur;
        foreach (var e in Edge[cur])
        {
            if (e.to == par) continue;
            return Leaf(e.to, cur);
        }

        throw new Exception();
    }

    public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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