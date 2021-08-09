using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.IO;
using System.Reflection.Metadata;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        int t = sc.NextInt();
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

        // k
        // a * 2^(k-1)

        // r_i * 2^(k-i-1)

        for (int k = 1; k <= 50; k++)
        {
            // aの寄与
            long aa = k == 1 ? a : a * (1L << (k - 2));
            if (aa > b) break;
            long d = b - aa;
            long[] r = new long[k];
            // とりあえず全部1
            for (int i = 1; i < k; i++)
            {
                r[i] = 1;
                d -= i == k - 1 ? 1 : 1L << (k - i - 2);
            }
            if (d < 0) break;
            for (int i = 1; i < k; i++)
            {
                // r_iの寄与
                long q = i == k - 1 ? 1 : 1L << (k - i - 2);
                // 貪欲に取る
                long dd = Math.Min(m - 1, d / q);
                r[i] += dd;
                d -= dd * q;
            }

            if (d == 0)
            {
                long[] ans = new long[k];
                ans[0] = a;
                for (int i = 1; i < k; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        ans[i] += ans[j];
                    }
                    ans[i] += r[i];
                }
                Console.WriteLine($"{k} {string.Join(" ", ans)}");
                return;
            }
        }
        Console.WriteLine("-1");
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
