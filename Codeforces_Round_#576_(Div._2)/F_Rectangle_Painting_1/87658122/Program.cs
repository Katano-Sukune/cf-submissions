using System;
using CompLib.Util;

public class Program
{
    private int N;
    private string[] S;

    private int[] Sum;
    private int[] Memo;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        S = new string[N];
        for (int i = 0; i < N; i++)
        {
            S[i] = sc.Next();
        }

        Sum = new int[(N + 1) * (N + 1)];
        for (int i = 0; i < N; i++)
        {
            var ln = new int[N + 1];
            for (int j = 0; j < N; j++)
            {
                ln[j + 1] = ln[j];
                if (S[i][j] == '#') ln[j + 1]++;
                Sum[Conv2(i + 1, j + 1)] = Sum[Conv2(i, j + 1)] + ln[j + 1];
            }
        }

        Memo = new int[(N + 1) * (N + 1) * (N + 1) * (N + 1)];
        for (int i = 0; i < Memo.Length; i++)
        {
            Memo[i] = int.MaxValue;
        }

        Console.WriteLine(Calc(0, 0, N, N));
    }

    int Calc(int x1, int y1, int x2, int y2)
    {
        ref int result = ref Memo[Conv(x1, y1, x2, y2)];
        if (result != int.MaxValue) return result;
        // xで切り分け

        int s = Sum[Conv2(x2, y2)] - Sum[Conv2(x2, y1)] - Sum[Conv2(x1, y2)] + Sum[Conv2(x1, y1)];
        result = s == 0 ? 0 : Math.Max(x2 - x1, y2 - y1);

        if (x2 - x1 >= 2)
        {
            for (int x3 = x1 + 1; x3 < x2; x3++)
            {
                result = Math.Min(result, Calc(x1, y1, x3, y2) + Calc(x3, y1, x2, y2));
            }
        }

        //y
        if (y2 - y1 >= 2)
        {
            for (int y3 = y1 + 1; y3 < y2; y3++)
            {
                result = Math.Min(result, Calc(x1, y1, x2, y3) + Calc(x1, y3, x2, y2));
            }
        }

        return result;
    }

    int Conv(int x1, int y1, int x2, int y2)
    {
        return x1 * (N + 1) * (N + 1) * (N + 1) + y1 * (N + 1) * (N + 1) + x2 * (N + 1) + y2;
    }

    int Conv2(int x, int y)
    {
        return x * (N + 1) + y;
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