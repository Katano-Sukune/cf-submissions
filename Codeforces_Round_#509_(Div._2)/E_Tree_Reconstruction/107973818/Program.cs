using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{
    private int N;
    private int[] A, B;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = new int[N - 1];
        B = new int[N - 1];
        for (int i = 0; i < N - 1; i++)
        {
            A[i] = sc.NextInt();
            B[i] = sc.NextInt();
        }

        for (int i = 0; i < N - 1; i++)
        {
            if (B[i] != N)
            {
                Console.WriteLine("NO");
                return;
            }
        }

        int[] cnt = new int[N + 1];
        for (int i = 0; i < N - 1; i++)
        {
            cnt[A[i]]++;
        }

        var ls = new List<int>[N];
        var ls2 = new List<int>();
        var st = new Stack<(int v, int cnt)>();
        for (int i = N - 1; i >= 1; i--)
        {
            if (cnt[i] > 0)
            {
                if (cnt[i] >= 2) st.Push((i, cnt[i] - 1));
                ls2.Add(i);
                ls[i] = new List<int>();
            }
            else
            {
                if (st.Count <= 0)
                {
                    Console.WriteLine("NO");
                    return;
                }

                var d = st.Pop();
                ls[d.v].Add(i);
                if (d.cnt >= 2) st.Push((d.v, d.cnt - 1));
            }
        }

        if (st.Count > 0)
        {
            Console.WriteLine("NO");
            return;
        }

#if !DEBUG
System.Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
#endif
        Console.WriteLine("YES");
        foreach (int v in ls2)
        {
            int tmp = N;
            foreach (var w in ls[v])
            {
                Console.WriteLine($"{w} {tmp}");
                tmp = w;
            }

            Console.WriteLine($"{v} {tmp}");
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