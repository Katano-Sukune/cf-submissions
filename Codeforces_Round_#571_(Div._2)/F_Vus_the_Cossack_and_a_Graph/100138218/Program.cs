using System;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, M;
    private int[] U, V;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        U = new int[M];
        V = new int[M];
        for (int i = 0; i < M; i++)
        {
            (U[i], V[i]) = (sc.NextInt() - 1, sc.NextInt() - 1);
        }

        /*
         * d_i
         * 元グラフのiの次数
         *
         * ceil((n+m)/2)辺以下になるまで辺を消す
         *
         * f_i
         * 消した後のiの次数
         *
         * ceil(d_i / 2) <= f_i
         *
         * 残す辺
         *
         * 辺消す
         * 次数全体 -2
         * 辺 -1
         */

        int[] d = new int[N];
        for (int i = 0; i < M; i++)
        {
            d[U[i]]++;
            d[V[i]]++;
        }

        int[] f = new int[N];
        for (int i = 0; i < N; i++)
        {
            f[i] = (d[i] + 1) / 2;
        }

        int[] a = new int[M];
        for (int i = 0; i < M; i++)
        {
            a[i] = i;
        }

        Array.Sort(a, (l, r) => Math.Min(U[l], V[l]).CompareTo(Math.Min(U[r], V[r])));

        bool[] flag = new bool[M];
        foreach (int i in a)
        {
            if (d[U[i]] > f[U[i]] && d[V[i]] > f[V[i]])
            {
                flag[i] = true;
                d[U[i]]--;
                d[V[i]]--;
            }
        }

        int k = flag.Count(b => !b);
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        Console.WriteLine(k);
        for (int i = 0; i < M; i++)
        {
            if (flag[i]) continue;
            Console.WriteLine($"{U[i] + 1} {V[i] + 1}");
        }

        Console.Out.Flush();
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