using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        long[] a = sc.LongArray();

        /*
         * x書く
         * x%3 == 0 x/=3
         * or
         * x *= 2
         * x書く
         *
         * aを並び替える
         */

        // 素因数3最大 2最小 最初

        int[] cnt2 = new int[n];
        int[] cnt3 = new int[n];
        for (int i = 0; i < n; i++)
        {
            long tmp = a[i];
            while (tmp % 2 == 0)
            {
                cnt2[i]++;
                tmp /= 2;
            }

            while (tmp % 3 == 0)
            {
                cnt3[i]++;
                tmp /= 3;
            }
        }

        var sorted = new int[n];
        for (int i = 0; i < n; i++)
        {
            sorted[i] = i;
        }

        Array.Sort(sorted, (l, r) =>
        {
            if (cnt3[l] != cnt3[r]) return cnt3[r].CompareTo(cnt3[l]);
            else return cnt2[l].CompareTo(cnt2[r]);
        });

        var ans = new long[n];
        for (int i = 0; i < n; i++)
        {
            ans[i] = a[sorted[i]];
        }

        Console.WriteLine(string.Join(" ", ans));
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