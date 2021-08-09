using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int q = sc.NextInt();
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < q; i++)
        {
            sb.Append(Query(sc.NextInt(), sc.NextInt()));
        }
        Console.Write(sb);
    }

    string Query(int b, int w)
    {
        // 市松模様
        // 連結マス、黒がb個、白がw個
        // (1,1)は白色
        // 白>黒
        // 白黒白黒... 余り黒の上下
        StringBuilder sb = new StringBuilder();
        if (b > w)
        {
            // 黒>白
            if (b > 3 * w + 1) return "NO\n";
            for (int i = 1; i <= 2 * w + 1; i++)
            {
                sb.AppendLine($"2 {i}");
            }
            int d = b - w - 1;
            for (int i = 0; i < d / 2; i++)
            {
                sb.AppendLine($"1 {(i + 1) * 2}");
                sb.AppendLine($"3 {(i + 1) * 2}");
            }
            if (d % 2 == 1)
            {
                sb.AppendLine($"1 {(d / 2 + 1) * 2}");
            }
        }
        else
        {
            if (w > 3 * b + 1) return "NO\n";
            if (w == b)
            {
                for (int i = 1; i <= w + b; i++)
                {
                    sb.AppendLine($"1 {i}");
                }
            }
            else
            {
                for (int i = 1; i <= 2 * b + 1; i++)
                {
                    sb.AppendLine($"3 {i}");
                }
                int d = w - b - 1;
                for (int i = 0; i < d / 2; i++)
                {
                    sb.AppendLine($"2 {(i + 1) * 2}");
                    sb.AppendLine($"4 {(i + 1) * 2}");
                }
                if (d % 2 == 1)
                {
                    sb.AppendLine($"2 {(d / 2 + 1) * 2}");
                }
            }
        }
        return $"YES\n{sb}";
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
