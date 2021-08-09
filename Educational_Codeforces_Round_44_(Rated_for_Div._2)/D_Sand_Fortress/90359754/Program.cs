using System;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using CompLib.Util;

public class Program
{
    long N, H;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextLong();
        H = sc.NextLong();

        /*
         * n個砂持ってきた
         * iの高さh_i i個使う
         * 
         * h_1....h_k
         * h_1 <= H
         * 
         * |h_i-h_{i+1}| <= 1
         * 
         * を満たす最小k
         * 
         * 2 2 1
         */

        long ng = 0;
        long ok = long.MaxValue / 2;
        while (ok - ng > 1)
        {
            long mid = (ok + ng) / 2;
            if (F(mid)) ok = mid;
            else ng = mid;
        }
        Console.WriteLine(ok);
    }

    bool F(long k)
    {
        if (k <= H)
        {
          
            return (BigInteger)(k + 1) * (k) >= 2 * N;
        }

        if (k % 2 == H % 2)
        {
            // 一番高いところ1つ
            // highest g
            BigInteger g = (k + H) / 2;
            return ((H + g - 1) * (k - g) + (g + 1) * g) >= 2 * N;
        }
        else
        {
            // highest 
            // (k + h - 1)/2
            BigInteger g = (k + H - 1) / 2;
            return ((H + g) * (k - g) + (g + 1) * g) >= 2 * N;
        }
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
