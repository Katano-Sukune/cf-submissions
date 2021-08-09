using CompLib.Util;
using System;
using System.Linq;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        var a = sc.IntArray();

        // 増加 -> 減少
        var ans = new int[n];
        var max = 0L;
        for (int i = 0; i < n; i++)
        {
            var t = a.ToArray();
            int p = a[i];
            for (int j = i - 1; j >= 0; j--)
            {
                if (p >= a[j])
                {

                }
                else
                {
                    t[j] = p;
                }
                p = t[j];
            }
            p = a[i];
            for (int j = i + 1; j < n; j++)
            {
                if (p < a[j])
                {
                    t[j] = p;
                }
                p = t[j];
            }

            var sum = 0L;
            foreach (int j in t)
            {
                sum += j;
            }

            if (max < sum)
            {
                max = sum;
                ans = t;
            }
        }
        Console.WriteLine(string.Join(" ",ans));
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