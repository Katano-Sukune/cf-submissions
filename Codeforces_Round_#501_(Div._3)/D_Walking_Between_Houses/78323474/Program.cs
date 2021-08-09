using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{
    long N, K;
    long S;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        K = sc.NextInt();
        S = sc.NextLong();

        // N個の家がある
        // K回別の家に移動する　距離 |x-y|
        // 距離を丁度Sにできるか?

        // 最長　移動 1->N 

        if (S > (N - 1) * K)
        {
            Console.WriteLine("NO");
            return;
        }

        if (S < K)
        {
            Console.WriteLine("NO");
            return;
        }

        long d = S / K;
        long m = S % K;
        long p = 1;
        var ans = new List<long>();
        for (int i = 0; i < m; i++)
        {
            if (i % 2 == 0)
            {
                p += S / K + 1;
            }
            else
            {
                p -= S / K + 1;
            }
            ans.Add(p);
        }

        for (long i = m; i < K; i++)
        {
            if (i % 2 == 0)
            {
                p += S / K;
            }
            else
            {
                p -= S / K;
            }

            ans.Add(p);
        }

        Console.WriteLine("YES");
        Console.WriteLine(string.Join(" ", ans));
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
