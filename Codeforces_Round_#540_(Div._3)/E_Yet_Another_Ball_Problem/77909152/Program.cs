using System;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{
    int N, K;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();

        // Nペア
        // K種類

        // b_iを男の衣装の色, g_iを女の服の色

        // b_i != g_i
        // b_i = b_j, g_i = g_jなペアは存在しない
        // b_i != b_(i+1)
        // g_i != g_(i+1)

        // ペアを見つける

        // K^2

        // K(K-1)

        if (N > (long)K * (K - 1))
        {
            Console.WriteLine("NO");
            return;
        }

        Console.WriteLine("YES");
        var sb = new StringBuilder();

        for (int i = 0; i < N; i++)
        {
            int d = i / K;
            int m = i % K;

            int b = m + 1;
            int g = (m + d + 1) % K + 1;
            sb.AppendLine($"{b} {g}");
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
