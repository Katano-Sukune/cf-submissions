using System;
using System.Linq;
using CompLib.Util;

public class Program
{
    int N, X, Y;
    int[] A;
    public void Solve()
    {
        var sc = new Scanner();
        N = sc.NextInt();
        X = sc.NextInt();
        Y = sc.NextInt();
        A = sc.IntArray();

        // n個扉
        // iの耐久値 A[i]
        // iを選ぶと x減らす
        // 0以外の扉選ぶ y増やす

        // 最終的に壊れた扉はいくつか?
        // 
        // 

        if (X > Y)
        {
            Console.WriteLine(N);
            return;
        }

        // x以下のやつ
        int cnt = 0;
        foreach (int c in A)
        {
            if (c <= X) cnt++;
        }

        Console.WriteLine((cnt + 1) / 2);
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
