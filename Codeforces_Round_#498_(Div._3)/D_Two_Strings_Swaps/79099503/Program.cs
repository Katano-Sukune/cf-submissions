using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    string A, B;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.Next();
        B = sc.Next();

        int cnt = 0;
        for (int i = 0; i < N / 2; i++)
        {
            // 全部同じ
            // 2個違う
            if (B[i] == B[N - i - 1])
            {
                if (A[i] != A[N - i - 1])
                {
                    cnt++;
                }
            }
            else
            {
                if (A[i] == B[i] && A[N - i - 1] == B[N - i - 1]) continue;
                if (A[i] == B[N - i - 1] && A[N - i - 1] == B[i]) continue;
                if (A[i] == B[i] || A[i] == B[N - i - 1]) cnt++;
                else if (A[N - i - 1] == B[i] || A[N - i - 1] == B[N-i - 1]) cnt++;
                else cnt += 2;
            }
        }
        if (N % 2 == 1 && A[N / 2] != B[N / 2]) cnt++;
        Console.WriteLine(cnt);
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
