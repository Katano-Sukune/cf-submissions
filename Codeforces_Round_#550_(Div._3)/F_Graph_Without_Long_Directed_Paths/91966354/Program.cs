using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, M;
    Edge[] E;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        E = new Edge[M];
        for (int i = 0; i < M; i++)
        {
            E[i] = new Edge(sc.NextInt() - 1, sc.NextInt() - 1, i);
        }

        /*
         * n頂点m辺グラフ
         * 連結
         * 
         * 辺に向きつける
         * 
         * 長さ2以上のパスが無いようにする
         */

        var ls = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            ls[i] = new List<int>();
        }
        for (int i = 0; i < M; i++)
        {
            ls[E[i].U].Add(E[i].V);
            ls[E[i].V].Add(E[i].U);
        }

        int[] parity = new int[N];
        var q = new Queue<int>();

        q.Enqueue(0);
        parity[0] = 1;

        while (q.Count > 0)
        {
            var d = q.Dequeue();
            foreach (var to in ls[d])
            {
                if (parity[to] == 0)
                {
                    parity[to] = -parity[d];
                    q.Enqueue(to);
                }
                else if (parity[to] == parity[d])
                {
                    Console.WriteLine("NO");
                    return;
                }
            }
        }

        int[] ans = new int[M];
        for (int i = 0; i < M; i++)
        {
            ans[i] = parity[E[i].U] < parity[E[i].V] ? 0 : 1;
        }

        Console.WriteLine("YES");
        Console.WriteLine(string.Join("",ans));
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

struct Edge
{
    public int U, V, I;
    public Edge(int u, int v, int i)
    {
        U = u;
        V = v;
        I = i;
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
