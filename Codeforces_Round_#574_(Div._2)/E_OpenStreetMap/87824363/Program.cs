using CompLib.Util;
using System;
using System.Collections.Generic;

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

        G0 = sc.NextInt();
        X = sc.NextInt();
        Y = sc.NextInt();
        Z = sc.NextInt();

        int[] H = new int[N * M];
        H[0] = G0;
        for (int i = 1; i < N * M; i++)
        {
            H[i] = (int) (((long) H[i - 1] * X + Y) % Z);
        }

        MinimumQueue[] q = new MinimumQueue[N];
        for (int i = 0; i < N; i++)
        {
            q[i] = new MinimumQueue();
            for (int j = 0; j < B - 1; j++)
            {
                q[i].Enqueue(H[i * M + j]);
            }
        }

        long ans = 0;
        for (int j = B - 1; j < M; j++)
        {
            var q2 = new MinimumQueue();
            for (int i = 0; i < N; i++)
            {
                q[i].Enqueue(H[i * M + j]);
                q2.Enqueue(q[i].Min());
                if (i >= A - 1)
                {
                    ans += q2.Min();
                    q2.Dequeue();
                }

                q[i].Dequeue();
            }
        }

        Console.WriteLine(ans);
    }

    public static void Main(string[] args) => new Program().Solve();
}

class MinimumStack
{
    private readonly Stack<(int num, int min)> stack;

    public MinimumStack()
    {
        stack = new Stack<(int num, int min)>();
    }

    public void Push(int num)
    {
        if (Count == 0) stack.Push((num, num));
        else stack.Push((num, Math.Min(num, stack.Peek().min)));
    }

    public int Peek()
    {
        return stack.Peek().num;
    }

    public int Pop()
    {
        return stack.Pop().num;
    }

    public int Min()
    {
        return Count == 0 ? int.MaxValue : stack.Peek().min;
    }

    public int Count => stack.Count;
}

class MinimumQueue
{
    private readonly MinimumStack s1, s2;

    public MinimumQueue()
    {
        s1 = new MinimumStack();
        s2 = new MinimumStack();
    }

    public void Enqueue(int l)
    {
        s2.Push(l);
    }

    private void Exec()
    {
        if (s1.Count == 0)
        {
            while (s2.Count > 0)
            {
                s1.Push(s2.Pop());
            }
        }
    }

    public int Peek()
    {
        Exec();
        return s1.Peek();
    }

    public int Dequeue()
    {
        Exec();
        return s1.Pop();
    }

    public int Min()
    {
        return Math.Min(s1.Min(), s2.Min());
    }

    public int Count => s1.Count + s2.Count;
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