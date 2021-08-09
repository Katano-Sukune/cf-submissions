using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private HashSet<(int to, int w)>[] E;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        E = new HashSet<(int to, int w)>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new HashSet<(int to, int w)>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            int w = sc.NextInt();
            E[u].Add((v, w));
            E[v].Add((u, w));
        }

        bool[] isLeaf = new bool[N];
        for (int i = 0; i < N; i++)
        {
            isLeaf[i] = E[i].Count == 1;
        }

        bool[] rm = new bool[N];
        // 次数2の頂点消す
        for (int i = 0; i < N; i++)
        {
            if (E[i].Count != 2) continue;

            var ar = E[i].ToArray();
            if (ar[0].w != ar[1].w)
            {
                Console.WriteLine("NO");
                return;
            }

            int w = ar[0].w;
            var f = ar[0].to;
            var t = ar[1].to;
            rm[i] = true;

            E[f].Remove((i, w));
            E[f].Add((t, w));
            E[t].Remove((i, w));
            E[t].Add((f, w));
        }

        // 残っている辺
        List<(int u, int v, int w)> edge = new List<(int u, int v, int w)>();
        for (int i = 0; i < N; i++)
        {
            if (rm[i]) continue;
            foreach ((int to, int w) in E[i])
            {
                if (i < to) edge.Add((i, to, w));
            }
        }

        // 端点が葉 or 2箇所の葉に向かう

        int m = 0;
        StringBuilder sb = new StringBuilder();
        foreach ((int u, int v, int w) in edge)
        {
            int[] lv = new int[2], lu = new int[2];
            if (!isLeaf[v])
            {
                lv = Go2(v, u);
            }

            if (!isLeaf[u])
            {
                lu = Go2(u, v);
            }

            if (isLeaf[u])
            {
                if (isLeaf[v])
                {
                    m++;
                    sb.AppendLine($"{u + 1} {v + 1} {w}");
                }
                else
                {
                    m++;
                    sb.AppendLine($"{u + 1} {lv[0] + 1} {w / 2}");
                    m++;
                    sb.AppendLine($"{u + 1} {lv[1] + 1} {w / 2}");
                    m++;
                    sb.AppendLine($"{lv[0] + 1} {lv[1] + 1} {-w / 2}");
                }
            }
            else
            {
                if (isLeaf[v])
                {
                    m++;
                    sb.AppendLine($"{lu[0] + 1} {v + 1} {w / 2}");
                    m++;
                    sb.AppendLine($"{lu[1] + 1} {v + 1} {w / 2}");
                    m++;
                    sb.AppendLine($"{lu[0] + 1} {lu[1] + 1} {-w / 2}");
                }
                else
                {
                    m++;
                    sb.AppendLine($"{lu[0] + 1} {lv[0] + 1} {w / 2}");
                    m++;
                    sb.AppendLine($"{lu[1] + 1} {lv[1] + 1} {w / 2}");
                    m++;
                    sb.AppendLine($"{lu[0] + 1} {lu[1] + 1} {-w / 2}");
                    m++;
                    sb.AppendLine($"{lv[0] + 1} {lv[1] + 1} {-w / 2}");
                }
            }
        }

        Console.WriteLine("YES");
        Console.WriteLine(m);
        Console.WriteLine(sb);
    }

    int Go(int cur, int par)
    {
        if (E[cur].Count == 1)
        {
            return cur;
        }

        foreach ((int to, int w) in E[cur])
        {
            if (to == par) continue;
            return Go(to, cur);
        }

        return -1;
    }

    int[] Go2(int cur, int par)
    {
        int[] res = new int[2];
        int ptr = 0;
        foreach ((int to, int w) in E[cur])
        {
            if (ptr >= 2) break;
            if (to == par) continue;
            res[ptr++] = Go(to, cur);
        }

        return res;
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