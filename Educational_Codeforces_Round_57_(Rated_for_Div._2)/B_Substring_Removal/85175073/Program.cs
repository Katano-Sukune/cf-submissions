using System;
using CompLib.Util;

public class Program
{
    private const long Mod = 998244353;

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        string s = sc.Next();

        int left = 1;
        for (int i = 1; i < n; i++)
        {
            if (s[i] == s[0]) left++;
            else break;
        }

        // 全部同じ
        if (left == n)
        {
            Console.WriteLine(((long) n * (n + 1) / 2) % Mod);
            return;
        }

        int right = 1;
        for (int i = n - 2; i >= 0; i--)
        {
            if (s[i] == s[n - 1]) right++;
            else break;
        }

        if (s[0] == s[n - 1])
        {
            Console.WriteLine(((long) (left + 1) * (right + 1)) % Mod);
        }
        else
        {
            Console.WriteLine((left + right + 1) % Mod);
        }
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
                string s = Console.ReadLine();
                while (s.Length == 0)
                {
                    s = Console.ReadLine();
                }

                _line = s.Split(Separator);
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