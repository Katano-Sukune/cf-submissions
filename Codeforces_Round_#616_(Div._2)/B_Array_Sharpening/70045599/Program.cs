using CompLib.Util;
using System;
using System.Collections.Generic;
using System.Text;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        var sb = new StringBuilder();
        for (int i = 0; i < t; i++)
        {
            sb.AppendLine(Q(sc.NextInt(), sc.IntArray()));
        }
        Console.Write(sb.ToString());
    }

    private string Q(int n, int[] a)
    {
        // aの要素を増加、減少にできるか aの要素を1ずつ減らす
        var left = new bool[n];
        var right = new bool[n];
        for (int i = 0; i < n; i++)
        {
            int j = n - i - 1;
            if (i == 0)
            {
                left[i] = true;
                right[j] = true;
            }
            else
            {
                left[i] = left[i - 1] && a[i] >= i;
                right[j] = right[j + 1] && a[j] >= i;
            }
        }

        for (int i = 0; i < n; i++)
        {
            if (left[i] && right[i])
            {
                return "Yes";
            }
        }
        return "No";
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