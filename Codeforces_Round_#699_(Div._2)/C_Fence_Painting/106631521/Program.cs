using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        int t = sc.NextInt();
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
        int[] a = new int[n];
        for (int i = 0; i < n; i++)
        {
            a[i] = sc.NextInt() - 1;
        }

        int[] b = new int[n];
        for (int i = 0; i < n; i++)
        {
            b[i] = sc.NextInt() - 1;
        }

        int[] c = new int[m];
        for (int i = 0; i < m; i++)
        {
            c[i] = sc.NextInt() - 1;
        }

        int p1 = -1;

        for (int i = 0; i < n; i++)
        {
            if (c[^1] == b[i] && (p1 == -1 || a[i] != b[i]))
            {
                p1 = i;
            }
        }

        if (p1 == -1)
        {
            Console.WriteLine("NO");
            return;
        }

        var q = new Queue<int>[n];
        for (int i = 0; i < n; i++)
        {
            q[i] = new Queue<int>();
        }

        for (int i = 0; i < m - 1; i++)
        {
            q[c[i]].Enqueue(i);
        }

        int[] ans = new int[m];
        for (int i = 0; i < n; i++)
        {
            if (i == p1) continue;
            if(a[i] == b[i]) continue;
            if (q[b[i]].Count <= 0)
            {
                Console.WriteLine("NO");
                return;
            }

            ans[q[b[i]].Dequeue()] = i + 1;
        }

        for (int i = 0; i < n; i++)
        {
            while (q[i].Count > 0) ans[q[i].Dequeue()] = p1 + 1;
        }

        ans[^1] = p1 + 1;
        Console.WriteLine("YES");
        Console.WriteLine(string.Join(" ", ans));
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