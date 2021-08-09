using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int[] a = sc.IntArray();
        Array.Sort(a);
        int m = sc.NextInt();
        int[] b = sc.IntArray();
        Array.Sort(b);

        long sumA = (long)-1e18;
        long sumB = (long)1e18;

        for (int i = 0; i <= n; i++)
        {
            // Aの最初からi個2点
            // Bを最小化したいので d = a[i]-1

            long d = i == n ? long.MaxValue : a[i] - 1;
            long tmpA = i * 2 + (n - i) * 3;

            // d未満のb

            int ok = -1;
            int ng = m;
            while (ng - ok > 1)
            {
                int mid = (ok + ng) / 2;
                if (b[mid] <= d)
                {
                    ok = mid;
                }
                else
                {
                    ng = mid;
                }
            }

            long tmpB = ng * 2 + (m - ng) * 3;

            if (sumA - sumB < tmpA - tmpB)
            {
                sumA = tmpA;
                sumB = tmpB;
            }
        }

        Console.WriteLine($"{sumA}:{sumB}");
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
