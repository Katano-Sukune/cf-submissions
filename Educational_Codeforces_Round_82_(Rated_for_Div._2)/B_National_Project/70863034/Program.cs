using CompLib.Util;
using System;
using System.Text;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        var sb = new StringBuilder();
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Q(sc.NextInt(), sc.NextInt(), sc.NextInt()));
        }

        Console.Write(sb.ToString());
    }

    private String Q(int n, int g, int b)
    {
        // 長さnのハイウェイを直す
        /*
         * g日 天気良い日 b日　悪い日　繰り返し
         * 半分以上いい日に敷く
         * 最小日数
         */


        // 半分
        int h = (n + 1) / 2;
        // ループ回数
        int l = (h - 1) / g;
        int m = h - (g * l);

        long cnt = (long)(g + b) * l + m;

        long ans = cnt + Math.Max(0, n - cnt);

        return ans.ToString();
    }

    public static void Main(string[] args) => new Program().Solve();
}

namespace CompLib.Util
{
    using System;
    using System.Linq;
    class Scanner
    {
        private int _index;
        private string[] _line;
        const char separator = ' ';

        public Scanner()
        {
            _index = 0;
            _line = new string[0];
        }

        public string Next()
        {
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(separator);
                _index = 0;
            }
            return _line[_index++];
        }
        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(separator);
            _index = _line.Length;
            return _line;
        }
        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
    }
}