using CompLib.Util;
using System;
using System.Text;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();

        // 凸多角形が与えられる 原点を含むように並行移動させてできる範囲　元の多角形と相似ならYES

        // 点対称ならOK?

        int n = sc.NextInt();

        int[] x = new int[n];
        int[] y = new int[n];

        for (int i = 0; i < n; i++)
        {
            x[i] = sc.NextInt();
            y[i] = sc.NextInt();
        }

        if (n % 2 != 0)
        {
            Console.WriteLine("NO");
            return;
        }

        for (int i = 0; i < n; i++)
        {
            int next = (i + 1) % n;

            int j = (i + n / 2) % n;
            int jNext = (j + 1) % n;

            bool a = x[i] - x[next] == x[jNext] - x[j];
            bool b = y[i] - y[next] == y[jNext] - y[j];

            if(!a | !b)
            {
                Console.WriteLine("NO");
                return;
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
        private int _index;
        private string[] _line;
        const char separator = ' ';

        public Scanner()
        {
            _index = 0;
            _line = new string[0];
        }

        public string Next()
        {
            while (_index >= _line.Length)
            {
                _line = Console.ReadLine().Split(separator);
                _index = 0;
            }
            return _line[_index++];
        }
        public int NextInt() => int.Parse(Next());
        public long NextLong() => long.Parse(Next());
        public double NextDouble() => double.Parse(Next());
        public char NextChar() => Next()[0];
        public char[] NextCharArray() => Next().ToCharArray();

        public string[] Array()
        {
            _line = Console.ReadLine().Split(separator);
            _index = _line.Length;
            return _line;
        }
        public int[] IntArray() => Array().Select(int.Parse).ToArray();
        public long[] LongArray() => Array().Select(long.Parse).ToArray();
        public double[] DoubleArray() => Array().Select(double.Parse).ToArray();
    }
}