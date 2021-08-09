using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CompLib.Util;

public class Program
{
    int N;
    long[] A;
    int[] B, C;
    List<int>[] E;

    long Ans;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = new long[N];
        B = new int[N];
        C = new int[N];
        for (int i = 0; i < N; i++)
        {
            A[i] = sc.NextLong();
            B[i] = sc.NextInt();
            C[i] = sc.NextInt();
        }

        E = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<int>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            E[u].Add(v);
            E[v].Add(u);
        }
        Ans = 0;
        // 木
        // 元々 c
        // 部分木uの子孫k個選んで シャッフル コスト A[u] * k
        var t = DFS(0, -1, A[0]);
        if (t.One != 0 || t.Zero != 0)
        {
            Console.WriteLine("-1");
            return;
        }
        Console.WriteLine(Ans);
    }

    S DFS(int cur, int par, long min)
    {
        // Console.WriteLine(cur);
        // 目標0,1の数　一致
        // K 一致してない数

        int one = 0;
        int tone = 0;
        int zero = 0;
        int tzero = 0;
        if (B[cur] != C[cur])
        {
            if (B[cur] == 1)
            {
                one++;
            }
            else
            {
                zero++;
            }

            if (C[cur] == 1)
            {
                tone++;
            }
            else
            {
                tzero++;
            }
        }



        foreach (var to in E[cur])
        {
            if (to == par) continue;
            var s = DFS(to, cur, Math.Min(min, A[to]));
            one += s.One;
            tone += s.TOne;
            zero += s.Zero;
            tzero += s.TZero;
        }

        int zz = Math.Min(zero, tzero);
        int oo = Math.Min(one, tone);


        Ans += min * (zz + oo);

        one -= oo;
        zero -= zz;
        tzero -= zz;
        tone -= oo;
        // Console.WriteLine($"{cur} {one} {zero} {one} {tone} {Ans}");
        return new S(zero, tzero, one, tone);
    }


    public static void Main(string[] args)
    {
        new Thread(new ThreadStart(new Program().Solve), 100000000).Start();
    }
}

struct S
{
    public int Zero, TZero, One, TOne;
    public S(int z, int tz, int o, int to)
    {
        Zero = z;
        TZero = tz;
        One = o;
        TOne = to;
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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
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
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}
