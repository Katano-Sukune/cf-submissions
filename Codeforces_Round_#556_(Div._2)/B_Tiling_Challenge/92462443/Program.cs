using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    readonly int[] Dx = new int[] { 0, -1, 0, 1, 0 };
    readonly int[] Dy = new int[] { 0, 1, 1, 1, 2 };
    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        char[][] s = new char[n][];
        for (int i = 0; i < n; i++)
        {
            s[i] = sc.NextCharArray();
        }

        bool[,] f = new bool[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (f[i, j] || s[i][j] == '#') continue;
                for (int k = 0; k < 5; k++)
                {
                    int nx = j + Dx[k];
                    int ny = i + Dy[k];
                    if (nx < 0 || ny < 0)
                    {
                        Console.WriteLine("NO");
                        return;
                    }
                    if (nx >= n || ny >= n)
                    {
                        Console.WriteLine("NO");
                        return;
                    }
                    if (s[ny][nx] == '#' || f[ny, nx])
                    {
                        Console.WriteLine("NO");
                        return;
                    }
                    f[ny, nx] = true;
                }
            }
        }

        Console.WriteLine("YES");
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
