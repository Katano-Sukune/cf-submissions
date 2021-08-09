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

        // n文字 sがある

        // s_iに隣接する文字がs_iより1つ小さいならs_iを消せる

        // 消せる 最大

        var ar = s.ToCharArray();

        for (char c = 'z'; c >= 'b'; c--)
        {
            var stack = new Stack<char>();
            foreach (char ch in ar)
            {
                if (ch == c && stack.Count > 0 && stack.Peek() == c - 1)
                {
                    continue;
                }
                else if (ch == c - 1)
                {
                    while (stack.Count > 0 && stack.Peek() == c)
                    {
                        stack.Pop();
                    }
                    stack.Push(ch);
                }
                else
                {
                    stack.Push(ch);
                }
            }

            ar = new char[stack.Count];
            for (int i = ar.Length - 1; i >= 0; i--)
            {
                ar[i] = stack.Pop();
            }
        }

        Console.WriteLine(n - ar.Length);
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
