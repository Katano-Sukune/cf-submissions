using System;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int h = sc.NextInt();
        int w = sc.NextInt();
        string[] s = new string[h];
        for (int i = 0; i < h; i++)
        {
            s[i] = sc.Next();
        }

        int cnt = 0;
        bool[,] flag = new bool[h, w];
        for (int i = 1; i < h - 1; i++)
        {
            for (int j = 1; j < w - 1; j++)
            {
                bool f = true;
                f &= s[i][j] == '*';
                f &= s[i + 1][j] == '*';
                f &= s[i - 1][j] == '*';
                f &= s[i][j + 1] == '*';
                f &= s[i][j - 1] == '*';

                if (f)
                {
                    if (cnt > 0)
                    {
                        Console.WriteLine("NO");
                        return;
                    }

                    cnt++;
                    for (int k = i; k >= 0 && s[k][j] == '*'; k--)
                    {
                        flag[k, j] = true;
                    }

                    for (int k = i; k < h && s[k][j] == '*'; k++)
                    {
                        flag[k, j] = true;
                    }

                    for (int k = j; k >= 0 && s[i][k] == '*'; k--)
                    {
                        flag[i, k] = true;
                    }

                    for (int k = j; k < w && s[i][k] == '*'; k++)
                    {
                        flag[i, k] = true;
                    }
                }
            }
        }

        if (cnt != 1)
        {
            Console.WriteLine("NO");
            return;
        }
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                if (s[i][j] == '*' && !flag[i, j])
                {
                    Console.WriteLine("NO");
                    return;
                }
            }
        }

        Console.WriteLine("YES");
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