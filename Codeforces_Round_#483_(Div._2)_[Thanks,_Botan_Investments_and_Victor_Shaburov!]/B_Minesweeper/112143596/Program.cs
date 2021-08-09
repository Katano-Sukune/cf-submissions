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

        int[] dx = new int[] {1, 1, 1, 0, 0, -1, -1, -1};
        int[] dy = new int[] {1, 0, -1, 1, -1, 1, 0, -1};
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if ('1' <= s[i][j] && s[i][j] <= '8' || s[i][j] == '.')
                {
                    int num = s[i][j] == '.' ? 0 : s[i][j] - '0';
                    int cnt = 0;
                    for (int k = 0; k < 8; k++)
                    {
                        int i2 = i + dx[k];
                        int j2 = j + dy[k];
                        if (i2 < 0 || j2 < 0) continue;
                        if (i2 >= n || j2 >= m) continue;
                        if (s[i2][j2] == '*') cnt++;
                    }

                    if (num != cnt)
                    {
                        Console.WriteLine("NO");
                        return;
                    }
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