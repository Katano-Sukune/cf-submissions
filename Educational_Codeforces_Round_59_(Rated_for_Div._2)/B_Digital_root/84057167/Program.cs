using System;
using System.Text;
using CompLib.Util;

public class Program
{
    private int N;
    private long[] K;
    private int[] X;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = new long[N];
        X = new int[N];
        for (int i = 0; i < N; i++)
        {
            K[i] = sc.NextLong();
            X[i] = sc.NextInt();
        }

        // 1桁になるまで各桁の値の和で置き換える
        // 結果がXになる数 K番目

        // 最小 X
        // 次 10 + X-1

        // 9足す
        var sb = new StringBuilder();
        for (int i = 0; i < N; i++)
        {
            long ans = X[i] + 9 * (K[i] - 1);
            sb.AppendLine(ans.ToString());
        }

        Console.Write(sb);
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