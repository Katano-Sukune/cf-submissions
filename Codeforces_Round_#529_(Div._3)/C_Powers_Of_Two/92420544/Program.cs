using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int k = sc.NextInt();
        if (n < k)
        {
            Console.WriteLine("NO");
            return;
        }
        int ptr = 0;
        int cnt = 0;
        int[] ans = new int[30];
        {

            int tmp = n;
            while (tmp > 0)
            {
                if (tmp % 2 == 1)
                {
                    cnt++;
                    ans[ptr]++;
                }
                ptr++;
                tmp /= 2;
            }
        }

        if (cnt > k)
        {
            Console.WriteLine("NO");
            return;
        }

        for (int p = 29; p >= 1 && cnt < k; p--)
        {
            int t = Math.Min(k - cnt, ans[p]);
            ans[p] -= t;
            ans[p - 1] += 2 * t;
            cnt += t;
        }

        List<int> a = new List<int>();
        for (int i = 0; i < 30; i++)
        {
            for (int j = 0; j < ans[i]; j++)
            {
                a.Add(1 << i);
            }
        }

        Console.WriteLine("YES");
        Console.WriteLine(string.Join(" ", a));
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
