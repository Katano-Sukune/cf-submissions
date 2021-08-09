using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N;
    private int[] P;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        P = sc.IntArray();

        // 先手 x選ぶ
        // 後手 y

        // 先手 x 隣 p_xより小さい 相手とかぶらない

        // 後手 y 隣 p_yより大きい

        // 動けなくなったら負け

        // 先手が勝てるxの個数


        // 左右片方だけ動ける
        // 動ける方塞がれて負け

        // 両方動ける
        // 距離2

        // そこ以外 距離 2未満

        int[] f = new int[N];
        for (int i = 0; i < N; i++)
        {
            if (i == 0 || P[i - 1] >= P[i]) f[i] = 0;
            else f[i] = f[i - 1] + 1;
        }


        int[] b = new int[N];
        for (int i = N - 1; i >= 0; i--)
        {
            if (i == N - 1 || P[i + 1] >= P[i]) b[i] = 0;
            else b[i] = b[i + 1] + 1;
        }

        int idx = -1;
        int max = -1;
        for (int i = 0; i < N; i++)
        {
            if (b[i] == f[i] && max < b[i] && b[i] % 2 == 0)
            {
                idx = i;
                max = b[i];
            }
        }

        if (idx == -1)
        {
            Console.WriteLine("0");
            return;
        }

        bool flag2 = true;
        for (int i = 0; i < N; i++)
        {
            flag2 &= i == idx || (b[i] < max && f[i] < max);
        }

        if (flag2)
        {
            Console.WriteLine("1");
        }
        else
        {
            Console.WriteLine("0");
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