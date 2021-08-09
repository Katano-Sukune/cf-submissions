using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, K;
    public void Solve()
    {
        checked
        {
            var sc = new Scanner();
            N = sc.NextInt();
            K = sc.NextInt();

            /*
             * n順列p
             * 
             * n-k個以上 p_i = i
             * 
             * 
             */


            long ans = 0;
            for (int i = 0; i <= K; i++)
            {
                // i個選ぶ
                // i個並べる
                // !i

                ans += C(N, i) * A(i);
            }
            ans++;

            Console.WriteLine(ans);
        }
    }

    long C(int n, int m)
    {
        long r = 1;
        for (int i = 0; i < m; i++)
        {
            r *= n - i;
        }

        for (int i = 1; i <= m; i++)
        {
            r /= i;
        }
        return r;
    }

    long A(int n)
    {
        if (n <= 1) return 0;
        if (n == 2) return 1;
        else return (n - 1) * (A(n - 1) + A(n - 2));
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
