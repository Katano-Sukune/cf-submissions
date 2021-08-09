using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{
    private int N, M, A, B;
    private int G0, X, Y, Z;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        A = sc.NextInt();
        B = sc.NextInt();


        int[] g = new int[N * M];
        g[0] = sc.NextInt();
        X = sc.NextInt();
        Y = sc.NextInt();
        Z = sc.NextInt();
        for (int i = 1; i < N * M; i++)
        {
            g[i] = (int) (((long) g[i - 1] * X + Y) % Z);
        }

        var mq = new MinQueue[N];
        for (int i = 0; i < N; i++)
        {
            mq[i] = new MinQueue();
        }

        for (int j = 0; j < B - 1; j++)
        {
            for (int i = 0; i < N; i++)
            {
                mq[i].Enqueue(g[i * M + j]);
            }
        }

        long ans = 0;
        for (int j = B - 1; j < M; j++)
        {
            for (int i = 0; i < N; i++)
            {
                mq[i].Enqueue(g[i * M + j]);
            }

            var mq2 = new MinQueue();
            for (int i = 0; i < A - 1; i++)
            {
                mq2.Enqueue(mq[i].GetMin());
            }

            for (int i = A - 1; i < N; i++)
            {
                mq2.Enqueue(mq[i].GetMin());
                ans += mq2.GetMin();
                mq2.Dequeue();
            }

            for (int i = 0; i < N; i++)
            {
                mq[i].Dequeue();
            }
        }

        Console.WriteLine(ans);
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
}

class MinStack
{
    private Stack<(int val, int min)> S;

    public MinStack()
    {
        S = new Stack<(int val, int min)>();
    }

    public int GetMin()
    {
        if (S.Count == 0) return int.MaxValue;
        return S.Peek().min;
    }

    public void Push(int val)
    {
        S.Push((val, Math.Min(val, GetMin())));
    }

    public int Pop()
    {
        return S.Pop().val;
    }

    public int Count
    {
        get { return S.Count; }
    }
}

class MinQueue
{
    public MinStack L, R;

    public MinQueue()
    {
        L = new MinStack();
        R = new MinStack();
    }

    public void Enqueue(int val)
    {
        R.Push(val);
    }

    public void Dequeue()
    {
        if (L.Count == 0)
        {
            while (R.Count > 0)
            {
                L.Push(R.Pop());
            }
        }

        L.Pop();
    }

    public int GetMin()
    {
        return Math.Min(L.GetMin(), R.GetMin());
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