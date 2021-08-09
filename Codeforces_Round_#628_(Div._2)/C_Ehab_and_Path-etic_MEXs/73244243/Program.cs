using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{
    private int N;
    private List<E>[] E;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        E = new List<E>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<E>();
        }

        for (int i = 0; i < N-1; i++)
        {
            int a = sc.NextInt() - 1;
            int b = sc.NextInt() - 1;
            E[a].Add(new E(b, i));
            E[b].Add(new E(a, i));
        }

        /*
         * 1本 適当
         *
         * 枝がある
         *
         * Aに0 Bに1 Cに2を割り当てる
         * のこりてきとう
         */
        var ans = new int[N - 1];
        if (E.Count(l => l.Count == 1) < 3)
        {
            for (int i = 0; i < N - 1; i++)
            {
                ans[i] = i;
            }
        }
        else
        {


            for (int i = 0; i < N - 1; i++)
            {
                ans[i] = -1;
            }

            int c = 0;
            for (int i = 0; i < N; i++)
            {
                if (E[i].Count == 1 && c < 3)
                {
                    ans[E[i][0].Index] = c++;
                }
            }

            for (int i = 0; i < N - 1; i++)
            {
                if (ans[i] == -1)
                {
                    ans[i] = c++;
                }
            }
        }

        Console.WriteLine(string.Join("\n", ans));
    }

    public static void Main(string[] args) => new Program().Solve();
}

struct E
{
    public int To;
    public int Index;

    public E(int t, int i)
    {
        To = t;
        Index = i;
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