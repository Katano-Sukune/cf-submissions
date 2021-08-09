using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < t; i++)
        {
            int n = sc.NextInt();
            int x = sc.NextInt();
            int[] d = new int[n];
            int[] h = new int[n];
            for (int j = 0; j < n; j++)
            {
                d[j] = sc.NextInt();
                h[j] = sc.NextInt();
            }
            sb.AppendLine(Query(n, x, d, h));
        }
        Console.Write(sb);
    }

    string Query(int n, int x, int[] d, int[] h)
    {
        // 最初頭 x個
        // i番目の攻撃すると 頭 min(d_i, curX)減る
        // まだ頭あるなら h_i頭増える

        // 頭0にする最小回数

        // d-hの最大
        int max = int.MinValue;
        // dの最大
        int max2 = int.MinValue;
        for (int i = 0; i < n; i++)
        {
            max = Math.Max(max, d[i] - h[i]);
            max2 = Math.Max(max2, d[i]);
        }

        if (max2 >= x) return "1";
        if (max <= 0) return "-1";
        return ((x - max2 + max - 1) / max + 1).ToString();
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
