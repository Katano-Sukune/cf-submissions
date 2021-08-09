using System;
using System.Linq;
using CompLib.Util;
using System.Threading;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        const int n = 10;
        string[] s = new string[n];
        for (int i = 0; i < n; i++)
        {
            s[i] = sc.Next();
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i + 5 <= n)
                {
                    int cnt = 0;
                    bool f = true;
                    for (int k = 0; f && k < 5; k++)
                    {
                        if (s[i + k][j] == 'O') f = false;
                        else if (s[i + k][j] == '.') cnt++;
                    }

                    if (f && cnt <= 1)
                    {
                        Console.WriteLine("YES");
                        return;
                    }
                }

                if (j + 5 <= n)
                {
                    int cnt = 0;
                    bool f = true;
                    for (int k = 0; f && k < 5; k++)
                    {
                        if (s[i][j + k] == 'O') f = false;
                        else if (s[i][j + k] == '.') cnt++;
                    }

                    if (f && cnt <= 1)
                    {
                        Console.WriteLine("YES");
                        return;
                    }
                }

                if (i + 5 <= n && j + 5 <= n)
                {
                    int cnt = 0;
                    bool f = true;
                    for (int k = 0; f && k < 5; k++)
                    {
                        if (s[i + k][j + k] == 'O') f = false;
                        else if (s[i + k][j + k] == '.') cnt++;
                    }

                    if (f && cnt <= 1)
                    {
                        Console.WriteLine("YES");
                        return;
                    }
                }

                if (i + 5 <= n && j - 5 + 1 >= 0)
                {
                    int cnt = 0;
                    bool f = true;
                    for (int k = 0; f && k < 5; k++)
                    {
                        if (s[i + k][j - k] == 'O') f = false;
                        else if (s[i + k][j - k] == '.') cnt++;
                    }

                    if (f && cnt <= 1)
                    {
                        Console.WriteLine("YES");
                        return;
                    }
                }
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