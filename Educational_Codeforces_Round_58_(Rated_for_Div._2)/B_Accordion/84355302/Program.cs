using System;
using CompLib.Util;

public class Program
{
    private string S;

    public void Solve()
    {
        var sc = new Scanner();
        S = sc.Next();

        // [:||||....||||:]

        // 最初の[:の位置
        int l = -1;
        bool f1 = false;
        for (int i = 0; i < S.Length; i++)
        {
            if (!f1 && S[i] == '[') f1 = true;
            else if (f1 && S[i] == ':')
            {
                l = i;
                break;
            }
        }

        if (l == -1)
        {
            Console.WriteLine("-1");
            return;
        }

        int r = -1;
        bool f2 = false;
        for (int i = S.Length - 1; i >= 0; i--)
        {
            if (!f2 && S[i] == ']') f2 = true;
            if (f2 && S[i] == ':')
            {
                r = i;
                break;
            }
        }

        if (r == -1 || l >= r)
        {
            Console.WriteLine("-1");
            return;
        }

        int cnt = 0;
        for (int i = l + 1; i < r; i++)
        {
            if (S[i] == '|') cnt++;
        }

        Console.WriteLine(4 + cnt);
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