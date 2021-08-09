using System;
using System.Collections.Generic;
using System.Diagnostics;
using CompLib.Util;

public class Program
{
    int N;
    int[] A;
    bool[] FF;
    // iを割る最小の素因数
    int[] Div;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();
        int[] index = new int[N];
        long max = 0;
        for (int i = 0; i < N; i++)
        {
            max = Math.Max(max, A[i]);
            index[i] = i;
        }

        Div = new int[max + 1];
        for (int i = 0; i <= max; i++)
        {
            Div[i] = i;
        }

        for (int i = 2; i * i <= max; i++)
        {
            if (Div[i] == i)
            {
                // iは素数
                for (int j = i + i; j <= max; j += i)
                {
                    Div[j] = Math.Min(Div[j], i);
                }
            }
        }

        Array.Sort(index, (l, r) => A[l].CompareTo(A[r]));

        int[] a = new int[max + 1];
        FF = new bool[max + 1];
        for (int i = 0; i <= max; i++)
        {
            a[i] = -1;
        }

        long min = long.MaxValue;
        int aa = -1;
        int bb = -1;

        for (int i = 0; i < N; i++)
        {
            int ind = index[i];
            var dd = F(A[ind]);
            // Console.WriteLine($"{A[ind]}:{string.Join(" ", dd)}");
            foreach (var d in dd)
            {
                if (a[d] == -1) a[d] = ind;
                else
                {
                    // Console.WriteLine($"{A[a[d]]}*{A[ind]}/{d}");
                    FF[d] = true;
                    long t = (long)A[a[d]] * A[ind] / d;
                    if (t < min)
                    {
                        aa = a[d] + 1;
                        bb = ind + 1;
                        min = t;
                    }
                }
            }
        }
        // Console.WriteLine(min);
        if (aa > bb)
        {
            int t = bb;
            bb = aa;
            aa = t;
        }
        Console.WriteLine($"{aa} {bb}");
    }

    List<int> F(int l)
    {
        // lの約数列挙
        var result = new List<int>();
        result.Add(1);
        while (l != 1)
        {
            int t = result.Count;
            int p = Div[l];
            int pp = 1;
            while (l % p == 0)
            {
                pp *= p;
                for (int i = 0; i < t; i++)
                {
                    result.Add(result[i] * pp);
                }
                l /= p;
            }
        }
        var r = new List<int>();
        foreach (var i in result)
        {
            if (FF[i]) continue;
            r.Add(i);
        }
        return r;
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
