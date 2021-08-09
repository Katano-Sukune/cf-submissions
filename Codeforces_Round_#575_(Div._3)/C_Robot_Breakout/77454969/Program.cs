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
            int n = sc.NextInt();
            int[] x = new int[n];
            int[] y = new int[n];
            int[][] f = new int[n][];
            for (int j = 0; j < n; j++)
            {
                x[j] = sc.NextInt();
                y[j] = sc.NextInt();
                f[j] = new int[4];
                for (int k = 0; k < 4; k++)
                {
                    f[j][k] = sc.NextInt();
                }
            }
            sb.AppendLine(Query(n, x, y, f));
        }
        Console.Write(sb);
    }

    string Query(int n, int[] x, int[] y, int[][] f)
    {
        // ロボット x,yにいる
        // f[j] = 1なら移動可能 0なら不可能
        // 1箇所に集められるか? 可能なら場所

        // 左上、右下
        int x1, y1, x2, y2;
        x1 = y1 = -100000;
        x2 = y2 = 100000;
        for (int i = 0; i < n; i++)
        {
            int xx1 = f[i][0] == 1 ? -100000 : x[i];
            int yy2 = f[i][1] == 1 ? 100000 : y[i];
            int xx2 = f[i][2] == 1 ? 100000 : x[i];
            int yy1 = f[i][3] == 1 ? -100000 : y[i];

            if (x2 < xx1 || y2 < yy1) return "0";
            if (x1 > xx2 || y1 > yy2) return "0";
            x1 = Math.Max(x1, xx1);
            y1 = Math.Max(y1, yy1);
            x2 = Math.Min(x2, xx2);
            y2 = Math.Min(y2, yy2);
        }
        return $"1 {x1} {y1}";
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
