using System;
using System.Linq;
using CompLib.Util;
using System.Threading;
using System.Collections.Generic;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int a = sc.NextInt();
        int b = sc.NextInt();
        int x = sc.NextInt();
        // 0 a
        // 1 b
        List<char> ans = new List<char>(a + b);

        // a + b
        if(x == 1)
        {
            Console.WriteLine(new string('0',a) + new string('1',b));
            return;
        }
        if (a >= b)
        {
            // 010101.....

            for (int i = 0; i < x / 2; i++)
            {
                ans.Add('0');
                ans.Add('1');
            }
            int bb = b - x / 2 - (x % 2);
            for (int i = 0; i < bb; i++)
            {
                ans.Add('1');
            }

            for (int i = 0; i < a - x / 2; i++)
            {
                ans.Add('0');
            }

            if (x % 2 == 1)
            {
                ans.Add('1');
            }
        }
        else
        {
            for (int i = 0; i < x / 2; i++)
            {
                ans.Add('1');
                ans.Add('0');
            }
            int aa = a - x / 2 - (x % 2);
            for (int i = 0; i < aa; i++)
            {
                ans.Add('0');
            }

            for (int i = 0; i < b - x / 2; i++)
            {
                ans.Add('1');
            }

            if (x % 2 == 1)
            {
                ans.Add('0');
            }
        }

        Console.WriteLine(new string(ans.ToArray()));
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
