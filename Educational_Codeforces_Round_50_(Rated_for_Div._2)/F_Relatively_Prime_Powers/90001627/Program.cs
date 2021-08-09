using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    long[] Max = { 0, 0, 1000000000, 1000000, 31622, 3981, 1000, 372, 177, 100, 63, 43, 31, 24, 19, 15, 13, 11, 10, 8, 7, 7, 6, 6, 5, 5, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 };
    long MaxN = (long)1e18;
    public void Solve()
    {
        //Max = new long[61];
        //for (int i = 2; i <= 60; i++)
        //{
        //    long ok = 1;
        //    long ng = int.MaxValue;
        //    while (ng - ok > 1)
        //    {
        //        long mid = (ok + ng) / 2;
        //        long tmp = 1;
        //        bool f = true;
        //        for (int j = 0; j < i; j++)
        //        {
        //            if (tmp > MaxN / mid)
        //            {
        //                f = false;
        //                break;
        //            }
        //            tmp *= mid;
        //        }

        //        if (f) ok = mid;
        //        else ng = mid;
        //    }

        //    Max[i] = ok;
        //}

        //Console.WriteLine(string.Join(", ", Max));


        var sc = new Scanner();
#if DEBUG
        int t = 100000;
#else
        int t = sc.NextInt();
#endif
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
#if DEBUG
        long n = 1000000000000000000;
#else
        long n = sc.NextLong();
#endif


        // xの素因数分解 p_1^e_1 .... 

        // eのgcdが1ならelegant

        // [2,n]のelegantな整数

        // elegantじゃない
        // 2以上t
        // eが全部tの倍数

        long[] sum = new long[61];
        for (int t = 60; t >= 2; t--)
        {
            // s^t < nなsの個数
            long ng = Max[t] + 1;
            long ok = 1;
            while (ng - ok > 1)
            {
                long mid = (ok + ng) / 2;
                long tmp = 1;
                bool f = true;
                for (int i = 0; i < t; i++)
                {
                    tmp *= mid;
                }

                if (tmp <= n) ok = mid;
                else ng = mid;
            }

            sum[t] += ok - 1;
            for (int i = 2; i * i <= t; i++)
            {
                if (t % i == 0)
                {
                    if (i < t) sum[i] -= sum[t];
                    if (t / i < t && t / i != i) sum[t / i] -= sum[t];
                }
            }
        }

        long ans = 0;
        for (int i = 2; i <= 60; i++) ans += sum[i];
        Console.WriteLine(n - 1 - ans);
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
