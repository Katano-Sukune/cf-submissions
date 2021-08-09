using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private string[] T;
    private int[] L, R;

    private int[] D;

    private bool[] Rev;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        T = new string[N];
        L = new int[N];
        R = new int[N];
        for (int i = 0; i < N; i++)
        {
            T[i] = sc.Next();
            switch (T[i])
            {
                case "IN":
                    L[i] = sc.NextInt();
                    break;
                case "NOT":
                    L[i] = sc.NextInt() - 1;
                    break;
                default:
                    L[i] = sc.NextInt() - 1;
                    R[i] = sc.NextInt() - 1;
                    break;
            }
        }

        D = new int[N];

        Go(0);
        // Console.WriteLine(string.Join(" ", D));
        Rev = new bool[N];

        Go2(0);


        // Console.WriteLine(string.Join(" ", Rev.Select(f => f? 1:0)));
        List<int> ans = new List<int>();
        for (int i = 0; i < N; i++)
        {
            if (T[i] == "IN")
            {
                if (Rev[i]) ans.Add(1 - D[0]);
                else ans.Add(D[0]);
            }
        }

        Console.WriteLine(string.Join("", ans));
    }

    void Go(int cur)
    {
        if (T[cur] == "IN")
        {
            D[cur] = L[cur];
        }
        else if (T[cur] == "NOT")
        {
            Go(L[cur]);
            D[cur] = 1 - D[L[cur]];
        }
        else if (T[cur] == "AND")
        {
            Go(L[cur]);
            Go(R[cur]);
            D[cur] = D[L[cur]] & D[R[cur]];
        }
        else if (T[cur] == "OR")
        {
            Go(L[cur]);
            Go(R[cur]);
            D[cur] = D[L[cur]] | D[R[cur]];
        }
        else if (T[cur] == "XOR")
        {
            Go(L[cur]);
            Go(R[cur]);
            D[cur] = D[L[cur]] ^ D[R[cur]];
        }
    }

    void Go2(int cur)
    {
        // Console.WriteLine(cur);
        if (T[cur] == "IN")
        {
            Rev[cur] = true;
        }
        else if (T[cur] == "NOT")
        {
            Go2(L[cur]);
        }
        else if (T[cur] == "AND")
        {
            // 0 0
            // 1 0 r
            // 0 1 l
            // 1 1 lr
            if (D[L[cur]] == 1)
            {
                Go2(R[cur]);
            }

            if (D[R[cur]] == 1)
            {
                Go2(L[cur]);
            }
        }
        else if (T[cur] == "OR")
        {
            // 1 1
            // 1 0 l
            // 0 1 r
            // 0 0 l,r
            if (D[L[cur]] == 0)
            {
                Go2(R[cur]);
            }

            if (D[R[cur]] == 0)
            {
                Go2(L[cur]);
            }
        }
        else if (T[cur] == "XOR")
        {
            // 0 0 l r
            // 0 1

            Go2(L[cur]);
            Go2(R[cur]);
        }
    }

    // public static void Main(string[] args) => new Program().Solve();
    public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 29).Start();
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