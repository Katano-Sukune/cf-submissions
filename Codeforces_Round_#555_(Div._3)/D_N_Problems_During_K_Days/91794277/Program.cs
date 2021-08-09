using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int k = sc.NextInt();

        var a = new long[k];
        long tmp = 0;
        for (int i = 0; i < k; i++)
        {
            a[i] = i + 1;
            tmp += a[i];
        }
        if (n < tmp)
        {
            Console.WriteLine("NO");
            return;
        }

        long plus = 0;
        for (int i = 0; i < k; i++)
        {
            a[i] += plus;
            long diff = n - tmp;
            long l = k - i;

            long p = Math.Min(diff / l, (i == 0 ? long.MaxValue : a[i - 1] * 2 - a[i]));

            // Console.WriteLine($"{i} {diff} {p}");
            a[i] += p;
            tmp += l * p;
            plus += p;

        }

        if (tmp != n)
        {
            Console.WriteLine("NO");
            return;
        }

        Console.WriteLine("YES");
        Console.WriteLine(string.Join(" ", a));
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
