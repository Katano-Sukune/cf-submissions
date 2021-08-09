using System;
using System.Text;
using CompLib.Util;

public class Program
{
    private int N, M, K;


    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        M = sc.NextInt();
        K = sc.NextInt();

        for (int i = 0; i < K; i++)
        {
            sc.Array();
            sc.Array();
        }

        var sb = new StringBuilder();
        // 左上
        sb.Append(new string('L', M - 1));
        sb.Append(new string('U', N - 1));
        sb.Append(new string('R', M - 1));
        for (int i = 0; i < N - 1; i++)
        {
            sb.Append('D');
            sb.Append(new string((i % 2 == 0 ? 'L' : 'R'), M - 1));
        }

        Console.WriteLine(sb.Length);
        Console.WriteLine(sb.ToString());
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