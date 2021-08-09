using System;
using CompLib.Util;

public class Program
{
    private int N;
    private long K;
    private long[] A;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextLong();
        A = sc.LongArray();

        // A_i*B_j = x^k i,jのペア

        int[] cnt = new int[100001];
        long ans = 0;
        foreach (var i in A)
        {
            long cp = i;
            long tmp = 1;
            long target = 1;
            bool flag = true;
            for (long p = 2; p * p <= cp && flag; p++)
            {
                long c = 0;
                while (cp % p == 0)
                {
                    c++;
                    cp /= p;
                }

                c %= K;
                if (c != 0)
                {
                    for (int j = 0; j < c; j++)
                    {
                        tmp *= p;
                    }

                    for (int j = 0; j < (K - c); j++)
                    {
                        target *= p;
                        if (target > 100000)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
            }

            if (flag && cp != 1)
            {
                tmp *= cp;
                for (int j = 0; j < (K - 1); j++)
                {
                    target *= cp;
                    if (target > 100000)
                    {
                        flag = false;
                        break;
                    }
                }
            }

            if (flag)
            {
                ans += cnt[target];
                cnt[tmp]++;
            }
        }

        Console.WriteLine(ans);
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
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(Separator);
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
            _line = Console.ReadLine().Split(Separator);
            _index = _line.Length;
            return _line;
        }

        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
        public decimal[] DecimalArray() => Array().Select(decimal.Parse).ToArray();
    }
}