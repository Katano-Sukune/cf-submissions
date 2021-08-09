using System;
using System.IO;
using CompLib.Util;

public class Program
{
    public void Solve()
    {
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false});
        var sc = new Scanner();
        int t = sc.NextInt();
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }

        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        int x = sc.NextInt();
        int m = sc.NextInt();
    
        int[] l = new int[m];
        int[] r = new int[m];
        for (int i = 0; i < m; i++)
        {
            l[i] = sc.NextInt();
            r[i] = sc.NextInt();
        }

        // 最初 a_x = 1 それ以外 0 長さn配列

        // l_i <=c,d <= r_i なcd選んでswap

        // a_k = 1になるようなkの数

        int min = x;
        int max = x;
        for (int i = 0; i < m; i++)
        {
            if (r[i] < min || max < l[i]) continue;
            min = Math.Min(l[i], min);
            max = Math.Max(r[i], max);
        }

        Console.WriteLine(max - min + 1);
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