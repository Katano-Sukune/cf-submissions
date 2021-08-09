using System;
using CompLib.Util;

public class Program
{
    private int N;
    private char[] S;
    int[] Counter;

    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        S = sc.NextCharArray();
        int cnt = 0;
        int min = 0;
        int ans = 0;
        int len = 0;
        foreach (char c in S)
        {
            if (c == '(') cnt++;
            else cnt--;
            len++;
            min = Math.Min(min, cnt);
            if (cnt == 0)
            {
                if (min < 0) ans += len;
                min = 0;
                len = 0;
            }
        }

        if (cnt != 0)
        {
            Console.WriteLine("-1");
            return;
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