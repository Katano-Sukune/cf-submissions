using System;
using System.Collections.Generic;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int n = sc.NextInt();
        string s = sc.Next();

        // 1つ選んで消す 先手の手番で 長さ11 先頭8にできるか?

        var eight = new Queue<int>();
        var other = new Queue<int>();
        for (int i = 0; i < n; i++)
        {
            if (s[i] == '8') eight.Enqueue(i);
            else other.Enqueue(i);
        }

        for (int i = 0; i < (n - 11) / 2; i++)
        {
            if (other.Count > 0) other.Dequeue();
            else eight.Dequeue();

            if (eight.Count > 0) eight.Dequeue();
            else other.Dequeue();
        }
        if (other.Count == 0) Console.WriteLine("YES");
        else if (eight.Count == 0) Console.WriteLine("NO");
        else Console.WriteLine(eight.Peek() < other.Peek() ? "YES" : "NO");

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
