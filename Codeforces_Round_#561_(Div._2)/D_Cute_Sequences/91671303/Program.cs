using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        long a = sc.NextLong();
        long b = sc.NextLong();
        long m = sc.NextLong();

        /*
         * mが与えられる
         * 
         * x_1, x_2..... x_n
         * 
         * 2 <= i <= nにちいて
         * 
         * x_i = Σ x_k + r_i
         * 
         * 1 <= r_i <= m
         * 
         * ならm-cute
         * 
         * 初項 a 末項 bのm-cuteな数列
         * 
         * 
         */

        /*
         * BのAが寄与する分
         * 
         * 2^k倍
         * 
         * r_1が寄与する
         * 2^(k-i)倍
         */

        if (a == b)
        {
            Console.WriteLine($"1 {a}");
            return;
        }

        for (int k = 2; k <= 50; k++)
        {
            // k項
            long[] x = new long[k];

            // 全部に1を割り当てる
            // (a+1) * (2^(k-2)) <= B

            if ((a + 1) > b / (1L << (k - 2))) break;
            x[0] = a;
            for (int i = 1; i < k; i++) x[i] = 1;

            // 残り

            long tmp = b - (a + 1) * (1L << (k - 2));

            // 上から貪欲に取る
            for (int i = 1; i < k; i++)
            {
                // iの寄与する
                long mul = i == k - 1 ? 1 : (1L << (k - i - 2));
                // m-1まで
                long t = Math.Min(m - 1, tmp / mul);
                x[i] += t;
                tmp -= mul * t;
            }
            if (tmp != 0) continue;
            for (int i = 1; i < k; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    x[i] += x[j];
                }
            }
            Console.WriteLine($"{k} {string.Join(" ", x)}");
            return;
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
