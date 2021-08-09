using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
#if !DEBUG
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
#endif
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        checked
        {
            int n = sc.NextInt();
            long c = sc.NextInt();

            long[] a = sc.LongArray();
            long[] b = sc.LongArray();

            // i
            // a_i稼げる 昇順

            // xにいるとき
            // a_i稼ぐ
            // b払ってx+1に行く

            // c以上になる最短

            long ans = long.MaxValue;

            // 所持金
            long t = 0;
            // 経過日数
            long d = 0;
            for (int x = 0; x < n; x++)
            {
                // Console.WriteLine($"{x} {t} {d}");
                // 現在の位置で頑張る
                ans = Math.Min(ans, (c - t + a[x] - 1) / a[x] + d);
                // 昇給
                if (x + 1 < n)
                {
                    long day = b[x] <= t ? 0 : (b[x] - t + a[x] - 1) / a[x];
                    t = t + day * a[x] - b[x];
                    d += day + 1;
                }
            }

            Console.WriteLine(ans);
        }
    }

    public static void Main(string[] args) => new Program().Solve();
    // public static void Main(string[] args) => new Thread(new Program().Solve, 1 << 27).Start();
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
                string s;
                do
                {
                    s = Console.ReadLine();
                } while (s.Length == 0);

                _line = s.Split(Separator);
                _index = 0;
            }

            return _line[_index++];
        }

        public string ReadLine()
        {
            _index = _line.Length;
            return Console.ReadLine();
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