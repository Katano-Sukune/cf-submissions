using System;
using System.Linq;
using System.Text;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        int m = sc.NextInt();
        bool[][] a = new bool[n][];
        for (int i = 0; i < n; i++)
        {
            a[i] = sc.IntArray().Select(ii => ii == 1).ToArray();
        }

        // 行列 A,Bがある

        bool[][] b = new bool[n][];
        for (int i = 0; i < n; i++)
        {
            b[i] = new bool[m];
        }
        var sb = new StringBuilder();
        int cnt = 0;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < m - 1; j++)
            {
                if (a[i][j] && a[i + 1][j] && a[i][j + 1] && a[i + 1][j + 1])
                {
                    cnt++;
                    sb.AppendLine($"{i + 1} {j + 1}");
                    b[i][j] = b[i + 1][j] = b[i][j + 1] = b[i + 1][j + 1] = true;
                }
            }
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (a[i][j] != b[i][j])
                {
                    Console.WriteLine("-1");
                    return;
                }
            }
        }
        Console.WriteLine(cnt);
        Console.Write(sb);
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
