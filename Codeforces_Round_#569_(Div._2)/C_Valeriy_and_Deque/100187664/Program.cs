using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, Q;
    private int[] A;
    private long[] M;


    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        Q = sc.NextInt();
        A = sc.IntArray();
        M = new long[Q];
        for (int i = 0; i < Q; i++)
        {
            M[i] = sc.NextLong();
        }

        /*
         * deque
         * 最初a
         *
         * 先頭2個取り出す A,B
         * 大きい方先頭
         * 小さい方末尾
         *
         * M_j回目に取り出すA,B
         */

        // 最大取り出す -> ループ

        int max = A.Max();
        List<(int a, int b)> ls = new List<(int a, int b)>();
        List<(int a, int b)> ls2 = new List<(int a, int b)>();
        var deq = new LinkedList<int>();
        for (int i = 0; i < N; i++)
        {
            deq.AddLast(A[i]);
        }

        int d = -1;
        for (int i = 0;; i++)
        {
            int a = deq.First();
            deq.RemoveFirst();
            int b = deq.First();
            deq.RemoveFirst();


            if (a >= b)
            {
                deq.AddFirst(a);
                deq.AddLast(b);
            }
            else
            {
                deq.AddFirst(b);
                deq.AddLast(a);
            }

            if (a == max)
            {
                ls2.Add((a, b));
                d = i;
                break;
            }

            ls.Add((a, b));
        }

        for (int i = 0; i < N - 2; i++)
        {
            int a = deq.First();
            deq.RemoveFirst();
            int b = deq.First();
            deq.RemoveFirst();
            ls2.Add((a, b));
            if (a >= b)
            {
                deq.AddFirst(a);
                deq.AddLast(b);
            }
            else
            {
                deq.AddFirst(b);
                deq.AddLast(a);
            }
        }

        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        for (int i = 0; i < Q; i++)
        {
            int a, b;
            if (M[i] <= d)
            {
                (a, b) = ls[(int) (M[i] - 1)];
            }
            else
            {
                (a, b) = ls2[(int) ((M[i] - d - 1) % (N - 1))];
            }

            Console.WriteLine($"{a} {b}");
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