using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CompLib.Util;

public class Program
{
    private int N;
    private List<(int to, int num)>[] E;
    private int[] U, V;
    private int Max;

    private int R;
    private bool[] IsX;
    private bool Found;
    private int XSize;

    private int[] Len;
    private int[] Dist;
    private int TmpX;
    private int TmpY;

    void Scan()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        E = new List<(int to, int num)>[N];
        U = new int[N - 1];
        V = new int[N - 1];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<(int to, int num)>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            U[i] = u + 1;
            V[i] = v + 1;
            E[u].Add((v, i));
            E[v].Add((u, i));
        }

        Max = 2 * N * N / 9;
    }

    void SetFlag(int cur, int par)
    {
        foreach ((int to, int num) e in E[cur])
        {
            if (e.to == par) continue;
            SetFlag(e.to, cur);
        }

        IsX[cur] = true;
    }

    int Go(int cur, int par)
    {
        var ls = new List<(int to, int size)>();
        int size = 1;
        foreach (var e in E[cur])
        {
            if (e.to == par) continue;
            int tmp = Go(e.to, cur);
            ls.Add((e.to, tmp));
            size += tmp;
        }


        if (par != -1)
        {
            ls.Add((par, N - size));
        }


        if (!Found)
        {
            bool[,] dp = new bool[ls.Count + 1, N];
            dp[0, 0] = true;
            for (int i = 0; i < ls.Count; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (dp[i, j])
                    {
                        dp[i + 1, j] = true;
                        dp[i + 1, j + ls[i].size] = true;
                    }
                }
            }

            int x = -1;
            for (int i = 0; i < N; i++)
            {
                if (!dp[ls.Count, i]) continue;
                int y = N - 1 - i;
                if (Max < (i + 1) * (y + 1))
                {
                    x = i;
                    break;
                }
            }

            if (x != -1)
            {
                XSize = x;
                R = cur;
                for (int i = ls.Count - 1; i >= 0; i--)
                {
                    if (x - ls[i].size >= 0 && dp[i, x - ls[i].size])
                    {
                        SetFlag(ls[i].to, cur);
                        x -= ls[i].size;
                    }
                }

                Found = true;
            }
        }


        return size;
    }

    // r、x,yの頂点を決める
    void SearchR()
    {
        IsX = new bool[N];
        Found = false;
        Go(0, -1);
        if (!Found) throw new Exception();
    }


    // 現在地、親、par-curが辺num
    void SetX(int cur, int par, int num)
    {
        Dist[cur] = TmpX++;
        if (par != -1)
        {
            Len[num] = Dist[cur] - Dist[par];
        }

        foreach ((int to, int num) e in E[cur])
        {
            if (e.to == par) continue;
            if (!IsX[e.to]) continue;
            SetX(e.to, cur, e.num);
        }
    }

    void SetY(int cur, int par, int num)
    {
        Dist[cur] = TmpY;
        TmpY += (XSize + 1);
        if (par != -1)
        {
            Len[num] = Dist[cur] - Dist[par];
        }

        foreach ((int to, int num) e in E[cur])
        {
            if (e.to == par) continue;
            if (IsX[e.to]) continue;
            SetY(e.to, cur, e.num);
        }
    }

    void SetLen()
    {
        // 辺の長さ
        Len = new int[N - 1];
        // R->頂点までの距離
        Dist = new int[N];
        // X側の頂点はRに近い順に距離1~Xを割り当てる
        // それぞれの頂点までの距離になるように頂点の長さを割り当てる
        TmpX = 0;
        SetX(R, -1, -1);

        // Y (X+1)刻みで割り当てる
        TmpY = 0;
        SetY(R, -1, -1);
    }

    void Write()
    {
        var sb = new StringBuilder();
        for (int i = 0; i < N - 1; i++)
        {
            sb.AppendLine($"{U[i]} {V[i]} {Len[i]}");
        }

        Console.Write(sb);
    }

    public void Solve()
    {
        // ある頂点rを境目に2つに分ける
        // 片方x個 もう片方 y個 x + y + 1 = N
        // 片方でrからの距離 0~xをつくる
        // 残り x+1刻みで y個
        // 0 <= p < (x+1)(y+1)できる

        Scan();
        SearchR();
        SetLen();
        Write();
    }

    public static void Main(string[] args)
    {
        new Thread(new ThreadStart(new Program().Solve), 1 << 27).Start();
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