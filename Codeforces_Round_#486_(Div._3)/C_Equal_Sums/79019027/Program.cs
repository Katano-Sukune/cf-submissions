using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int k = sc.NextInt();
        int[] n = new int[k];
        int[][] a = new int[k][];
        for (int i = 0; i < k; i++)
        {
            n[i] = sc.NextInt();
            a[i] = sc.IntArray();
        }

        // a_iのj番目を消したときの和
        long[][] tmp = new long[k][];
        var map = new Dictionary<long, Pair>();
        for (int i = 0; i < k; i++)
        {
            long s = 0;
            foreach (var j in a[i])
            {
                s += j;
            }
            for (int j = 0; j < n[i]; j++)
            {
                long t = s - a[i][j];
                Pair o;
                if (map.TryGetValue(t, out o))
                {
                    if (o.I != i)
                    {
                        Console.WriteLine("YES");
                        Console.WriteLine($"{o.I + 1} {o.J + 1}");
                        Console.WriteLine($"{i + 1} {j + 1}");
                        return;
                    }
                }
                else
                {
                    map[t] = new Pair() { I = i, J = j };
                }
            }

        }
        Console.WriteLine("NO");
    }

    public static void Main(string[] args) => new Program().Solve();
}

struct Pair
{
    public int I, J;
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
