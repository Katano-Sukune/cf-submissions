using System;
using CompLib.Util;

public class Program
{
    private long N, P;
    private int D, W;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextLong();
        P = sc.NextLong();
        W = sc.NextInt();
        D = sc.NextInt();

        // 勝ったチームにWポイント 負けに0ポイント
        // 引き分けなら 両方にDポイント

        // N回の試合して得点がPポイント

        // (勝ち、引き分け、負け) = (x,y,z)
        // 一つ出力

        // 無いなら-1

        // 引き分け回数をW回まで調べる

        for (long draw = 0; draw < W; draw++)
        {
            long m = P - draw * D;

            if (m >= 0 && m % W == 0)
            {
                long win = m / W;
                if (win >= 0 && draw + win <= N)
                {
                    Console.WriteLine($"{win} {draw} {N - win - draw}");
                    return;
                }
            }
        }

        Console.WriteLine("-1");
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