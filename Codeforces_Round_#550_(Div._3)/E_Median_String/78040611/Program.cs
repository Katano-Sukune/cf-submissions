using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int K;
    string S, T;
    public void Solve()
    {
        var sc = new Scanner();
        K = sc.NextInt();
        S = sc.Next();
        T = sc.Next();
        int[] ss = S.Select(c => c - 'a').ToArray();
        Array.Reverse(ss);
        int[] tt = T.Select(c => c - 'a').ToArray();
        Array.Reverse(tt);
        int[] sum = new int[K + 1];
        for (int i = 0; i < K; i++)
        {
            sum[i] += ss[i] + tt[i];
            if (sum[i] >= 26)
            {
                sum[i + 1]++;
                sum[i] -= 26;
            }
        }
        int[] ans = new int[K + 1];
        for (int i = K; i >= 0; i--)
        {
            ans[i] = sum[i] / 2;
            if (i - 1 >= 0 && sum[i] % 2 == 1)
            {
                sum[i - 1] += 26;
            }
        }

        char[] cc = new char[K];
        for (int i = 0; i < K; i++)
        {
            cc[K - 1 - i] = (char)(ans[i] + 'a');
        }
        Console.WriteLine(new string(cc));
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
