using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int m = sc.NextInt();
        var e = new List<int>[n];
        for (int i = 0; i < n; i++)
        {
            e[i] = new List<int>();
        }

        for (int i = 0; i < m; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            e[v].Add(u);
            e[u].Add(v);
        }
        int ans = 0;
        bool[] f = new bool[n];
        for (int i = 0; i < n; i++)
        {
            if (f[i]) continue;
            if (e[i].Count != 2) continue;
            var stack = new List<int>();
            stack.Add(i);
            bool flag = true;
            int p = e[i][0];
            for (int j = 0; p != stack[0];j++)
            {
            
                if (f[p] || e[p].Count != 2)
                {
                    flag = false;
                    break;
                }
                f[p] = true;
                stack.Add(p);
                if(stack[j] == e[p][0])
                {
                    p = e[p][1];
                }
                else
                {
                    p = e[p][0];
                }
            }
            if (flag)
            {
                ans++;
            }
        }
        Console.WriteLine(ans);
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
