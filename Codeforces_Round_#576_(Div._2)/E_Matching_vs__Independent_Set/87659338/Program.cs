using System;
using System.Collections.Generic;
using System.IO;
using CompLib.Util;
using Microsoft.VisualBasic;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        int m = sc.NextInt();
        E[] edge = new E[m];
        for (int i = 0; i < m; i++)
        {
            edge[i] = new E(sc.NextInt() - 1, sc.NextInt() - 1);
        }

        bool[] ind = new bool[3 * n];
        List<int> matching = new List<int>();
        for (int i = 0; i < m; i++)
        {
            if (ind[edge[i].F] || ind[edge[i].T]) continue;
            matching.Add(i);
            ind[edge[i].F] = ind[edge[i].T] = true;
        }

        if (matching.Count >= n)
        {
            Console.WriteLine("Matching");
            var ans = new int[n];
            for (int i = 0; i < n; i++)
            {
                ans[i] = matching[i] + 1;
            }

            Console.WriteLine(string.Join(" ", ans));
        }
        else
        {
            int[] indSet = new int[n];
            int index = 0;
            for (int i = 0; i < 3 * n && index < n; i++)
            {
                if (!ind[i]) indSet[index++] = i + 1;
            }

            if (index >= n)
            {
                Console.WriteLine("IndSet");
                Console.WriteLine(string.Join(" ", indSet));
            }
            else
            {
                Console.WriteLine("Impossible");
            }
        }
    }

    public static void Main(string[] args) => new Program().Solve();
}

struct E
{
    public int F, T;

    public E(int f, int t)
    {
        F = f;
        T = t;
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