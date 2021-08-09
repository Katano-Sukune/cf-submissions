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
        int m = sc.NextInt();

        string[] s = new string[n];
        for (int i = 0; i < n; i++)
        {
            s[i] = sc.Next();
        }

        string rgb = "RGB";
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (!rgb.Contains(s[i][j]))
                {
                    Console.WriteLine("NO");
                    return;
                }
            }
        }

        if (n % 3 == 0 && s[0][0] != s[n / 3][0] && s[n / 3][0] != s[2 * n / 3][0] && s[2 * n / 3][0] != s[0][0])
        {
            bool f = true;
            for (int i = 0; f && i < n / 3; i++)
            {
                for (int j = 0; f && j < m; j++)
                {
                    f &= s[i][j] == s[0][0];
                    f &= s[i + n / 3][j] == s[n / 3][0];
                    f &= s[i + 2 * n / 3][j] == s[2 * n / 3][0];
                }
            }

            if (f)
            {
                Console.WriteLine("YES");
                return;
            }
        }

        if (m % 3 == 0 && s[0][0] != s[0][m / 3] && s[0][m / 3] != s[0][2 * m / 3] && s[0][2 * m / 3] != s[0][0])
        {
            bool f = true;
            for (int i = 0; f && i < n; i++)
            {
                for (int j = 0; f && j < m / 3; j++)
                {
                    f &= s[i][j] == s[0][0];
                    f &= s[i][j + m / 3] == s[0][m / 3];
                    f &= s[i][j + 2 * m / 3] == s[0][2 * m / 3];
                }
            }

            if (f)
            {
                Console.WriteLine("YES");
                return;
            }
        }

        Console.WriteLine("NO");
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