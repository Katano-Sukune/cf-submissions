using System;
using CompLib.Util;

public class Program
{
    private int N, K;
    private string S;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        S = sc.Next();

        // WW ずっと変わらない

        //

        // WBWBWBW のやつ

        // 端に近いやつ 近い方の端に合わせる
        // 偶奇で WBが入れ替わる

        int[] d = new int[N];
        for (int i = 0; i < N; i++)
        {
            d[i] = int.MaxValue;
        }

        int pp = int.MinValue;

        for (int i = 1; i < N * 2; i++)
        {
            if (S[i % N] == S[(i - 1) % N])
            {
                pp = i;
            }

            if (pp != int.MinValue)
                d[i % N] = Math.Min(d[i % N], i - pp);
        }

        pp = int.MaxValue;
        for (int i = N * 2 - 2; i >= 0; i--)
        {
            if (S[(i + 1) % N] == S[i % N])
            {
                pp = i;
            }

            if (pp != int.MaxValue)
                d[i % N] = Math.Min(d[i % N], pp - i);
        }

        char[] ans = new char[N];
        for (int i = 0; i < N; i++)
        {
            int p = Math.Min(d[i], K) % 2;
            if (p == 0) ans[i] = S[i];
            else ans[i] = S[i] == 'B' ? 'W' : 'B';
        }

        Console.WriteLine(new string(ans));
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
                string s = Console.ReadLine();
                while (s.Length == 0)
                {
                    s = Console.ReadLine();
                }

                _line = s.Split(Separator);
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