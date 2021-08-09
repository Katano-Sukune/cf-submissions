using System;
using System.IO;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        for (int i = 0; i < t; i++)
        {
            char[][] ans = new char[9][];
            for (int j = 0; j < 9; j++)
            {
                ans[j] = sc.NextCharArray();
            }

            ans[0][0] = ans[0][0] == '1' ? '2' : '1';
            ans[1][3] = ans[1][3] == '1' ? '2' : '1';
            ans[2][6] = ans[2][6] == '1' ? '2' : '1';
            ans[3][1] = ans[3][1] == '1' ? '2' : '1';
            ans[4][4] = ans[4][4] == '1' ? '2' : '1';
            ans[5][7] = ans[5][7] == '1' ? '2' : '1';
            ans[6][2] = ans[6][2] == '1' ? '2' : '1';
            ans[7][5] = ans[7][5] == '1' ? '2' : '1';
            ans[8][8] = ans[8][8] == '1' ? '2' : '1';

            for (int j = 0; j < 9; j++)
            {
                Console.WriteLine(new string(ans[j]));
            }
        }

        Console.Out.Flush();
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