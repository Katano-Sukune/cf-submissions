using System;
using System.IO;
using System.Linq;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});

        for (int i = 0; i < t; i++)
        {
            Console.WriteLine(Q(sc.NextInt(), sc.IntArray()));
        }

        Console.Out.Flush();
    }

    string Q(int n, int[] q)
    {
        int[] p = new int[n];
        int max = 0;
        bool[] used = new bool[n + 1];
        used[max] = true;
        int u = 1;

        for (int i = 0; i < n; i++)
        {
            if (max < q[i])
            {
                p[i] = q[i];
                max = q[i];
                used[q[i]] = true;
            }
            else
            {
                while (u <= n && used[u])
                {
                    u++;
                }

                if (u < max)
                {
                    p[i] = u;
                    used[u] = true;
                }
                else
                {
                    return "-1";
                }
            }
        }

        return string.Join(" ", p);
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