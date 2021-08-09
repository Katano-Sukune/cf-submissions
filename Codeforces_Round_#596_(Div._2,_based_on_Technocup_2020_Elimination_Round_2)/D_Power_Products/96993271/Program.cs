using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    private int N, K;
    private int[] A;
    private const long MaxA = 100000;
    private const long MaxA2 = MaxA * MaxA;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        A = sc.IntArray();
        long ans = 0;
        var map = new int[MaxA + 1];
        foreach (int i in A)
        {
            long t1 = 1;
            long t2 = 1;
            long tmp = i;

            bool f = true;
            for (int p = 2; p * p <= tmp; p++)
            {
                if (tmp % p == 0)
                {
                    int cnt = 0;
                    while (tmp % p == 0)
                    {
                        cnt++;
                        tmp /= p;
                    }

                    cnt %= K;
                    for (int j = 0; j < cnt; j++)
                    {
                        t1 *= p;
                    }

                    for (int j = 0; j < (K - cnt) % K; j++)
                    {
                        if (t2 * p > MaxA) f = false;
                        else t2 *= p;
                    }
                }
            }

            if (tmp != 1)
            {
                t1 *= tmp;

                for (int j = 0; j < K - 1; j++)
                {
                    if (t2 * tmp > MaxA) f = false;
                    else t2 *= tmp;
                }
            }

            if (f)
            {
                ans += map[t2];
            }

            map[t1]++;
        }

        Console.WriteLine(ans);
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