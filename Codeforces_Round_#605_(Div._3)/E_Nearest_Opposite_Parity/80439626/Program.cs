using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    int[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.IntArray();

        List<int>[] rev = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            rev[i] = new List<int>();
        }
        List<int> odd = new List<int>();
        List<int> even = new List<int>();
        for (int i = 0; i < N; i++)
        {
            if (A[i] % 2 == 0) even.Add(i);
            else odd.Add(i);
            if (i + A[i] < N) rev[i + A[i]].Add(i);
            if (i - A[i] >= 0) rev[i - A[i]].Add(i);
        }
        int[] ans = new int[N];
        {
            int[] dist = new int[N];
            for (int i = 0; i < N; i++)
            {
                dist[i] = -1;
            }
            var q = new Queue<int>();
            foreach (int i in odd)
            {
                q.Enqueue(i);
                dist[i] = 0;
            }

            while (q.Count > 0)
            {
                var d = q.Dequeue();
                foreach (int to in rev[d])
                {
                    if (dist[to] != -1) continue;
                    q.Enqueue(to);
                    dist[to] = dist[d] + 1;
                }
            }

            foreach (int i in even)
            {
                ans[i] = dist[i];
            }
        }

        {
            int[] dist = new int[N];
            for (int i = 0; i < N; i++)
            {
                dist[i] = -1;
            }
            var q = new Queue<int>();
            foreach (int i in even)
            {
                q.Enqueue(i);
                dist[i] = 0;
            }

            while (q.Count > 0)
            {
                var d = q.Dequeue();
                foreach (int to in rev[d])
                {
                    if (dist[to] != -1) continue;
                    q.Enqueue(to);
                    dist[to] = dist[d] + 1;
                }
            }

            foreach (int i in odd)
            {
                ans[i] = dist[i];
            }
        }
        Console.WriteLine(string.Join(" ", ans));
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
