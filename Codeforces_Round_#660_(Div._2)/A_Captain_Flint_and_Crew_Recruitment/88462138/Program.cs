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
            Q(sc);
        }

        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        int n = sc.NextInt();
        // 素数p,q p < q p*qで表せる数

        // 上の数3つとあと1つの和でnができるか?
        // 6, 10 15...
        // 最小 6 + 10 + 14 + 1 = 31
        if (n < 31)
        {
            Console.WriteLine("NO");
            return;
        }


        // 6 10 14 n-(6+10+14) n -20
        if (n - 30 == 6)
        {
            // 36
            // 6 10 14   
            Console.WriteLine("YES");
            Console.WriteLine("5 6 10 15");
        }
        else if (n - 30 == 10)
        {
            // 40
            // 6 10 15 9
            Console.WriteLine("YES");
            Console.WriteLine("6 9 10 15");
        }
        else if (n - 30 == 14)
        {
            // 44
            Console.WriteLine("YES");
            Console.WriteLine("6 7 10 21");
        }
        else
        {
            Console.WriteLine("YES");
            Console.WriteLine($"6 10 14 {n - 30}");
        }
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