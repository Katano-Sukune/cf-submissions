using System;
using System.Linq;
using CompLib.Util;

public class Program
{

    public void Solve()
    {
        var sc = new Scanner();
        int t = sc.NextInt();
        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
        for (int i = 0; i < t; i++)
        {
            Q(sc);
        }
        Console.Out.Flush();
    }

    void Q(Scanner sc)
    {
        long n = sc.NextLong();

        int cnt = 0;
        while (n % 5 == 0)
        {
            cnt++;
            n = n / 5 * 4;
        }

        while (n % 3 == 0)
        {
            cnt++;
            n = n / 3 * 2;
        }

        while (n % 2 == 0)
        {
            cnt++;
            n = n / 2;
        }

        if(n != 1)
        {
            Console.WriteLine("-1");
            return;
        }

        Console.WriteLine(cnt);
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
