using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Collections.Generic;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
#if !DEBUG
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
#endif
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    int N, M, A, B;
    List<int>[] E;
    void Q(Scanner sc)
    {
        N = sc.NextInt();
        M = sc.NextInt();
        A = sc.NextInt() - 1;
        B = sc.NextInt() - 1;
        E = new List<int>[N];
        for (int i = 0; i < N; i++)
        {
            E[i] = new List<int>();
        }

        for (int i = 0; i < M; i++)
        {
            int u = sc.NextInt() - 1;
            int v = sc.NextInt() - 1;
            E[u].Add(v);
            E[v].Add(u);
        }

        // AからBを通らずに行けない頂点集合 A'
        // BからA                     B'
        // 
        // |A'|*|B'|
        // 
        Console.WriteLine((long)BFS(A, B) * BFS(B, A));
    }

    int BFS(int source, int t)
    {
        bool[] flag = new bool[N];
        var q = new Queue<int>();
        q.Enqueue(source);
        flag[source] = true;

        while (q.Count > 0)
        {
            int dq = q.Dequeue();
            if (dq == t) continue;
            foreach (int to in E[dq])
            {
                if (flag[to]) continue;
                flag[to] = true;
                q.Enqueue(to);
            }
        }

        return flag.Count(b => !b);
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