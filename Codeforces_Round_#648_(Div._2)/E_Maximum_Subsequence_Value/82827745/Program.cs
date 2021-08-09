using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N;
    long[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        A = sc.LongArray();

        // 長さK Aの部分配列 S

        // Sの値 max(K-2, 1) 個以上 Sの立っているbit立たせられる

        // 桁最大のやつ使う

        long ans = long.MinValue;

        for (int i = 0; i < N; i++)
        {
            ans = Math.Max(ans, A[i]);
            for (int j = i + 1; j < N; j++)
            {
                ans = Math.Max(ans, A[i] | A[j]);
                for (int k = j + 1; k < N; k++)
                {
                    ans = Math.Max(ans, A[i] | A[j] | A[k]);
                }
            }
        }
        Console.WriteLine(ans);
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
