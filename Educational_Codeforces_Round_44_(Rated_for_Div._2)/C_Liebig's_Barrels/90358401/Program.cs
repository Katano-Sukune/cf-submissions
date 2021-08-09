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
        int l = sc.NextInt();
        int[] a = sc.IntArray();

        // m = n*k本の棒がある
        // iの長さa_i
        // k本の棒でn個樽を作る

        // 樽jの容量 樽jの最小の棒

        // max - min <= l かつ総和最大

        Array.Sort(a);
        // 最小の樽の体積 a_0

        // 最大 a_0 + l以下最大のa

        int m = n * k;
        long sum = 0;
        int cnt = 0;
        int b = 0;
        for (int i = m - 1; i >= 0; i--)
        {
            if (cnt >= (k - 1) && a[i] - a[0] <= l)
            {
                cnt -= (k - 1);
                sum += a[i];
                b++;
            }
            else
            {
                cnt++;
            }
        }
        if(b < n)
        {
            Console.WriteLine(0);
            return;
        }
        Console.WriteLine(sum);
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
