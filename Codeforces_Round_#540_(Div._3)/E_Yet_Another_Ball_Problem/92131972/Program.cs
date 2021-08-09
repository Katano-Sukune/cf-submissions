using System;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{
    long N, K;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();

        /*
         * n組男女ペア
         * 
         * b_i 男の色
         * g_i 女の色
         * 
         * 1 <= b_i,g_i <= k
         * 
         * i != jなら (b_i, g_i) != (b_j, g_i)
         * 
         * i+1 = j なら b_i != b_j かつ g_i != g_i
         * 
         * ペアが無い or ペアを見つける
         */

        // ペアの個数

        if (K * K - K < N)
        {
            Console.WriteLine("NO");
            return;
        }

        // 1 1 ~ K
        // 2 1 ~ K

        // 1 k
        // 2 k-1
        // k k-2

        // 1 k-1
        // 2 k-2
        // k-1 1

        // 1 k-2
        // 2 k-3
        // k-2 1
        // k-1 k
        // k k-1

        // 1 k-3
        // 2 k-4
        // k-3 1
        // k-2

        long c = 0;
        var sb = new StringBuilder();
        for (int p = 1; c < N && p < K; p++)
        {
            for (int q = 0; c < N && q < K; q++)
            {
                long b = q;
                long g = (p + q) % K;
                if (b == g) continue;
                c++;
                sb.AppendLine($"{b + 1} {g + 1}");

            }
        }
        Console.WriteLine("YES");
        Console.Write(sb);
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
