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
        long a = sc.NextInt();
        long b = sc.NextInt();
        long n = sc.NextInt();

        // a+=b or b+=aのどちらかをnより大きくする
        // 何回必要か?

        // a+2b
        if (a > b)
        {
            Swap(ref a, ref b);
        }

        for (int i = 0;; i++)
        {
            if (b > n)
            {
                Console.WriteLine(i);
                return;
            }

            a += b;
            Swap(ref a, ref b);
        }
    }

    void Swap(ref long l, ref long r)
    {
        long t = l;
        l = r;
        r = t;
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
                string s = Console.ReadLine();
                while (s.Length == 0)
                {
                    s = Console.ReadLine();
                }

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