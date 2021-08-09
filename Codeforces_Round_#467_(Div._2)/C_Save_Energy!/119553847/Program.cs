using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    long K, D, T;
    public void Solve()
    {
        var sc = new Scanner();
        K = sc.NextLong();
        D = sc.NextLong();
        T = sc.NextLong();

        // 電源をいれてからK分でオフになる
        // 
        // D分ごとにチェック オフになってたらオンにする
        // 
        // オンのときT分
        // オフのとき2T分
        // 
        // 何分かかる?

        long Q = (D - K % D) % D;
        double t1 = (double)K / T + (double)Q / (2 * T);

        double ng = 0;
        double ok = 2 * T;
        for (int t = 0; t < 100; t++)
        {
            double mid = (ok + ng) / 2;





            double cnt = Math.Floor(mid / (K + Q));

            double p1 = t1 * cnt;

            double time = cnt * (K + Q);
            double m = mid - time;

            if (m <= K)
            {
                double p2 = m / T;

                if (p1 + p2 >= 1) ok = mid;
                else ng = mid;
            }
            else
            {
                double p2 = (double)K / T;
                double p3 = (m - K) / (2 * T);
                if (p1 + p2 + p3 >= 1) ok = mid;
                else ng = mid;
            }
        }

        Console.WriteLine(ok);


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