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

        int[] a = sc.IntArray();

        Array.Sort(a);

        /*
         * かぶり2個まで
         */

        var ls1 = new List<int>();
        var ls2 = new List<int>();
        foreach (var i in a)
        {
            if (ls1.Count == 0 || ls1[ls1.Count - 1] < i)
            {
                ls1.Add(i);
            }
            else if (ls2.Count == 0 || ls2[ls2.Count - 1] < i)
            {
                ls2.Add(i);
            }
            else
            {
                Console.WriteLine("NO");
                return;
            }
        }
        ls2.Reverse();
        Console.WriteLine("YES");
        Console.WriteLine(ls1.Count);
        Console.WriteLine(string.Join(" ", ls1));
        Console.WriteLine(ls2.Count);
        Console.WriteLine(string.Join(" ", ls2));
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
